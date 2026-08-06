using CourtApp.Application.Common;
using CourtApp.Application.DTOs.DropdownDtos;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Matter.Queries.MatterType.GetDropdown;

public sealed class GetMatterTypeDropdownQueryHandler
    : IRequestHandler<GetMatterTypeDropdownQuery, Result<List<DdlGuidStringDto>>>
{
    private readonly IRepositoryAsync<MatterTypeEntity> _repository;

    public GetMatterTypeDropdownQueryHandler(
        IRepositoryAsync<MatterTypeEntity> repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<DdlGuidStringDto>>> Handle(
        GetMatterTypeDropdownQuery request,
        CancellationToken cancellationToken)
    {
        var data = await _repository.Entities
            .AsNoTracking()
            .OrderBy(x => x.DisplayOrder)
            .ThenBy(x => x.Name)
            .Select(x => new DdlGuidStringDto
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync(cancellationToken);

        return await Result<List<DdlGuidStringDto>>
            .SuccessAsync(data);
    }
}