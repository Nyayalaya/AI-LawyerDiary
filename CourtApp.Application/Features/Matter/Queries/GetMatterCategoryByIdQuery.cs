using CourtApp.Application.Common;
using CourtApp.Application.Features.Matter.Dtos;
using MediatR;
using System;

namespace CourtApp.Application.Features.Matter.Queries.MatterCategory.GetById;

public sealed record GetMatterCategoryByIdQuery(Guid Id)
    : IRequest<Result<MatterCategoryDto>>;