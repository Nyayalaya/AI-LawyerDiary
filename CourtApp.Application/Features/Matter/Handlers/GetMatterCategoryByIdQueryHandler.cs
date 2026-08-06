using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.Matter.Dtos;
using CourtApp.Application.Features.Matter.Queries.MatterCategory.GetById;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Matter.Handlers;

public sealed class GetMatterCategoryByIdQueryHandler
    : IRequestHandler<GetMatterCategoryByIdQuery, Result<MatterCategoryDto>>
{
    private readonly IRepositoryAsync<MatterCategoryEntity> _repository;
    private readonly IMapper _mapper;

    public GetMatterCategoryByIdQueryHandler(
        IRepositoryAsync<MatterCategoryEntity> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<MatterCategoryDto>> Handle(
        GetMatterCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _repository.Entities
            .AsNoTracking()
            .Include(x => x.MatterType)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            return await Result<MatterCategoryDto>.FailAsync("Matter Category not found.");

        return await Result<MatterCategoryDto>.SuccessAsync(
            _mapper.Map<MatterCategoryDto>(entity));
    }
}