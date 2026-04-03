using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Application.Features.CourtType.Services;

namespace CourtApp.Application.Features.CaseKinds.Commands
{
    public class CreateCaseKindCommand:IRequest<Result<Guid>>
    {
        public Guid CourtTypeId { get; set; }
        public string CaseKind { get; set; }

        public CreateCaseKindCommand()
        {

        }
    }

    public class CreateCaseKindCommandHandler : IRequestHandler<CreateCaseKindCommand, Result<Guid>>
    {
        private readonly ICaseKindRepository repository;
        private readonly ICourtTypeRepository _CourtTypeRepo;
        private readonly IMapper mapper;
        private IUnitOfWork _unitOfWork { get; set; }

        public CreateCaseKindCommandHandler(ICaseKindRepository repository, IMapper mapper
            , IUnitOfWork _unitOfWork
            , ICourtTypeRepository courtTypeRepo)
        {
            this.repository = repository;
            this.mapper = mapper;
            this._unitOfWork = _unitOfWork;
            _CourtTypeRepo = courtTypeRepo;
        }
        public async Task<Result<Guid>> Handle(CreateCaseKindCommand request, CancellationToken cancellationToken)
        {
            var mappeddata = mapper.Map<CaseKindEntity>(request);
            //mappeddata.CourtType = _CourtTypeRepo.GetByIdAsync(request.CourtTypeId).Result;
            await repository.InsertAsync(mappeddata);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(mappeddata.Id);
        }
    }
}
