using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Lawyer
{
    public class LawyerUpdateCommand : IRequest<Result<string>>
    {
        public Guid Id { get; set; }
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
    public class LawyerUpdateCommandHandler : IRequestHandler<LawyerUpdateCommand, Result<string>>
    {
        private readonly ILawyerRepository repository;
        private readonly IMapper _mapper;
        private IUnitOfWork _uow { get; set; }
        public LawyerUpdateCommandHandler(ILawyerRepository repository,
            IMapper _mapper, IUnitOfWork _uow)
        {
            this.repository = repository;
            this._mapper = _mapper;
            this._uow = _uow;
        }
        public async Task<Result<string>> Handle(LawyerUpdateCommand request, CancellationToken cancellationToken)
        {
            var EntityDetail = await repository.GetByIdAsync(request.Id);
            string oldProfileImgPath = string.Empty;
            if (EntityDetail != null)
            {
                oldProfileImgPath = EntityDetail.ProfileImgPath;
                EntityDetail.EnrollNumber = request.EnrollNumber;
                EntityDetail.Mobile = request.Mobile;
                EntityDetail.Email = request.Email;
                EntityDetail.Address = request.Address;
                EntityDetail.FirstName = request.FirstName;
                EntityDetail.LastName = request.LastName;
                EntityDetail.Relegion = request.Religion;
                EntityDetail.RelPerson = request.RelatedPerson;
                EntityDetail.Dob = request.Dob;
                EntityDetail.Address = request.Address;
                EntityDetail.ProfileImgPath = request.ProfileImgPath;
                await _uow.Commit(cancellationToken);
                return Result<string>.Success(oldProfileImgPath);
            }
            return Result<string>.Fail("Record is not exists!");
        }
    }
}
