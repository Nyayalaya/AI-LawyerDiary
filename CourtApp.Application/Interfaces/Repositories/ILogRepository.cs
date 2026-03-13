using CourtApp.Application.DTOs;
using CourtApp.Application.DTOs.Logs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ILogRepository
    {
        Task<List<AuditLogResponse>> GetAuditLogsAsync(string userId);

        Task AddLogAsync(string action, string userId,string tableName, string pk);
    }
}