using CourtApp.Application.Common;
using MediatR;
using System;

namespace CourtApp.Application.Features.Matter.Commands.MatterCategory.Delete;

public sealed record DeleteMatterCategoryCommand(Guid Id)
    : IRequest<Result<Guid>>;