using CourtApp.Domain.Entities.Masters;
using CourtApp.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.DataSeeder
{
    public static class CourtTypeSeeder
    {
        public static async Task SeedCourtTypeAsync(ApplicationDbContext context, IdentityContext identityContext)
        {
            var systemUser = "superadmin@gmail.com";

            var superAdmin = await identityContext.Users
                .FirstOrDefaultAsync(u => u.Email == systemUser);

            var userId = superAdmin != null ? superAdmin.Id : "system";

            var dbLevels = await context.courtLevelEntities.ToListAsync();

            var existingTypeCodes = await context.CourtTypes
            .Select(t => t.Code)
            .ToListAsync();

            var courtTypes = GetCourtTypes(dbLevels);

            var newCourtTypes = courtTypes
            .Where(t => !existingTypeCodes.Contains(t.Code))
            .ToList();

            foreach (var type in newCourtTypes)
            {
                type.CreatedBy = userId;
            }

            if (newCourtTypes.Any())
            {
                await context.CourtTypes.AddRangeAsync(newCourtTypes);
                await context.SaveChangesAsync(userId);
            }
        }

        public static List<CourtTypeEntity> GetCourtTypes(List<CourtLevelEntity> levels)
        {
            Guid GetLevelId(string code) =>
                levels.First(x => x.Code == code).Id;

            return new List<CourtTypeEntity>
        {
            // SUPREME
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Supreme Court of India",
                Code = "SCI",
                CourtLevelId = GetLevelId("SUPREME")
            },

            // HIGH COURT
            new()
            {
                Id = Guid.NewGuid(),
                Name = "High Court",
                Code = "HC",
                CourtLevelId = GetLevelId("HIGH")
            },

            // DISTRICT
            new()
            {
                Id = Guid.NewGuid(),
                Name = "District Court",
                Code = "DC",
                CourtLevelId = GetLevelId("DISTRICT")
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Sessions Court",
                Code = "SESSIONS",
                CourtLevelId = GetLevelId("DISTRICT")
            },

            // SUBORDINATE
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Civil Court",
                Code = "CIVIL",
                CourtLevelId = GetLevelId("SUBORDINATE")
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Criminal Court",
                Code = "CRIMINAL",
                CourtLevelId = GetLevelId("SUBORDINATE")
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Family Court",
                Code = "FAMILY",
                CourtLevelId = GetLevelId("SUBORDINATE")
            },

            // TRIBUNAL
            new()
            {
                Id = Guid.NewGuid(),
                Name = "National Company Law Tribunal (NCLT)",
                Code = "NCLT",
                CourtLevelId = GetLevelId("TRIBUNAL")
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Central Administrative Tribunal (CAT)",
                Code = "CAT",
                CourtLevelId = GetLevelId("TRIBUNAL")
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Debt Recovery Tribunal (DRT)",
                Code = "DRT",
                CourtLevelId = GetLevelId("TRIBUNAL")
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Consumer Forum",
                Code = "CONSUMER",
                CourtLevelId = GetLevelId("TRIBUNAL")
            },

            // SPECIAL
            new()
            {
                Id = Guid.NewGuid(),
                Name = "CBI Court",
                Code = "CBI",
                CourtLevelId = GetLevelId("SPECIAL")
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "NDPS Court",
                Code = "NDPS",
                CourtLevelId = GetLevelId("SPECIAL")
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Fast Track Court",
                Code = "FAST_TRACK",
                CourtLevelId = GetLevelId("SPECIAL")
            },
            new()
{
    Id = Guid.NewGuid(),
    Name = "Arbitration Tribunal",
    Code = "TRIBUNAL",
    CourtLevelId = GetLevelId("TRIBUNAL")
},
new()
{
    Id = Guid.NewGuid(),
    Name = "Revenue Court",
    Code = "REVENUE",
    CourtLevelId = GetLevelId("SUBORDINATE")
},
new()
{
    Id = Guid.NewGuid(),
    Name = "Panchayati Raj Court",
    Code = "PANCHAYAT",
    CourtLevelId = GetLevelId("SUBORDINATE")
}
        };
        }
    }
}
