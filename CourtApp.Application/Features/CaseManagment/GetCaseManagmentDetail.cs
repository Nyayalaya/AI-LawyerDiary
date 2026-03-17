using CourtApp.Application.Common;
using CourtApp.Application.Extensions;
using CourtApp.Application.Features.CaseCategory.Services;
using CourtApp.Application.Features.CourtType.Services;
using CourtApp.Application.Features.ProceedingHead;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using KT3Core.Areas.Global.Classes;
using MediatR;

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseManagment
{
    public class GetCaseManagmentDetail : IRequest<PaginatedResult<GetCaseManagmentResponse>>
    {
        public Guid Id { get; set; }
        public int ActionType { get; set; }
        public DateTime InstitutionDate { get; set; }
        public Guid ClientId { get; set; }
        public Guid NatureId { get; set; }
        public Guid TypeCaseId { get; set; }
        public Guid CourtTypeId { get; set; }
        public Guid CourtId { get; set; }
        public Guid CaseTypeId { get; set; }
        public string Number { get; set; }
        public int CaseYear { get; set; }
        public string FirstTitle { get; set; }
        public int TitleTypeFirst { get; set; }
        public string SecondTitle { get; set; }
        public int TitleTypeSecond { get; set; }
        public DateTime NextDate { get; set; }
        public string CaseStageCode { get; set; }
        //public List<AgainstCaseDecision> AgainstCaseDetails { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
      
        public GetCaseManagmentDetail(int PageNumber,int PageSize)
        {
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
        }
    }
    public class GetCaseManagmentDetailHandler : IRequestHandler<GetCaseManagmentDetail, PaginatedResult<GetCaseManagmentResponse>>
    {
        private readonly ICourtTypeCacheRepository _RepoCourtType;
        private readonly ICaseStageCacheRepository _RepoStage;
        private readonly ICaseCategoryCacheRepository _RepoNature;
        private readonly IUserCaseRepository _RepoCase;
        public GetCaseManagmentDetailHandler(ICaseCategoryCacheRepository repoNature,
            IUserCaseRepository repoCase, ICourtTypeCacheRepository RepoCourtType,
            ICaseStageCacheRepository RepoStage)
        {
            _RepoNature = repoNature;
            _RepoCase = repoCase;
            _RepoCourtType= RepoCourtType;
            _RepoStage= RepoStage;
        }
        public async Task<PaginatedResult<GetCaseManagmentResponse>> Handle(GetCaseManagmentDetail request, CancellationToken cancellationToken)
        {
            Expression<Func<CaseDetailEntity, GetCaseManagmentResponse>> expression = e => new GetCaseManagmentResponse
            {
                Id = e.Id,
                Number = String.Concat(e.CaseNo, " ", e.CaseYear),
                //kin=e.CaseType.CaseKind,
                //CourtName=e.Court.Name_En,
                FirstTitle=e.FirstTitle,
                SecondTitle=e.SecondTitle,
                //NextHearingDate=e.NextDate.ToString(),
                //Status=e.
               // CaseStage = e.,                
            };

            var predicate = PredicateBuilder.True<CaseDetailEntity>();
            if (predicate != null)
            {
                //if (request.Year != 0)
                  //  predicate = predicate.And(y => y.Year == request.Year);
                //if (request.CaseNumber != string.Empty)
                    //predicate = predicate.And(x => x.Number == request.CaseNumber);
                //if (request.CourtId != Guid.Empty)
                //    predicate = predicate.And(x => x.CourtType.Id==request.CourtId);
                //if (request.CourtTypeId != Guid.Empty)
                //    predicate = predicate.And(x => x.CourtType.Id == request.CourtTypeId);

            }
            try
            {
                var paginatedList = await _RepoCase.Entites
                   // .Include(c => c.CourtType)
                    //.Include(c => c.Court)
                    .Where(predicate)
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return paginatedList;
            }
            catch(Exception ex) {
                Console.WriteLine(ex.StackTrace);
            }
            return null;
        }
    }
}
