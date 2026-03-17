using CourtApp.Application.Common;
using CourtApp.Application.DTOs.DOTypes;
using CourtApp.Application.Features.DOType;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading;
using System;
using CourtApp.Application.DTOs.FSTitle;
using System.Linq;
using CourtApp.Application.Extensions;
namespace CourtApp.Application.Features.FSTitle
{
    public class FSTitleGetAllQuery : IRequest<PaginatedResult<FSTitleResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TypeId { get; set; }
    }
    public class FSTitleGetAllQueryHandler : IRequestHandler<FSTitleGetAllQuery, PaginatedResult<FSTitleResponse>>
    {
        private readonly IFSTitleRepository _repository;
        public FSTitleGetAllQueryHandler(IFSTitleRepository _repository)
        {
            this._repository = _repository;
        }
        public async Task<PaginatedResult<FSTitleResponse>> Handle(FSTitleGetAllQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<FSTitleEntity, FSTitleResponse>> expression = e => new FSTitleResponse
            {
                Id = e.Id,
                Name_En = e.Name_En,
                Type = e.TypeId == 1 ? "First Title" : "Second Title"
            };
            var paginatedList = await _repository.Entities
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, 500);
            //paginatedList.TotalCount = _repository.Entities.Count();
            return paginatedList;
        }
    }
}
