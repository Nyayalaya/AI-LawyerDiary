using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Domain.Entities.Masters;

namespace CourtApp.Application.Features.CourtComplex
{
    public class CreateCourtComplexCommand : IRequest<Result<Guid>>
    {
        public int StateId { get; set; }
        public Guid CourtDistrictId { get; set; }
        public List<DistrictComplex> Complexes { get; set; }
    }

    public class DistrictComplex
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
