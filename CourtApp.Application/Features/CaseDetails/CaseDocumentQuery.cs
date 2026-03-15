using CourtApp.Application.Common;
using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.Features.CaseDocuments.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails
{
    public class CaseDocumentQuery : IRequest<Result<List<CaseUploadedDocument>>>
    {
        public Guid CaseId { get; set; }
    }
    public class CaseDocumentQueryHandler : IRequestHandler<CaseDocumentQuery, Result<List<CaseUploadedDocument>>>
    {
        private readonly ICaseDocsRepository _docRepo;
        public CaseDocumentQueryHandler(ICaseDocsRepository _docRepo)
        {
            this._docRepo = _docRepo;
        }
        public async Task<Result<List<CaseUploadedDocument>>> Handle(CaseDocumentQuery request, CancellationToken cancellationToken)
        {
            var caseDocs = await _docRepo.Entities
                   .Include(d => d.DO)
                   .Where(w => w.CaseId == request.CaseId)
                   .Select(s => new CaseUploadedDocument
                   {
                       Id = s.Id,
                       DocType = s.DOTypeId == 1 ? "Drafting" : "Order",
                       DocFilePath = s.Path,
                       DocName = s.DO.Name_En,
                       DocDate = s.DocDate.ToString("dd/MM/yyyy")
                   }).ToListAsync(cancellationToken); ;

            return await Result<List<CaseUploadedDocument>>.SuccessAsync(caseDocs);
        }
    }
}
