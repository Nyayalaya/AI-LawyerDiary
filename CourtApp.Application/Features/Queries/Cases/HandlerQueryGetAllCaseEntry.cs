using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Extensions;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Queries.Cases
{
    public class HandlerQueryGetAllCaseEntry : IRequestHandler<QueryGetAllCaseEntry, PaginatedResult<ResponseGetAllCaseEntry>>
    {
        private readonly IUserCaseRepository _Repository;
        private readonly IMapper _mapper;
        public HandlerQueryGetAllCaseEntry(IUserCaseRepository _Repository, IMapper _mapper)
        {
            this._mapper = _mapper;
            this._Repository = _Repository;
        }
        public async Task<PaginatedResult<ResponseGetAllCaseEntry>> Handle(QueryGetAllCaseEntry request, CancellationToken cancellationToken)
        {
            Expression<Func<CaseEntity, ResponseGetAllCaseEntry>> expression = e => new ResponseGetAllCaseEntry
            {
                Id = e.Id,
                CaseYear = e.CaseYear,
                CaseNumber = e.CaseNumber,
                CaseTypeName = e.CaseType.CaseKind,
                CourtType = e.CourtType.CourtType,
                CourtName = e.Court.CourtName,
                Title = e.TitleFirst + " Vs " + e.TitleSecond,
                NextHearingDate = e.NextDate.Value.ToString("dd/MM/yyyy")
            };

            var paginatedList = await _Repository.Entites
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }


}
