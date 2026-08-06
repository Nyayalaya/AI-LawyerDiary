using CourtApp.Application.Common;
using CourtApp.Application.DTOs.DropdownDtos;
using CourtApp.Application.Features.Matter.Queries;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Matter.Handlers;

public sealed class GetMatterCategoryDropdownQueryHandler
    : IRequestHandler<GetMatterCategoryDropdownQuery, Result<List<DdlGuidStringDto>>>
{
    private readonly IRepositoryAsync<MatterCategoryEntity> _repository;

    public GetMatterCategoryDropdownQueryHandler(
        IRepositoryAsync<MatterCategoryEntity> repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<DdlGuidStringDto>>> Handle(
        GetMatterCategoryDropdownQuery request,
        CancellationToken cancellationToken)
    {
        var items = await _repository.Entities
            .AsNoTracking()
            .Where(x => x.MatterTypeId == request.MatterTypeId)
            .OrderBy(x => x.DisplayOrder).ThenBy(x => x.Name)
            .Select(x => new DdlGuidStringDto { Id = x.Id, Name = x.Name })
            .ToListAsync(cancellationToken);

        return Result<List<DdlGuidStringDto>>.Success(items);
    }
}
