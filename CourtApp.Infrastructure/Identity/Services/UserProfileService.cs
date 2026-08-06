using CourtApp.Application.Features.Profile.DTOs;
using CourtApp.Application.Features.Profile.Services;
using CourtApp.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Identity.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IdentityContext _db;
        private readonly ILogger<UserProfileService> _logger;

        public UserProfileService(
            UserManager<ApplicationUser> userManager,
            IdentityContext db,
            ILogger<UserProfileService> logger)
        {
            _userManager = userManager;
            _db = db;
            _logger = logger;
        }
        public async Task<string> CompleteProfileAsync(string userId, CompleteProfileRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            if (user.IsActive == false)
                throw new Exception("User not approved yet");


            user.FirstName = request.FirstName?.Trim();
            user.LastName = request.LastName?.Trim();
            user.DateOfBirth = request.DateOfBirth;
            user.Gender = request.Gender;
            user.ProfileImageUrl = request.ProfileImageUrl;

            await _userManager.UpdateAsync(user);


            if (request.Addresses?.Any() == true)
            {
                var addresses = request.Addresses.Select(a => new UserAddress
                {
                    UserId = user.Id,
                    AddressLine1 = a.AddressLine1,
                    AddressLine2 = a.AddressLine2,
                    City = a.City,
                    StateId = a.StateId,
                    Pincode = a.Pincode,
                    Type = a.Type,
                    IsPrimary = a.IsPrimary
                });

                await _db.UserAddresses.AddRangeAsync(addresses);
            }


            if (request.Contacts?.Any() == true)
            {
                var contacts = request.Contacts.Select(c => new UserContact
                {
                    UserId = user.Id,
                    ContactType = c.ContactType,
                    Email = c.Email,
                    ContactNumber = c.ContactNumber,
                    IsPrimary = c.IsPrimary
                });

                await _db.UserContacts.AddRangeAsync(contacts);
            }


            if (request.ProfessionalInfo != null)
            {
                var professional = new ProfessionalInfoEntity
                {
                    UserId = user.Id,
                    BarCouncil = request.ProfessionalInfo.BarCouncil,
                    ExperienceYears = request.ProfessionalInfo.ExperienceYears
                };

                await _db.ProfessionalInfos.AddAsync(professional);
            }

            if (request.WorkLocations != null)
            {
                var workLocations = request.WorkLocations.Select(w => new UserWorkLocation
                {
                    UserId = user.Id,
                    CourtId = w.CourtId,
                    CourtComplexId = w.CourtComplexId,
                    CourtHallId = w.CourtHallId
                });
                await _db.UserWorkLocations.AddRangeAsync(workLocations);
            }

            await _db.SaveChangesAsync();

            return "Profile completed successfully";
        }


        public async Task<string> UpdateProfileAsync(string userId, UpdateProfileRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");


            user.FirstName = request.FirstName?.Trim();
            user.LastName = request.LastName?.Trim();
            user.DateOfBirth = request.DateOfBirth;
            user.Gender = request.Gender;
            user.ProfileImageUrl = request.ProfileImageUrl;

            await _userManager.UpdateAsync(user);


            if (request.Addresses != null)
            {
                var oldAddresses = _db.UserAddresses.Where(x => x.UserId == user.Id);
                _db.UserAddresses.RemoveRange(oldAddresses);

                var newAddresses = request.Addresses.Select(a => new UserAddress
                {
                    UserId = user.Id,
                    AddressLine1 = a.AddressLine1,
                    AddressLine2 = a.AddressLine2,
                    City = a.City,
                    StateId = a.StateId,
                    Pincode = a.Pincode,
                    IsPrimary = a.IsPrimary
                });

                await _db.UserAddresses.AddRangeAsync(newAddresses);
            }


            if (request.Contacts != null)
            {
                var oldContacts = _db.UserContacts.Where(x => x.UserId == user.Id);
                _db.UserContacts.RemoveRange(oldContacts);

                var newContacts = request.Contacts.Select(c => new UserContact
                {
                    UserId = user.Id,
                    ContactNumber = c.ContactNumber,
                    Email = c.Email,
                    IsPrimary = c.IsPrimary
                });

                await _db.UserContacts.AddRangeAsync(newContacts);
            }


            if (request.ProfessionalInfo != null)
            {
                var existing = await _db.ProfessionalInfos
                    .FirstOrDefaultAsync(x => x.UserId == user.Id);

                if (existing == null)
                {
                    await _db.ProfessionalInfos.AddAsync(new ProfessionalInfoEntity
                    {
                        UserId = user.Id,
                        BarCouncil = request.ProfessionalInfo.BarCouncil,
                        ExperienceYears = request.ProfessionalInfo.ExperienceYears
                    });
                }
                else
                {
                    existing.BarCouncil = request.ProfessionalInfo.BarCouncil;
                    existing.ExperienceYears = request.ProfessionalInfo.ExperienceYears;
                }
            }

            await _db.SaveChangesAsync();

            return "Profile updated successfully";
        }


        public async Task<UserProfileDto> GetProfileAsync(string userId)
        {
            var user = await _db.Users
                .Include(x => x.Addresses)
                .Include(x => x.Contacts)
                .Include(x => x.ProfessionalInfo)
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
                throw new Exception("User not found");

            var userProfile = new UserProfileDto
            {
                UserId = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfileImageUrl = user.ProfileImageUrl,

                Addresses = user.Addresses.Select(a => new UserAddressDto
                {
                    AddressLine1 = a.AddressLine1,
                    AddressLine2 = a.AddressLine2,
                    City = a.City,
                    StateId = a.StateId,
                    Pincode = a.Pincode,
                    IsPrimary = a.IsPrimary
                }).ToList(),

                Contacts = user.Contacts.Select(c => new UserContactDto
                {
                    ContactNumber = c.ContactNumber,
                    Email = c.Email,
                    IsPrimary = c.IsPrimary
                }).ToList(),

                ProfessionalInfo = user.ProfessionalInfo == null ? null : new ProfessionalInfoDto
                {
                    BarCouncil = user.ProfessionalInfo.BarCouncil,
                    ExperienceYears = user.ProfessionalInfo.ExperienceYears
                }
            };

            return userProfile;
        }
    }
}
