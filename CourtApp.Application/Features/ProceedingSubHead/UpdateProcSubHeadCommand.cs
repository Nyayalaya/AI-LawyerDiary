using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.ProceedingSubHead
{
    public class UpdateProcSubHeadCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public Guid HeadId { get; set; }
        public int ActionType { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
    public class UpdateProcSubHeadCommandHandler : IRequestHandler<UpdateProcSubHeadCommand, Result<Guid>>
    {
        private readonly IProceedingSubHeadRepository _Repository;
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateProcSubHeadCommandHandler(IProceedingSubHeadRepository _Repository, IMapper _mapper, IUnitOfWork _unitOfWork)
        {

            this._Repository = _Repository;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(UpdateProcSubHeadCommand request, CancellationToken cancellationToken)
        {
            // Fetch the record to update
            var existingRecord = await _Repository.GetByIdAsync(request.Id);
            if (existingRecord == null)
                return Result<Guid>.Fail("Record does not exist.");

            string normializedProcName = request.Name_En.ToUpper().Trim();
            // Check for name conflict with other records (excluding the current record)
            var duplicateRecord = await _Repository.Entities
                .Where(e => e.Id != request.Id
                                && e.HeadId == request.HeadId
                                && e.Name_En.ToUpper().Trim() == normializedProcName)
                .FirstOrDefaultAsync(cancellationToken);

            if (duplicateRecord != null)
                return Result<Guid>.Fail("Another record with the same name already exists.");

            // Update the entity fields
            existingRecord.Name_En = normializedProcName;
            existingRecord.Name_Hn = request.Name_Hn;
            existingRecord.HeadId = request.HeadId;

            await _Repository.UpdateAsync(existingRecord);
            await _unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(existingRecord.Id);
            //var detailById = await _Repository.GetByIdAsync(request.Id);
            //if (detailById == null)
            //    return Result<Guid>.Fail($"Proceeding sub head not found.");
            //else
            //{
            //    detailById.Name_En = request.Name_En;
            //    detailById.Name_Hn = request.Name_Hn;
            //    detailById.HeadId = request.HeadId;

            //    await _Repository.UpdateAsync(detailById);
            //    await _unitOfWork.Commit(cancellationToken);
            //    return Result<Guid>.Success(detailById.Id);
            //}
        }
    }
}
