using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails
{
    public class UpdateCaseDocumentCommand : IRequest<Result<string>>
    {
        public Guid Id { get; set; }
        public string DocId { get; set; }
    }
    public class UpdateCaseDocumentCommandHandler : IRequestHandler<UpdateCaseDocumentCommand, Result<string>>
    {
        private readonly ICaseDocsRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCaseDocumentCommandHandler(IMapper _mapper, IUnitOfWork _unitOfWork, ICaseDocsRepository _repository)
        {
            this._mapper = _mapper;
            this._repository = _repository;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<string>> Handle(UpdateCaseDocumentCommand request, CancellationToken cancellationToken)
        {
            var docDetail = await _repository.Entities
                .Where(w => w.Id == request.Id).FirstOrDefaultAsync();

            if (docDetail == null)
                return await Result<string>.FailAsync(" No record found!");

            docDetail.Path = request.DocId;

            await _repository.UpdateAsync(docDetail);
            await _unitOfWork.Commit(cancellationToken);

            return await Result<string>.SuccessAsync("Record successfully updated!");
        }
    }
}
