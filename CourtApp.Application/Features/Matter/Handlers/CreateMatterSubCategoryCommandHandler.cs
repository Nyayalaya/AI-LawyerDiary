using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.Matter.Commands;
using CourtApp.Application.Features.Matter.Services;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Matter.Handlers
{
    public class CreateMatterSubCategoryCommandHandler
    : IRequestHandler<CreateMatterSubCategoryCommand, Result<Guid>>
    {
        private readonly IRepositoryAsync<MatterSubCategoryEntity> _repository;
        private readonly IRepositoryAsync<MatterCategoryEntity> _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMatterService _matterService; 

        public CreateMatterSubCategoryCommandHandler(
            IRepositoryAsync<MatterSubCategoryEntity> repository,
            IRepositoryAsync<MatterCategoryEntity> categoryRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMatterService matterService)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _matterService = matterService;
        }

        public async Task<Result<Guid>> Handle(
            CreateMatterSubCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var parentExists = await _categoryRepository.Entities
                .AnyAsync(x => x.Id == request.MatterCategoryId, cancellationToken);

            if (!parentExists)
                return await Result<Guid>.FailAsync("Matter Category not found.");

            request.Code = request.Code.Trim().ToUpperInvariant();
            request.Name = request.Name.Trim();

            var validation = await _matterService.ValidateMatterSubCategoryAsync(
                null,
                request.MatterCategoryId,
                request.Code,
                request.Name,
                cancellationToken);

            if (!validation.Succeeded)
            {
                return await Result<Guid>.FailAsync(validation.Message);
            }

            var entity = _mapper.Map<MatterSubCategoryEntity>(request);

            await _repository.AddAsync(entity);

            await _unitOfWork.Commit(cancellationToken);

            return await Result<Guid>.SuccessAsync(entity.Id);
        }
    }
}
