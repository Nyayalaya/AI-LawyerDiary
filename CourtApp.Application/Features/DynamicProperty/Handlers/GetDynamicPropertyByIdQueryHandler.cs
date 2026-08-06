using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.DynamicProperty.Dtos;
using CourtApp.Application.Features.DynamicProperty.Queries.GetById;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.DynamicProperty.Handlers;

public sealed class GetDynamicPropertyByIdQueryHandler
    : IRequestHandler<GetDynamicPropertyByIdQuery, Result<DynamicPropertyDto>>
{
    private readonly IRepositoryAsync<DynamicPropertyEntity> _repository;
    private readonly IMapper _mapper;

    public GetDynamicPropertyByIdQueryHandler(
        IRepositoryAsync<DynamicPropertyEntity> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<DynamicPropertyDto>> Handle(GetDynamicPropertyByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.Entities.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            return await Result<DynamicPropertyDto>.FailAsync("Dynamic property not found.");

        return await Result<DynamicPropertyDto>.SuccessAsync(_mapper.Map<DynamicPropertyDto>(entity));
    }
}
