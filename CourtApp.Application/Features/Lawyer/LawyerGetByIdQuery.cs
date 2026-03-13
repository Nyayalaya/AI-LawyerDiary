using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.DTOs.FSTitle;
using CourtApp.Application.DTOs.Lawyer;
using CourtApp.Application.Features.FSTitle;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Lawyer
{
    public class LawyerGetByIdQuery : IRequest<Result<LawyerResponseById>>
    {
        public Guid Id { get; set; }
    }
    public class LawyerGetByIdQueryHandler : IRequestHandler<LawyerGetByIdQuery, Result<LawyerResponseById>>
    {
        private readonly ILawyerRepository _repository;
        private readonly IMapper _mapper;
        public LawyerGetByIdQueryHandler(ILawyerRepository _repository, IMapper _mapper)
        {
            this._repository = _repository;
            this._mapper = _mapper;
        }
        public async Task<Result<LawyerResponseById>> Handle(LawyerGetByIdQuery request, CancellationToken cancellationToken)
        {
            var detail = await _repository.GetByIdAsync(request.Id);
            var mappedProduct = _mapper.Map<LawyerResponseById>(detail);
            return Result<LawyerResponseById>.Success(mappedProduct);
        }
    }
}
