using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Application.Features.CaseDocuments.Services;

namespace CourtApp.Application.Features.CaseDetails
{
    public class CaseDocsCreateCommand : IRequest<Result<Guid>>
    {
        public Guid CaseId { get; set; }
        public List<DocumentAttachmentModel> Documents { get; set; }        
    }

    public class DocumentAttachmentModel
    {
        public int TypeId { get; set; }
        public Guid DocId { get; set; }
        public string DocPath { get; set; }
        public DateTime DocDate { get; set; }
    }

    public class CaseDocsCreateCommandHandler : IRequestHandler<CaseDocsCreateCommand, Result<Guid>>
    {
        private readonly ICaseDocsRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CaseDocsCreateCommandHandler(IMapper _mapper, IUnitOfWork _unitOfWork, ICaseDocsRepository _repository)
        {
            this._mapper = _mapper;
            this._repository = _repository;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CaseDocsCreateCommand request, CancellationToken cancellationToken)
        {

            if (request.Documents != null)
            {
                foreach (var item in request.Documents)
                {
                    CaseDocsEntity ce = new CaseDocsEntity() { CreatedBy="" };
                    ce.CaseId = request.CaseId;
                    ce.DOTypeId = item.TypeId;
                    ce.DOId = item.DocId;
                    ce.Path = item.DocPath;
                    ce.DocDate = item.DocDate;
                    await _repository.SaveCaseDocAsync(ce);
                }
            }            
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success();
        }
    }
}
