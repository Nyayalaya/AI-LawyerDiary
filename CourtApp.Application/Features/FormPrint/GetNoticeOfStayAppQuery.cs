using CourtApp.Application.Common;
using CourtApp.Application.DTOs.FormPrint;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace CourtApp.Application.Features.FormPrint
{
    public class GetNoticeOfStayAppQuery : IRequest<Result<List<NoticeOfStayAppResponse>>>
    {
        public List<Guid> CaseIds { get; set; }
    }
    public class GetNoticeOfStayAppQueryHandler : IRequestHandler<GetNoticeOfStayAppQuery, Result<List<NoticeOfStayAppResponse>>>
    {
        private readonly IUserCaseRepository _CaseRepo;
        private readonly ICaseTitleRepository _TitleRepo;
        public GetNoticeOfStayAppQueryHandler(IUserCaseRepository _CaseRepo, ICaseTitleRepository _TitleRepo)
        {
            this._CaseRepo = _CaseRepo;
            this._TitleRepo = _TitleRepo;
        }

        public async Task<Result<List<NoticeOfStayAppResponse>>> Handle(GetNoticeOfStayAppQuery request, CancellationToken cancellationToken)
        {

            var Results = from c in _CaseRepo.Entites.Include(ct => ct.CaseType)
                          .Where(w => request.CaseIds.Contains(w.Id))
                          join t in _TitleRepo.Titles on c.Id equals t.CaseId into CompTitles
                          from ct in CompTitles.DefaultIfEmpty()
                          select new NoticeOfStayAppResponse
                          {
                              Petitioner = c.FirstTitle,
                              Respondent = c.SecondTitle,
                              //FirstTitle = ct.TypeId == 1 ? ct.Title : "",
                              //SecondTitle = ct.TypeId == 2 ? ct.Title : "",
                              CaseNoYear = c.CaseNo + "/" + c.CaseYear,
                              CaseType = c.CaseType.Name_En
                          };
            return await Result<List<NoticeOfStayAppResponse>>.SuccessAsync(Results.ToList());
        }
    }
}
