using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.Matter.Services;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Matter.Commands.MatterSubCategory.Update;

public sealed class UpdateMatterSubCategoryCommandHandler
    : IRequestHandler<UpdateMatterSubCategoryCommand, Result<Guid>>
{
    private readonly IRepositoryAsync<MatterSubCategoryEntity> _repository;
    private readonly IMatterService _matterService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateMatterSubCategoryCommandHandler(
        IRepositoryAsync<MatterSubCategoryEntity> repository,
        IMatterService matterService,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _repository = repository;
        _matterService = matterService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(
        UpdateMatterSubCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);

        if (entity == null)
        {
            return await Result<Guid>.FailAsync("Matter Sub Category not found.");
        }

        var validation = await _matterService.ValidateMatterSubCategoryAsync(
            request.Id,
            request.MatterCategoryId,
            request.Code,
            request.Name,
            cancellationToken);

        if (!validation.Succeeded)
        {
            return await Result<Guid>.FailAsync(validation.Message);
        }

        request.Code = request.Code.Trim().ToUpperInvariant();
        request.Name = request.Name.Trim();
        request.Description = request.Description?.Trim();

        _mapper.Map(request, entity);

        await _repository.UpdateAsync(entity);

        await _unitOfWork.Commit(cancellationToken);

        return await Result<Guid>.SuccessAsync(
            entity.Id,
            "Matter Sub Category updated successfully.");
    }
}