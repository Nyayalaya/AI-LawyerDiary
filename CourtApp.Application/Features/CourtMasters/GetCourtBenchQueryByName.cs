using CourtApp.Application.Common;
using CourtApp.Application.DTOs.CourtMaster;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using KT3Core.Areas.Global.Classes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtMasters
{
    public class GetCourtBenchQueryByName : IRequest<Result<List<CourtBenchResponse>>>
    {
        public string CourtName { get; set; }

    }
    public class GetCourtBenchQueryByNameHandler : IRequestHandler<GetCourtBenchQueryByName, Result<List<CourtBenchResponse>>>
    {
        private readonly ICourtBenchRepository _cBenchRepo;
        public GetCourtBenchQueryByNameHandler(ICourtBenchRepository _cBenchRepo)
        {
            this._cBenchRepo = _cBenchRepo;
        }
        public async Task<Result<List<CourtBenchResponse>>> Handle(GetCourtBenchQueryByName request, CancellationToken cancellationToken)
        {
            var predecate = PredicateBuilder.True<CourtBenchEntity>();
            if (request.CourtName != string.Empty)
                predecate.And(w => w.CourtBench_En.ToLower().Contains(request.CourtName));

            var crtDt = await _cBenchRepo.Entities
                .Where(predecate)
                .Select(s => new CourtBenchResponse
                {
                    Id = s.Id,
                    CourtBench_En = s.CourtBench_En
                })
                .ToListAsync();
            return Result<List<CourtBenchResponse>>.Success(crtDt);

        }
    }
}
