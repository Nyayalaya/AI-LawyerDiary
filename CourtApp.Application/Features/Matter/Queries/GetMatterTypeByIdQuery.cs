using CourtApp.Application.Common;
using CourtApp.Application.Features.Matter.Dtos;

using MediatR;
using System;

namespace CourtApp.Application.Features.Matter.Queries;

public sealed record GetMatterTypeByIdQuery(Guid Id)
    : IRequest<Result<MatterTypeDto>>;