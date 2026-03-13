using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.FSTitle
{
    public class FSTitleCreateCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public int TypeId { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }

    public class FSTitleCreateCommandHandler : IRequestHandler<FSTitleCreateCommand, Result<Guid>>
    {
        private readonly IFSTitleRepository _repoFsTitle;
        private readonly IMapper _mapper;
        private IUnitOfWork unitOfWork { get; set; }
        public FSTitleCreateCommandHandler(IFSTitleRepository _Repo,
            IMapper _mapper,
            IUnitOfWork _uow)
        {
            this._repoFsTitle = _Repo;
            this._mapper = _mapper;
            this.unitOfWork = _uow;
        }
        public async Task<Result<Guid>> Handle(FSTitleCreateCommand request, CancellationToken cancellationToken)
        {
            // Check if a record with the same name already exists (case-insensitive)
            var existingFS = _repoFsTitle.Entities
                .Where(e => e.TypeId.Equals(request.TypeId) && e.Name_En.ToLower().Trim().Contains(request.Name_En.ToLower().Trim()))
                .FirstOrDefault();

            if (existingFS != null)
            {
                return Result<Guid>.Fail("Record already exists.");
            }

            // Map the request to the entity
            var newFs = _mapper.Map<FSTitleEntity>(request);

            // Insert the new entity
            await _repoFsTitle.InsertAsync(newFs);

            // Commit the transaction
            await unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(newFs.Id);
        }
    }
}
