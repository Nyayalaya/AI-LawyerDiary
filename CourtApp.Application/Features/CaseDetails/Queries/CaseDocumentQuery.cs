using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseDetails.Dtos;
using MediatR;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.CaseDetails.Queries
{
    public class CaseDocumentQuery : IRequest<Result<List<CaseDocumentDto>>>
    {
        public Guid CaseId { get; set; }
    }
}
