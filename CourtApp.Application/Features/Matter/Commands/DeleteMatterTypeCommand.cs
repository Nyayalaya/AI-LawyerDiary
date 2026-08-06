using CourtApp.Application.Common;
using MediatR;
using System;

namespace CourtApp.Application.Features.Matter.Commands;

public sealed class DeleteMatterTypeCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public DeleteMatterTypeCommand(Guid id)
    {
        Id = id;
    }
}