using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseDetails.Dtos;
using MediatR;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.Features.CaseDetails.Queries
{
    public class GetCasesQuery:IRequest<PaginatedResult<CaseDataListDto>>
    {
        public string UserId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchText { get; set; }
        public Guid? CaseTypeId { get; set; }
        public Guid? CourtId { get; set; }
        public string Status { get; set; }
        public DateTime? NextDate { get; set; }
        public DateTime? InstitutionDate { get; set; }
        public List<string> LinkedIds { get; set; }
        public List<string> ConnectedUserIds { get; set; }
        public Guid? ParentCaseId { get; set; }
        public bool? IsImportant { get; set; }
        public bool? IsUrgent { get; set; }

        public string CallingFrom { get; set; }
    }
}
