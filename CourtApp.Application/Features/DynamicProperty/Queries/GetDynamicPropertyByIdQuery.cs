using CourtApp.Application.Common;
using CourtApp.Application.Features.DynamicProperty.Dtos;
using MediatR;
using System;

namespace CourtApp.Application.Features.DynamicProperty.Queries.GetById;

public sealed record GetDynamicPropertyByIdQuery(Guid Id)
    : IRequest<Result<DynamicPropertyDto>>;
