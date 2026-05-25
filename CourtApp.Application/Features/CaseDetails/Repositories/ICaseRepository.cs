using CourtApp.Domain.Entities.CaseDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails.Repositories
{
    public interface ICaseRepository
    {
        IQueryable<CaseEntity> Cases { get; }
    }
}
