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

public sealed class GetAllMatterSubCategoryQueryHandler
    : IRequestHandler<GetAllMatterSubCategoryQuery, PaginatedResult<MatterSubCategoryDto>>
{
    private readonly IRepositoryAsync<MatterSubCategoryEntity> _repository;
    private readonly IMapper _mapper;

    public GetAllMatterSubCategoryQueryHandler(
        IRepositoryAsync<MatterSubCategoryEntity> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<MatterSubCategoryDto>> Handle(
        GetAllMatterSubCategoryQuery request,
        CancellationToken cancellationToken)
    {
        var query = _repository.Entities.AsNoTracking().Include(x => x.MatterCategory).AsQueryable();

        if (request.MatterCategoryId.HasValue)
            query = query.Where(x => x.MatterCategoryId == request.MatterCategoryId.Value);

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

        var subCategories = await query
            .ProjectTo<MatterSubCategoryDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);

        return Result<PaginatedResult<MatterSubCategoryDto>>.Success(subCategories);
    }
}
