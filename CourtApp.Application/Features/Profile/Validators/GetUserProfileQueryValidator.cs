using CourtApp.Application.Features.Profile.Commands;
using FluentValidation;

namespace CourtApp.Application.Features.Profile.Validators
{
    public class GetUserProfileQueryValidator : AbstractValidator<GetUserProfileQuery>
    {
        public GetUserProfileQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required")
                .NotNull().WithMessage("User ID cannot be null");
        }
    }
}
