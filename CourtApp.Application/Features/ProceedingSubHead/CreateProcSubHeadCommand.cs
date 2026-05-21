using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Domain.Entities.Masters;

namespace CourtApp.Application.Features.ProceedingSubHead
{
    public class CreateProcSubHeadCommand : IRequest<Result<Guid>>
    {
        public Guid ProceedingTypeId { get; set; }
        public string Name { get; set; }
        public List<ProceedingHead> ProcHeads { get; set; }
    }
    public class ProceedingHead
    {
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
    public class CreateProcSubHeadCommandHandler : IRequestHandler<CreateProcSubHeadCommand, Result<Guid>>
    {
        private readonly IProceedingSubHeadRepository _Repository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public CreateProcSubHeadCommandHandler(IProceedingSubHeadRepository _Repository, IMapper _mapper, IUnitOfWork _unitOfWork)
        {
            this._mapper = _mapper;
            this._Repository = _Repository;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CreateProcSubHeadCommand request, CancellationToken cancellationToken)
        {
            if (request.ProcHeads == null || !request.ProcHeads.Any())
                return Result<Guid>.Fail("Case type is not supplied!");

            Guid lastInsertedId = Guid.Empty;

            foreach (var c in request.ProcHeads)
            {
                bool isDuplicate = _Repository.Entities.Any(w =>
                    w.Name.ToLower() == c.Name_En.ToLower().Trim() &&
                    w.ProceedingTypeId == request.ProceedingTypeId
                );

                if (isDuplicate)
                    return Result<Guid>.Fail($"The name '{c.Name_En}' already exists for the given Court Type and Nature.");

                var entity = new ProceedingEntity
                {
                    Name = c.Name_En.ToUpper().Trim(),
                    Code = c.Name_En.ToUpper().Trim(),
                    ProceedingTypeId = request.ProceedingTypeId
                };

                await _Repository.InsertAsync(entity);
                lastInsertedId = entity.Id;
            }

            // Commit once after loop for better performance
            await _unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(lastInsertedId);
        }
    }
}
