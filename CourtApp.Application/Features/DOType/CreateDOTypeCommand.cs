using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.DOType
{
    public class CreateDOTypeCommand : IRequest<Result<Guid>>
    {
        public int TypeId { get; set; }
        public string Name_En { get; set; }
    }

    public class CreateDOTypeCommandHandler : IRequestHandler<CreateDOTypeCommand, Result<Guid>>
    {
        private readonly IDOTypeRepository repository;
        private readonly IMapper mapper;
        private IUnitOfWork unitOfWork { get; set; }

        public CreateDOTypeCommandHandler(IDOTypeRepository _Repo, IMapper _mapper, IUnitOfWork _unitOfWork)
        {
            this.repository = _Repo;
            this.mapper = _mapper;
            this.unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CreateDOTypeCommand request, CancellationToken cancellationToken)
        {
            // Check if a record with the same name already exists (case-insensitive)
            var existingDraft = repository.Entities
                .Where(e => e.Name_En.ToLower().Trim().Contains(request.Name_En.ToLower().Trim()))
                .FirstOrDefault();

            if (existingDraft != null)
            {
                return Result<Guid>.Fail("Record already exists.");
            }

            // Map the request to the entity
            var newDraftOrder = mapper.Map<DOTypeEntity>(request);

            // Insert the new entity
            await repository.InsertAsync(newDraftOrder);

            // Commit the transaction
            await unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(newDraftOrder.Id);
        }
    }
}
