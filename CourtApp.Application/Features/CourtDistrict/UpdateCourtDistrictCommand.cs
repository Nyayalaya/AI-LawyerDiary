using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtDistrict
{
    public class UpdateCourtDistrictCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public int StateId { get; set; }
        //public int DistrictId { get; set; }
        public string Abbreviation { get; set; }
    }
    public class UpdateCourtDistrictCommandHandler : IRequestHandler<UpdateCourtDistrictCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourtDistrictRepository repository;

        public UpdateCourtDistrictCommandHandler(ICourtDistrictRepository repository,
            IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateCourtDistrictCommand request, CancellationToken cancellationToken)
        {
            // Fetch the record to update
            var existingRecord = await repository.GetByIdAsync(request.Id);
            if (existingRecord == null)
                return Result<Guid>.Fail("Record does not exist.");


            // Check for name conflict with other records (excluding the current record)
            var duplicateRecord = await repository.Entities
                .Where(e => e.Id != request.Id
                                && e.StateId == request.StateId
                                && e.Name_En.ToLower() == request.Name_En.ToLower().Trim())
                .FirstOrDefaultAsync(cancellationToken);

            if (duplicateRecord != null)
                return Result<Guid>.Fail("Another record with the same name already exists.");

            // Update the entity fields
            existingRecord.Name_En = request.Name_En.ToLower().Trim();
            existingRecord.Name_Hn = request.Name_Hn;
            existingRecord.StateId = request.StateId;

            await repository.UpdateAsync(existingRecord);
            await _unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(existingRecord.Id);

            //var detailById = await repository.GetByIdAsync(command.Id);
            //if (detailById == null)
            //    return Result<Guid>.Fail($"Court District not found.");
            //else
            //{                
            //    //detailById.DistrictCode=command.DistrictId;
            //    detailById.StateId=command.StateId;                
            //    detailById.Name_En=command.Name_En;
            //    detailById.Name_Hn=command.Name_Hn;  
            //    detailById.Abbreviation=command.Abbreviation;
            //    await repository.UpdateAsync(detailById);
            //    await _unitOfWork.Commit(cancellationToken);
            //    return Result<Guid>.Success(detailById.Id);
            //}
        }
    }
}
