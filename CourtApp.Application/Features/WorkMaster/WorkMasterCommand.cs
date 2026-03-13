using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Enums;
using CourtApp.Application.Features.ProceedingHead;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CourtApp.Application.Features.WorkMaster
{
    public class WorkMasterCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public int ActionType { get; set; }
        public string Work_En { get; set; }
        public string Work_Hn { get; set; }
        public string Abbreviation { get; set; }
    }
    public class CreateWorkMasterCommandHandler : IRequestHandler<WorkMasterCommand, Result<Guid>>
    {
        private readonly IWorkMasterRepository _Repository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public CreateWorkMasterCommandHandler(IWorkMasterRepository _Repository, IMapper _mapper, IUnitOfWork _unitOfWork)
        {
            this._mapper = _mapper;
            this._Repository = _Repository;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(WorkMasterCommand request, CancellationToken cancellationToken)
        {
            WorkMasterEntity entity = null;
            if (request.ActionType == ((int)ActionTypes.Add))
            {
                var wmdt = _Repository.Entities
                    .Where(x => x.Work_En.Equals(request.Work_En))
                    .FirstOrDefault();
                if (wmdt != null) return Result<Guid>.Fail("This work type is already exists!");
                entity = _mapper.Map<WorkMasterEntity>(request);
                await _Repository.InsertAsync(entity);
            }
            if (request.ActionType == ((int)ActionTypes.Update))
            {
                entity.Work_En = request.Work_En;
                entity.Work_Hn = request.Work_Hn;
                entity.Abbreviation = request.Abbreviation;
                await _Repository.UpdateAsync(entity);
            }
            if (request.ActionType == ((int)ActionTypes.Delete))
            {
                var dt =await _Repository.GetByIdAsync(request.Id);
                await _Repository.DeleteAsync(dt);
            }
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(entity.Id);
        }
    }
}
