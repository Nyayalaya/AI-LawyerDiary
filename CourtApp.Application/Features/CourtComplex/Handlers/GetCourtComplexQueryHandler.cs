using CourtApp.Application.Common;
using CourtApp.Application.DTOs.CourtComplex;
using CourtApp.Application.Extensions;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using KT3Core.Areas.Global.Classes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtComplex.Handlers
{
    public class GetCourtComplexQueryHandler : IRequestHandler<GetCourtComplexQuery, PaginatedResult<CourtComplexResponse>>
    {
        private readonly ICourtComplexRepository repository;

        public GetCourtComplexQueryHandler(ICourtComplexRepository repository)
        {
            this.repository = repository;
        }

        public async Task<PaginatedResult<CourtComplexResponse>> Handle(GetCourtComplexQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<CourtComplexEntity, CourtComplexResponse>> expression = e => new CourtComplexResponse
            {
                Id = e.Id,
                Name = e.Name,
                StateName = e.State != null ? e.State.Name : string.Empty,
                CDistrictName = e.CourtDistrict != null ? e.CourtDistrict.Name : string.Empty,
                Languages = e.Languages
            };

            var predicate = PredicateBuilder.True<CourtComplexEntity>();
            
            if (request.StateId != 0)
                predicate = predicate.And(y => y.StateId == request.StateId);
            
            if (request.CourtDistrictId != Guid.Empty)
                predicate = predicate.And(x => x.CourtDistrictId == request.CourtDistrictId);

            try
            {
                var paginatedList = await repository.Entities
                    .Include(c => c.State)
                    .Include(c => c.CourtDistrict)
                    .Include(c => c.Languages)
                    .Where(predicate)
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);

                return paginatedList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            
            return null;
        }
    }
}
