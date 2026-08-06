using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseDetails.Dtos;
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

    public class CaseDocumentQueryHandler : IRequestHandler<Queries.CaseDocumentQuery, Result<List<CaseDocumentDto>>>
    {
        private readonly ICaseDocsRepository _docRepo;
        public CaseDocumentQueryHandler(ICaseDocsRepository _docRepo)
        {
            this._docRepo = _docRepo;
        }
        public async Task<Result<List<CaseDocumentDto>>> Handle(Queries.CaseDocumentQuery request, CancellationToken cancellationToken)
        {
            var caseDocs = await _docRepo.Entities
                   .Include(d => d.DO)
                   .Where(w => w.CaseId == request.CaseId)
                   .Select(s => new CaseDocumentDto
                   {
                       Id = s.Id,
                       DocType = s.DOTypeId == 1 ? "Drafting" : "Order",
                       DocFilePath = s.Path,
                       DocName = s.DO.Name_En,
                       DocDate = s.DocDate.ToString("dd/MM/yyyy")
                   }).ToListAsync(cancellationToken); ;

            return await Result<List<CaseDocumentDto>>.SuccessAsync(caseDocs);
        }
    }
}
