using CourtApp.Application.Common;
using CourtApp.Application.Extensions;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using KT3Core.Areas.Global.Classes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.BookMasters.Query
{
    public class GetAllBookMasterQuery : IRequest<PaginatedResult<GetAllBookMasterResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Guid BookTypeId { get; set; }
        public Guid PublisherId { get; set; }

        public GetAllBookMasterQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
    public class GGetAllBookMasterQueryHandler : IRequestHandler<GetAllBookMasterQuery, PaginatedResult<GetAllBookMasterResponse>>
    {
        private readonly IBookMasterRepository _repository;

        public GGetAllBookMasterQueryHandler(IBookMasterRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetAllBookMasterResponse>> Handle(GetAllBookMasterQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<LDBookEntity, GetAllBookMasterResponse>> expression = e => new GetAllBookMasterResponse
            {
                Id = e.Id,
                Year = e.Year,
                BookType = e.BookType.Name_En,
                PublisherName = e.Publisher.PropriatorName,
                BookName = ""
            };
            var predicate = PredicateBuilder.True<LDBookEntity>();
            if (request.BookTypeId != Guid.Empty)
                predicate = predicate.And(b => b.BookType.Id == request.BookTypeId);

            if (request.PublisherId != Guid.Empty)
                predicate = predicate.And(p => p.Publisher.Id == request.PublisherId);

            if (request.PublisherId != Guid.Empty && request.BookTypeId != Guid.Empty)
                predicate = predicate.And(p => p.Publisher.Id == request.PublisherId && p.BookType.Id == request.BookTypeId);

            var paginatedList = await _repository.BookMasters.Where(predicate)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}
