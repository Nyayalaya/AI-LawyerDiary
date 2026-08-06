using CourtApp.Application.Common;
using CourtApp.Application.DTOs.Common;
using CourtApp.Application.Features.CourtDistrict.DTOs;
using MediatR;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.CourtDistrict.Commands
{
    public class CreateCourtDistrictCommand : IRequest<Result<Guid>>
    {
        public List<CourtDistricCreateRequest> createRequestData { get; set; }
        public List<LangDto> Languages { get; set; }
    }


}
