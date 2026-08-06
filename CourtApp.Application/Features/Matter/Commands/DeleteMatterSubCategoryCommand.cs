using CourtApp.Application.Common;
using MediatR;
using System;
namespace CourtApp.Application.Features.Matter.Commands;

public record DeleteMatterSubCategoryCommand(Guid Id)
    : IRequest<Result<Guid>>;