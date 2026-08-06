using AutoMapper;
using AutoMapper.QueryableExtensions;
using CourtApp.Application.Common;
using CourtApp.Application.Extensions;
using CourtApp.Application.Features.Matter.Dtos;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Matter.Queries.MatterType.GetAll;

public sealed class GetAllMatterTypeQueryHandler
    : IRequestHandler<GetAllMatterTypeQuery, PaginatedResult<MatterTypeDto>>
{
    private readonly IRepositoryAsync<MatterTypeEntity> _repository;
    private readonly IMapper _mapper;

    public GetAllMatterTypeQueryHandler(
        IRepositoryAsync<MatterTypeEntity> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<MatterTypeDto>> Handle(
        GetAllMatterTypeQuery request,
        CancellationToken cancellationToken)
    {
        var query = _repository.Entities.AsNoTracking();

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
        
        var matterTypes = await query
            .ProjectTo<MatterTypeDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);

        return Result<PaginatedResult<MatterTypeDto>>.Success(matterTypes);
    }
}