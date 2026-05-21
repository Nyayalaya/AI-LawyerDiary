using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.DTOs.ProcSubHead;
using CourtApp.Application.Extensions;
using CourtApp.Application.Interfaces.Repositories;
using KT3Core.Areas.Global.Classes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Domain.Entities.Masters;

namespace CourtApp.Application.Features.ProceedingSubHead
{
    public class GetProceedingSubHeadQuery : IRequest<PaginatedResult<GetProcSubHeadResponse>>
    {
        public Guid HeadId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
    }
    public class GetProceedingSubHeadQueryHandler : IRequestHandler<GetProceedingSubHeadQuery, PaginatedResult<GetProcSubHeadResponse>>
    {
        private readonly IProceedingSubHeadRepository repository;
        private readonly IMapper mapper;
        public GetProceedingSubHeadQueryHandler(IProceedingSubHeadRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<PaginatedResult<GetProcSubHeadResponse>> Handle(GetProceedingSubHeadQuery request, CancellationToken cancellationToken)
        {
            // Projection: maps directly to response DTO
            Expression<Func<ProceedingEntity, GetProcSubHeadResponse>> expression = e => new GetProcSubHeadResponse
            {
                Id = e.Id,
                Name = e.Name,
                ProceedingType = e.ProceedingType.Name.ToString()
            };

            // Build predicate for filtering
            var predicate = PredicateBuilder.True<ProceedingEntity>();

            if (request.HeadId != Guid.Empty)
                predicate = predicate.And(e => e.ProceedingTypeId == request.HeadId);

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                predicate = predicate.And(e =>
                    e.Name.ToLower().Contains(request.Search.ToLower()) ||
                  
                    e.ProceedingType.ToString().ToLower().Contains(request.Search.ToLower()));
            }

            
                var query = repository.Entities

                    .Include(e => e.ProceedingType)
                    .Where(predicate);

                // Apply sorting
                if (!string.IsNullOrEmpty(request.SortColumn))
                {
                    query = request.SortDirection?.ToLower() == "desc"
                        ? query.OrderByDescendingDynamic(request.SortColumn)
                        : query.OrderByDynamic(request.SortColumn);
                }
                else
                {
                    // Default sorting (optional)
                    query = query.OrderBy(e => e.Name);
                }

                // Project and paginate
                var paginatedList = await query
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);

                return paginatedList;
            
        }

    }
}
