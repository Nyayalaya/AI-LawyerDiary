using CourtApp.Application.Common;
using CourtApp.Application.Features.Matter.Dtos;
using MediatR;
using System;

namespace CourtApp.Application.Features.Matter.Queries.MatterSubCategory.GetById;

public sealed record GetMatterSubCategoryByIdQuery(Guid Id)
    : IRequest<Result<MatterSubCategoryDto>>;
