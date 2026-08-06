using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.Cadre.Commands;
using CourtApp.Application.Features.Cadre.Services;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Masters;
using MediatR;

namespace CourtApp.Application.Features.Cadre.Handlers
{
    public class CreateCadreCommandHandler : IRequestHandler<CreateCadreCommand, Result<string>>
    {
        private readonly ICadreMasterRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCadreCommandHandler(ICadreMasterRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(CreateCadreCommand request, CancellationToken cancellationToken)
        {
            var cadreEntity = _mapper.Map<CadreEntity>(request);
            cadreEntity.Id = Guid.NewGuid();
            await _repository.InsertAsync(cadreEntity);
            await _unitOfWork.Commit(cancellationToken);
            return Result<string>.Success("Cadre created successfully");
        }
    }
}
