using CourtApp.Domain.Entities.Masters;
using CourtApp.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.DataSeeder
{
    public static class CourtFormSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context, IdentityContext identity)
        {
            const string systemEmail = "superadmin@gmail.com";

            var userId = await identity.Users
                .Where(u => u.Email == systemEmail)
                .Select(u => u.Id)
                .FirstOrDefaultAsync() ?? "system";

            // =========================
            // 1. FORM TYPE SEEDING
            // =========================
            await SeedFormTypes(context, userId);

            // =========================
            // 2. FORM MASTER SEEDING
            // =========================
            await SeedFormMasters(context, userId);

            // =========================
            // 3. FORM SUBTYPE SEEDING
            // =========================
            await SeedFormSubtypes(context, userId);
        }

        // =====================================================
        // FORM TYPE
        // =====================================================
        public static readonly List<FormTypeEntity> FormTypes = new()
        {
           // CORE
            new() { Code = "NOTICE", Name = "Notice" },
            new() { Code = "APPLICATION", Name = "Application" },
            new() { Code = "PETITION", Name = "Petition" },
            new() { Code = "AFFIDAVIT", Name = "Affidavit" },

            // COURT PROCESS
            new() { Code = "APPEAL", Name = "Appeal" },
            new() { Code = "REVISION", Name = "Revision" },
            new() { Code = "EXECUTION", Name = "Execution" },

            // ADMIN / FILING
            new() { Code = "FILING", Name = "Filing" },
            new() { Code = "ADMIN", Name = "Administrative" },

            // SPECIAL (IMPORTANT FOR COURTS)
            new() { Code = "WRIT", Name = "Writ Jurisdiction" },
            new() { Code = "CRIMINAL", Name = "Criminal Matters" },
            new() { Code = "CIVIL", Name = "Civil Matters" }
        };

        private static async Task SeedFormTypes(ApplicationDbContext context, string userId)
        {
            var existing = await context.Set<FormTypeEntity>()
                .Select(x => x.Code)
                .ToListAsync();

            var data = FormTypes
                .Where(x => !existing.Contains(x.Code))
                .ToList();

            foreach (var item in data)
                item.CreatedBy = userId;

            await context.Set<FormTypeEntity>().AddRangeAsync(data);
            await context.SaveChangesAsync(userId);
        }

        // =====================================================
        // FORM MASTER
        // =====================================================
        public static readonly List<(string Code, string Name, string FormTypeCode)> FormMasters = new()
        {
            // ================= NOTICE =================
            ("SCN", "Show Cause Notice", "NOTICE"),
            ("ADM", "Notice of Admission", "NOTICE"),
            ("SUMMON", "Summons", "NOTICE"),
            ("STAY_NOTICE", "Notice of Stay Application", "NOTICE"),
            ("CAVEAT", "Caveat Notice", "NOTICE"),

            // ================= APPLICATION =================
            ("BAIL", "Bail Application", "CRIMINAL"),
            ("ANT_BAIL", "Anticipatory Bail Application", "CRIMINAL"),
            ("STAY_APP", "Stay Application", "APPLICATION"),
            ("DELAY", "Delay Condonation Application", "APPLICATION"),
            ("COPY", "Copying Application", "APPLICATION"),
            ("INSPECT", "Inspection Application", "APPLICATION"),
            ("PERMISSION", "Permission Slip", "APPLICATION"),

            // ================= PETITION =================
            ("WRIT", "Writ Petition", "WRIT"),
            ("CIVIL_PET", "Civil Petition", "CIVIL"),
            ("CRIMINAL_PET", "Criminal Petition", "CRIMINAL"),
            ("PIL", "Public Interest Litigation", "WRIT"),
            ("SLP", "Special Leave Petition", "WRIT"),
            ("REVIEW", "Review Petition", "CIVIL"),

            // ================= APPEAL =================
            ("APPEAL_CIVIL", "Civil Appeal", "APPEAL"),
            ("APPEAL_CRIMINAL", "Criminal Appeal", "APPEAL"),

            // ================= REVISION =================
            ("REVISION", "Revision Petition", "REVISION"),

            // ================= AFFIDAVIT =================
            ("AFF_EVIDENCE", "Affidavit of Evidence", "AFFIDAVIT"),
            ("AFF_SERVICE", "Affidavit of Service", "AFFIDAVIT"),
            ("COUNTER_AFF", "Counter Affidavit", "AFFIDAVIT"),

            // ================= EXECUTION =================
            ("EXECUTION", "Execution Petition", "EXECUTION"),

            // ================= ADMIN =================
            ("VAKALAT", "Vakalatnama", "ADMIN"),
            ("INDEX", "Index Sheet", "ADMIN"),
            ("ENVELOPE", "Filing Envelope", "ADMIN"),

            // ================= FILING =================
            ("COURT_FEE", "Court Fee Form", "FILING"),
            ("TALWANA", "Talwana (Process Fee)", "FILING"),
            ("FILING_MEMO", "Filing Memo", "FILING")
        };

        private static async Task SeedFormMasters(ApplicationDbContext context, string userId)
        {
            var existing = await context.Set<FormMasterEntity>()
                .Select(x => x.Code)
                .ToListAsync();

            // 🔥 dynamic FormType lookup from DB
            var formTypeDict = await context.Set<FormTypeEntity>()
                .ToDictionaryAsync(x => x.Code, x => x.Id);

            var data = new List<FormMasterEntity>();

            foreach (var item in FormMasters)
            {
                if (existing.Contains(item.Code))
                    continue;

                formTypeDict.TryGetValue(item.FormTypeCode, out var formTypeId);

                data.Add(new FormMasterEntity
                {
                    Id = Guid.NewGuid(),
                    Code = item.Code,
                    Name = item.Name,
                    FormTypeId = formTypeId,
                    CreatedBy = userId
                });
            }

            await context.AddRangeAsync(data);
            await context.SaveChangesAsync(userId);
        }

        // =====================================================
        // FORM SUBTYPE (REAL JUDICIARY FORMS)
        // =====================================================
        public static readonly List<(string Code, string Name, string FormMasterCode)> FormSubtypes = new()
{
    // ================= NOTICE =================
    ("NOTICE_SCN", "Show Cause Notice", "SCN"),
    ("NOTICE_ADM", "Notice of Admission", "ADM"),
    ("NOTICE_SUMMON", "Summons", "SUMMON"),
    ("NOTICE_STAY", "Notice of Stay Application", "STAY_NOTICE"),
    ("NOTICE_CAVEAT", "Caveat Notice", "CAVEAT"),
    ("NOTICE_CIVIL", "Notice (Civil)", "SCN"),
    ("NOTICE_WRIT", "Notice (Writ)", "SCN"),

    // ================= APPLICATION =================
    ("APP_BAIL", "Bail Application", "BAIL"),
    ("APP_ANT_BAIL", "Anticipatory Bail Application", "ANT_BAIL"),
    ("APP_STAY", "Stay Application", "STAY_APP"),
    ("APP_DELAY", "Delay Condonation Application", "DELAY"),
    ("APP_COPY", "Copying Application", "COPY"),
    ("APP_INSPECT", "Inspection Application", "INSPECT"),
    ("APP_PERMISSION", "Permission Slip", "PERMISSION"),
    

    // ================= WRIT =================
    ("WRIT_MAIN", "Writ Petition (General)", "WRIT"),
    ("WRIT_HABEAS", "Writ of Habeas Corpus", "WRIT"),
    ("WRIT_MANDAMUS", "Writ of Mandamus", "WRIT"),
    ("WRIT_PROHIBITION", "Writ of Prohibition", "WRIT"),
    ("WRIT_CERTIORARI", "Writ of Certiorari", "WRIT"),
    ("WRIT_QUO_WARRANTO", "Writ of Quo Warranto", "WRIT"),

    // ================= PETITION =================
    ("PET_CIVIL", "Civil Petition", "CIVIL_PET"),
    ("PET_CRIMINAL", "Criminal Petition", "CRIMINAL_PET"),
    ("PET_PIL", "Public Interest Litigation", "PIL"),
    ("PET_SLP", "Special Leave Petition", "SLP"),
    ("PET_REVIEW", "Review Petition", "REVIEW"),
    ("PET_REVISION", "Revision Petition", "REVISION"),

    // ================= APPEAL =================
    ("APPEAL_CIVIL", "Civil Appeal", "APPEAL_CIVIL"),
    ("APPEAL_CRIMINAL", "Criminal Appeal", "APPEAL_CRIMINAL"),

    // ================= AFFIDAVIT =================
    ("AFF_EVIDENCE", "Affidavit of Evidence", "AFF_EVIDENCE"),
    ("AFF_SERVICE", "Affidavit of Service", "AFF_SERVICE"),
    ("AFF_COUNTER", "Counter Affidavit", "COUNTER_AFF"),
   

    // ================= EXECUTION =================
    ("EXEC_MAIN", "Execution Petition", "EXECUTION"),
    

    // ================= ADMIN =================
    ("ADMIN_VAKALAT", "Vakalatnama", "VAKALAT"),
    ("ADMIN_INDEX", "Index Sheet", "INDEX"),
    ("ADMIN_ENVELOPE", "Filing Envelope", "ENVELOPE"),

    // ================= FILING =================
    ("FILING_COURT_FEE", "Court Fee Form", "COURT_FEE"),
    ("FILING_TALWANA", "Talwana (Process Fee)", "TALWANA"),
    ("FILING_MEMO", "Filing Memo", "FILING_MEMO")

    
};



        private static async Task SeedFormSubtypes(ApplicationDbContext context, string userId)
        {
            var existing = await context.Set<FormSubtypeEntity>()
                .Select(x => x.Code)
                .ToListAsync();

            // 🔥 FormMaster lookup
            var formMasterDict = await context.Set<FormMasterEntity>()
                .ToDictionaryAsync(x => x.Code, x => x.Id);

            var data = new List<FormSubtypeEntity>();

            foreach (var item in FormSubtypes)
            {
                if (existing.Contains(item.Code))
                    continue;

                if (!formMasterDict.TryGetValue(item.FormMasterCode, out var formMasterId))
                    throw new Exception($"FormMaster '{item.FormMasterCode}' not found");

                data.Add(new FormSubtypeEntity
                {
                    Id = Guid.NewGuid(),
                    Code = item.Code,
                    Name = item.Name,
                    FormId = formMasterId,
                    CreatedBy = userId
                });
            }

            if (data.Any())
            {
                await context.Set<FormSubtypeEntity>().AddRangeAsync(data);
                await context.SaveChangesAsync(userId);
            }
        }

    }
}
