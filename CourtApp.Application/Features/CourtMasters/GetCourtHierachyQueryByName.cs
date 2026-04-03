using CourtApp.Application.Common;
using CourtApp.Application.DTOs.CourtMaster;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtMasters
{
    public class GetCourtHierachyQueryByName : IRequest<Result<GetCourtHierachyByNameDto>>
    {
        public Guid CourtId { get; set; }
    }

    public class GetCourtHierachyQueryHandlerByName : IRequestHandler<GetCourtHierachyQueryByName, Result<GetCourtHierachyByNameDto>>
    {
        private readonly ICourtBenchRepository _cBenchRepo;
        private readonly ICourtMasterRepository _cMasterRepo;
        public GetCourtHierachyQueryHandlerByName(ICourtBenchRepository _cBenchRepo, ICourtMasterRepository _cMasterRepo)
        {
            this._cBenchRepo = _cBenchRepo;
            this._cMasterRepo = _cMasterRepo;
        }
        public async Task<Result<GetCourtHierachyByNameDto>> Handle(GetCourtHierachyQueryByName request, CancellationToken cancellationToken)
        {

            var cMstId = await _cBenchRepo.GetByIdAsync(request.CourtId);
            var cDetails = await _cMasterRepo.Entities
                    .Where(w => w.Id == cMstId.CourtMasterId)
                    .Select(s => new GetCourtHierachyByNameDto
                    {
                        Id = s.Id,
                        CourtTypeId = s.CourtTypeId,
                        CourtAbbreviation = s.CourtType.Abbreviation,
                        //CourtComplexId = s.CourtComplexId.HasValue ? s.CourtComplexId.Value : Guid.Empty,
                        //CourtDistrictId = s.CourtDistrictId.HasValue ? s.CourtDistrictId.Value : Guid.Empty,
                        CourtId = cMstId.Id
                    })
                    .FirstOrDefaultAsync();
            return Result<GetCourtHierachyByNameDto>.Success(cDetails);
        }
    }
}
