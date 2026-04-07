using CourtApp.Application.Common;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtComplex
{
    public class UpdateCourtComplexCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public int StateId { get; set; }
        public Guid CourtDistrictId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
