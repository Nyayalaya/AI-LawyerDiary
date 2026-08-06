using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.DTOs.CourtMaster;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtMasters.Command
{
    public class UpdateCourtMasterCommand : IRequest<Result<Guid>>
    {

        //public Guid CourtTypeId { get; set; }
        //public string CourtName { get; set; }
        //
        //public string CourtFullName { get; set; }
        //public string Bench { get; set; }
        //public string HeadQuerter { get; set; }
        //public string Address { get; set; }
        //public int DistrictCode { get; set; }
        //public int StateCode { get; set; }
        public Guid Id { get; set; }
        public Guid CourtTypeId { get; set; }
        public string CourtName { get; set; }
        public string Name_Hn { get; set; }
        public int DistrictCode { get; set; }
        public int StateCode { get; set; }
        public Guid CourtDistrictId { get; set; }
        public Guid CourtComplexId { get; set; }
        public List<CourtBenchResponse> CBAddress { get; set; }
    }

    public class UpdateCourtMasterCommandHandler : IRequestHandler<UpdateCourtMasterCommand, Result<Guid>>
    {
        private readonly ICourtMasterRepository repository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateCourtMasterCommandHandler(ICourtMasterRepository repository, 
            IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            this.repository = repository;
            this._unitOfWork = _unitOfWork;
            this._mapper=_mapper;
        }
        public async Task<Result<Guid>> Handle(UpdateCourtMasterCommand request, CancellationToken cancellationToken)
        {
            var detail = await repository.GetByIdAsync(request.Id);
            if (detail == null)
                return Result<Guid>.Fail($"Court detail Not Found.");
            else
            {
                //detail.Name_En = request.CourtName;
                //detail.Name_Hn=request.Name_Hn;
                //detail.CourtTypeId = request.CourtTypeId;
                //detail.CourtComplexId=request.CourtComplexId;
                //detail.CourtDistrictId=request.CourtDistrictId;
                //detail.DistrictId=request.DistrictCode;                              
                //detail.StateId=request.StateCode;
                //detail.LastModifiedOn = DateTime.Now;
                //detail.CourtBenches=_mapper.Map<List<CourtBenchEntity>>(request.CBAddress);
                await repository.UpdateAsync(detail);
                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(detail.Id);
            }
        }
    }
}
