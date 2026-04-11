using CourtApp.Domain.Entities.Masters;
using CourtApp.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.DataSeeder
{
    public static class CadreSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context, IdentityContext identity)
        {
            const string systemEmail = "superadmin@gmail.com";

            var userId = await identity.Users
                .Where(u => u.Email == systemEmail)
                .Select(u => u.Id)
                .FirstOrDefaultAsync() ?? "system";

            // ✅ Correct DbSet
            var existingCodes = await context.Set<CadreMasterEntity>()
                .Select(s => s.Code)
                .ToHashSetAsync();

            // ✅ Correct usage (no brackets)
            var cadres = _cadres;

            var newCadres = cadres
                .Where(s => !existingCodes.Contains(s.Code))
                .ToList();

            if (!newCadres.Any())
                return;

            foreach (var cadre in newCadres)
            {
                cadre.CreatedBy = userId;
            }

            // ✅ Correct DbSet
            await context.Set<CadreMasterEntity>().AddRangeAsync(newCadres);

            await context.SaveChangesAsync(userId);
        }

        private static readonly List<CadreMasterEntity> _cadres = new List<CadreMasterEntity>
        {
            new() { Name = "Supreme Court Judge Cadre", Code = "SCJ" },
            new() { Name = "Supreme Court Registry Service", Code = "SCRS" },

            // =========================================
            // HIGH COURT (COMMON FOR ALL STATES)
            // =========================================
            new() { Name = "High Court Judge Cadre", Code = "HCJ" },
            new() { Name = "High Court Registry Service", Code = "HCRS" },

            // =========================================
            // STATE JUDICIARY (GENERIC - ALL STATES)
            // =========================================
            new() { Name = "State Judicial Service (Entry Level)", Code = "SJS" },
            new() { Name = "Higher Judicial Service", Code = "HJS" },

            // =========================================
            // DISTRICT JUDICIARY (COMMON STRUCTURE)
            // =========================================
            new() { Name = "District Judge Cadre", Code = "DJ" },
            new() { Name = "Additional District Judge Cadre", Code = "ADJ" },
            new() { Name = "Civil Judge Cadre", Code = "CJ" },
            new() { Name = "Judicial Magistrate Cadre", Code = "JM" },

            // =========================================
            // STATE-SPECIFIC (IMPORTANT - YOUR CASE)
            // =========================================
            new() { Name = "Rajasthan Judicial Service (RJS)", Code = "RJS" },
            new() { Name = "Rajasthan Higher Judicial Service (RHJS)", Code = "RHJS" },

            new() { Name = "Uttar Pradesh Judicial Service (UPJS)", Code = "UPJS" },
            new() { Name = "Bihar Judicial Service (BJS)", Code = "BJS" },
            new() { Name = "Madhya Pradesh Judicial Service (MPJS)", Code = "MPJS" },
            new() { Name = "Delhi Judicial Service (DJS)", Code = "DJS" },
            new() { Name = "Maharashtra Judicial Service (MJS)", Code = "MJS" },

            // =========================================
            // PROSECUTION / LEGAL SUPPORT
            // =========================================
            new() { Name = "Public Prosecutor Cadre", Code = "PP" },
            new() { Name = "Assistant Public Prosecutor Cadre", Code = "APP" },
            new() { Name = "Government Advocate Cadre", Code = "GA" },
            new() { Name = "Legal Service Cadre", Code = "LS" }
        };
    }
}
