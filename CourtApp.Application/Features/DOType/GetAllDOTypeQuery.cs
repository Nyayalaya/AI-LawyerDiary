using CourtApp.Application.Common;
using CourtApp.Application.DTOs.DOTypes;
using CourtApp.Application.Extensions;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.DOType
{
    public class GetAllDOTypeQuery : IRequest<PaginatedResult<DOTypeResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TypeId { get; set; }

        public GetAllDOTypeQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
    public class GetAllDOTypeQueryHandler : IRequestHandler<GetAllDOTypeQuery, PaginatedResult<DOTypeResponse>>
    {
        private readonly IDOTypeRepository _repository;
        public GetAllDOTypeQueryHandler(IDOTypeRepository _repository)
        {
            this._repository = _repository;
        }
        public async Task<PaginatedResult<DOTypeResponse>> Handle(GetAllDOTypeQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<DOTypeEntity, DOTypeResponse>> expression = e => new DOTypeResponse
            {
                Id = e.Id,
                Name_En = e.Name_En,
                Type = e.TypeId == 1 ? "Order" : "Drafting"
            };
            var paginatedList = await _repository.Entities
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            //paginatedList.TotalCount = _repository.Entities.Count();
            return paginatedList;
        }
    }
}
