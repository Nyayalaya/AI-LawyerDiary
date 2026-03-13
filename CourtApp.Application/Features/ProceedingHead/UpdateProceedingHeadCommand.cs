using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.ProceedingHead
{
    public class UpdateProceedingHeadCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
    }
    public class UpdateProceedingHeadCommandHandler : IRequestHandler<UpdateProceedingHeadCommand, Result<Guid>>
    {
        private readonly IProceedingHeadRepository repository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateProceedingHeadCommandHandler(IProceedingHeadRepository repository, IMapper _mapper, IUnitOfWork _unitOfWork)
        {
            this._mapper = _mapper;
            this.repository = repository;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(UpdateProceedingHeadCommand request, CancellationToken cancellationToken)
        {
            // Fetch the record to update
            var existingRecord = await repository.GetByIdAsync(request.Id);
            if (existingRecord == null)
                return Result<Guid>.Fail("Record does not exist.");


            // Check for name conflict with other records (excluding the current record)
            var duplicateRecord = await repository.Entities
                .Where(e => e.Id != request.Id
                                && e.Abbreviation.ToLower().Trim() == request.Abbreviation.ToLower().Trim()
                                && e.Name_En.ToLower() == request.Name_En.ToLower().Trim())
                .FirstOrDefaultAsync(cancellationToken);

            if (duplicateRecord != null)
                return Result<Guid>.Fail("Another record with the same name already exists.");

            // Update the entity fields
            existingRecord.Name_En = request.Name_En.ToLower().Trim();
            existingRecord.Name_Hn = request.Name_Hn;
            existingRecord.Abbreviation = request.Abbreviation.ToLower().Trim();

            await repository.UpdateAsync(existingRecord);
            await _unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(existingRecord.Id);
            //var detailById = await repository.GetByIdAsync(request.Id);
            //if (detailById == null)
            //    return Result<Guid>.Fail($"Proceeding head not found.");
            //else
            //{               
            //    detailById.Name_En = request.Name_En;
            //    detailById.Name_Hn = request.Name_Hn;
            //    detailById.Abbreviation = request.Abbreviation;
            //    await repository.UpdateAsync(detailById);
            //    await _unitOfWork.Commit(cancellationToken);
            //    return Result<Guid>.Success(detailById.Id);
            //}
        }
    }
}
