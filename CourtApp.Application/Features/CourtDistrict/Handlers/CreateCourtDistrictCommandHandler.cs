using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Domain.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtDistrict.Handlers
{
    public class CreateCourtDistrictCommandHandler : IRequestHandler<CreateCourtDistrictCommand, Result<Guid>>
    {
        private readonly ICourtDistrictRepository repository;
        private readonly IMapper mapper;
        private IUnitOfWork _unitOfWork { get; set; }

        public CreateCourtDistrictCommandHandler(ICourtDistrictRepository repository, 
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateCourtDistrictCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return Result<Guid>.Fail("Name is required!");

            if (string.IsNullOrWhiteSpace(request.Code))
                return Result<Guid>.Fail("Code is required!");

            var normalizedName = request.Name.Trim().ToLower();
            var normalizedCode = request.Code.Trim().ToLower();

            // Check if name already exists in the same state
            var existingByName = await repository.Entities
                .Where(w => w.StateId == request.StateId && w.Name.ToLower() == normalizedName)
                .FirstOrDefaultAsync(cancellationToken);

            if (existingByName != null)
                return Result<Guid>.Fail($"A Court District with name '{request.Name}' already exists in this state.");

            // Check if code already exists in the same state
            var existingByCode = await repository.Entities
                .Where(w => w.StateId == request.StateId && w.Code.ToLower() == normalizedCode)
                .FirstOrDefaultAsync(cancellationToken);

            if (existingByCode != null)
                return Result<Guid>.Fail($"A Court District with code '{request.Code}' already exists in this state.");

            var newEntity = new CourtDistrictEntity
            {
                Name = request.Name.Trim(),
                Code = request.Code.Trim(),
                StateId = request.StateId,
                Languages = request.Languages ?? new List<LangEntity>()
            };

            await repository.InsertAsync(newEntity);
            await _unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(newEntity.Id, "Court District created successfully.");
        }
    }
}