using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.Matter.Dtos;
using CourtApp.Application.Features.Matter.Queries.MatterSubCategory.GetById;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Matter.Handlers;

public sealed class GetMatterSubCategoryByIdQueryHandler
    : IRequestHandler<GetMatterSubCategoryByIdQuery, Result<MatterSubCategoryDto>>
{
    private readonly IRepositoryAsync<MatterSubCategoryEntity> _repository;
    private readonly IMapper _mapper;

    public GetMatterSubCategoryByIdQueryHandler(
        IRepositoryAsync<MatterSubCategoryEntity> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<MatterSubCategoryDto>> Handle(
        GetMatterSubCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _repository.Entities
            .AsNoTracking()
            .Include(x => x.MatterCategory)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            return await Result<MatterSubCategoryDto>.FailAsync("Matter Sub Category not found.");

        return await Result<MatterSubCategoryDto>.SuccessAsync(
            _mapper.Map<MatterSubCategoryDto>(entity));
    }
}
