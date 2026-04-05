using CourtApp.Domain.Entities.Masters;
using CourtApp.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class CaseStageSeeder
{
    public static async Task SeedCaseStagesAsync(ApplicationDbContext context, IdentityContext identityContext)
    {
        const string systemEmail = "superadmin@gmail.com";

        var userId = await identityContext.Users
            .Where(u => u.Email == systemEmail)
            .Select(u => u.Id)
            .FirstOrDefaultAsync() ?? "system";

        // Use HashSet for O(1) lookup
        var existingCodes = await context.CaseStages
            .Select(s => s.Code)
            .ToHashSetAsync();

        var stages = GetStages();

        var newStages = stages
            .Where(s => !existingCodes.Contains(s.Code))
            .ToList();

        if (newStages.Count == 0)
            return;

        foreach (var stage in newStages)
        {
            stage.CreatedBy = userId;
        }

        await context.CaseStages.AddRangeAsync(newStages);
        await context.SaveChangesAsync(userId);
    }

    private static readonly List<CaseStageEntity> _stages = new()
    {
        new() { Name="Draft", Code="DRAFT", Sequence=1 },
        new() { Name="Filed", Code="FILED", Sequence=2 },
        new() { Name="Scrutiny", Code="SCRUTINY", Sequence=3 },
        new() { Name="Registered", Code="REGISTERED", Sequence=4 },
        new() { Name="Notice Issued", Code="NOTICE", Sequence=5 },
        new() { Name="Summons Issued", Code="SUMMONS", Sequence=6 },
        new() { Name="First Hearing", Code="FIRST_HEARING", Sequence=7 },
        new() { Name="Evidence Stage", Code="EVIDENCE", Sequence=8 },
        new() { Name="Arguments", Code="ARGUMENTS", Sequence=9 },
        new() { Name="Judgment Reserved", Code="JUDGMENT_RESERVED", Sequence=10 },
        new() { Name="Judgment Delivered", Code="JUDGMENT", Sequence=11 },
        new() { Name="Disposed", Code="DISPOSED", Sequence=12 },
        new() { Name="Closed", Code="CLOSED", Sequence=13 }
    };

    public static List<CaseStageEntity> GetStages() => _stages;
}