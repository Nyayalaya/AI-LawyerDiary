using CourtApp.Application.Common;
using CourtApp.Application.DTOs.DropdownDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.State.Query
{
    public class GetStateLookupQuery : IRequest<Result<List<DdlIntStringDto>>>
    {
    }
}
