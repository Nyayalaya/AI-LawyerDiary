using CourtApp.Application.Features.SystemUsers.Queries;
using FluentValidation;

namespace CourtApp.Application.Features.SystemUsers.Validators
{
    /// <summary>
    /// Validator for GetSystemUsersQuery
    /// </summary>
    public class GetSystemUsersQueryValidator : AbstractValidator<GetSystemUsersQuery>
    {
        public GetSystemUsersQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("Page number must be greater than 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than 0.")
                .LessThanOrEqualTo(100).WithMessage("Page size cannot exceed 100.");

            RuleFor(x => x.SearchTerm)
                .MaximumLength(200).WithMessage("Search term cannot exceed 200 characters.")
                .When(x => !string.IsNullOrEmpty(x.SearchTerm));
        }
    }
}
