using AutoMapper;
using AutoMapper.QueryableExtensions;
using CourtApp.Application.Common;
using CourtApp.Application.Extensions;
using CourtApp.Application.Features.CaseDetails.Dtos;
using CourtApp.Application.Features.CaseDetails.Extention;
using CourtApp.Application.Features.CaseDetails.Queries;
using CourtApp.Application.Features.CaseDetails.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails.Handlers
{
    public class GetCasesQueryHandler : IRequestHandler<GetCasesQuery, PaginatedResult<CaseDataListDto>>
    {
        private readonly ICaseRepository _userCaseRepository;
        private readonly IMapper mapper;
        public GetCasesQueryHandler(ICaseRepository userCaseRepository, IMapper mapper)
        {
            _userCaseRepository = userCaseRepository;
            this.mapper = mapper;
        }
        public async Task<PaginatedResult<CaseDataListDto>> Handle(GetCasesQuery request, CancellationToken cancellationToken)
        {
            ExpressionStarter<CaseEntity> predicate = PredicateBuilder.New<CaseEntity>(true); ;
            if (!string.IsNullOrEmpty(request.UserId))
            {
                predicate = predicate.And(x => x.CreatedBy == request.UserId);
            }

            if (!string.IsNullOrWhiteSpace(request.Status))
            {
                if (request.Status == "Pending")
                {
                    predicate = predicate.And(x => !x.IsDisposed);
                }

                if (request.Status == "Disposed")
                {
                    predicate = predicate.And(x => x.IsDisposed);
                }
            }

            if (!string.IsNullOrWhiteSpace(request.SearchText))
            {
                var search = request.SearchText.Trim();
                predicate = predicate.And(x => x.CaseNo.Contains(search)
                    || x.DiaryNumber.Contains(search)
                    || x.CaseFirstTitle.Contains(search)
                    || x.CaseSecondTitle.Contains(search)
                    || x.CaseCategory.Name.Contains(search)
                    || x.Court.Name.Contains(search)
                    || (x.CourtDistrict != null && x.CourtDistrict.Name.Contains(search))
                    || (x.CourtComplex != null && x.CourtComplex.Name.Contains(search))
                    || (x.CourtHall != null && x.CourtHall.Name.Contains(search))
                    || x.Act.Contains(search)
                    || x.Section.Contains(search)
                );
            }

            if (request.CaseTypeId.HasValue)
            {
                predicate = predicate.And(x => x.CaseCategoryId == request.CaseTypeId.Value);
            }

            if (request.CourtId.HasValue)
            {
                predicate = predicate.And(x => x.CourtId == request.CourtId.Value);
            }

            if (request.NextDate.HasValue)
            {
                var nextDate = request.NextDate.Value.Date;
                predicate = predicate.And(x => x.NextDate.HasValue && x.NextDate.Value.Date == nextDate);
            }

            if (request.InstitutionDate.HasValue)
            {
                var filingDate =
                    request.InstitutionDate
                        .Value.Date;

                predicate = predicate.And(x =>
                    x.InstitutionDate.Date == filingDate
                );
            }

            if (request.ParentCaseId.HasValue)
            {
                predicate = predicate.And(x =>

                    x.ParentCaseId
                        == request.ParentCaseId.Value
                );
            }

            if (request.IsImportant.HasValue && request.IsImportant.Value)
            {
                predicate = predicate.And(x =>

                    x.IsImportant
                );
            }

            if (request.IsUrgent.HasValue && request.IsUrgent.Value)
            {
                predicate = predicate.And(x =>

                    x.IsUrgent
                );
            }

            if (!string.IsNullOrEmpty(request.CallingFrom))
            {
                predicate = predicate.And(x => x.IsDisposed == false);
            }

            var query = _userCaseRepository.Cases
                         .AsNoTracking()
                         .AsExpandable()
                         .Where(predicate)
                         .ApplyCaseAccessFilter(request.ConnectedUserIds)
                         .ApplyDefaultOrdering();

            var data = await query
                .ProjectTo<CaseDataListDto>(mapper.ConfigurationProvider)
                .ToPaginatedListAsync(
                    request.PageNumber,
                    request.PageSize,
                    cancellationToken);

            if (request.ConnectedUserIds?.Any() == true)
            {
                var linkedSet = request.ConnectedUserIds.ToHashSet();

                foreach (var item in data.Data)
                {
                    item.Reference =
                        !string.IsNullOrEmpty(item.AssignedLawyerId) &&
                        linkedSet.Contains(item.AssignedLawyerId)
                            ? "Assigned"
                            : "Self";
                }
            }
            else
            {
                foreach (var item in data.Data)
                    item.Reference = "Self";
            }

            return data;
        }


    }

}
