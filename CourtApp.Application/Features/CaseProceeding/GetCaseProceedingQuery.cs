using CourtApp.Application.Common;
using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseProceeding
{
    public class GetCaseProceedingQuery:IRequest<PaginatedResult<CaseProceedingResponse>>
    {
        public Guid CaseId { get; set; }
        public Guid StageId { get; set; }
        public DateTime NextDate { get; set; }

    }
    public class GetCaseProceedingQueryHandler : IRequestHandler<GetCaseProceedingQuery, PaginatedResult<CaseProceedingResponse>>
    {
        private readonly ICaseProceedingRepository _Repository;
        public GetCaseProceedingQueryHandler(ICaseProceedingRepository _Repository)
        {
            this._Repository= _Repository;
        }
        public Task<PaginatedResult<CaseProceedingResponse>> Handle(GetCaseProceedingQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
