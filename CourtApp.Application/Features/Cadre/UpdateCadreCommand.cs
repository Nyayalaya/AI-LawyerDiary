using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Cadre
{
    public class UpdateCadreCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
    public class UpdateCadreCommandHandler : IRequestHandler<UpdateCadreCommand, Result<Guid>>
    {
        private readonly ICadreMasterRepository repository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public UpdateCadreCommandHandler(ICadreMasterRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(UpdateCadreCommand request, CancellationToken cancellationToken)
        {
            // Fetch the record to update
            var existingRecord = await repository.GetByIdAsync(request.Id);
            if (existingRecord == null)
                return Result<Guid>.Fail("Record does not exist.");


            // Check for name conflict with other records (excluding the current record)
            var duplicateRecord = await repository.Entities
                .Where(e => e.Id != request.Id && e.Name_En.ToLower() == request.Name_En.ToLower().Trim())
                .FirstOrDefaultAsync(cancellationToken);

            if (duplicateRecord != null)
                return Result<Guid>.Fail("Another record with the same name already exists.");

            // Update the entity fields
            existingRecord.Name_En = request.Name_En;
            existingRecord.Name_Hn = request.Name_Hn;

            await repository.UpdateAsync(existingRecord);
            await unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(existingRecord.Id);
        }
    }
}
