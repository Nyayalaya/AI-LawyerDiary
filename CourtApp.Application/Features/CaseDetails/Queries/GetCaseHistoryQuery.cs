using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseDetails.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails.Queries
{
    public class GetCaseHistoryQuery : IRequest<Result<CaseHistoryDto>>
    {
        public Guid CaseId { get; set; }
    }
}
