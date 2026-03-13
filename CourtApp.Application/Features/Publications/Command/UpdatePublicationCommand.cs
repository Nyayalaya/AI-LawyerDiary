using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Publications.Command
{
    public class UpdatePublicationCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public string PublicationName { get; set; }
        public string PropriatorName { get; set; }
    }

    public class UpdatePublicationCommandHandler : IRequestHandler<UpdatePublicationCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublicationRepository _Repository;
        public UpdatePublicationCommandHandler(IUnitOfWork _unitOfWork, IPublicationRepository _Repository)
        {
            this._Repository = _Repository;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(UpdatePublicationCommand request, CancellationToken cancellationToken)
        {
            var publicationDetail = await _Repository.GetByIdAsync(request.Id);
            if (publicationDetail == null)
                return Result<Guid>.Fail($"Book Type Not Found.");
            else
            {
                publicationDetail.PropriatorName = request.PropriatorName;
                publicationDetail.PublicationName = request.PublicationName;
                publicationDetail.LastModifiedOn = DateTime.Now;
                await _Repository.UpdateAsync(publicationDetail);
                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(publicationDetail.Id);
            }
        }
    }
}
