using CourtApp.Application.Common;
using MediatR;
using System;

namespace CourtApp.Application.Features.CaseCategory.Commands
{
    public record CaseCategoryUpdateCommand:IRequest<Result<string>>
    {
        public Guid Id { get; set; }
        public Guid CourtTypeId { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
}
