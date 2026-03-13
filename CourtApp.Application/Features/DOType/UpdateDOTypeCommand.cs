using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.DOType
{
    public class UpdateDOTypeCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public int TypeId { get; set; }
        public string Name_En { get; set; }
    }
    public class UpdateDOTypeCommandHandler : IRequestHandler<UpdateDOTypeCommand, Result<Guid>>
    {
        private readonly IDOTypeRepository repository;
        private readonly IMapper mapper;
        private IUnitOfWork unitOfWork { get; set; }
        public UpdateDOTypeCommandHandler(IDOTypeRepository _Repo, IMapper _mapper, IUnitOfWork _uow)
        {
            this.mapper = _mapper;
            this.repository = _Repo;
            this.unitOfWork = _uow;
        }
        public async Task<Result<Guid>> Handle(UpdateDOTypeCommand request, CancellationToken cancellationToken)
        {
            // Fetch the record to update
            var existingRecord = await repository.GetByIdAsync(request.Id);
            if (existingRecord == null)
                return Result<Guid>.Fail("Record does not exist.");


            // Check for name conflict with other records (excluding the current record)
            var duplicateRecord = await repository.Entities
                .Where(e => e.Id != request.Id && e.TypeId == request.TypeId && e.Name_En.ToLower() == request.Name_En.ToLower().Trim())
                .FirstOrDefaultAsync(cancellationToken);

            if (duplicateRecord != null)
                return Result<Guid>.Fail("Another record with the same name already exists.");

            // Update the entity fields
            existingRecord.Name_En = request.Name_En;
            existingRecord.TypeId = request.TypeId;

            await repository.UpdateAsync(existingRecord);
            await unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(existingRecord.Id);
            //var entity = await _Repo.GetByIdAsync(command.Id);

            //if (entity == null)
            //    return Result<Guid>.Fail($"Draft & Order Not Found.");
            //else
            //{
            //    entity.Name_En = command.Name_En ?? entity.Name_En;
            //    entity.TypeId = (command.TypeId == 0) ? entity.TypeId : command.TypeId;
            //    await _Repo.UpdateAsync(entity);
            //    await _uow.Commit(cancellationToken);
            //    return Result<Guid>.Success(entity.Id);
            //}
        }
    }
}
