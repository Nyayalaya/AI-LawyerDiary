using CourtApp.Domain.Entities.Masters;
using CourtApp.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class WorkTypeSeeder
{
    public static async Task SeedAsync(
        ApplicationDbContext context,
        IdentityContext identityContext)
    {
        var systemUser = "superadmin@gmail.com";

        var superAdmin = await identityContext.Users
            .FirstOrDefaultAsync(x => x.Email == systemUser);

        var userId = superAdmin?.Id ?? "system";

        var courtTypes = await context.CourtTypes
            .AsNoTracking()
            .ToListAsync();

        var courtTypeDict = courtTypes
            .ToDictionary(x => x.Code, x => x.Id);

        Guid GetCourtTypeId(string code) =>
            courtTypeDict.TryGetValue(code, out var id)
                ? id
                : throw new Exception($"CourtType '{code}' not found");

        var existingCodes = await context.WorkTypes
            .Select(x => x.Code)
            .ToHashSetAsync();

        var workTypes = GetWorkTypes(GetCourtTypeId);

        var newData = workTypes
            .Where(x => !existingCodes.Contains(x.Code))
            .ToList();

        if (!newData.Any())
            return;

        foreach (var item in newData)
        {
            item.CreatedBy = userId;
        }

        await context.WorkTypes.AddRangeAsync(newData);

        await context.SaveChangesAsync(userId);
    }

    private static List<WorkTypeEntity> GetWorkTypes(
        Func<string, Guid> GetCourtTypeId)
    {
        return new List<WorkTypeEntity>
        {
            // =====================================================
            // COMMON COURT FILING / LITIGATION WORK
            // =====================================================

            new()
            {
                Name = "Affidavit",
                Code = "AFFIDAVIT",
                CourtTypeId = GetCourtTypeId("DC")
            },

            new()
            {
                Name = "Appeal",
                Code = "APPEAL",
                CourtTypeId = GetCourtTypeId("HC")
            },

            new()
            {
                Name = "Application",
                Code = "APPLICATION",
                CourtTypeId = GetCourtTypeId("DC")
            },

            new()
            {
                Name = "Caveat",
                Code = "CAVEAT",
                CourtTypeId = GetCourtTypeId("HC")
            },

            new()
            {
                Name = "Claim Petition",
                Code = "CLAIM_PETITION",
                CourtTypeId = GetCourtTypeId("TRIBUNAL")
            },

            new()
            {
                Name = "Compliance",
                Code = "COMPLIANCE",
                CourtTypeId = GetCourtTypeId("DC")
            },

            new()
            {
                Name = "Counter Affidavit",
                Code = "COUNTER_AFFIDAVIT",
                CourtTypeId = GetCourtTypeId("HC")
            },

            new()
            {
                Name = "Defect Compliance",
                Code = "DEFECT_COMPLIANCE",
                CourtTypeId = GetCourtTypeId("HC")
            },

            new()
            {
                Name = "Document Filing",
                Code = "DOCUMENT_FILING",
                CourtTypeId = GetCourtTypeId("DC")
            },

            new()
            {
                Name = "Drafting",
                Code = "DRAFTING",
                CourtTypeId = GetCourtTypeId("DC")
            },

            new()
            {
                Name = "Inspection",
                Code = "INSPECTION",
                CourtTypeId = GetCourtTypeId("DC")
            },

            new()
            {
                Name = "Judgment Copy",
                Code = "JUDGMENT_COPY",
                CourtTypeId = GetCourtTypeId("HC")
            },

            new()
            {
                Name = "Objection",
                Code = "OBJECTION",
                CourtTypeId = GetCourtTypeId("DC")
            },

            new()
            {
                Name = "Paper Book",
                Code = "PAPER_BOOK",
                CourtTypeId = GetCourtTypeId("HC")
            },

            new()
            {
                Name = "Power of Attorney",
                Code = "POWER_OF_ATTORNEY",
                CourtTypeId = GetCourtTypeId("DC")
            },

            new()
            {
                Name = "Process Fee Filing",
                Code = "PROCESS_FEE",
                CourtTypeId = GetCourtTypeId("DC")
            },

            new()
            {
                Name = "Reply",
                Code = "REPLY",
                CourtTypeId = GetCourtTypeId("DC")
            },

            new()
            {
                Name = "Restoration Application",
                Code = "RESTORATION",
                CourtTypeId = GetCourtTypeId("DC")
            },

            new()
            {
                Name = "Synopsis",
                Code = "SYNOPSIS",
                CourtTypeId = GetCourtTypeId("SCI")
            },

            new()
            {
                Name = "Vakalatnama",
                Code = "VAKALATNAMA",
                CourtTypeId = GetCourtTypeId("DC")
            },

            new()
            {
                Name = "Written Arguments",
                Code = "WRITTEN_ARGUMENTS",
                CourtTypeId = GetCourtTypeId("HC")
            },

            new()
            {
                Name = "Written Statement",
                Code = "WRITTEN_STATEMENT",
                CourtTypeId = GetCourtTypeId("DC")
            },

            // =====================================================
            // NOTICE / SUMMON RELATED
            // =====================================================

            new()
            {
                Name = "Fresh Notice",
                Code = "FRESH_NOTICE",
                CourtTypeId = GetCourtTypeId("DC")
            },

            new()
            {
                Name = "Notice of Admission",
                Code = "NOTICE_ADMISSION",
                CourtTypeId = GetCourtTypeId("HC")
            },

            new()
            {
                Name = "Notice to Legal Representatives",
                Code = "NOTICE_LR",
                CourtTypeId = GetCourtTypeId("DC")
            },

            new()
            {
                Name = "Show Cause Notice",
                Code = "SHOW_CAUSE_NOTICE",
                CourtTypeId = GetCourtTypeId("DC")
            },

            new()
            {
                Name = "Summons",
                Code = "SUMMONS",
                CourtTypeId = GetCourtTypeId("DC")
            },

            new()
            {
                Name = "Accused Summons",
                Code = "ACCUSED_SUMMONS",
                CourtTypeId = GetCourtTypeId("CRIMINAL")
            },

            new()
            {
                Name = "Witness Summons",
                Code = "WITNESS_SUMMONS",
                CourtTypeId = GetCourtTypeId("CRIMINAL")
            },

            new()
            {
                Name = "Substituted Service",
                Code = "SUBSTITUTED_SERVICE",
                CourtTypeId = GetCourtTypeId("DC")
            },

            // =====================================================
            // COPY / CERTIFIED COPY
            // =====================================================

            new()
            {
                Name = "Certified Copy",
                Code = "CERTIFIED_COPY",
                CourtTypeId = GetCourtTypeId("DC")
            },

            new()
            {
                Name = "Copy Application",
                Code = "COPY_APPLICATION",
                CourtTypeId = GetCourtTypeId("DC")
            },

            new()
            {
                Name = "Supply of Copy",
                Code = "SUPPLY_COPY",
                CourtTypeId = GetCourtTypeId("DC")
            },

            // =====================================================
            // FAMILY / CIVIL SPECIFIC
            // =====================================================

            new()
            {
                Name = "Amended Pleading",
                Code = "AMENDED_PLEADING",
                CourtTypeId = GetCourtTypeId("FAMILY")
            },

            new()
            {
                Name = "Amended Reply",
                Code = "AMENDED_REPLY",
                CourtTypeId = GetCourtTypeId("FAMILY")
            },

            new()
            {
                Name = "List of Witnesses",
                Code = "WITNESS_LIST",
                CourtTypeId = GetCourtTypeId("CIVIL")
            },

            // =====================================================
            // SPECIAL / ADMINISTRATIVE
            // =====================================================

            new()
            {
                Name = "Cost Deposit",
                Code = "COST_DEPOSIT",
                CourtTypeId = GetCourtTypeId("DC")
            },

            new()
            {
                Name = "Death Certificate Filing",
                Code = "DEATH_CERTIFICATE",
                CourtTypeId = GetCourtTypeId("DC")
            },

            new()
            {
                Name = "Enquiry",
                Code = "ENQUIRY",
                CourtTypeId = GetCourtTypeId("DC")
            },

            new()
            {
                Name = "Extra Set Filing",
                Code = "EXTRA_SET",
                CourtTypeId = GetCourtTypeId("HC")
            },

            new()
            {
                Name = "Intimation",
                Code = "INTIMATION",
                CourtTypeId = GetCourtTypeId("DC")
            },

            new()
            {
                Name = "Order Sheet",
                Code = "ORDER_SHEET",
                CourtTypeId = GetCourtTypeId("DC")
            },

            new()
            {
                Name = "Requisition",
                Code = "REQUISITION",
                CourtTypeId = GetCourtTypeId("DC")
            },

            new()
            {
                Name = "Step for Legal Representatives",
                Code = "STEP_LR",
                CourtTypeId = GetCourtTypeId("DC")
            }
        };
    }
}
