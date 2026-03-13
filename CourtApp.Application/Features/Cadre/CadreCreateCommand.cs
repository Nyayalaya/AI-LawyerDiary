using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Cadre
{
    public class CadreCreateCommand : IRequest<Result<Guid>>
    {
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
    public class CadreCreateCommandHandler : IRequestHandler<CadreCreateCommand, Result<Guid>>
    {
        private readonly ICadreMasterRepository repository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public CadreCreateCommandHandler(ICadreMasterRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CadreCreateCommand request, CancellationToken cancellationToken)
        {
            // Check if a record with the same name already exists (case-insensitive)
            var existingCadre = repository.Entities
                .Where(e => e.Name_En.ToLower().Trim().Contains(request.Name_En.ToLower().Trim()))
                .FirstOrDefault();

            if (existingCadre != null)
            {
                return Result<Guid>.Fail("Record already exists.");
            }

            // Map the request to the entity
            var newCadre = mapper.Map<CadreMasterEntity>(request);

            // Insert the new entity
            await repository.InsertAsync(newCadre);

            // Commit the transaction
            await unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(newCadre.Id);
        }
    }
}
