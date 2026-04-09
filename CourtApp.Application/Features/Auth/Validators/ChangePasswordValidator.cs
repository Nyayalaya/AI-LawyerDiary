using CourtApp.Application.Features.Auth.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Auth.Validators
{
    public class ChangePasswordValidator:AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.Request.UserId)
            .NotEmpty();

            RuleFor(x => x.Request.CurrentPassword)
                .NotEmpty();

            RuleFor(x => x.Request.NewPassword)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(x => x.Request.ConfirmPassword)
                .Equal(x => x.Request.NewPassword)
                .WithMessage("Passwords do not match");
        }
    }
}
