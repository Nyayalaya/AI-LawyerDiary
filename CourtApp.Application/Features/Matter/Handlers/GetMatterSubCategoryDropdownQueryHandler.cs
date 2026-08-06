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

public sealed class GetMatterSubCategoryDropdownQueryHandler
    : IRequestHandler<GetMatterSubCategoryDropdownQuery, Result<List<DdlGuidStringDto>>>
{
    private readonly IRepositoryAsync<MatterSubCategoryEntity> _repository;

    public GetMatterSubCategoryDropdownQueryHandler(
        IRepositoryAsync<MatterSubCategoryEntity> repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<DdlGuidStringDto>>> Handle(
        GetMatterSubCategoryDropdownQuery request,
        CancellationToken cancellationToken)
    {
        var items = await _repository.Entities
            .AsNoTracking()
            .Where(x => x.MatterCategoryId == request.MatterCategoryId)
            .OrderBy(x => x.DisplayOrder).ThenBy(x => x.Name)
            .Select(x => new DdlGuidStringDto { Id = x.Id, Name = x.Name })
            .ToListAsync(cancellationToken);

        return Result<List<DdlGuidStringDto>>.Success(items);
    }
}
