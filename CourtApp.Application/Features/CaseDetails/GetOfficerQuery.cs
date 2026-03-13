using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using KT3Core.Areas.Global.Classes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails
{
    public class GetOfficerQuery : IRequest<Result<List<string>>>
    {
        public string OfficerName { get; set; }
        public Guid CourtId { get; set; }
    }

    public class GetOfficerQueryHandler : IRequestHandler<GetOfficerQuery, Result<List<string>>>
    {
        private readonly ICaseAgainstRepository againstRepository;
        public GetOfficerQueryHandler(ICaseAgainstRepository againstRepository)
        {
            this.againstRepository = againstRepository;
        }
        public async Task<Result<List<string>>> Handle(GetOfficerQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<CaseDetailAgainstEntity>();
            if (request.CourtId != Guid.Empty)
                predicate = predicate.And(b => b.CourtBenchId == request.CourtId);
            if (!string.IsNullOrEmpty(request.OfficerName))
                predicate = predicate.And(b => b.OfficerName.ToLower().Contains(request.OfficerName.ToLower()));
            var result = await againstRepository.Entities
                .Where(predicate)
                .Select(s => s.OfficerName.ToUpper())
                .Distinct()
                .ToListAsync();
            return await Result<List<string>>.SuccessAsync(result);
        }
    }
}
