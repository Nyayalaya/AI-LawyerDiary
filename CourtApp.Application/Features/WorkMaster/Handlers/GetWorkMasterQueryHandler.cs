using AutoMapper;
using CourtApp.Application.Common;
using CourtApp.Application.Extensions;
using CourtApp.Application.Features.WorkMaster.Dtos;
using CourtApp.Application.Features.WorkMaster.Queries;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMaster.Handlers
{
    public class GetWorkMasterQueryHandler : IRequestHandler<GetWorkMasterQuery, PaginatedResult<WorkMasterResponse>>
    {
        private readonly IWorkMasterRepository _repository;
        private readonly IMapper _mapper;

        public GetWorkMasterQueryHandler(IWorkMasterRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<WorkMasterResponse>> Handle(GetWorkMasterQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.Entities.AsQueryable();

            if (request.CourtTypeId != Guid.Empty)
                query = query.Where(x => x.CourtTypeId == request.CourtTypeId);

            var works = await query.ToListAsync(cancellationToken);
            var responses = _mapper.Map<List<WorkMasterResponse>>(works);

            var result = responses.ToPaginatedResult(request.PageNumber, request.PageSize);
            return result;
        }
    }
}
