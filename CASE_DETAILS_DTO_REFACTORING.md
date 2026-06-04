# CaseDetails DTO Structure - Refactoring Documentation

## Overview

This document outlines the refactored and standardized DTO structure for the CaseDetails feature, designed to be reusable across the entire application while maintaining consistency and reducing code duplication.

## Architecture

### File Structure
```
CourtApp.Application/
├── Dtos/
│   ├── Common/
│   │   └── BaseDto.cs                    # Base classes for all DTOs
│   └── CaseDetails/
│       └── CaseDetailInfoDto.cs          # Complex case detail response
├── Constants/
│   └── CaseConstants.cs                  # Enums and constants
├── Features/
│   └── CaseDetails/
│       ├── Dtos/
│       │   ├── CaseBasicInfoDto.cs       # Base case DTO (refactored)
│       │   ├── CaseDataListDto.cs        # Extended with assignment info
│       │   ├── CaseRequestDto.cs         # Create/Update request DTO
│       │   ├── CaseResponseDto.cs        # Simple response DTO
│       │   └── CaseAgainstRequestDto.cs  # Against case DTO
│       ├── Queries/
│       ├── Commands/
│       └── Handlers/
└── Mapping/
    └── CaseDetailsMappingProfile.cs      # AutoMapper profile
```

## Key Changes & Fixes

### 1. CaseBasicInfoDto Refactoring

**Before:**
```csharp
public abstract class CaseBasicInfoDto  // ❌ Was abstract (unnecessary)
{
    public Guid Id { get; set; }
    public string InsititutionDate { get; set; }  // ❌ Typo: "InsititutionDate"
    public string status { get; set; }  // ❌ Duplicate (lowercase)
    public string Status { get; set; }  // ❌ Duplicate (uppercase)
    public Guid ParentCaseId { get; set; }  // ❌ Not nullable
    // ... missing properties
}
```

**After:**
```csharp
public class CaseBasicInfoDto  // ✅ Concrete class
{
    public Guid Id { get; set; }
    public string InstitutionDate { get; set; }  // ✅ Fixed typo
    public string Status { get; set; }  // ✅ Single property
    public Guid? ParentCaseId { get; set; }  // ✅ Nullable

    // ✅ Added comprehensive properties
    public string CaseNumber { get; set; }
    public int? CaseYear { get; set; }
    public string CourtDistrict { get; set; }
    public string CourtComplex { get; set; }
    public bool IsImportant { get; set; }
    public bool IsUrgent { get; set; }
    public bool IsDisposed { get; set; }

    // ✅ XML documentation for all properties
}
```

### 2. Bug Fixes

| Bug | Location | Fix |
|-----|----------|-----|
| Typo: `InsititutionDate` | CaseBasicInfoDto | Renamed to `InstitutionDate` |
| Duplicate `status` and `Status` | CaseBasicInfoDto | Removed lowercase, kept `Status` |
| Abstract class (unnecessary) | CaseBasicInfoDto | Changed to concrete class |
| Non-nullable `ParentCaseId` | CaseBasicInfoDto | Changed to `Guid?` |
| Missing properties | CaseBasicInfoDto | Added: CaseNumber, CaseYear, CourtDistrict, etc. |
| No documentation | All DTOs | Added comprehensive XML comments |
| No inheritance pattern | DTOs | Established base DTO pattern |

### 3. Generic/Reusable Structure

**BaseDto Pattern:**
```csharp
// Base class for all response DTOs
public abstract class BaseResponseDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}

// CaseBasicInfoDto and others can optionally extend this
```

**Inheritance Hierarchy:**
```
BaseResponseDto (optional)
    └─ CaseBasicInfoDto ✅ Reusable across features
        └─ CaseDataListDto ✅ Extended with role-specific data
```

## DTO Specifications

### CaseBasicInfoDto
**Purpose:** Generic case information, reusable across all features
**Usage:** Case lists, case cards, case summaries, case details
**Properties:** 22 essential properties
**Inheritance:** Standalone (can extend BaseResponseDto if needed)

### CaseDataListDto
**Purpose:** Case with assignment context
**Usage:** Lawyer's case list, assigned cases view
**Extends:** CaseBasicInfoDto
**Additional Properties:** AssignedLawyerId, AssignedLawyerName

### CaseResponseDto
**Purpose:** Simplified case information for quick responses
**Usage:** API responses, frontend displays
**Properties:** 9 core properties
**Relation:** Maps to/from CaseBasicInfoDto

### CaseRequestDto
**Purpose:** Input for create/update operations
**Usage:** Case creation, case updates
**Properties:** 45 input properties
**Validation:** Should include FluentValidation validators

## Usage Examples

### 1. Getting a Case List
```csharp
// Query handler returning list of cases with lawyer assignment
var cases = await _repository.GetCasesAsync();
var result = _mapper.Map<List<CaseDataListDto>>(cases);
// Returns: CaseBasicInfoDto properties + AssignedLawyerId/Name
```

### 2. Creating a Case
```csharp
// Command handler for case creation
var request = new CaseRequestDto { /* properties */ };
var validationResult = await _validator.ValidateAsync(request);

var caseEntity = _mapper.Map<UserCase>(request);
await _repository.CreateAsync(caseEntity);

var responseDto = _mapper.Map<CaseBasicInfoDto>(caseEntity);
return responseDto;
```

### 3. Getting Case Details
```csharp
// Query handler for detailed case information
var caseEntity = await _repository.GetDetailAsync(caseId);
var detailedDto = new CaseDetailInfoDto { /* manually mapped */ };
var basicDto = _mapper.Map<CaseBasicInfoDto>(caseEntity);
// Use basicDto + additional details as needed
```

### 4. Filtering Cases
```csharp
// Search by status, type, importance
var query = _dbContext.Cases
    .Where(c => c.Status == CaseStatus.Active)
    .Where(c => c.IsImportant == true)
    .Select(c => new CaseBasicInfoDto { /* mapping */ })
    .ToList();
```

## Constants Reference

### CaseStatus
```csharp
CaseStatus.Pending
CaseStatus.Active
CaseStatus.OnHearing
CaseStatus.Disposed
CaseStatus.Withdrawn
CaseStatus.Dismissed
CaseStatus.Adjourned
CaseStatus.Reserved
```

### CaseStage
```csharp
CaseStage.Filing
CaseStage.Registration
CaseStage.FirstHearing
CaseStage.Hearing
CaseStage.Arguments
CaseStage.Judgment
CaseStage.PostJudgment
CaseStage.Appeal
```

### CaseType
```csharp
CaseType.Civil
CaseType.Criminal
CaseType.Constitutional
CaseType.Administrative
CaseType.Commercial
```

## Mapping Configuration

### AutoMapper Profile
```csharp
public class CaseDetailsMappingProfile : Profile
{
    public CaseDetailsMappingProfile()
    {
        // DTO to DTO
        CreateMap<CaseBasicInfoDto, CaseDataListDto>().ReverseMap();
        CreateMap<CaseBasicInfoDto, CaseResponseDto>()
            .ForMember(dest => dest.CaseNumber, opt => opt.MapFrom(src => src.CaseNumberYear))
            .ForMember(dest => dest.CourtName, opt => opt.MapFrom(src => src.Court))
            .ReverseMap();

        // Entity to DTO (add when domain entities are available)
        // CreateMap<UserCase, CaseBasicInfoDto>()
        //     .ForMember(dest => dest.CaseNumberYear, 
        //         opt => opt.MapFrom(src => $"{src.CaseNo}-{src.CaseYear}"))
        //     .ReverseMap();
    }
}
```

## Best Practices

### 1. Always Use CaseBasicInfoDto for Lists
```csharp
// ✅ Good: Using the generic DTO
public async Task<List<CaseBasicInfoDto>> GetCases()
{
    var cases = await _repository.GetCasesAsync();
    return _mapper.Map<List<CaseBasicInfoDto>>(cases);
}

// ❌ Avoid: Creating new DTO just for this method
public class GetCasesResponseDto { ... }
```

### 2. Extend with Specific Data When Needed
```csharp
// ✅ Good: Extend base DTO for specific contexts
public class CaseWithLawyerDto : CaseBasicInfoDto
{
    public string LawyerName { get; set; }
    public string LawyerEmail { get; set; }
}

// ❌ Avoid: Duplicating all properties
public class CaseWithLawyerDto
{
    public Guid Id { get; set; }
    public string CaseTitle { get; set; }
    // ... repeat all properties
}
```

### 3. Use Constants for String Values
```csharp
// ✅ Good: Use constants
if (case.Status == CaseStatus.Active) { }

// ❌ Avoid: Magic strings
if (case.Status == "Active") { }
```

### 4. Add XML Documentation
```csharp
// ✅ Good: Every property documented
/// <summary>
/// Case number combined with year (e.g., "2023-001")
/// </summary>
public string CaseNumberYear { get; set; }

// ❌ Avoid: No documentation
public string CaseNumberYear { get; set; }
```

## Integration Checklist

- [x] Created refactored `CaseBasicInfoDto`
- [x] Fixed typos and duplicate properties
- [x] Added comprehensive XML documentation
- [x] Updated `CaseDataListDto` inheritance
- [x] Created `BaseDto` for future use
- [x] Created `CaseConstants.cs` with enums
- [x] Created `CaseDetailsMappingProfile.cs`
- [ ] Update all queries/handlers to use new DTO structure
- [ ] Add FluentValidation validators for `CaseRequestDto`
- [ ] Update AutoMapper configurations in existing handlers
- [ ] Update API controllers to return new DTO structure
- [ ] Add unit tests for DTO mappings
- [ ] Update API documentation/Swagger

## Migration Guide

### For Existing Code

1. **Replace typo:**
   ```csharp
   // Old
   dto.InsititutionDate

   // New
   dto.InstitutionDate
   ```

2. **Remove duplicate status:**
   ```csharp
   // Old
   public string status { get; set; }
   public string Status { get; set; }

   // New
   public string Status { get; set; }
   ```

3. **Handle nullable ParentCaseId:**
   ```csharp
   // Old
   public Guid ParentCaseId { get; set; }

   // New
   public Guid? ParentCaseId { get; set; }
   ```

## Performance Considerations

1. **Select Only Needed Properties:** Use projections with LINQ to select only required properties
2. **Lazy Loading:** Consider lazy loading for nested objects in detailed views
3. **Caching:** Cache frequently accessed case lists with proper invalidation
4. **Indexing:** Ensure database indexes on frequently queried fields (Status, Stage, etc.)

## Testing

### Unit Test Examples
```csharp
[TestClass]
public class CaseBasicInfoDtoTests
{
    [TestMethod]
    public void CaseBasicInfoDto_ShouldMapFromEntity()
    {
        var caseEntity = new UserCase 
        { 
            Id = Guid.NewGuid(),
            CaseNo = "001",
            CaseYear = 2023
        };

        var dto = _mapper.Map<CaseBasicInfoDto>(caseEntity);

        Assert.AreEqual(caseEntity.Id, dto.Id);
        Assert.AreEqual("001-2023", dto.CaseNumberYear);
    }
}
```

## Future Enhancements

1. **Add validation:** Create FluentValidation validators for DTOs
2. **Add versioning:** Support multiple DTO versions if API versioning is needed
3. **Add localization:** Support multiple languages for case types, statuses, etc.
4. **Add caching:** Implement caching for frequently used constants
5. **Add filtering:** Create advanced filtering DTO for complex queries

## Questions & Support

For questions or issues with this DTO structure:
1. Refer to the usage examples above
2. Check the AutoMapper profile for mapping configurations
3. Review the constants file for available enum values
4. Consult the XML documentation in each DTO class
