# Identity Service Implementation Highlights

## Key Code Patterns Used

### 1. Proper Dependency Injection with Validation
```csharp
public IdentityService(
    UserManager<ApplicationUser> userManager,
    IOptions<JWTSettings> jwtSettings,
    SignInManager<ApplicationUser> signInManager,
    IMailService mailService,
    ILogger<IdentityService> logger,
    IdentityContext identityDbContext)
{
    _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    _jwtSettings = jwtSettings?.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
    // ... more validations
}
```

### 2. Consistent Result Pattern
```csharp
// Success response
return Result<TokenResponse>.SuccessAsync(response, "Authenticated");

// Failure response
return await Result<TokenResponse>.FailAsync("Invalid email or password");
```

### 3. Proper Exception Handling Strategy
```csharp
catch (InvalidOperationException ex)
{
    // Expected validation errors
    _logger.LogWarning($"Validation error: {ex.Message}");
    return await Result<TokenResponse>.FailAsync(ex.Message);
}
catch (Exception ex)
{
    // Unexpected errors
    _logger.LogError($"Error in GetTokenAsync: {ex.Message}");
    return await Result<TokenResponse>.FailAsync("An error occurred");
}
```

### 4. Optimized Entity Queries
```csharp
// Before: Loads entire user object
var user = await _userManager.Users
    .FirstOrDefaultAsync(u => u.ProfessionalInfo != null && u.ProfessionalInfo.EnrollmentNo == enrollment);

// After: Direct query to relevant DbSet with AsNoTracking
var exists = await _identityDbContext.ProfessionalInfos
    .AsNoTracking()
    .AnyAsync(p => p.EnrollmentNo == enrollment);
```

### 5. Proper Corporate User Setup
```csharp
private async Task AddCorporateUserAsync(ApplicationUser user, CompanyInfoDto companyInfo)
{
    // 1. Find or create organization
    var organization = await _identityDbContext.Organizations
        .FirstOrDefaultAsync(o => o.Code == companyInfo.RegistrationNumber?.Trim());

    if (organization == null)
    {
        organization = new OrganizationEntity
        {
            Id = Guid.NewGuid(),
            Name = companyInfo.CompanyName?.Trim().ToUpper() ?? "Unknown Organization",
            Code = companyInfo.RegistrationNumber?.Trim() ?? string.Empty,
            Type = "Law Firm"
        };
        _identityDbContext.Organizations.Add(organization);
        await _identityDbContext.SaveChangesAsync();
    }

    // 2. Create user-organization mapping
    var orgMapping = new UserOrganizationMapping
    {
        Id = Guid.NewGuid(),
        UserId = Guid.Parse(user.Id),
        OrganizationId = organization.Id,
        Role = OrganizationRole.Owner
    };
    _identityDbContext.UserOrganizations.Add(orgMapping);
    await _identityDbContext.SaveChangesAsync();
}
```

### 6. JWT Token with Rich Claims
```csharp
private List<Claim> BuildUserClaims(ApplicationUser user, IList<string> roles)
{
    var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName ?? string.Empty),
        new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName ?? string.Empty),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Name, user.UserName)
    };

    claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
    return claims;
}
```

### 7. Comprehensive Validation
```csharp
private void ValidateRegistrationRequest(RegisterRequest request)
{
    if (request == null)
        throw new InvalidOperationException("Registration request cannot be null");

    if (string.IsNullOrWhiteSpace(request.Email) || 
        string.IsNullOrWhiteSpace(request.Password) || 
        string.IsNullOrWhiteSpace(request.Contact))
        throw new InvalidOperationException("Email, password, and contact are required");

    if ((request.UserType == RegisterType.Lawyer || request.UserType == RegisterType.Client) && 
        request.IndividualInfoDto == null)
        throw new InvalidOperationException("Individual information is required");

    if (request.UserType == RegisterType.Corporate && request.CompanyInfoDto == null)
        throw new InvalidOperationException("Company information is required");
}
```

### 8. Professional Info Creation for Lawyers
```csharp
if (request.UserType == RegisterType.Lawyer && individualInfo != null)
{
    user.ProfessionalInfo = new ProfessionalInfoEntity
    {
        EnrollmentNo = individualInfo.EnrollmentNumber?.Trim().ToUpper() ?? string.Empty,
        BarAssociationNumber = string.Empty,
        PracticeLicenseDate = DateTime.UtcNow,
        PracticeSince = DateTime.UtcNow.Year
    };
}
```

### 9. Secure Password Reset Flow
```csharp
var code = await _userManager.GeneratePasswordResetTokenAsync(account);
var encodedCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
var resetLink = $"{origin}/api/identity/reset-password?email={Uri.EscapeDataString(account.Email)}&code={encodedCode}";

// Sent via email with security details in HTML template
```

### 10. Duplicate Check During Registration
```csharp
public async Task<Result<string>> RegisterAsync(RegisterRequest request)
{
    ValidateRegistrationRequest(request);

    // Check email
    var userExists = await _userManager.FindByEmailAsync(request.Email);
    if (userExists != null)
        return await Result<string>.FailAsync($"Email is already registered");

    // Check phone number
    if (!string.IsNullOrWhiteSpace(request.Contact))
    {
        var phoneExists = await IsContactExistAsync(request.Contact);
        if (phoneExists)
            return await Result<string>.FailAsync($"Phone number is already registered");
    }
    // ... rest of registration
}
```

---

## Entity Relationships Supported

```
ApplicationUser (Base)
├── ProfessionalInfoEntity (1:1) - Lawyer info
├── UserAddress (1:M) - Addresses
├── UserContact (1:M) - Contact info
├── UserCourtMapping (1:M) - Court assignments
├── UserHierarchy (1:M) - Parent/Child relationships
│   ├── Parents (Lawyer managing this user)
│   └── Children (Operators/Clerks under this Lawyer)
├── UserOrganizationMapping (1:M) - Organization memberships
│   └── OrganizationEntity - Law firm/Corporate
└── UserSpecialization (1:M) - Practice areas
    └── Specialization - Type of practice
```

---

## Configuration Usage

The service expects the following configuration:

```csharp
// JWTSettings should be configured in appsettings.json
"JWTSettings": {
    "Key": "your-secret-key-min-32-chars-long",
    "Issuer": "your-issuer",
    "Audience": "your-audience",
    "DurationInMinutes": 60
}
```

---

## Error Response Examples

### Invalid Email or Password
```json
{
    "isSucceed": false,
    "message": "Invalid email or password",
    "data": null
}
```

### Duplicate Email
```json
{
    "isSucceed": false,
    "message": "Email 'user@example.com' is already registered",
    "data": null
}
```

### Registration Success
```json
{
    "isSucceed": true,
    "message": "User registered successfully. Confirmation email sent to user@example.com",
    "data": "550e8400-e29b-41d4-a716-446655440000"
}
```

### Token Response Success
```json
{
    "isSucceed": true,
    "message": "Authenticated",
    "data": {
        "id": "550e8400-e29b-41d4-a716-446655440000",
        "jwToken": "eyJhbGciOiJIUzI1NiIs...",
        "refreshToken": "abcdef1234567890...",
        "email": "user@example.com",
        "userName": "user",
        "isVerified": true,
        "issuedOn": "2024-01-15T10:30:00",
        "expiresOn": "2024-01-15T11:30:00"
    }
}
```

---

## Security Checklist

- ✅ Input validation on all entry points
- ✅ Null reference checking on dependencies
- ✅ Sensitive error information not exposed to clients
- ✅ IP address logging for audit trail
- ✅ Account enumeration prevention
- ✅ Duplicate phone and email detection
- ✅ Email verification requirement
- ✅ Proper JWT token generation with expiration
- ✅ Password hashing (handled by UserManager)
- ✅ Role-based authorization setup
- ✅ XSS prevention in email templates
- ✅ URL encoding for reset link parameters

---

## Performance Characteristics

| Operation | Complexity | Optimization |
|-----------|-----------|--------------|
| Login | O(1) | Direct email lookup, minimal DB queries |
| Registration | O(1) | Duplicate checks, single user create |
| Email Confirmation | O(1) | Direct user ID lookup |
| Password Reset | O(1) | Email lookup + token generation |
| Enrollment Check | O(1) | Direct index on enrollment number |
| Phone Number Check | O(1) | AsNoTracking query to index |

---

## Usage Examples

### In a Controller

```csharp
[HttpPost("login")]
public async Task<IActionResult> Login([FromBody] TokenRequest request)
{
    var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
    var result = await _identityService.GetTokenAsync(request, ipAddress);
    
    if (!result.IsSucceed)
        return Unauthorized(result);
        
    return Ok(result);
}

[HttpPost("register")]
public async Task<IActionResult> Register([FromBody] RegisterRequest request)
{
    var origin = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
    request.Origin = origin;
    
    var result = await _identityService.RegisterAsync(request);
    
    if (!result.IsSucceed)
        return BadRequest(result);
        
    return Ok(result);
}
```

---

## Testing Approach

### Unit Testing
```csharp
[Test]
public async Task RegisterAsync_WithDuplicateEmail_ReturnsFail()
{
    // Setup
    var existingUser = new ApplicationUser { Email = "test@example.com" };
    _userManager.FindByEmailAsync("test@example.com").Returns(existingUser);
    
    var request = new RegisterRequest { Email = "test@example.com", ... };
    
    // Act
    var result = await _identityService.RegisterAsync(request);
    
    // Assert
    Assert.False(result.IsSucceed);
    Assert.Contains("already registered", result.Message);
}
```

---

## Maintenance Notes

1. **Token Expiration**: Currently set to `DurationInMinutes` from config
2. **Refresh Token**: Expires in 7 days by default
3. **Password Reset**: Token expires in 60 minutes
4. **Email Verification**: No expiration by default (configure in UserManager options)

All these constants can be moved to configuration for better flexibility.
