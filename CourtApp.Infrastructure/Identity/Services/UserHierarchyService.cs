using CourtApp.Application.Features.Profile.DTOs;
using CourtApp.Application.Features.Profile.Services;
using CourtApp.Domain.Enums;
using CourtApp.Infrastructure.DbContexts;
using CourtApp.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Identity.Services
{
    public class UserHierarchyService : IUserHierarchyService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IdentityContext _db;
        private readonly ILogger<UserHierarchyService> _logger;
        public UserHierarchyService(
            UserManager<ApplicationUser> userManager,
            IdentityContext db, 
            ILogger<UserHierarchyService> logger)
        {
            _db = db;
            _logger = logger;
            _userManager = userManager;
        }
        public async Task<string> CreateSubUserAsync(CreateSubUserRequest request, string parentUserId)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            // 🔹 Validate Parent
            var parent = await _userManager.FindByIdAsync(parentUserId);
            if (parent == null)
                throw new Exception("Parent user not found");

            // 🔹 Check duplicate email
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                throw new Exception("Email already exists");

            // 🔹 Create User
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(), 
                UserName = request.Email.ToLower(),
                Email = request.Email.ToLower(),
                PhoneNumber = request.PhoneNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                EnrollmentNumber = request.EnrollmentNumber,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            // 🔹 Assign Role (based on relation)
            var role = request.RelationType switch
            {
                UserRelationType.JuniorLawyer => "Lawyer",
                UserRelationType.Clerk => "Clerk",
                UserRelationType.Associate => "Assistant",
                _ => "User"
            };

            var roleResult = await _userManager.AddToRoleAsync(user, role);

            if (!roleResult.Succeeded)
                throw new Exception(string.Join(", ", roleResult.Errors.Select(e => e.Description)));

            // 🔹 Prevent duplicate hierarchy
            var exists = await _db.UserHierarchies
                .AnyAsync(x => x.ChildUserId.ToString() == user.Id && x.IsActive);

            if (exists)
                throw new Exception("User already mapped");

            // 🔹 Create Hierarchy Mapping
            var hierarchy = new UserHierarchy
            {
                Id = Guid.NewGuid(),
                ParentUserId = Guid.Parse(parentUserId),
                ChildUserId = Guid.Parse(user.Id),
                RelationType = request.RelationType,
                IsActive = true
            };

            await _db.UserHierarchies.AddAsync(hierarchy);

            await _db.SaveChangesAsync();

            return "Sub-user created successfully";
        }

        public async Task<List<string>> GetAllParentIdsAsync(string userId)
        {
            var result = new List<string>();

            var parents = await _db.UserHierarchies
                .Where(x => x.ChildUserId.ToString() == userId && x.IsActive)
                .Select(x => x.ParentUserId.ToString())
                .ToListAsync();

            foreach (var parent in parents)
            {
                result.Add(parent);
                result.AddRange(await GetAllParentIdsAsync(parent));
            }

            return result.Distinct().ToList();
        }

        public async Task<List<string>> GetAllChildIdsAsync(string userId)
        {
            var result = new List<string>();

            var children = await _db.UserHierarchies
                .Where(x => x.ParentUserId.ToString() == userId && x.IsActive)
                .Select(x => x.ChildUserId.ToString())
                .ToListAsync();

            foreach (var child in children)
            {
                result.Add(child);
                result.AddRange(await GetAllChildIdsAsync(child));
            }

            return result.Distinct().ToList();
        }

        public async Task<List<UserDto>> GetSubUsersAsync(string userId)
        {
            var parentId = Guid.Parse(userId);

            var users = await _db.UserHierarchies
                .Where(x => x.ParentUserId == parentId && x.IsActive)
                .Include(x => x.ChildUser)
                .Select(x => new UserDto
                {
                    UserId = x.ChildUser.Id,
                    Email = x.ChildUser.Email,
                    FirstName = x.ChildUser.FirstName,
                    LastName = x.ChildUser.LastName,
                    RelationType = x.RelationType.ToString()
                })
                .ToListAsync();

            return users;
        }

        public async Task<string> RemoveSubUserAsync(string userId)
        {
            var childId = Guid.Parse(userId);

            var hierarchy = await _db.UserHierarchies
                .FirstOrDefaultAsync(x => x.ChildUserId == childId && x.IsActive);

            if (hierarchy == null)
                throw new Exception("Sub-user not found");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            // 🔹 Soft delete mapping
            hierarchy.IsActive = false;

            // 🔹 Deactivate user
            user.IsActive = false;

            await _userManager.UpdateAsync(user);
            await _db.SaveChangesAsync();

            return "Sub-user removed successfully";
        }

        public async Task<bool> IsParentAsync(string parentId, string childId)
        {
            return await _db.UserHierarchies
            .AnyAsync(x =>
                x.ParentUserId.ToString() == parentId &&
                x.ChildUserId.ToString() == childId &&
                x.IsActive);
        }

        public async Task<bool> IsChildAsync(string childId, string parentId)
        {
            return await IsParentAsync(parentId, childId);
        }
    }
}
