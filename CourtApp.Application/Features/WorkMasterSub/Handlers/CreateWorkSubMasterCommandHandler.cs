using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.WorkMasterSub.Commands;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMasterSub.Handlers
{
    public class CreateWorkSubMasterCommandHandler : IRequestHandler<CreateWorkSubMasterCommand, Result<Guid>>
    {
        private readonly IWorkMasterSubRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateWorkSubMasterCommandHandler(IWorkMasterSubRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateWorkSubMasterCommand request, CancellationToken cancellationToken)
        {
            if (request.Works == null || !request.Works.Any())
                return Result<Guid>.Fail("Work type is not supplied!");

            Guid lastInsertedId = Guid.Empty;

            foreach (var item in request.Works)
            {
                bool isDuplicate = _repository.Entities.Any(w =>
                    w.Name.ToLower() == item.Name_En.ToLower().Trim() &&
                    w.WorkId == request.WorkId &&
                    w.CourtTypeId == request.CourtTypeId
                );

                if (isDuplicate)
                    return Result<Guid>.Fail($"The name '{item.Name_En}' already exists for the given Work Master and Court Type.");

                var entity = new WorksEntity
                {
                    Name = item.Name_En.ToUpper().Trim(),
                    Code = item.Abbreviation?.Trim(),
                    WorkId = request.WorkId,
                    CourtTypeId = request.CourtTypeId
                };

                await _repository.InsertAsync(entity);
                lastInsertedId = entity.Id;
            }

            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(lastInsertedId);
        }
    }
}
