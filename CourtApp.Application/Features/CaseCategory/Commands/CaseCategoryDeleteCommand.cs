using CourtApp.Application.Common;
using MediatR;
using System;

namespace CourtApp.Application.Features.CaseCategory.Commands
{
    public record CaseCategoryDeleteCommand(Guid Id):IRequest<Result<string>>
    {
    }
}
