using FluentValidation;
using CourtApp.Application.Features.CaseDetails.Dtos;

namespace CourtApp.Application.Features.CaseDetails.Validators
{
    /// <summary>
    /// Validator for CaseBasicInfoDto
    /// Ensures data integrity and required fields are present
    /// </summary>
    public class CaseBasicInfoDtoValidator : AbstractValidator<CaseBasicInfoDto>
    {
        public CaseBasicInfoDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Case ID is required");

            RuleFor(x => x.CaseTitle)
                .NotEmpty()
                .WithMessage("Case title is required")
                .MaximumLength(500)
                .WithMessage("Case title cannot exceed 500 characters");

            RuleFor(x => x.CaseNumberYear)
                .NotEmpty()
                .WithMessage("Case number and year are required")
                .MaximumLength(50)
                .WithMessage("Case number/year cannot exceed 50 characters");

            RuleFor(x => x.Court)
                .NotEmpty()
                .WithMessage("Court information is required")
                .MaximumLength(200)
                .WithMessage("Court name cannot exceed 200 characters");

            RuleFor(x => x.CaseType)
                .NotEmpty()
                .WithMessage("Case type is required")
                .MaximumLength(100)
                .WithMessage("Case type cannot exceed 100 characters");

            RuleFor(x => x.Stage)
                .MaximumLength(100)
                .WithMessage("Case stage cannot exceed 100 characters");

            RuleFor(x => x.Status)
                .MaximumLength(100)
                .WithMessage("Status cannot exceed 100 characters");

            RuleFor(x => x.InstitutionDate)
                .NotEmpty()
                .WithMessage("Institution date is required")
                .Matches(@"^\d{2}/\d{2}/\d{4}$")
                .WithMessage("Institution date must be in dd/MM/yyyy format");

            RuleFor(x => x.NextDate)
                .MaximumLength(20)
                .When(x => !string.IsNullOrWhiteSpace(x.NextDate))
                .WithMessage("Next date format is invalid");

            RuleFor(x => x.Reference)
                .MaximumLength(100)
                .WithMessage("Reference number cannot exceed 100 characters");

            RuleFor(x => x.CaseNumber)
                .MaximumLength(50)
                .WithMessage("Case number cannot exceed 50 characters");

            RuleFor(x => x.CaseYear)
                .InclusiveBetween(1900, System.DateTime.Now.Year + 1)
                .When(x => x.CaseYear > 0)
                .WithMessage("Case year must be valid");

            RuleFor(x => x.CourtDistrict)
                .MaximumLength(200)
                .WithMessage("Court district cannot exceed 200 characters");

            RuleFor(x => x.CourtComplex)
                .MaximumLength(200)
                .WithMessage("Court complex cannot exceed 200 characters");
        }
    }

    /// <summary>
    /// Validator for CaseDataListDto
    /// Includes validation for base properties + assignment information
    /// </summary>
    public class CaseDataListDtoValidator : AbstractValidator<CaseDataListDto>
    {
        public CaseDataListDtoValidator()
        {
            // Include all base validations
            Include(new CaseBasicInfoDtoValidator());

            RuleFor(x => x.AssignedLawyerId)
                .NotEmpty()
                .WithMessage("Assigned lawyer ID is required")
                .When(x => !string.IsNullOrEmpty(x.AssignedLawyerName));

            RuleFor(x => x.AssignedLawyerName)
                .MaximumLength(200)
                .WithMessage("Lawyer name cannot exceed 200 characters")
                .When(x => !string.IsNullOrEmpty(x.AssignedLawyerName));
        }
    }

    /// <summary>
    /// Validator for CaseResponseDto
    /// Ensures response integrity when returning case data
    /// </summary>
    public class CaseResponseDtoValidator : AbstractValidator<CaseResponseDto>
    {
        public CaseResponseDtoValidator()
        {
            // Include all base validations since CaseResponseDto extends CaseBasicInfoDto
            Include(new CaseBasicInfoDtoValidator());
        }
    }

    /// <summary>
    /// Validator for CaseMinimalResponseDto
    /// Validates minimal case information
    /// </summary>
    public class CaseMinimalResponseDtoValidator : AbstractValidator<CaseMinimalResponseDto>
    {
        public CaseMinimalResponseDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Case ID is required");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Case title is required")
                .MaximumLength(500)
                .WithMessage("Title cannot exceed 500 characters");

            RuleFor(x => x.CaseNumber)
                .NotEmpty()
                .WithMessage("Case number is required")
                .MaximumLength(50)
                .WithMessage("Case number cannot exceed 50 characters");

            RuleFor(x => x.CourtName)
                .NotEmpty()
                .WithMessage("Court name is required")
                .MaximumLength(200)
                .WithMessage("Court name cannot exceed 200 characters");

            RuleFor(x => x.CaseCategory)
                .NotEmpty()
                .WithMessage("Case category is required")
                .MaximumLength(100)
                .WithMessage("Category cannot exceed 100 characters");

            RuleFor(x => x.Status)
                .MaximumLength(100)
                .WithMessage("Status cannot exceed 100 characters");
        }
    }

    /// <summary>
    /// Validator for CaseRequestDto
    /// Validates input for case creation and updates
    /// </summary>
    public class CaseRequestDtoValidator : AbstractValidator<CaseRequestDto>
    {
        public CaseRequestDtoValidator()
        {
            RuleFor(x => x.CaseNo)
                .NotEmpty()
                .WithMessage("Case number is required")
                .MaximumLength(50)
                .WithMessage("Case number cannot exceed 50 characters");

            RuleFor(x => x.CaseYear)
                .NotEmpty()
                .WithMessage("Case year is required")
                .InclusiveBetween(1900, System.DateTime.Now.Year + 1)
                .WithMessage("Case year must be valid");

            RuleFor(x => x.CourtId)
                .NotEmpty()
                .WithMessage("Court selection is required");

            RuleFor(x => x.CourtHallId)
                .NotEmpty()
                .WithMessage("Court hall selection is required");

            RuleFor(x => x.CaseCategoryId)
                .NotEmpty()
                .WithMessage("Case category is required");

            RuleFor(x => x.FirstTitleId)
                .NotEmpty()
                .WithMessage("First title is required");

            RuleFor(x => x.SecondTitleId)
                .NotEmpty()
                .WithMessage("Second title is required");

            RuleFor(x => x.InstitutionDate)
                .NotEmpty()
                .WithMessage("Institution date is required")
                .LessThanOrEqualTo(System.DateTime.Now)
                .When(x => x.InstitutionDate.HasValue)
                .WithMessage("Institution date cannot be in the future");

            RuleFor(x => x.NextDate)
                .GreaterThanOrEqualTo(System.DateTime.Now.Date)
                .When(x => x.NextDate.HasValue)
                .WithMessage("Next date cannot be in the past");

            RuleFor(x => x.DisposalDate)
                .GreaterThan(x => x.InstitutionDate)
                .When(x => x.DisposalDate.HasValue && x.InstitutionDate.HasValue)
                .WithMessage("Disposal date must be after institution date");

            RuleFor(x => x.CaseFirstTitle)
                .NotEmpty()
                .WithMessage("First title text is required")
                .MaximumLength(500)
                .WithMessage("First title cannot exceed 500 characters");

            RuleFor(x => x.CaseSecondTitle)
                .NotEmpty()
                .WithMessage("Second title text is required")
                .MaximumLength(500)
                .WithMessage("Second title cannot exceed 500 characters");

            RuleFor(x => x.Act)
                .MaximumLength(200)
                .WithMessage("Act cannot exceed 200 characters");

            RuleFor(x => x.Section)
                .MaximumLength(100)
                .WithMessage("Section cannot exceed 100 characters");

            RuleFor(x => x.FIRNumber)
                .MaximumLength(50)
                .WithMessage("FIR number cannot exceed 50 characters");

            RuleFor(x => x.FIRYear)
                .InclusiveBetween(1900, System.DateTime.Now.Year + 1)
                .When(x => x.FIRYear.HasValue)
                .WithMessage("FIR year must be valid");

            RuleFor(x => x.Remarks)
                .MaximumLength(1000)
                .WithMessage("Remarks cannot exceed 1000 characters");

            RuleFor(x => x.InternalRemarks)
                .MaximumLength(1000)
                .WithMessage("Internal remarks cannot exceed 1000 characters");

            RuleFor(x => x.CisNumber)
                .MaximumLength(50)
                .WithMessage("CIS number cannot exceed 50 characters");

            RuleFor(x => x.CnrNumber)
                .MaximumLength(50)
                .WithMessage("CNR number cannot exceed 50 characters");

            // Validate disposal date when case is marked as disposed
            RuleFor(x => x.DisposalDate)
                .NotEmpty()
                .WithMessage("Disposal date is required when case is marked as disposed")
                .When(x => x.IsDisposed);

            // Validate linked IDs format
            RuleForEach(x => x.LinkedIds)
                .Matches(@"^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                .WithMessage("Invalid linked case ID format");
        }
    }
}