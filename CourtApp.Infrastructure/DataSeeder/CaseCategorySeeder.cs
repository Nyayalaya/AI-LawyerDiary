using CourtApp.Domain.Entities.Masters;
using CourtApp.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class CaseCategorySeeder
{
    public static async Task SeedCaseCategoriesAsync(ApplicationDbContext context, IdentityContext identityContext)
    {
        var systemUser = "superadmin@gmail.com";

        var superAdmin = await identityContext.Users
            .FirstOrDefaultAsync(u => u.Email == systemUser);

        var userId = superAdmin != null ? superAdmin.Id : "system";
        var courtTypes = await context.CourtTypes.AsNoTracking().ToListAsync();

        var courtTypeDict = courtTypes.ToDictionary(x => x.Code, x => x.Id);

        Guid GetCourtTypeId(string code) =>
            courtTypeDict.ContainsKey(code)
                ? courtTypeDict[code]
                : throw new Exception($"CourtType '{code}' not found");

        var existingCodes = await context.CaseCategories
            .Select(x => x.Code)
            .ToHashSetAsync();

        var categories = GetCategories(GetCourtTypeId);

        var newData = categories
            .Where(x => !existingCodes.Contains(x.Code))
            .ToList();

        if (!newData.Any()) return;

        foreach (var category in newData)
        {
            category.CreatedBy = userId;
        }

        await context.CaseCategories.AddRangeAsync(newData);
        await context.SaveChangesAsync(userId);
    }

    private static List<CaseCategoryEntity> GetCategories(Func<string, Guid> GetCourtTypeId)
    {
        return new List<CaseCategoryEntity>
        {
            // =========================
            // SUPREME COURT
            // =========================
            new() { Name="Constitutional Matters", Code="SCI_CONST", CourtTypeId=GetCourtTypeId("SCI") },
            new() { Name="SLP (Special Leave Petition)", Code="SCI_SLP", CourtTypeId=GetCourtTypeId("SCI") },

            // =========================
            // HIGH COURT
            // =========================
            new() { Name="Civil", Code="HC_CIVIL", CourtTypeId=GetCourtTypeId("HC") },
            new() { Name="Criminal", Code="HC_CRIMINAL", CourtTypeId=GetCourtTypeId("HC") },
            new() { Name="Writ", Code="HC_WRIT", CourtTypeId=GetCourtTypeId("HC") },

            // =========================
            // DISTRICT COURT
            // =========================
            new() { Name="Civil", Code="DC_CIVIL", CourtTypeId=GetCourtTypeId("DC") },
            new() { Name="Criminal", Code="DC_CRIMINAL", CourtTypeId=GetCourtTypeId("DC") },

            // =========================
            // SESSIONS COURT
            // =========================
            new() { Name="Sessions Criminal", Code="SESS_CRIMINAL", CourtTypeId=GetCourtTypeId("SESSIONS") },

            // =========================
            // CIVIL COURT
            // =========================
            new() { Name="Civil Suit", Code="CIVIL_SUIT", CourtTypeId=GetCourtTypeId("CIVIL") },

            // =========================
            // CRIMINAL COURT
            // =========================
            new() { Name="Criminal Trial", Code="CRIMINAL_TRIAL", CourtTypeId=GetCourtTypeId("CRIMINAL") },

            // =========================
            // FAMILY COURT
            // =========================
            new() { Name="Divorce", Code="FAMILY_DIVORCE", CourtTypeId=GetCourtTypeId("FAMILY") },
            new() { Name="Maintenance", Code="FAMILY_MAINT", CourtTypeId=GetCourtTypeId("FAMILY") },

            // =========================
            // CONSUMER COURT
            // =========================
            new() { Name="Consumer Cases", Code="CONSUMER_CASE", CourtTypeId=GetCourtTypeId("CONSUMER") },

            // =========================
            // CAT (TRIBUNAL)
            // =========================
            new() { Name="Service Matter", Code="CAT_SERVICE", CourtTypeId=GetCourtTypeId("CAT") },

            // =========================
            // DRT
            // =========================
            new() { Name="Debt Recovery", Code="DRT_RECOVERY", CourtTypeId=GetCourtTypeId("DRT") },

            // =========================
            // NCLT
            // =========================
            new() { Name="Insolvency", Code="NCLT_INSOLVENCY", CourtTypeId=GetCourtTypeId("NCLT") },

            // =========================
            // ARBITRATION TRIBUNAL ✅ (NEW)
            // =========================
            new() { Name="Arbitration Claim Case", Code="ARBITRATION_CLAIM", CourtTypeId=GetCourtTypeId("TRIBUNAL") },

            // =========================
            // REVENUE COURT ✅ (NEW)
            // =========================
            new() { Name="Tenancy Laws Case", Code="REVENUE_TENANCY", CourtTypeId=GetCourtTypeId("TRIBUNAL") },

            // =========================
            // PANCHAYATI RAJ ✅ (NEW)
            // =========================
            //new() { Name="Panchayati Raj Matters", Code="PANCHAYAT_CASE", CourtTypeId=GetCourtTypeId("SUBORDINATE") },

            // =========================
            // SPECIAL COURTS
            // =========================
            new() { Name="CBI Case", Code="CBI_CASE", CourtTypeId=GetCourtTypeId("CBI") },
            new() { Name="NDPS Case", Code="NDPS_CASE", CourtTypeId=GetCourtTypeId("NDPS") },
            new() { Name="Fast Track Case", Code="FAST_TRACK", CourtTypeId=GetCourtTypeId("FAST_TRACK") }
        };
    }
}
