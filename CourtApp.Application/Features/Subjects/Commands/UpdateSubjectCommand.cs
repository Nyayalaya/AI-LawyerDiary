using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.Common;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace CourtApp.Application.Features.Subjects.Commands
{
    public class UpdateSubjectCommand : IRequest<Result<Guid>>
    {
        public string Subject { get; set; }
        public Guid Id { get; set; }
    }

    public class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectCommand, Result<Guid>>
    {
        private readonly ISubjectRepository _repository;
        private readonly IMapper mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateSubjectCommandHandler(IMapper mapper, ISubjectRepository _repository, IUnitOfWork _unitOfWork)
        {
            this.mapper = mapper;
            this._repository = _repository;
            this._unitOfWork = _unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
        {
            var Detail = await _repository.GetByIdAsync(request.Id);

            if (Detail == null)
            {
                return Result<Guid>.Fail($"Subject Not Found.");
            }
            else
            {
                Detail.Name_En = request.Subject;
                await _repository.UpdateAsync(Detail);
                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(Detail.Id);
            }
        }
    }
}
