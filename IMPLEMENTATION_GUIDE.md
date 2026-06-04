# CaseDetails Feature - Implementation Guide

## Quick Start

### Step 1: Register Dependencies (Program.cs)

```csharp
// Add AutoMapper profiles
builder.Services.AddAutoMapper(typeof(CaseDetailsMappingProfile));

// Add FluentValidation
builder.Services
    .AddValidatorsFromAssemblyContaining<CaseBasicInfoDtoValidator>();

// Or if using MediatR pipeline:
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<GetCasesQuery>();
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
});
```

### Step 2: Update Your Handlers

**Before:**
```csharp
public class GetCasesQueryHandler : IRequestHandler<GetCasesQuery, PaginatedResult<CaseDataListDto>>
{
    public async Task<PaginatedResult<CaseDataListDto>> Handle(GetCasesQuery request, CancellationToken cancellationToken)
    {
        var cases = await _repository.GetCasesAsync();
        return cases.Select(x => new CaseDataListDto { ... }).ToList();
    }
}
```

**After:**
```csharp
public class GetCasesQueryHandler : IRequestHandler<GetCasesQuery, PaginatedResult<CaseDataListDto>>
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public GetCasesQueryHandler(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<CaseDataListDto>> Handle(GetCasesQuery request, CancellationToken cancellationToken)
    {
        var cases = await _repository.GetCasesAsync();
        return _mapper.Map<List<CaseDataListDto>>(cases);
    }
}
```

### Step 3: Use Constants in Code

**Before:**
```csharp
Status = x.IsDisposed ? "Disposed" : "Pending"
```

**After:**
```csharp
using CourtApp.Application.Constants;

Status = x.IsDisposed ? CaseStatus.Disposed : CaseStatus.Pending
```

### Step 4: Add Validation to Handlers

```csharp
public class CreateCaseCommandHandler : IRequestHandler<CreateCaseCommand, Result<Guid>>
{
    private readonly IValidator<CaseRequestDto> _validator;

    public async Task<Result<Guid>> Handle(CreateCaseCommand request, CancellationToken cancellationToken)
    {
        // Validate input
        var validationResult = await _validator.ValidateAsync(request.CaseData, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result<Guid>.Fail(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }

        // ... rest of handler
    }
}
```

## DTO Usage Reference

### Getting Case List
```csharp
// Returns list of cases with lawyer assignment
Task<PaginatedResult<CaseDataListDto>> GetCases(GetCasesQuery query);
```

### Getting Single Case
```csharp
// Returns detailed case information
Task<Result<CaseBasicInfoDto>> GetCaseById(GetCaseByIdQuery query);
```

### Creating Case
```csharp
// Input DTO for creation
var request = new CaseRequestDto 
{ 
    CaseNo = "2023-001",
    CaseYear = 2023,
    // ... other properties
};

// Validate
await validator.ValidateAsync(request);

// Create
Task<Result<CaseBasicInfoDto>> CreateCase(CreateCaseCommand command);
```

### Updating Case
```csharp
var updateRequest = new CaseRequestDto { /* updated data */ };
Task<Result<bool>> UpdateCase(UpdateCaseCommand command);
```

## DTO Inheritance Hierarchy

```
BaseResponseDto (optional base)
├── CaseBasicInfoDto
│   ├── CaseDataListDto (+ AssignedLawyerId, AssignedLawyerName)
│   └── CaseResponseDto (same as base, can add more in future)
└── CaseMinimalResponseDto (lightweight, specific fields only)
```

## Constants Usage

### Case Status
```csharp
using CourtApp.Application.Constants;

// In validation
if (status != CaseStatus.Active && status != CaseStatus.Pending)
{
    throw new InvalidOperationException("Invalid case status");
}

// In queries
var activeCases = cases.Where(c => c.Status == CaseStatus.Active);
```

### Case Stage
```csharp
var hearingCases = cases.Where(c => c.Stage == CaseStage.Hearing);
var justmentPending = cases.Where(c => c.Stage == CaseStage.Arguments);
```

### Case Type
```csharp
var civilCases = cases.Where(c => c.CaseType == CaseType.Civil);
var criminalCases = cases.Where(c => c.CaseType == CaseType.Criminal);
```

## Validation Examples

### Validating Create Request
```csharp
[HttpPost]
public async Task<IActionResult> Create(CaseRequestDto request)
{
    var validationResult = await _validator.ValidateAsync(request);
    if (!validationResult.IsValid)
    {
        return BadRequest(new { errors = validationResult.Errors });
    }

    var result = await Mediator.Send(new CreateCaseCommand { CaseData = request });
    return result.IsSuccessful ? Ok(result.Data) : BadRequest(result);
}
```

### Validating Response DTO
```csharp
var responseDto = _mapper.Map<CaseBasicInfoDto>(caseEntity);
var validationResult = await _responseValidator.ValidateAsync(responseDto);

if (!validationResult.IsValid)
{
    _logger.LogWarning("Invalid DTO: {Errors}", validationResult.Errors);
    // Handle invalid response
}
```

## Mapping Examples

### Entity to DTO
```csharp
// Single entity
var dto = _mapper.Map<CaseBasicInfoDto>(caseEntity);

// List of entities
var dtos = _mapper.Map<List<CaseBasicInfoDto>>(caseEntities);

// With specific type
var listDto = _mapper.Map<CaseDataListDto>(caseEntity);
```

### DTO to DTO
```csharp
// Convert between DTO types
var basicDto = new CaseBasicInfoDto { /* ... */ };
var dataListDto = _mapper.Map<CaseDataListDto>(basicDto);
```

### Manual Configuration
```csharp
// In AutoMapper profile
CreateMap<CaseEntity, CaseBasicInfoDto>()
    .ForMember(dest => dest.CaseNumberYear, 
        opt => opt.MapFrom(src => $"{src.CaseNo}-{src.CaseYear}"))
    .ForMember(dest => dest.InstitutionDate,
        opt => opt.MapFrom(src => src.InstitutionDate.ToString("dd/MM/yyyy")))
    .ReverseMap();
```

## API Controller Example

```csharp
[ApiController]
[Route("api/[controller]")]
public class CasesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<CaseRequestDto> _validator;

    public CasesController(IMediator mediator, IValidator<CaseRequestDto> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetCases(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetCasesQuery { PageNumber = pageNumber, PageSize = pageSize };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCaseById(Guid id)
    {
        var query = new GetCaseByIdQuery { CaseId = id };
        var result = await _mediator.Send(query);
        return result.IsSuccessful ? Ok(result.Data) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCase([FromBody] CaseRequestDto request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new { errors = validationResult.Errors });
        }

        var command = new CreateCaseCommand { CaseData = request };
        var result = await _mediator.Send(command);
        return result.IsSuccessful ? Created(nameof(GetCaseById), result.Data) : BadRequest(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCase(Guid id, [FromBody] CaseRequestDto request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new { errors = validationResult.Errors });
        }

        var command = new UpdateCaseCommand { CaseId = id, CaseData = request };
        var result = await _mediator.Send(command);
        return result.IsSuccessful ? Ok(result.Data) : BadRequest(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCase(Guid id)
    {
        var command = new DeleteCaseCommand { CaseId = id };
        var result = await _mediator.Send(command);
        return result.IsSuccessful ? Ok() : BadRequest(result);
    }
}
```

## Common Patterns

### Pattern 1: Query with Filters
```csharp
var query = new GetCasesQuery 
{
    PageNumber = pageNumber,
    PageSize = pageSize,
    Status = CaseStatus.Active,
    CaseType = CaseType.Civil
};

var result = await _mediator.Send(query);
```

### Pattern 2: Create with Validation
```csharp
var request = new CaseRequestDto { /* data */ };
var validationResult = await _validator.ValidateAsync(request);

if (!validationResult.IsValid)
{
    return Result<Guid>.Fail(validationResult.Errors);
}

var command = new CreateCaseCommand { CaseData = request };
return await _mediator.Send(command);
```

### Pattern 3: Update with Concurrency Check
```csharp
var existingCase = await _repository.GetByIdAsync(id);
if (existingCase == null)
{
    return Result<bool>.Fail("Case not found");
}

// Validate new data
var validationResult = await _validator.ValidateAsync(request);
if (!validationResult.IsValid)
{
    return Result<bool>.Fail(validationResult.Errors);
}

// Update and save
_mapper.Map(request, existingCase);
await _repository.UpdateAsync(existingCase);
return Result<bool>.Success(true);
```

## Testing

### Unit Test Template
```csharp
[TestClass]
public class CaseBasicInfoDtoTests
{
    private IMapper _mapper;

    [TestInitialize]
    public void Setup()
    {
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<CaseDetailsMappingProfile>())
            .CreateMapper();
    }

    [TestMethod]
    public void MapFromEntity_ShouldMapAllProperties()
    {
        var entity = new CaseEntity 
        { 
            Id = Guid.NewGuid(),
            CaseNo = "2023-001",
            CaseYear = 2023
        };

        var dto = _mapper.Map<CaseBasicInfoDto>(entity);

        Assert.IsNotNull(dto);
        Assert.AreEqual(entity.Id, dto.Id);
        Assert.AreEqual("2023-001-2023", dto.CaseNumberYear);
    }

    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void Validate_EmptyTitle_ShouldThrow()
    {
        var validator = new CaseBasicInfoDtoValidator();
        var dto = new CaseBasicInfoDto { CaseTitle = "" };

        var result = validator.Validate(dto);
        if (!result.IsValid) throw new ValidationException("Validation failed");
    }
}
```

## Troubleshooting

### Issue: "InstitutionDate property not found"
**Solution:** Make sure you're using `InstitutionDate` not `InsititutionDate`

### Issue: "ParentCaseId is Guid.Empty when null"
**Solution:** Check for null using `.HasValue` for nullable Guid
```csharp
if (dto.ParentCaseId.HasValue)
{
    // Handle parent case
}
```

### Issue: "Status not matching constant"
**Solution:** Use constants instead of hardcoded strings
```csharp
Status = x.IsDisposed ? CaseStatus.Disposed : CaseStatus.Pending
```

### Issue: "Mapping not working"
**Solution:** Ensure AutoMapper profile is registered in DI and used correctly
```csharp
var dto = _mapper.Map<CaseBasicInfoDto>(entity);  // Not: new CaseBasicInfoDto { ... }
```

## Performance Tips

1. **Use Projections:** Project to DTO in LINQ query, not after fetching
2. **Eager Load:** Include related entities before mapping
3. **Cache Constants:** Cache CaseStatus, CaseType, etc. values
4. **Select Only Needed Columns:** Don't fetch entire entities if not needed
5. **Use Async:** Always use async/await for database operations

---

## Files Reference

| File | Purpose |
|------|---------|
| `CaseBasicInfoDto.cs` | Base case DTO (refactored) |
| `CaseDataListDto.cs` | Extended with assignment info |
| `CaseResponseDto.cs` | API response DTO |
| `CaseRequestDto.cs` | Create/Update input DTO |
| `BaseDto.cs` | Base DTO pattern |
| `CaseConstants.cs` | Constants for types, statuses, etc. |
| `CaseDetailsMappingProfile.cs` | AutoMapper configuration |
| `CaseDetailsDtoValidators.cs` | Validation rules |

---

For more details, see `CASE_DETAILS_DTO_REFACTORING.md` and `BUG_REPORT_AND_FIXES.md`
