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

namespace CourtApp.Application.Features.Publications.Command
{
    public class CreatePublicationCommand : IRequest<Result<Guid>>
    {
        public string PublicationName { get; set; }
        public string PropriatorName { get; set; }
    }

    public class CreatePublicationCommandHandler : IRequestHandler<CreatePublicationCommand, Result<Guid>>
    {
        private readonly IPublicationRepository _repository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public CreatePublicationCommandHandler(IPublicationRepository _repository, IMapper _mapper, IUnitOfWork _unitOfWork)
        {
            this._repository = _repository;
            this._unitOfWork = _unitOfWork;
            this._mapper = _mapper;
        }
        public async Task<Result<Guid>> Handle(CreatePublicationCommand request, CancellationToken cancellationToken)
        {
            var objpublication= _mapper.Map<PublisherEntity>(request);
            await _repository.InsertAsync(objpublication);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(objpublication.Id);
        }
    }
}
