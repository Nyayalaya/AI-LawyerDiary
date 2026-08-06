using CourtApp.Domain.Entities.Masters;
using CourtApp.Infrastructure.DbContexts;
using CourtApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.DataSeeder
{
    public static class MatterCategorySeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context,
        IdentityContext identity)
        {
            const string systemEmail = "superadmin@gmail.com";

            var userId = await identity.Users
                .Where(x => x.Email == systemEmail)
                .Select(x => x.Id)
                .FirstOrDefaultAsync() ?? "system";

            var matterTypes = await context.Set<MatterTypeEntity>()
                .ToDictionaryAsync(x => x.Code, x => x.Id);

            var existing = await context.Set<MatterCategoryEntity>()
                .Select(x => new
                {
                    x.MatterTypeId,
                    x.Code
                })
                .ToListAsync();

            var existingSet = existing
                .Select(x => $"{x.MatterTypeId}:{x.Code}")
                .ToHashSet();

            var folder = Path.Combine(
    AppContext.BaseDirectory,
    "DataSeeder",
    "MatterCategoryData");
            if (!Directory.Exists(folder))
            {
                throw new DirectoryNotFoundException($"Folder not found: {folder}");
            }


            var files = Directory.GetFiles(folder, "*.csv");

            foreach (var file in files)
            {
                var rows = CsvReaderService.Read<MatterCategoryCsv>(file);

                var entities = rows
                    .Where(x => matterTypes.ContainsKey(x.MatterTypeCode))
                    .Where(x => !existingSet.Contains(
                        $"{matterTypes[x.MatterTypeCode]}:{x.Code}"))
                    .Select(x => new MatterCategoryEntity
                    {
                        MatterTypeId = matterTypes[x.MatterTypeCode],
                        Code = x.Code,
                        Name = x.Name,
                        Description = x.Description,
                        DisplayOrder = x.DisplayOrder,
                        CreatedBy = userId
                    })
                    .ToList();

                if (entities.Count == 0)
                    continue;

                await context.AddRangeAsync(entities);

                foreach (var e in entities)
                    existingSet.Add($"{e.MatterTypeId}:{e.Code}");
            }

            await context.SaveChangesAsync(userId);
        }
    }
}
