using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.ProceedingHead
{
    public class CreateProceedingHeadCommand : IRequest<Result<Guid>>
    {
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
    }
    public class CreateProceedingHeadCommandHandler : IRequestHandler<CreateProceedingHeadCommand, Result<Guid>>
    {
        private readonly IProceedingHeadRepository _Repository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public CreateProceedingHeadCommandHandler(IProceedingHeadRepository _Repository, IMapper _mapper, IUnitOfWork _unitOfWork)
        {
            this._mapper = _mapper;
            this._Repository = _Repository;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CreateProceedingHeadCommand request, CancellationToken cancellationToken)
        {
            var dt = _Repository.Entities
                .Where(w => w.Name_En.Equals(request.Name_En) || w.Abbreviation.Equals(request.Abbreviation))
                .FirstOrDefault();
            if (dt != null) return Result<Guid>.Fail("Abbreviation or Name is already exist");
            var entity = _mapper.Map<ProceedingHeadEntity>(request);
            await _Repository.InsertAsync(entity);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(entity.Id);
        }
    }
}
