using CourtApp.Application.DTOs;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.Interfaces.Shared;
//using AspNetCoreHero.EntityFrameworkCore.AuditTrail.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourtApp.Application.DTOs.Logs;
using AuditTrail.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CourtApp.Infrastructure.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryAsync<Audit> _repository;
        private readonly IDateTimeService _dateTimeService;

        public LogRepository(IRepositoryAsync<Audit> repository, IMapper mapper, IDateTimeService dateTimeService)
        {
            _repository = repository;
            _mapper = mapper;
            _dateTimeService = dateTimeService;
        }

        public async Task AddLogAsync(string action, string userId, string tableName, string pk)
        {
            var audit = new Audit()
            {
                Type = action,
                UserId = userId,
                DateTime = _dateTimeService.NowUtc,
                PrimaryKey = pk,
                TableName = tableName
            };
            await _repository.AddAsync(audit);
        }

        public async Task<List<AuditLogResponse>> GetAuditLogsAsync(string userId)
        {
            var logs = await _repository.Entities.Where(a => a.UserId == userId).OrderByDescending(a => a.Id).Take(250).ToListAsync();
            var mappedLogs = _mapper.Map<List<AuditLogResponse>>(logs);
            return mappedLogs;
        }
    }

    public class LogProfile : Profile
    {
        public LogProfile()
        {
            CreateMap<AuditLogResponse, Audit>().ReverseMap();
        }
    }
}