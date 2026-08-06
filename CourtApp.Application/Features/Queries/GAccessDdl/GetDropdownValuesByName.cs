using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Application.DTOs.DropdownDtos;

namespace CourtApp.Application.Features.Queries.GAccessDdl
{
    public class GetDropdownValuesByName : IRequest<Result<List<DdlGuidStringDto>>>
    {
        public string DdlName { get; set; }
    }
    public class GetDropdownValuesByNameCommand : IRequestHandler<GetDropdownValuesByName, Result<List<DdlGuidStringDto>>>
    {
        private readonly IMapper _mapper;
        private readonly ILawyerRepository _LRepo;
        public GetDropdownValuesByNameCommand(ILawyerRepository _LRepo, IMapper _mapper)
        {
            this._LRepo = _LRepo;
            this._mapper = _mapper;
        }
        public async Task<Result<List<DdlGuidStringDto>>> Handle(GetDropdownValuesByName request, CancellationToken cancellationToken)
        {
            List<DdlGuidStringDto> ddlValues = new List<DdlGuidStringDto>();
            if (request.DdlName.Equals("lawyer"))
            {
                var ldt = await _LRepo.Entities.IgnoreQueryFilters().ToListAsync();
                ddlValues = _mapper.Map<List<DdlGuidStringDto>>(ldt);
            }
            if (ddlValues.Count > 0)
                return Result<List<DdlGuidStringDto>>.Success(ddlValues);
            else
                return Result<List<DdlGuidStringDto>>.Fail("There is some problem.");
        }
    }
}
