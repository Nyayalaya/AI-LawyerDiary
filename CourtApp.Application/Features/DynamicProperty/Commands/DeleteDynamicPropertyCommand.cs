using CourtApp.Application.Common;
using MediatR;
using System;

namespace CourtApp.Application.Features.DynamicProperty.Commands;

public sealed class DeleteDynamicPropertyCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public DeleteDynamicPropertyCommand(Guid id) => Id = id;
}
