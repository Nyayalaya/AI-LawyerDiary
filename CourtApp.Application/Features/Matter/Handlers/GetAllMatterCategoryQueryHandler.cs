using AutoMapper;
using AutoMapper.QueryableExtensions;
using CourtApp.Application.Common;
using CourtApp.Application.Extensions;
using CourtApp.Application.Features.Matter.Dtos;
using CourtApp.Application.Features.Matter.Queries;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Matter.Handlers;

public sealed class GetAllMatterCategoryQueryHandler
    : IRequestHandler<GetAllMatterCategoryQuery, PaginatedResult<MatterCategoryDto>>
{
    private readonly IRepositoryAsync<MatterCategoryEntity> _repository;
    private readonly IMapper _mapper;

    public GetAllMatterCategoryQueryHandler(
        IRepositoryAsync<MatterCategoryEntity> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<MatterCategoryDto>> Handle(
        GetAllMatterCategoryQuery request,
        CancellationToken cancellationToken)
    {
        var query = _repository.Entities.AsNoTracking().Include(x => x.MatterType).AsQueryable();

        if (request.MatterTypeId.HasValue)
            query = query.Where(x => x.MatterTypeId == request.MatterTypeId.Value);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim();

            query = query.Where(x =>
                EF.Functions.Like(x.Code, $"%{search}%") ||
                EF.Functions.Like(x.Name, $"%{search}%"));
        }

        query = request.SortBy?.ToLower() switch
        {
            "code" => request.Descending
                ? query.OrderByDescending(x => x.Code)
                : query.OrderBy(x => x.Code),

            "name" => request.Descending
                ? query.OrderByDescending(x => x.Name)
                : query.OrderBy(x => x.Name),

            "displayorder" => request.Descending
                ? query.OrderByDescending(x => x.DisplayOrder)
                : query.OrderBy(x => x.DisplayOrder),

            _ => query.OrderBy(x => x.DisplayOrder)
                      .ThenBy(x => x.Name)
        };

        var categories = await query
            .ProjectTo<MatterCategoryDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);

        return Result<PaginatedResult<MatterCategoryDto>>.Success(categories);
    }
}
