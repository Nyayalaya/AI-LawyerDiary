using CourtApp.Application.Features.Auth.Commands;
using CourtApp.Application.Features.CourtType.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtType.Validator
{
    public class CreateCourtTypeCommandValidator : AbstractValidator<CreateCourtTypeCommand>
    {
        public CreateCourtTypeCommandValidator()
        {
            RuleFor(x => x.CourtType)
               .NotEmpty().WithMessage("Type of court is required");

            RuleFor(x => x.Abbreviation)
               .NotEmpty().WithMessage("Type abbreviation is required");

        }
    }
}
