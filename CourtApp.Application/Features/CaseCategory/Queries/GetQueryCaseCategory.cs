using CourtApp.Application.Common;
using CourtApp.Application.Extensions;
using CourtApp.Application.Features.BookMasters.Query;
using CourtApp.Application.Features.CaseCategory.Dto;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using KT3Core.Areas.Global.Classes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseCategory.Queries
{
    public class GetQueryCaseCategory : IRequest<Result<PaginatedResult<CaseCategoryResponse>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Guid CourtTypeId { get; set; }
    }
}
