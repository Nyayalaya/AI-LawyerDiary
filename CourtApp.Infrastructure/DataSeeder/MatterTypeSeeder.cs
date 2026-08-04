using CourtApp.Domain.Entities.Masters;
using CourtApp.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CourtApp.Infrastructure.DataSeeder
{
    public static class MatterTypeSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context, IdentityContext identity)
        {
            const string systemEmail = "superadmin@gmail.com";

            var userId = await identity.Users
                .Where(u => u.Email == systemEmail)
                .Select(u => u.Id)
                .FirstOrDefaultAsync() ?? "system";

            // ✅ Correct DbSet
            var existingCodes = await context.Set<MatterTypeEntity>()
                .Select(s => s.Code)
                .ToHashSetAsync();

            // ✅ Correct usage (no brackets)
            var matterTypes = _matterTypes;

            var newMatterTypes = matterTypes
                .Where(s => !existingCodes.Contains(s.Code))
                .ToList();

            if (!newMatterTypes.Any())
                return;

            foreach (var matterType in newMatterTypes)
            {
                matterType.CreatedBy = userId;
            }

            // ✅ Correct DbSet
            await context.Set<MatterTypeEntity>().AddRangeAsync(newMatterTypes);

            await context.SaveChangesAsync(userId);
        }

        private static readonly List<MatterTypeEntity> _matterTypes = new List<MatterTypeEntity>
        {
            new()
                {
                    Code = "CIVIL",
                    Name = "Civil Matters",
                    Description = "All civil disputes including property, contracts, injunctions and recovery matters.",
                    DisplayOrder = 1
                },

                new()
                {
                    Code = "CRIMINAL",
                    Name = "Criminal Matters",
                    Description = "All criminal prosecutions, bail applications, appeals and revisions.",
                    DisplayOrder = 2
                },

                new()
                {
                    Code = "CONSTITUTIONAL",
                    Name = "Constitutional Matters",
                    Description = "Constitutional petitions, writs and public interest litigation.",
                    DisplayOrder = 3
                },

                new()
                {
                    Code = "SERVICE",
                    Name = "Service Matters",
                    Description = "Government service, employment and disciplinary matters.",
                    DisplayOrder = 4
                },

                new()
                {
                    Code = "FAMILY",
                    Name = "Family Matters",
                    Description = "Marriage, divorce, maintenance, custody and succession matters.",
                    DisplayOrder = 5
                },

                new()
                {
                    Code = "COMMERCIAL",
                    Name = "Commercial Matters",
                    Description = "Commercial disputes, arbitration and business litigation.",
                    DisplayOrder = 6
                },

                new()
                {
                    Code = "CORPORATE",
                    Name = "Corporate Matters",
                    Description = "Company law, insolvency and corporate governance matters.",
                    DisplayOrder = 7
                },

                new()
                {
                    Code = "TAX",
                    Name = "Tax Matters",
                    Description = "Income tax, GST, customs and other taxation matters.",
                    DisplayOrder = 8
                },

                new()
                {
                    Code = "LABOUR",
                    Name = "Labour & Industrial Matters",
                    Description = "Industrial disputes, labour law and employee benefit matters.",
                    DisplayOrder = 9
                },

                new()
                {
                    Code = "PROPERTY",
                    Name = "Property Matters",
                    Description = "Land acquisition, revenue, tenancy and property disputes.",
                    DisplayOrder = 10
                },

                new()
                {
                    Code = "CONSUMER",
                    Name = "Consumer Matters",
                    Description = "Consumer disputes before consumer commissions.",
                    DisplayOrder = 11
                },

                new()
                {
                    Code = "BANKING",
                    Name = "Banking & Finance Matters",
                    Description = "Banking disputes, DRT, SARFAESI and financial recovery matters.",
                    DisplayOrder = 12
                },

                new()
                {
                    Code = "ARBITRATION",
                    Name = "Arbitration Matters",
                    Description = "Domestic and international arbitration proceedings.",
                    DisplayOrder = 13
                },

                new()
                {
                    Code = "TRIBUNAL",
                    Name = "Tribunal Matters",
                    Description = "Matters before statutory tribunals and commissions.",
                    DisplayOrder = 14
                },

                new()
                {
                    Code = "ELECTION",
                    Name = "Election Matters",
                    Description = "Election petitions and electoral disputes.",
                    DisplayOrder = 15
                },

                new()
                {
                    Code = "ENVIRONMENT",
                    Name = "Environmental Matters",
                    Description = "Environmental protection, pollution and NGT matters.",
                    DisplayOrder = 16
                },

                new()
                {
                    Code = "INTELLECTUAL_PROPERTY",
                    Name = "Intellectual Property Matters",
                    Description = "Trademark, copyright, patent and design disputes.",
                    DisplayOrder = 17
                },

                new()
                {
                    Code = "CYBER",
                    Name = "Cyber Law Matters",
                    Description = "Cyber crime, IT Act and digital evidence matters.",
                    DisplayOrder = 18
                },

                new()
                {
                    Code = "MOTOR_ACCIDENT",
                    Name = "Motor Accident Matters",
                    Description = "Motor accident claims and compensation matters.",
                    DisplayOrder = 19
                },

                new()
                {
                    Code = "EXECUTION",
                    Name = "Execution Matters",
                    Description = "Execution and enforcement of decrees and awards.",
                    DisplayOrder = 20
                },

                new()
                {
                    Code = "MISC",
                    Name = "Miscellaneous Matters",
                    Description = "Miscellaneous applications and proceedings.",
                    DisplayOrder = 21
                }
        };
    }
}
