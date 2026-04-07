using CourtApp.Application.Common;
using CourtApp.Application.DTOs.CourtDistrict;
using MediatR;
using System;

namespace CourtApp.Application.Features.CourtDistrict
{
    public class GetCourtDistrictQueryById : IRequest<Result<CourtDistrictByIdReponse>>
    {
        public Guid Id { get; set; }
    }
}
