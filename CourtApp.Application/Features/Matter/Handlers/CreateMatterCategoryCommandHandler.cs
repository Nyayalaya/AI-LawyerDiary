using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.Matter.Services;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Matter.Commands.MatterCategory.Create;

public sealed class CreateMatterCategoryCommandHandler
    : IRequestHandler<CreateMatterCategoryCommand, Result<Guid>>
{
    private readonly IRepositoryAsync<MatterCategoryEntity> _categoryRepository;
    private readonly IRepositoryAsync<MatterTypeEntity> _matterTypeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMatterService _matterService;

    public CreateMatterCategoryCommandHandler(
        IRepositoryAsync<MatterCategoryEntity> categoryRepository,
        IRepositoryAsync<MatterTypeEntity> matterTypeRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IMatterService matterService)
    {
        _categoryRepository = categoryRepository;
        _matterTypeRepository = matterTypeRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _matterService = matterService;
    }

    public async Task<Result<Guid>> Handle(
        CreateMatterCategoryCommand request,
        CancellationToken cancellationToken)
    {
        request.Code = request.Code.Trim().ToUpperInvariant();
        request.Name = request.Name.Trim();
        request.Description = request.Description?.Trim();

        var validation = await _matterService.ValidateMatterCategoryAsync(
            null,
            request.MatterTypeId,
            request.Code,
            request.Name,
            cancellationToken);

        if (!validation.Succeeded)
        {
            return await Result<Guid>.FailAsync(validation.Message);
        }

        var entity = _mapper.Map<MatterCategoryEntity>(request);

        await _categoryRepository.AddAsync(entity);

        await _unitOfWork.Commit(cancellationToken);

        return await Result<Guid>.SuccessAsync(
            entity.Id,
            "Matter Category created successfully.");
    }
}