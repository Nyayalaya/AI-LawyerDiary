using CourtApp.Domain.Entities.Masters;
using CourtApp.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Persistence.Seeds
{
    public static class ProceedingTypeSeeder
    {
        public static async Task SeedAsync(
            ApplicationDbContext context,
            IdentityContext identityContext)
        {
            var systemUser = "superadmin@gmail.com";

            var superAdmin = await identityContext.Users
                .FirstOrDefaultAsync(u => u.Email == systemUser);

            var userId = superAdmin != null
                ? superAdmin.Id
                : "system";

            var existingCodes = await context.ProceedingTypes
                .Select(x => x.Code)
                .ToHashSetAsync();

            var proceedingTypes = GetProceedingTypes();

            var newData = proceedingTypes
                .Where(x => !existingCodes.Contains(x.Code))
                .ToList();

            if (!newData.Any())
                return;

            foreach (var item in newData)
            {
                item.CreatedBy = userId;
            }

            await context.ProceedingTypes.AddRangeAsync(newData);

            await context.SaveChangesAsync(userId);
        }

        private static List<ProceedingTypeEntity> GetProceedingTypes()
        {
            return new List<ProceedingTypeEntity>
            {
                // =========================
                // ADMISSION / PRELIMINARY
                // =========================
                new() { Name = "Admission", Code = "ADMISSION" },
                new() { Name = "Admission Writ", Code = "ADMISSION_WRIT" },
                new() { Name = "Admission Stay", Code = "ADMISSION_STAY" },
                new() { Name = "Ad Interim", Code = "AD_INTERIM" },
                new() { Name = "Affidavit", Code = "AFFIDAVIT" },
                new() { Name = "Amendment", Code = "AMENDMENT" },
                new() { Name = "Amended Pleading", Code = "AMENDED_PLEADING" },
                new() { Name = "Appearance", Code = "APPEARANCE" },

                // =========================
                // APPLICATIONS
                // =========================
                new() { Name = "Application Under Order 41 Rule 27", Code = "APPLICATION_O41_R27" },
                new() { Name = "Additional Evidence", Code = "ADDITIONAL_EVIDENCE" },

                // =========================
                // ARGUMENT / EVIDENCE
                // =========================
                new() { Name = "Arguments", Code = "ARGUMENTS" },
                new() { Name = "Cross Examination", Code = "CROSS_EXAMINATION" },
                new() { Name = "Cross Objection", Code = "CROSS_OBJECTION" },
                new() { Name = "Counter Affidavit", Code = "COUNTER_AFFIDAVIT" },
                new() { Name = "Evidence", Code = "EVIDENCE" },
                new() { Name = "Written Arguments", Code = "WRITTEN_ARGUMENTS" },
                new() { Name = "Written Statement", Code = "WRITTEN_STATEMENT" },

                // =========================
                // BAIL
                // =========================
                new() { Name = "Bail", Code = "BAIL" },
                new() { Name = "Temporary Bail", Code = "TEMP_BAIL" },
                new() { Name = "Bail Jump", Code = "BAIL_JUMP" },

                // =========================
                // CRIMINAL
                // =========================
                new() { Name = "Attendance of Accused", Code = "ATTENDANCE_ACCUSED" },
                new() { Name = "Case Diary", Code = "CASE_DIARY" },
                new() { Name = "Challan", Code = "CHALLAN" },
                new() { Name = "Charge", Code = "CHARGE" },
                new() { Name = "Cognizance", Code = "COGNIZANCE" },
                new() { Name = "Statement of Accused", Code = "STATEMENT_ACCUSED" },
                new() { Name = "Investigation Officer", Code = "INVESTIGATION_OFFICER" },
                new() { Name = "FSL", Code = "FSL" },
                new() { Name = "Warrants", Code = "WARRANTS" },

                // =========================
                // NOTICE / SERVICE
                // =========================
                new() { Name = "Fresh Notices", Code = "FRESH_NOTICES" },
                new() { Name = "Notice Accepted", Code = "NOTICE_ACCEPTED" },
                new() { Name = "Notice Awaited", Code = "NOTICE_AWAITED" },
                new() { Name = "Notice to Legal Representatives", Code = "NOTICE_TO_LRS" },
                new() { Name = "Notice with Record", Code = "NOTICE_WITH_RECORD" },
                new() { Name = "Notices of Admission", Code = "NOTICES_ADMISSION" },
                new() { Name = "Show Cause Notice", Code = "SHOW_CAUSE_NOTICE" },
                new() { Name = "Show Cause Notice with Record", Code = "SHOW_CAUSE_NOTICE_RECORD" },
                new() { Name = "Service Complete", Code = "SERVICE_COMPLETE" },
                new() { Name = "Service Dispensed", Code = "SERVICE_DISPENSED" },
                new() { Name = "Service Incomplete", Code = "SERVICE_INCOMPLETE" },
                new() { Name = "Substituted Service", Code = "SUBSTITUTED_SERVICE" },
                new() { Name = "Summons", Code = "SUMMONS" },

                // =========================
                // STAY MATTERS
                // =========================
                new() { Name = "Stay", Code = "STAY" },
                new() { Name = "Interim Stay", Code = "INTERIM_STAY" },
                new() { Name = "Ex Parte Stay", Code = "EX_PARTE_STAY" },
                new() { Name = "Stay Modified", Code = "STAY_MODIFIED" },
                new() { Name = "Stay Vacation", Code = "STAY_VACATION" },
                new() { Name = "Proceedings Stayed", Code = "PROCEEDINGS_STAYED" },

                // =========================
                // HEARING
                // =========================
                new() { Name = "Adjournment", Code = "ADJOURNMENT" },
                new() { Name = "Early Hearing", Code = "EARLY_HEARING" },
                new() { Name = "Non Appearance", Code = "NON_APPEARANCE" },
                new() { Name = "Time Extension", Code = "TIME_EXTENSION" },
                new() { Name = "Time Sought", Code = "TIME_SOUGHT" },

                // =========================
                // DISPOSAL
                // =========================
                new() { Name = "Disposal", Code = "DISPOSAL" },
                new() { Name = "Disposed Off", Code = "DISPOSED_OFF" },
                new() { Name = "Disposal Recalled", Code = "DISPOSAL_RECALLED" },
                new() { Name = "Dismissal in Default", Code = "DISMISSAL_DEFAULT" },
                new() { Name = "Final Disposal", Code = "FINAL_DISPOSAL" },
                new() { Name = "Judgment", Code = "JUDGMENT" },
                new() { Name = "Restoration", Code = "RESTORATION" },

                // =========================
                // EXECUTION
                // =========================
                new() { Name = "Execution", Code = "EXECUTION" },
                new() { Name = "Possession Warrant", Code = "POSSESSION_WARRANT" },

                // =========================
                // SPECIAL PROCEEDINGS
                // =========================
                new() { Name = "Caveat", Code = "CAVEAT" },
                new() { Name = "Commissioner", Code = "COMMISSIONER" },
                new() { Name = "Compromise", Code = "COMPROMISE" },
                new() { Name = "Consolidation", Code = "CONSOLIDATION" },
                new() { Name = "Delay", Code = "DELAY" },
                new() { Name = "Denial and Admission", Code = "DENIAL_ADMISSION" },
                new() { Name = "Ex Parte", Code = "EX_PARTE" },
                new() { Name = "Habeas Corpus", Code = "HABEAS_CORPUS" },
                new() { Name = "Inspection", Code = "INSPECTION" },
                new() { Name = "Legal Representatives", Code = "LEGAL_REPRESENTATIVES" },
                new() { Name = "Lok Adalat", Code = "LOK_ADALAT" },
                new() { Name = "Mediation", Code = "MEDIATION" },
                new() { Name = "Mediation Center", Code = "MEDIATION_CENTER" },
                new() { Name = "Mesne Profit", Code = "MESNE_PROFIT" },
                new() { Name = "Paper Book", Code = "PAPER_BOOK" },
                new() { Name = "Power of Attorney", Code = "POWER_OF_ATTORNEY" },
                new() { Name = "Preliminary Objection", Code = "PRELIMINARY_OBJECTION" },
                new() { Name = "Publication", Code = "PUBLICATION" },
                new() { Name = "Rejection Order", Code = "REJECTION_ORDER" },
                new() { Name = "Rejoinder", Code = "REJOINDER" },
                new() { Name = "Status Report", Code = "STATUS_REPORT" },
                new() { Name = "Subsequent Event", Code = "SUBSEQUENT_EVENT" },
                new() { Name = "Substitution", Code = "SUBSTITUTION" },
                new() { Name = "Summoning Record", Code = "SUMMONING_RECORD" },
                new() { Name = "Suspension", Code = "SUSPENSION" },
                new() { Name = "Synopsis", Code = "SYNOPSIS" },
                new() { Name = "Transfer", Code = "TRANSFER" },
                new() { Name = "Undertaking", Code = "UNDERTAKING" },
                new() { Name = "Witness", Code = "WITNESS" },

                // =========================
                // SPECIAL ACTS
                // =========================
                new() { Name = "17B Industrial Disputes Act", Code = "17B_ID_ACT" }
            };
        }
    }
}
