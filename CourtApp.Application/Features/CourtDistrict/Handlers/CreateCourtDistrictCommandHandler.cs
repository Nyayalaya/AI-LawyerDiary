using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Domain.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Application.Features.CourtDistrict.Commands;

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
            // Map all request items to entities
            var languageEntities = request.Languages?
                .Select(l => new LangEntity
                {
                    Code = l.Code?.Trim(),
                    Name = l.Name?.Trim()
                })
                .ToList() ?? new List<LangEntity>();

            var courtDistricts = request.createRequestData
                .Where(x => !string.IsNullOrWhiteSpace(x.Name))
                .Select(requestItem => new CourtDistrictEntity
                {
                    Name = requestItem.Name.Trim(),
                    StateId = requestItem.StateId,

                    // IMPORTANT: create separate references if required by ORM tracking
                    Languages = languageEntities.Select(l => new LangEntity
                    {
                        Code = l.Code,
                        Name = l.Name
                    }).ToList()
                })
                .ToList();

            if (!courtDistricts.Any())
                return Result<Guid>.Fail("Invalid district data.");

            await repository.AddRangeAsync(courtDistricts);

            await _unitOfWork.Commit(cancellationToken); // ✅ Only once

            // Get last inserted ID safely
            var lastCreatedId = courtDistricts[^1].Id;

            return Result<Guid>.Success(lastCreatedId, "Court Districts created successfully.");
        }
    }
}