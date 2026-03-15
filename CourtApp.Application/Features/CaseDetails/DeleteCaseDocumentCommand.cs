using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseDocuments.Services;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails
{
    public class DeleteCaseDocumentCommand : IRequest<Result<string>>
    {
        public Guid DocId { get; set; }
    }
    public class DeleteCaseDocumentCommandHandler : IRequestHandler<DeleteCaseDocumentCommand, Result<string>>
    {
        private readonly ICaseDocsRepository _CaseDocRepo;
        private readonly IUnitOfWork unitOfWork;
        public DeleteCaseDocumentCommandHandler(ICaseDocsRepository _CaseDocRepo, IUnitOfWork unitOfWork)
        {
            this._CaseDocRepo = _CaseDocRepo;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<string>> Handle(DeleteCaseDocumentCommand request, CancellationToken cancellationToken)
        {
            var doc = _CaseDocRepo.Entities.Where(w => w.Id == request.DocId).FirstOrDefault();
            if (doc == null) return Result<string>.Fail("There is no document avaible for this Id");
            await _CaseDocRepo.DeleteAsync(doc);
            await unitOfWork.Commit(cancellationToken);
            return Result<string>.Success("Document deleted Successfully!");
        }
    }
}
