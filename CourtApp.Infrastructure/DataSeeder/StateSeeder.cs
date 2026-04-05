using CourtApp.Domain.Entities.Common;
using CourtApp.Domain.Entities.Masters;
using CourtApp.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.DataSeeder
{
    public static class StateSeeder
    {
        public static async Task SeedStatesAsync(ApplicationDbContext context,IdentityContext identityContext)
        {
            var systemUser = "superadmin@gmail.com";

            var superAdmin = await identityContext.Users
                .FirstOrDefaultAsync(u => u.Email == systemUser);

            var userId = superAdmin != null ? superAdmin.Id : "system";
            var existingCodes = await context.States
               .Select(s => s.Code)
               .ToListAsync();

            var states = GetStates();

            var newStates = states
                .Where(s => !existingCodes.Contains(s.Code))
                .ToList();

            if (!newStates.Any())
                return;

            // ✅ Assign audit fields (if your entity has them)
            foreach (var state in newStates)
            {
                state.CreatedBy = userId;
            }

            await context.States.AddRangeAsync(newStates);
            await context.SaveChangesAsync(userId);
        }

        public static List<StateEntity> GetStates()
        {
            return new List<StateEntity>
        {
            // States
            new StateEntity { Id = 1, Name = "Andhra Pradesh", Code = "AP", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 2, Name = "Arunachal Pradesh", Code = "AR", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 3, Name = "Assam", Code = "AS", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 4, Name = "Bihar", Code = "BR", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 5, Name = "Chhattisgarh", Code = "CG", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 6, Name = "Goa", Code = "GA", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 7, Name = "Gujarat", Code = "GJ", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 8, Name = "Haryana", Code = "HR", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 9, Name = "Himachal Pradesh", Code = "HP", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 10, Name = "Jharkhand", Code = "JH", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 11, Name = "Karnataka", Code = "KA", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 12, Name = "Kerala", Code = "KL", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 13, Name = "Madhya Pradesh", Code = "MP", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 14, Name = "Maharashtra", Code = "MH", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 15, Name = "Manipur", Code = "MN", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 16, Name = "Meghalaya", Code = "ML", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 17, Name = "Mizoram", Code = "MZ", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 18, Name = "Nagaland", Code = "NL", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 19, Name = "Odisha", Code = "OR", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 20, Name = "Punjab", Code = "PB", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 21, Name = "Rajasthan", Code = "RJ", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 22, Name = "Sikkim", Code = "SK", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 23, Name = "Tamil Nadu", Code = "TN", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 24, Name = "Telangana", Code = "TG", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 25, Name = "Tripura", Code = "TR", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 26, Name = "Uttar Pradesh", Code = "UP", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 27, Name = "Uttarakhand", Code = "UT", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 28, Name = "West Bengal", Code = "WB", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },

            // Union Territories
            new StateEntity { Id = 29, Name = "Andaman and Nicobar Islands", Code = "AN", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 30, Name = "Chandigarh", Code = "CH", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 31, Name = "Dadra and Nagar Haveli and Daman & Diu", Code = "DN", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 32, Name = "Delhi", Code = "DL", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 33, Name = "Jammu & Kashmir", Code = "JK", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 34, Name = "Ladakh", Code = "LA", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 35, Name = "Lakshadweep", Code = "LD", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } },
            new StateEntity { Id = 36, Name = "Puducherry", Code = "PY", Languages = new List<LangEntity> { new LangEntity { Code = "en", Name = "English" } } }
        };
        }
    }
}
