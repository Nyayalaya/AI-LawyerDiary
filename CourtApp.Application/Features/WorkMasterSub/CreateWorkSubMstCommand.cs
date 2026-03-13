using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMasterSub
{
    public class CreateWorkSubMstCommand : IRequest<Result<Guid>>
    {
        public Guid WorkId { get; set; }
        //public required string Name_En { get; set; }
        //public string Name_Hn { get; set; }
        //public string Abbreviation { get; set; }
        public List<WrkSubMaster> Works { get; set; }
    }
    public class WrkSubMaster
    {
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        //public string Abbreviation { get; set; }
    }
    public class CreateWorkSubMstCommandHandler : IRequestHandler<CreateWorkSubMstCommand, Result<Guid>>
    {
        private readonly IWorkMasterSubRepository repository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public CreateWorkSubMstCommandHandler(IWorkMasterSubRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CreateWorkSubMstCommand request, CancellationToken cancellationToken)
        {
            if (request.Works == null || !request.Works.Any())
                return Result<Guid>.Fail("Work type is not supplied!");

            Guid lastInsertedId = Guid.Empty;

            foreach (var c in request.Works)
            {
                bool isDuplicate = repository.Entities.Any(w =>
                    w.Name_En.ToLower() == c.Name_En.ToLower().Trim() &&
                    w.WorkId == request.WorkId
                );

                if (isDuplicate)
                    return Result<Guid>.Fail($"The name '{c.Name_En}' already exists for the given Court Type and Nature.");

                var entity = new WorkMasterSubEntity
                {
                    Name_En = c.Name_En.ToUpper().Trim(),
                    Name_Hn = c.Name_Hn?.Trim(),
                    WorkId = request.WorkId
                };

                await repository.InsertAsync(entity);
                lastInsertedId = entity.Id;
            }

            // Commit once after loop for better performance
            await unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(lastInsertedId);
        }
    }
}
