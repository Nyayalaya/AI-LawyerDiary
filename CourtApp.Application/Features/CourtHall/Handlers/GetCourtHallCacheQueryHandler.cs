using CourtApp.Application.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Application.Features.CourtHall.Query;
using CourtApp.Application.Features.CourtHall.DTOs;
using CourtApp.Application.Features.CourtHall.Interfaces;

namespace CourtApp.Application.Features.CourtHall.Handlers
{
    public class GetCourtHallCacheQueryHandler : IRequestHandler<GetCourtHallCacheQuery, Result<List<CourtHallResponse>>>
    {
        private readonly ICourtHallRepository repository;
        private readonly IMapper _mapper;
        private readonly ICourtHallCacheRepository _cacheRepository;

        public GetCourtHallCacheQueryHandler(
            ICourtHallRepository repository, 
            IMapper mapper,
            ICourtHallCacheRepository cacheRepository)
        {
            this.repository = repository;
            this._mapper = mapper;
            this._cacheRepository = cacheRepository;
        }

        public async Task<Result<List<CourtHallResponse>>> Handle(GetCourtHallCacheQuery request, CancellationToken cancellationToken)
        {
            // Try to get from cache first
            var cachedData = await _cacheRepository.GetByCourtComplexIdAsync(request.CourtComplexId);
            if (cachedData != null && cachedData.Any())
            {
                return Result<List<CourtHallResponse>>.Success(cachedData);
            }

            // If not in cache, fetch from database
            var halls = await repository.Entities
                .Include(c => c.Languages)
                .Where(w => w.CourtComplexId == request.CourtComplexId)
                .OrderBy(o => o.Name)
                .ToListAsync(cancellationToken);

            if (!halls.Any())
                return await Result<List<CourtHallResponse>>
                    .FailAsync("No court halls found for the selected complex!");

            var mappedData = _mapper.Map<List<CourtHallResponse>>(halls);
            var result = mappedData.Select(s => new CourtHallResponse
            {
                Id = s.Id,
                Name = s.Name.ToUpper(),
                JudgeName = s.JudgeName,
                RoomNumber = s.RoomNumber,
                CourtComplexId = s.CourtComplexId,
                CourtComplexName = s.CourtComplexName,
                Languages = s.Languages
            }).OrderBy(o => o.Name).ToList();

            return Result<List<CourtHallResponse>>.Success(result);
        }
    }
}
