using CourtApp.Application.Common;
using CourtApp.Application.DTOs.CourtComplex;
using CourtApp.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtComplex.Handlers
{
    public class GetCourtComplexCacheQueryHandler : IRequestHandler<GetCourtComplexCacheQuery, Result<List<CourtComplexResponse>>>
    {
        private readonly ICourtComplexRepository courComplextRepository;
        private readonly IMapper _mapper;

        public GetCourtComplexCacheQueryHandler(ICourtComplexRepository courComplextRepository, IMapper mapper)
        {
            this.courComplextRepository = courComplextRepository;
            this._mapper = mapper;
        }

        public async Task<Result<List<CourtComplexResponse>>> Handle(GetCourtComplexCacheQuery request, CancellationToken cancellationToken)
        {
            var complexes = await courComplextRepository
                .Entities
                .Include(c => c.Languages)
                .Where(w => w.CourtDistrictId == request.CourtDistrictId)
                .OrderBy(o => o.Name)
                .ToListAsync();

            if (!complexes.Any())
                return await Result<List<CourtComplexResponse>>
                    .FailAsync("Complexes not found for the selected district!");

            var mappedDt = _mapper.Map<List<CourtComplexResponse>>(complexes);
            var mdt = mappedDt.Select(s => new CourtComplexResponse
            {
                Id = s.Id,
                Name = s.Name.ToUpper(),
                Code = s.Code,
                CDistrictName = s.CDistrictName,
                StateName = s.StateName,
                Languages = s.Languages
            }).OrderBy(o => o.Name.ToUpper());

            return Result<List<CourtComplexResponse>>.Success(mdt.ToList());
        }
    }
}
