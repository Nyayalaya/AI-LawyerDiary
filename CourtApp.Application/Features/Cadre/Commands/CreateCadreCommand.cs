using CourtApp.Application.Common;
using MediatR;

namespace CourtApp.Application.Features.Cadre.Commands
{
    public class CreateCadreCommand : IRequest<Result<string>>
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
