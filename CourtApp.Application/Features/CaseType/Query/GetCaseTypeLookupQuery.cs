using CourtApp.Application.Common;
using CourtApp.Application.DTOs.DropdownDtos;
using MediatR;
using System.Collections.Generic;

namespace CourtApp.Application.Features.CaseType.Query
{
    public class GetCaseTypeLookupQuery:IRequest<Result<List<DdlGuidStringDto>>>
    {
    }
}
