using CourtApp.Application.Common;
using CourtApp.Application.Extensions;
using CourtApp.Application.Features.CaseType.Services;
using CourtApp.Application.Features.TypeOfCases.Query;
using CourtApp.Application.Features.Typeofcasess.Query;
using CourtApp.Domain.Entities.LawyerDiary;
using KT3Core.Areas.Global.Classes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseType.Handlers
{
    public class CaseTypeGetQueryHandler : IRequestHandler<GetAllTypeOfCasesQuery, Result<PaginatedResult<GetAllTypeOfCasesResponse>>>
    {
        private readonly ICaseTypeRepository _repository;
        public CaseTypeGetQueryHandler(ICaseTypeRepository _repository)
        {
            this._repository = _repository;
        }
        public async Task<Result<PaginatedResult<GetAllTypeOfCasesResponse>>> Handle(GetAllTypeOfCasesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<TypeOfCasesEntity, GetAllTypeOfCasesResponse>> expression = e => new GetAllTypeOfCasesResponse
            {
                Id = e.Id,
                CaseNature = e.Nature.Name.ToUpper(),
                Name_En = e.Name_En.ToUpper(),
                Name_Hn = e.Name_Hn,
                Abbreviation = e.Abbreviation.ToUpper(),
                CourtTypeName = e.CourtType.Name.ToUpper(),
               
            };
            var predicate = PredicateBuilder.True<TypeOfCasesEntity>();

            if (request.CategoryId != Guid.Empty)
                predicate = predicate.And(b => b.Nature.Id == request.CategoryId);

           
            if (request.CourtTypeId != Guid.Empty)
                predicate = predicate.And(b => b.CourtTypeId == request.CourtTypeId);
           
                var paginatedList = await _repository.QryEntities
                    .Include(c => c.CourtType)
                    .Include(c => c.Nature)
                    .Where(predicate)
                    .Select(expression)
                    .OrderBy(o => o.Name_En.ToUpper())
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                //paginatedList.TotalCount = _repository.QryEntities.Count();
                return Result<PaginatedResult<GetAllTypeOfCasesResponse>>.Success(paginatedList);
        }
    }
    
}
