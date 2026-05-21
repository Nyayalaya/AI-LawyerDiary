# SystemUser Entity Implementation - Summary

## Overview
Created a separate `SystemUser` entity to store subscription and registration details, linked to `ApplicationUser` via `UserId`.

## Changes Made

### 1. **New SystemUser Entity** ✅
**File**: `CourtApp.Domain/Entities/SystemUser.cs`
- Guid Id (primary key)
- string UserId (foreign key to ApplicationUser)
- Registration Type (Lawyer, Corporate)
- User Information (FirstName, LastName, Email, CompanyName)
- **Subscription Details**:
  - Subscription: SubscriptionType (Free, Trial, Premium, Annual)
  - SubscriptionExpiryDate: DateTime?
  - SubscriptionStartDate: DateTime?
- **Account Status**:
  - Status: UserAccountStatus (Pending, Active, Inactive, Locked)
  - StatusReason: string?
- **Tracking**:
  - RegisteredDate: DateTime
  - LastLoginDate: DateTime?
  - IsEmailVerified: bool
  - CreatedAt/UpdatedAt: DateTime

### 2. **Updated IdentityContext** ✅
**File**: `CourtApp.Infrastructure/DbContexts/IdentityContext.cs`
- Added using statement: `using CourtApp.Domain.Entities;`
- Configured SystemUser entity with:
  - Unique index on UserId (one-to-one relationship with ApplicationUser)
  - Indexes on Email, UserType, Status, Subscription, RegisteredDate
  - Property length constraints
  - NO navigation property (UserId is FK only)

### 3. **Updated ApplicationUser Model** ✅
**File**: `CourtApp.Infrastructure/Identity/Models/ApplicationUser.cs`
- Cleaned up: removed subscription fields
- ApplicationUser now focuses on Identity/Auth only
- Subscription details managed via separate SystemUser entity

### 4. **Updated SystemUserService** ✅
**File**: `CourtApp.Infrastructure/Identity/Services/SystemUserService.cs`
- Now queries `_identityContext.SystemUsers` (not ApplicationUser)
- GetAllRegisteredUsersAsync: filters by UserType, Status, search terms
- GetSystemUserByIdAsync: finds by UserId
- UpdateSystemUserAsync: updates subscription and status
- Maps SystemUser → SystemUserResponse DTO

### 5. **Updated RegistrationService** ✅
**File**: `CourtApp.Infrastructure/Identity/Services/RegistrationService.cs`
- Now accepts IdentityContext in constructor
- New method: `CreateSystemUserRecordAsync()` - creates SystemUser record on registration
- Sets free plan automatically:
  - Subscription = Free
  - SubscriptionExpiryDate = UtcNow.AddMonths(1)
  - Status = Pending (awaiting admin approval)
  - StatusReason = "New registration - Free plan enabled for 1 month"
- Sends email template with expiry date to user

### 6. **SystemUserResponse DTO** ✅
**File**: `CourtApp.Application/Features/SystemUsers/DTOs/SystemUserResponse.cs`
- Id: string (ApplicationUser.Id)
- UserType: RegisterType
- FirstName, LastName: string
- Email: string
- RegisteredDate: DateTime
- Subscription: SubscriptionType
- SubscriptionExpiryDate: DateTime?
- Status: UserAccountStatus
- LastLoginDate: DateTime?
- StatusReason: string?

### 7. **Cleaned Up Files** ✅
- Removed old `CourtApp.Infrastructure/Identity/Models/SystemUser.cs`
- Resolved ambiguous reference errors

## Database Migration Required

You need to run the following command to create and apply the migration:

```powershell
cd "D:\EnterpriseApps\DairyApps\LawyerDiary- AI\Service"

# Add migration
dotnet ef migrations add SystemUserEntity -p CourtApp.Infrastructure -s CourtApp.Api

# Apply migration
dotnet ef database update -p CourtApp.Infrastructure -s CourtApp.Api
```

## Registration Flow

1. User registers (Lawyer or Corporate)
2. **ApplicationUser** created with:
   - Email, Password, UserType
   - FirstName/LastName (for Lawyer) or CompanyName (for Corporate)
   - IsActive = false (pending admin approval)
3. **SystemUser** record created with:
   - UserId = ApplicationUser.Id
   - Subscription = Free
   - SubscriptionExpiryDate = UtcNow + 1 month
   - Status = Pending
   - All registration info duplicated for easy querying
4. **Email sent** with free plan info and expiry date
5. Admin reviews and approves/rejects via SystemUsers API

## API Endpoints (SystemUsersController)

- **GET /api/system-users** - List all registered users (with pagination, filters)
- **GET /api/system-users/{id}** - Get specific user details
- **PUT /api/system-users/{id}** - Update subscription/status

## Next Steps

1. ✅ Run migrations (command above)
2. 📋 Implement background job to lock accounts after free plan expires
3. 📧 Send reminder emails (7 days, 3 days, 1 day before expiry)
4. 🔐 Add subscription upgrade flow
5. 🧪 Test end-to-end registration → free plan → expiry → lockout

## Key Benefits

✅ Separation of concerns: Auth (ApplicationUser) vs Subscription (SystemUser)
✅ Easy tracking of all registered users who need approval
✅ Centralized subscription management
✅ Automatic free plan on registration
✅ Audit trail with CreatedAt/UpdatedAt
✅ All data together for quick queries and filtering
