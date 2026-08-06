using CourtApp.Domain.Entities.Masters;
using CourtApp.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Persistence.Seeds
{
    public static class ProceedingSeeder
    {
        public static async Task SeedAsync(
            ApplicationDbContext context,
            IdentityContext identityContext)
        {
            var systemUser = "superadmin@gmail.com";

            var superAdmin = await identityContext.Users
                .FirstOrDefaultAsync(x => x.Email == systemUser);

            var userId = superAdmin != null
                ? superAdmin.Id
                : "system";

            // =========================
            // Proceeding Types
            // =========================
            var proceedingTypes = await context.ProceedingTypes
                .AsNoTracking()
                .ToListAsync();

            var proceedingTypeDict = proceedingTypes
                .ToDictionary(x => x.Code, x => x.Id);

            Guid GetProceedingTypeId(string code)
            {
                return proceedingTypeDict.ContainsKey(code)
                    ? proceedingTypeDict[code]
                    : throw new Exception($"Proceeding Type '{code}' not found");
            }

            // =========================
            // Existing Proceedings
            // =========================
            var existingCodes = await context.Proceedings
                .Select(x => x.Code)
                .ToHashSetAsync();

            var proceedings = GetProceedings(GetProceedingTypeId);

            var newData = proceedings
                .Where(x => !existingCodes.Contains(x.Code))
                .ToList();

            if (!newData.Any())
                return;

            foreach (var item in newData)
            {
                item.CreatedBy = userId;
            }

            await context.Proceedings.AddRangeAsync(newData);

            await context.SaveChangesAsync(userId);
        }

        private static List<ProceedingEntity> GetProceedings(
            Func<string, Guid> GetProceedingTypeId)
        {
            return new List<ProceedingEntity>
            {
                // =====================================================
                // ADMISSION
                // =====================================================
                new()
                {
                    Name = "Admission Hearing",
                    Code = "ADMISSION_HEARING",
                    ProceedingTypeId = GetProceedingTypeId("ADMISSION")
                },
                new()
                {
                    Name = "Admission Allowed",
                    Code = "ADMISSION_ALLOWED",
                    ProceedingTypeId = GetProceedingTypeId("ADMISSION")
                },

                // =====================================================
                // ARGUMENTS
                // =====================================================
                new()
                {
                    Name = "Final Arguments",
                    Code = "FINAL_ARGUMENTS",
                    ProceedingTypeId = GetProceedingTypeId("ARGUMENTS")
                },
                new()
                {
                    Name = "Partial Arguments",
                    Code = "PARTIAL_ARGUMENTS",
                    ProceedingTypeId = GetProceedingTypeId("ARGUMENTS")
                },

                // =====================================================
                // BAIL
                // =====================================================
                new()
                {
                    Name = "Regular Bail",
                    Code = "REGULAR_BAIL",
                    ProceedingTypeId = GetProceedingTypeId("BAIL")
                },
                new()
                {
                    Name = "Anticipatory Bail",
                    Code = "ANTICIPATORY_BAIL",
                    ProceedingTypeId = GetProceedingTypeId("BAIL")
                },
                new()
                {
                    Name = "Interim Bail",
                    Code = "INTERIM_BAIL",
                    ProceedingTypeId = GetProceedingTypeId("BAIL")
                },

                // =====================================================
                // EVIDENCE
                // =====================================================
                new()
                {
                    Name = "Plaintiff Evidence",
                    Code = "PLAINTIFF_EVIDENCE",
                    ProceedingTypeId = GetProceedingTypeId("EVIDENCE")
                },
                new()
                {
                    Name = "Defendant Evidence",
                    Code = "DEFENDANT_EVIDENCE",
                    ProceedingTypeId = GetProceedingTypeId("EVIDENCE")
                },
                new()
                {
                    Name = "Additional Evidence",
                    Code = "ADDITIONAL_EVIDENCE_PROC",
                    ProceedingTypeId = GetProceedingTypeId("EVIDENCE")
                },

                // =====================================================
                // CROSS EXAMINATION
                // =====================================================
                new()
                {
                    Name = "Witness Cross Examination",
                    Code = "WITNESS_CROSS_EXAM",
                    ProceedingTypeId = GetProceedingTypeId("CROSS_EXAMINATION")
                },

                // =====================================================
                // NOTICE
                // =====================================================
                new()
                {
                    Name = "Fresh Notice Issued",
                    Code = "FRESH_NOTICE_ISSUED",
                    ProceedingTypeId = GetProceedingTypeId("FRESH_NOTICES")
                },
                new()
                {
                    Name = "Notice Served",
                    Code = "NOTICE_SERVED",
                    ProceedingTypeId = GetProceedingTypeId("NOTICE_ACCEPTED")
                },
                new()
                {
                    Name = "Notice Unserved",
                    Code = "NOTICE_UNSERVED",
                    ProceedingTypeId = GetProceedingTypeId("NOTICE_AWAITED")
                },

                // =====================================================
                // STAY
                // =====================================================
                new()
                {
                    Name = "Stay Granted",
                    Code = "STAY_GRANTED",
                    ProceedingTypeId = GetProceedingTypeId("STAY")
                },
                new()
                {
                    Name = "Stay Vacated",
                    Code = "STAY_VACATED",
                    ProceedingTypeId = GetProceedingTypeId("STAY_VACATION")
                },
                new()
                {
                    Name = "Interim Stay Granted",
                    Code = "INTERIM_STAY_GRANTED",
                    ProceedingTypeId = GetProceedingTypeId("INTERIM_STAY")
                },

                // =====================================================
                // ADJOURNMENT
                // =====================================================
                new()
                {
                    Name = "Adjourned for Arguments",
                    Code = "ADJ_FOR_ARGUMENTS",
                    ProceedingTypeId = GetProceedingTypeId("ADJOURNMENT")
                },
                new()
                {
                    Name = "Adjourned for Evidence",
                    Code = "ADJ_FOR_EVIDENCE",
                    ProceedingTypeId = GetProceedingTypeId("ADJOURNMENT")
                },
                new()
                {
                    Name = "Adjourned at Request",
                    Code = "ADJ_REQUEST",
                    ProceedingTypeId = GetProceedingTypeId("ADJOURNMENT")
                },

                // =====================================================
                // DISPOSAL
                // =====================================================
                new()
                {
                    Name = "Disposed as Withdrawn",
                    Code = "DISPOSED_WITHDRAWN",
                    ProceedingTypeId = GetProceedingTypeId("DISPOSAL")
                },
                new()
                {
                    Name = "Disposed on Merits",
                    Code = "DISPOSED_MERITS",
                    ProceedingTypeId = GetProceedingTypeId("DISPOSAL")
                },
                new()
                {
                    Name = "Finally Disposed",
                    Code = "FINALLY_DISPOSED",
                    ProceedingTypeId = GetProceedingTypeId("FINAL_DISPOSAL")
                },

                // =====================================================
                // JUDGMENT
                // =====================================================
                new()
                {
                    Name = "Judgment Pronounced",
                    Code = "JUDGMENT_PRONOUNCED",
                    ProceedingTypeId = GetProceedingTypeId("JUDGMENT")
                },
                new()
                {
                    Name = "Judgment Reserved",
                    Code = "JUDGMENT_RESERVED",
                    ProceedingTypeId = GetProceedingTypeId("JUDGMENT")
                },

                // =====================================================
                // EXECUTION
                // =====================================================
                new()
                {
                    Name = "Execution Filed",
                    Code = "EXECUTION_FILED",
                    ProceedingTypeId = GetProceedingTypeId("EXECUTION")
                },
                new()
                {
                    Name = "Execution Allowed",
                    Code = "EXECUTION_ALLOWED",
                    ProceedingTypeId = GetProceedingTypeId("EXECUTION")
                },

                // =====================================================
                // MEDIATION
                // =====================================================
                new()
                {
                    Name = "Matter Referred to Mediation",
                    Code = "REFERRED_MEDIATION",
                    ProceedingTypeId = GetProceedingTypeId("MEDIATION")
                },
                new()
                {
                    Name = "Mediation Successful",
                    Code = "MEDIATION_SUCCESS",
                    ProceedingTypeId = GetProceedingTypeId("MEDIATION")
                },
                new()
                {
                    Name = "Mediation Failed",
                    Code = "MEDIATION_FAILED",
                    ProceedingTypeId = GetProceedingTypeId("MEDIATION")
                },

                // =====================================================
                // LOK ADALAT
                // =====================================================
                new()
                {
                    Name = "Referred to Lok Adalat",
                    Code = "REFERRED_LOK_ADALAT",
                    ProceedingTypeId = GetProceedingTypeId("LOK_ADALAT")
                },

                // =====================================================
                // SUMMONS
                // =====================================================
                new()
                {
                    Name = "Summons Issued",
                    Code = "SUMMONS_ISSUED",
                    ProceedingTypeId = GetProceedingTypeId("SUMMONS")
                },
                new()
                {
                    Name = "Summons Served",
                    Code = "SUMMONS_SERVED",
                    ProceedingTypeId = GetProceedingTypeId("SUMMONS")
                },

                // =====================================================
                // WARRANTS
                // =====================================================
                new()
                {
                    Name = "Bailable Warrant",
                    Code = "BAILABLE_WARRANT",
                    ProceedingTypeId = GetProceedingTypeId("WARRANTS")
                },
                new()
                {
                    Name = "Non-Bailable Warrant",
                    Code = "NON_BAILABLE_WARRANT",
                    ProceedingTypeId = GetProceedingTypeId("WARRANTS")
                },

                // =====================================================
                // COMPROMISE
                // =====================================================
                new()
                {
                    Name = "Compromise Filed",
                    Code = "COMPROMISE_FILED",
                    ProceedingTypeId = GetProceedingTypeId("COMPROMISE")
                },
                new()
                {
                    Name = "Compromise Accepted",
                    Code = "COMPROMISE_ACCEPTED",
                    ProceedingTypeId = GetProceedingTypeId("COMPROMISE")
                },

                // =====================================================
                // STATUS REPORT
                // =====================================================
                new()
                {
                    Name = "Status Report Filed",
                    Code = "STATUS_REPORT_FILED",
                    ProceedingTypeId = GetProceedingTypeId("STATUS_REPORT")
                },

                // =====================================================
                // WRITTEN STATEMENT
                // =====================================================
                new()
                {
                    Name = "Written Statement Filed",
                    Code = "WRITTEN_STATEMENT_FILED",
                    ProceedingTypeId = GetProceedingTypeId("WRITTEN_STATEMENT")
                },

                // =====================================================
                // REJOINDER
                // =====================================================
                new()
                {
                    Name = "Rejoinder Filed",
                    Code = "REJOINDER_FILED",
                    ProceedingTypeId = GetProceedingTypeId("REJOINDER")
                },

                // =====================================================
                // POWER OF ATTORNEY
                // =====================================================
                new()
                {
                    Name = "Vakalatnama Filed",
                    Code = "VAKALATNAMA_FILED",
                    ProceedingTypeId = GetProceedingTypeId("POWER_OF_ATTORNEY")
                }
            };
        }
    }
}
