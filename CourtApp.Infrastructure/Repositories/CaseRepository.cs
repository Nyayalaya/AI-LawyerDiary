using CourtApp.Application.Features.CaseDetails.Repositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using CourtApp.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Repositories
{
    public class CaseRepository : ICaseRepository
    {
        private readonly IRepositoryAsync<CaseEntity> _repository;
        public CaseRepository(IRepositoryAsync<CaseEntity> repository)
        {
            this._repository = repository;
        }

        public IQueryable<CaseEntity> Cases => _repository.Entities;
    }
}
