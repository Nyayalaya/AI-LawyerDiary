using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseWork
{
    public class UpdateCWorkStatusCommand : IRequest<Result<Guid>>
    {
        //public List<Guid> CWorkId { get; set; }
        public int Status { get; set; }
        //public Guid ProcId { get; set; }
        public List<ProcWorks> ProcWorksDetails { get; set; }
    }

    public class ProcWorks
    {
        public Guid ProcId { get; set; }
        public List<Guid> WorkIds { get; set; }
    }

    public class UpdateCWorkStatusCommandHandler : IRequestHandler<UpdateCWorkStatusCommand, Result<Guid>>
    {
        private readonly ICaseWorkRepository _Repository;
        private readonly ICaseProceedingRepository _ProcRepo;
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateCWorkStatusCommandHandler(ICaseWorkRepository _Repository,
            IUnitOfWork _unitOfWork,
            ICaseProceedingRepository procRepo)
        {
            this._Repository = _Repository;
            this._unitOfWork = _unitOfWork;
            _ProcRepo = procRepo;
        }
        public async Task<Result<Guid>> Handle(UpdateCWorkStatusCommand request, CancellationToken cancellationToken)
        {
            if (request.ProcWorksDetails != null && request.ProcWorksDetails.Any())
            {
                List<Guid> updatedEntities = new List<Guid>();

                foreach (var procdt in request.ProcWorksDetails)
                {
                    var entity = await _ProcRepo.GetDetailById(procdt.ProcId);
                    if (entity == null) continue; // Skip if entity is not found

                    // Update only the relevant works
                    var worksToUpdate = entity.ProcWork.Works
                        .Where(w => procdt.WorkIds.Contains(w.WorkId))
                        .ToList();

                    if (!worksToUpdate.Any()) continue; // Skip if no works match

                    // Apply updates in a single iteration
                    foreach (var work in worksToUpdate)
                    {
                        work.AppliedOn = DateTime.Now;
                        work.Status = request.Status;
                    }

                    // Update the entity
                    await _ProcRepo.UpdateAsync(entity);
                    updatedEntities.Add(entity.Id);
                }

                // Commit changes once after processing all entities (better performance)
                if (updatedEntities.Any())
                {
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<Guid>.Success(updatedEntities.FirstOrDefault());
                }
            }

            return Result<Guid>.Fail("There is no proceeding Id");
            //if (request.ProcWorksDetails != null && request.ProcWorksDetails.Count > 0)
            //{
            //    foreach (var procdt in request.ProcWorksDetails)
            //    {
            //        var entity = await _ProcRepo.GetDetailById(procdt.ProcId);
            //        if (entity != null)
            //        {
            //            List<ProcWorkEntity> w = new List<ProcWorkEntity>();
            //            foreach (var item in procdt.WorkIds)
            //            {
            //                var pr = entity.ProcWork.Works
            //                    .Where(w => w.WorkId == item)
            //                    .FirstOrDefault();
            //                if (pr != null)
            //                {
            //                    pr.AppliedOn = DateTime.Now;
            //                    pr.Status = request.Status;
            //                    w.Add(pr);
            //                }
            //            }
            //            entity.ProcWork.Works = w;
            //            await _ProcRepo.UpdateAsync(entity);
            //            await _unitOfWork.Commit(cancellationToken);
            //            return Result<Guid>.Success(entity.Id);
            //        }
            //    }
            //}
            //return Result<Guid>.Fail("There is no proceeding Id");
        }
    }
}
