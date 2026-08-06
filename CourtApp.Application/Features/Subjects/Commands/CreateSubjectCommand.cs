using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.Common;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using CourtApp.Domain.Entities;
using CourtApp.Domain.Entities.Common;

namespace CourtApp.Application.Features.Subjects.Commands
{
    public class CreateSubjectCommand:IRequest<Result<Guid>>
    {
        public string Subject { get; set; }
    }

    public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, Result<Guid>>
    {
        private readonly ISubjectRepository _repository;
        private readonly IMapper mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public CreateSubjectCommandHandler(IMapper mapper, ISubjectRepository _repository, IUnitOfWork _unitOfWork)
        {
            this.mapper = mapper;
            this._repository = _repository;
            this._unitOfWork = _unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = mapper.Map<SubjectEntity>(request);
            //await _repository.InsertAsync(subject);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(subject.Id);
        }
    }
}
