using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace CourtApp.Application.Features.CaseTitle
{
    public class UpdateCaseTitleCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public Guid CaseId { get; set; }
        public int TypeId { get; set; }
        public List<CaseApplicantDetail> CaseApplicants { get; set; }
    }
    public class UpdateCaseTitleCommandHandler : IRequestHandler<UpdateCaseTitleCommand, Result<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IUserCaseRepository _UserCaseRepo;
        private readonly ICaseTitleRepository _CaseTitleRepository;
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateCaseTitleCommandHandler(IUserCaseRepository _UserCaseRepo,
            IMapper _mapper, IUnitOfWork unitOfWork, ICaseTitleRepository caseTitleRepository)
        {
            this._mapper = _mapper;
            this._UserCaseRepo = _UserCaseRepo;
            _unitOfWork = unitOfWork;
            _CaseTitleRepository = caseTitleRepository;

        }
        public async Task<Result<Guid>> Handle(UpdateCaseTitleCommand request, CancellationToken cancellationToken)
        {
            var titleDetail = await _CaseTitleRepository.GetByIdAsync(request.Id);

            if (titleDetail is null)
                return Result<Guid>.Fail("Case title not found.");

            _mapper.Map(request, titleDetail); // Map only updated fields

            titleDetail.CaseApplicants = _mapper.Map<List<CaseApplicantDetailEntity>>(request.CaseApplicants);

            await _CaseTitleRepository.UpdateAsync(titleDetail);
            await _unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(titleDetail.Id);
        }
    }
}
