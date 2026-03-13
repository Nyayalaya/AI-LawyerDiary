using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace CourtApp.Application.Features.Lawyer
{
    public class LawyerCreateCommand : IRequest<Result<Guid>>
    {
        public string FirstName { get; set; }
        public DateTime Dob { get; set; }
        public string LastName { get; set; }
        public string EnrollNumber { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string Caste { get; set; }
        public string RelatedPerson { get; set; }
        public string ProfileImgPath { get; set; }
    }
    public class LawyerCreateCommandHandler : IRequestHandler<LawyerCreateCommand, Result<Guid>>
    {
        private readonly ILawyerRepository repository;
        private readonly IMapper _mapper;
        private IUnitOfWork _uow { get; set; }
        public LawyerCreateCommandHandler(ILawyerRepository repository,
            IMapper _mapper, IUnitOfWork _uow)
        {
            this.repository = repository;
            this._mapper = _mapper;
            this._uow = _uow;
        }
        public async Task<Result<Guid>> Handle(LawyerCreateCommand request, CancellationToken cancellationToken)
        {
            var isEntityCount = repository.Entities
                .Where(w => w.EnrollNumber.Equals(request.EnrollNumber))
                .Count();
            if (isEntityCount == 0)
            {
                var entity = _mapper.Map<LawyerMasterEntity>(request);
                await repository.InsertAsync(entity);
                await _uow.Commit(cancellationToken);
                return Result<Guid>.Success(entity.Id);
            }
            return Result<Guid>.Fail("Given Lawyer is already exists!");
        }
    }
}
