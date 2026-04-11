
using MediatR;
using System;

namespace CourtApp.Application.Features.FormManagement.Commands
{
    public class CreateFormTypeCommand : IRequest<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateFormTypeCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class DeleteFormTypeCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
