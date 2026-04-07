using CourtApp.Application.Common;
using CourtApp.Application.DTOs.CourtDistrict;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.Extensions;
using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Application.Constants;
using MediatR;
using System.Linq.Expressions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using KT3Core.Areas.Global.Classes;

namespace CourtApp.Application.Features.CourtDistrict.Handlers
{
    public class GetCourtDistrictQueryHandler : IRequestHandler<GetCourtDistrictQuery, PaginatedResult<CourtDistrictReponse>>
    {
        private readonly ICourtDistrictRepository repository;

        public GetCourtDistrictQueryHandler(ICourtDistrictRepository repository)
        {
            this.repository = repository;
        }

        public async Task<PaginatedResult<CourtDistrictReponse>> Handle(GetCourtDistrictQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<CourtDistrictEntity, CourtDistrictReponse>> expression = e => new CourtDistrictReponse
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                StateName = e.State.Name,
                Languages = e.Languages
            };

            var predicate = PredicateBuilder.True<CourtDistrictEntity>();
            if (request.StateId != 0)
                predicate = predicate.And(y => y.StateId == request.StateId);

            var result = await repository.Entities
                .Include(x => x.State)
                .Include(x => x.Languages)
                .Where(predicate)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);

            return result;
        }
    }
}