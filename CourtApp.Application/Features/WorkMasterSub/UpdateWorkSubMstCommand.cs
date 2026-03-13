using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMasterSub
{
    public class UpdateWorkSubMstCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public Guid WorkId { get; set; }
        public required string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
    }
    public class UpdateWorkSubMstCommandHandler : IRequestHandler<UpdateWorkSubMstCommand, Result<Guid>>
    {
        private readonly IWorkMasterSubRepository repository;
        private readonly IUnitOfWork unitOfWork;
        public UpdateWorkSubMstCommandHandler(IWorkMasterSubRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(UpdateWorkSubMstCommand request, CancellationToken cancellationToken)
        {
            // Fetch the record to update
            var existingRecord = await repository.GetByIdAsync(request.Id);
            if (existingRecord == null)
                return Result<Guid>.Fail("Record does not exist.");

            string normializedProcName = request.Name_En.ToUpper().Trim();
            // Check for name conflict with other records (excluding the current record)
            var duplicateRecord = await repository.Entities
                .Where(e => e.Id != request.Id
                                && e.WorkId == request.WorkId
                                && e.Name_En.ToUpper().Trim() == normializedProcName)
                .FirstOrDefaultAsync(cancellationToken);

            if (duplicateRecord != null)
                return Result<Guid>.Fail("Another record with the same name already exists.");

            // Update the entity fields
            existingRecord.Name_En = normializedProcName;
            existingRecord.Name_Hn = request.Name_Hn;
            existingRecord.WorkId = request.WorkId;

            await repository.UpdateAsync(existingRecord);
            await unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(existingRecord.Id);

        }
    }
}
