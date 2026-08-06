using AutoMapper;
using AutoMapper.QueryableExtensions;
using CourtApp.Application.Common;
using CourtApp.Application.Extensions;
using CourtApp.Application.Features.DynamicProperty.Dtos;
using CourtApp.Application.Features.DynamicProperty.Queries;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.DynamicProperty.Handlers;

public sealed class GetAllDynamicPropertyQueryHandler
    : IRequestHandler<GetAllDynamicPropertyQuery, Result<PaginatedResult<DynamicPropertyDto>>>
{
    private readonly IRepositoryAsync<DynamicPropertyEntity> _repository;
    private readonly IMapper _mapper;

    public GetAllDynamicPropertyQueryHandler(
        IRepositoryAsync<DynamicPropertyEntity> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedResult<DynamicPropertyDto>>> Handle(GetAllDynamicPropertyQuery request, CancellationToken cancellationToken)
    {
        var query = _repository.Entities.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.EntityType))
            query = query.Where(x => x.EntityType == request.EntityType);

        if (request.EntityId.HasValue)
            query = query.Where(x => x.EntityId == request.EntityId.Value);

        if (request.IsActive.HasValue)
            query = query.Where(x => x.IsActive == request.IsActive.Value);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim();
            query = query.Where(x => EF.Functions.Like(x.PropertyCode, $"%{search}%") || EF.Functions.Like(x.PropertyName, $"%{search}%"));
        }

        query = request.SortBy?.ToLower() switch
        {
            "code" => request.Descending ? query.OrderByDescending(x => x.PropertyCode) : query.OrderBy(x => x.PropertyCode),
            "name" => request.Descending ? query.OrderByDescending(x => x.PropertyName) : query.OrderBy(x => x.PropertyName),
            "displayorder" => request.Descending ? query.OrderByDescending(x => x.DisplayOrder) : query.OrderBy(x => x.DisplayOrder),
            _ => query.OrderBy(x => x.DisplayOrder).ThenBy(x => x.PropertyName)
        };

        var items = await query.ProjectTo<DynamicPropertyDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);

        return Result<PaginatedResult<DynamicPropertyDto>>.Success(items);
    }
}
