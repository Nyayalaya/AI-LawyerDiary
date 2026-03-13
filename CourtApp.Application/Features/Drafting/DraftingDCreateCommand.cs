using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Drafting;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Drafting
{
    public class DraftingDCreateCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public Guid CaseId { get; set; }
        public int Gender { get; set; }
        public string PersonName { get; set; }
        public int PersonAge { get; set; }
        public int AgeAsPer { get; set; }
        public string Occupation { get; set; }
        public float InCome { get; set; }
        public DateTime AcciedentDate { get; set; }
        public string PlaceOfAcciedent { get; set; }
        public string VehicalType { get; set; }
        public string VehicalNumber { get; set; }
        public string InsuranceCompany { get; set; }
        public float AmountClaim { get; set; }
        public string FirNumber { get; set; }
        public DateTime FirDate { get; set; }
        public float MACTAmt { get; set; }
        public string ClaimantRelation { get; set; }
        public float Multiplier { get; set; }
        public string Dependency { get; set; }
        public float ConsAmt { get; set; }
        public float AmtAffection { get; set; }
        public string FProspects { get; set; }
        public bool IsInsurorExp { get; set; }
        public string ExpoGround { get; set; }
        public string DoctorName { get; set; }
        public float MedlExp { get; set; }
        public float TransAmt { get; set; }
        public float FuneralAmt { get; set; }
    }
    public class DraftingDCreateCommandHandler : IRequestHandler<DraftingDCreateCommand, Result<Guid>>
    {
        private readonly IFSTitleRepository _Repo;
        private readonly IMapper _mapper;
        private IUnitOfWork _uow { get; set; }
        public DraftingDCreateCommandHandler(IFSTitleRepository _Repo,
            IMapper _mapper,
            IUnitOfWork _uow)
        {
            this._Repo = _Repo;
            this._mapper = _mapper;
            this._uow = _uow;
        }
        public async Task<Result<Guid>> Handle(DraftingDCreateCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ClaimPetitionDeathEntity>(request);
            return await Result<Guid>.FailAsync();
        }
    }
}
