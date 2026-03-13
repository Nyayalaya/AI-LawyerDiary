using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.FSTitle
{
    public class FSTitleUpdateCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public int TypeId { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
    public class FSTitleUpdateCommandHandler : IRequestHandler<FSTitleUpdateCommand, Result<Guid>>
    {
        private readonly IFSTitleRepository _Repo;
        private readonly IMapper _mapper;
        private IUnitOfWork _uow { get; set; }
        public FSTitleUpdateCommandHandler(IFSTitleRepository _Repo, IMapper _mapper, IUnitOfWork _uow)
        {
            this._mapper = _mapper;
            this._Repo = _Repo;
            this._uow = _uow;
        }
        public async Task<Result<Guid>> Handle(FSTitleUpdateCommand request, CancellationToken cancellationToken)
        {
            // Fetch the record to update
            var existingRecord = await _Repo.GetByIdAsync(request.Id);
            if (existingRecord == null)
                return Result<Guid>.Fail("Record does not exist.");


            // Check for name conflict with other records (excluding the current record)
            var duplicateRecord = await _Repo.Entities
                .Where(e => e.Id != request.Id && e.Name_En.ToLower() == request.Name_En.ToLower().Trim())
                .FirstOrDefaultAsync(cancellationToken);

            if (duplicateRecord != null)
                return Result<Guid>.Fail("Another record with the same name already exists.");

            // Update the entity fields
            existingRecord.Name_En = request.Name_En;
            existingRecord.Name_Hn = request.Name_Hn;
            existingRecord.TypeId = request.TypeId;

            await _Repo.UpdateAsync(existingRecord);

            //Commit the transaction.
            await _uow.Commit(cancellationToken);

            return Result<Guid>.Success(existingRecord.Id);
        }
    }
}
