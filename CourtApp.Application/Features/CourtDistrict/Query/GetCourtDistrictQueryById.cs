using CourtApp.Application.Common;
using CourtApp.Application.Features.CourtDistrict.DTOs;
using MediatR;
using System;

namespace CourtApp.Application.Features.CourtDistrict.Query
{
    public class GetCourtDistrictQueryById : IRequest<Result<CourtDistrictByIdReponse>>
    {
        public Guid Id { get; set; }
    }
}
