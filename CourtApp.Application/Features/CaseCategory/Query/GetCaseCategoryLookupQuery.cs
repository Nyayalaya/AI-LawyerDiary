using CourtApp.Application.Common;
using CourtApp.Application.DTOs.DropdownDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseCategory.Query
{
    public class GetCaseCategoryLookupQuery:IRequest<Result<List<DdlGuidStringDto>>>
    {
        public Guid CourtTypeId { get; set; }
    }
}
