using CourtApp.Application.Common;
using CourtApp.Application.DTOs.CourtComplex;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtComplex
{
    public class GetCourtComplexCacheQuery : IRequest<Result<List<CourtComplexResponse>>>
    {
        public Guid CourtDistrictId { get; set; }
    }
}
