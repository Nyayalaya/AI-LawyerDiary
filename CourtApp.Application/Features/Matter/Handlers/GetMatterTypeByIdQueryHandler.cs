using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.Matter.Dtos;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Matter.Queries.MatterType.GetById;

public sealed class GetMatterTypeByIdQueryHandler
    : IRequestHandler<GetMatterTypeByIdQuery, Result<MatterTypeDto>>
{
    private readonly IRepositoryAsync<MatterTypeEntity> _repository;
    private readonly IMapper _mapper;

    public GetMatterTypeByIdQueryHandler(
        IRepositoryAsync<MatterTypeEntity> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<MatterTypeDto>> Handle(
        GetMatterTypeByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);

        if (entity is null)
            return await Result<MatterTypeDto>.FailAsync("Matter Type not found.");

        var dto = _mapper.Map<MatterTypeDto>(entity);

        return await Result<MatterTypeDto>.SuccessAsync(dto);
    }
}