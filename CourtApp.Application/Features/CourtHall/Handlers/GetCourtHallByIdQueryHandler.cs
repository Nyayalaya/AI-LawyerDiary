using CourtApp.Application.Common;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Application.Features.CourtHall.Query;
using CourtApp.Application.Features.CourtHall.DTOs;
using CourtApp.Application.Features.CourtHall.Interfaces;

namespace CourtApp.Application.Features.CourtHall.Handlers
{
    public class GetCourtHallByIdQueryHandler : IRequestHandler<GetCourtHallByIdQuery, Result<CourtHallByIdResponse>>
    {
        private readonly ICourtHallRepository repository;
        private readonly IMapper mapper;

        public GetCourtHallByIdQueryHandler(ICourtHallRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Result<CourtHallByIdResponse>> Handle(GetCourtHallByIdQuery request, CancellationToken cancellationToken)
        {
            var detail = await repository.GetByIdAsync(request.Id);
            if (detail == null)
                return Result<CourtHallByIdResponse>.Fail("Court hall record not found.");

            var result = mapper.Map<CourtHallByIdResponse>(detail);
            return Result<CourtHallByIdResponse>.Success(result);
        }
    }
}
