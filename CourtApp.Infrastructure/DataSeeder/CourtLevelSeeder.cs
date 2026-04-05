using CourtApp.Domain.Entities.Masters;
using CourtApp.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CourtApp.Infrastructure.DataSeeder
{
    public static class CourtLevelSeeder
    {
        public static async Task SeedCourtLevelAsync(ApplicationDbContext context,IdentityContext identityContext)
        {
            var systemUser = "superadmin@gmail.com";

            var superAdmin = await identityContext.Users
                .FirstOrDefaultAsync(u => u.Email == systemUser);

            var userId = superAdmin != null ? superAdmin.Id : "system";
            var existingCodes = await context.courtLevelEntities
               .Select(s => s.Code)
               .ToListAsync();

            var courtLevels = GetCourtLevels(); ;

            var newCourtLevels = courtLevels
                .Where(s => !existingCodes.Contains(s.Code))
                .ToList();

            if (!newCourtLevels.Any())
                return;

            // ✅ Assign audit fields (if your entity has them)
            foreach (var state in newCourtLevels)
            {
                state.CreatedBy = userId;
            }

            await context.courtLevelEntities.AddRangeAsync(newCourtLevels);
            await context.SaveChangesAsync(userId);
            
        }
        public static List<CourtLevelEntity> GetCourtLevels()
        {
            return new List<CourtLevelEntity>
        {
            new CourtLevelEntity
            {
                Id = Guid.NewGuid(),
                Name = "Supreme Court",
                Code = "SUPREME",
                CourtTypes = new List<CourtTypeEntity>() 
            },
            new CourtLevelEntity
            {
                Id = Guid.NewGuid(),
                Name = "High Court",
                Code = "HIGH",
                CourtTypes = new List<CourtTypeEntity>() // leave empty
            },
            new CourtLevelEntity
            {
                Id = Guid.NewGuid(),
                Name = "District Court",
                Code = "DISTRICT",
               
                CourtTypes = new List<CourtTypeEntity>() // leave empty
            },
            new CourtLevelEntity
            {
                Id = Guid.NewGuid(),
                Name = "Subordinate / Lower Court",
                Code = "SUBORDINATE",
                CourtTypes = new List<CourtTypeEntity>() // leave empty
            },
            new CourtLevelEntity
            {
                Id = Guid.NewGuid(),
                Name = "Tribunal Court",
                Code = "TRIBUNAL",
                CourtTypes = new List<CourtTypeEntity>() // leave empty
            },
            new CourtLevelEntity
            {
                Id = Guid.NewGuid(),
                Name = "Special Court",
                Code = "SPECIAL",
                CourtTypes = new List<CourtTypeEntity>() // leave empty
            },

        };
        }


    }
}