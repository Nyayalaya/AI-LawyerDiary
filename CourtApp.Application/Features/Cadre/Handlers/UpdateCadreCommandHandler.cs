using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Features.Cadre.Commands;
using CourtApp.Application.Features.Cadre.Services;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;

namespace CourtApp.Application.Features.Cadre.Handlers
{
    public class UpdateCadreCommandHandler : IRequestHandler<UpdateCadreCommand, Result<string>>
    {
        private readonly ICadreMasterRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCadreCommandHandler(ICadreMasterRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(UpdateCadreCommand request, CancellationToken cancellationToken)
        {
            var existingCadre = await _repository.GetByIdAsync(request.Id);
            if (existingCadre == null)
                return Result<string>.Fail("Cadre not found");

            _mapper.Map(request, existingCadre);
            await _repository.UpdateAsync(existingCadre);
            await _unitOfWork.Commit(cancellationToken);
            return Result<string>.Success("Cadre updated successfully");
        }
    }
}
