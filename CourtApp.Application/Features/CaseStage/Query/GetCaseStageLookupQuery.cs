using CourtApp.Application.Common;
using CourtApp.Application.DTOs.DropdownDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseStage.Query
{
    public class GetCaseStageLookupQuery:IRequest<Result<List<DdlGuidStringDto>>>
    {
    }
}
