using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using KT3Core.Areas.Global.Classes;
using MediatR;
using System.Linq.Expressions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CourtApp.Application.Extensions;
using CourtApp.Application.DTOs.CourtComplex;

namespace CourtApp.Application.Features.CourtComplex
{
    public class GetCourtComplexQuery : IRequest<PaginatedResult<CourtComplexResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public Guid CourtDistrictId { get; set; }
    }
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
                Abbreviation = e.Abbreviation,
                //DistrictName = e.District.Name_En,
                Name_En = e.Name_En,
                Name_Hn = e.Name_Hn,
                StateName = e.State.Name,
                CDistrictName = e.CourtDistrict.Name_En
            };
            var predicate = PredicateBuilder.True<CourtComplexEntity>();
            if (predicate != null)
            {
                if (request.StateId != 0)
                    predicate = predicate.And(y => y.StateId == request.StateId);
                //if (request.DistrictId != 0)
                //    predicate = predicate.And(x => x.DistrictCode == request.DistrictId);
                if (request.CourtDistrictId != Guid.Empty)
                    predicate = predicate.And(x => x.CourtDistrictId == request.CourtDistrictId);
            }
            try
            {

                var paginatedList = await repository.Entities
                    .Include(c => c.State)
                    //.Include(c => c.District)
                    .Include(c => c.CourtDistrict)
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
