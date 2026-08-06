using CourtApp.Domain.Entities.Masters;
using CourtApp.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.DataSeeder
{
    public static class FormCaseCategoryMappingSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context, IdentityContext identity)
        {
            const string systemEmail = "superadmin@gmail.com";

            var userId = await identity.Users
                .Where(u => u.Email == systemEmail)
                .Select(u => u.Id)
                .FirstOrDefaultAsync() ?? "system";

            // =========================
            // LOAD DATA FROM DB
            // =========================
            var formSubtypes = await context.Set<FormSubtypeEntity>().ToListAsync();
            var caseCategories = await context.CaseCategories.ToListAsync();

            var formDict = formSubtypes.ToDictionary(x => x.Code, x => x.Id);
            var caseDict = caseCategories.ToDictionary(x => x.Code, x => x.Id);

            // =========================
            // EXISTING CHECK
            // =========================
            var existing = await context.Set<FormCaseCategoryMapping>()
                .Select(x => x.FormSubtypeId.ToString() + "_" + x.CaseCategoryId.ToString())
                .ToHashSetAsync();

            // =========================
            // SEED DATA
            // =========================
            var data = GetMappings(formDict, caseDict);

            var newData = data
                .Where(x => !existing.Contains(x.FormSubtypeId + "_" + x.CaseCategoryId))
                .ToList();

            if (!newData.Any())
                return;

            foreach (var item in newData)
                item.CreatedBy = userId;

            await context.AddRangeAsync(newData);
            await context.SaveChangesAsync(userId);
        }

        // =====================================================
        // CORE MAPPING LOGIC (INDIAN JUDICIARY)
        // =====================================================
        private static List<FormCaseCategoryMapping> GetMappings(
    Dictionary<string, Guid> formDict,
    Dictionary<string, Guid> caseDict)
        {
            return new List<FormCaseCategoryMapping>
    {
        // ================= HIGH COURT WRIT =================
        Map(formDict, caseDict, "WRIT_MAIN", "HC_WRIT", true),
        Map(formDict, caseDict, "APP_STAY", "HC_WRIT"),
        Map(formDict, caseDict, "NOTICE_ADM", "HC_WRIT"),
        Map(formDict, caseDict, "NOTICE_SCN", "HC_WRIT"),

        // Optional: specific writs
        Map(formDict, caseDict, "WRIT_HABEAS", "HC_WRIT"),
        Map(formDict, caseDict, "WRIT_MANDAMUS", "HC_WRIT"),

        // ================= HIGH COURT CIVIL =================
        Map(formDict, caseDict, "PET_CIVIL", "HC_CIVIL", true),
        Map(formDict, caseDict, "APP_STAY", "HC_CIVIL"),
        Map(formDict, caseDict, "APP_COPY", "HC_CIVIL"),

        // ================= DISTRICT CIVIL =================
        Map(formDict, caseDict, "PET_CIVIL", "DC_CIVIL", true),
        Map(formDict, caseDict, "APP_COPY", "DC_CIVIL"),

        // ================= DISTRICT CRIMINAL =================
        Map(formDict, caseDict, "APP_BAIL", "DC_CRIMINAL", true),
        Map(formDict, caseDict, "APP_ANT_BAIL", "DC_CRIMINAL"),
        Map(formDict, caseDict, "NOTICE_SUMMON", "DC_CRIMINAL"),

        // ================= FAMILY =================
        Map(formDict, caseDict, "PET_CIVIL", "FAMILY_DIVORCE", true),
        

        // ================= CONSUMER =================
        Map(formDict, caseDict, "PET_PIL", "CONSUMER_CASE", true),

        // ================= SUPREME COURT =================
        Map(formDict, caseDict, "PET_SLP", "SCI_SLP", true),
        Map(formDict, caseDict, "WRIT_MAIN", "SCI_CONST"),

        // ================= ADMIN / COURT FORMS =================
        Map(formDict, caseDict, "ADMIN_VAKALAT", "HC_CIVIL", true),
        Map(formDict, caseDict, "ADMIN_INDEX", "HC_CIVIL"),
        Map(formDict, caseDict, "FILING_TALWANA", "HC_CIVIL"),
        Map(formDict, caseDict, "ADMIN_ENVELOPE", "HC_CIVIL"),
        Map(formDict, caseDict, "FILING_COURT_FEE", "HC_CIVIL")
    };
        }


        // =====================================================
        // SAFE MAPPING HELPER
        // =====================================================
        private static FormCaseCategoryMapping Map(
            Dictionary<string, Guid> formDict,
            Dictionary<string, Guid> caseDict,
            string formCode,
            string caseCode,
            bool isMandatory = false)
        {
            if (!formDict.ContainsKey(formCode))
                throw new Exception($"FormSubtype '{formCode}' not found");

            if (!caseDict.ContainsKey(caseCode))
                throw new Exception($"CaseCategory '{caseCode}' not found");

            return new FormCaseCategoryMapping
            {
                Id = Guid.NewGuid(),
                FormSubtypeId = formDict[formCode],
                CaseCategoryId = caseDict[caseCode],
                IsMandatory = isMandatory,
                IsActive = true
            };
        }
    }
}
