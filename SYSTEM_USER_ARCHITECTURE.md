# SystemUser & ApplicationUser Relationship

## Database Schema

```
┌─────────────────────────────────┐
│      ApplicationUser            │  (Identity table)
├─────────────────────────────────┤
│ Id (PK) [string]                │
│ Email [string, unique]          │
│ UserName [string, unique]       │
│ PhoneNumber [string]            │
│ UserType [RegisterType]         │
│ FirstName [string?]             │
│ LastName [string?]              │
│ CompanyName [string?]           │
│ EnrollmentNumber [string?]      │
│ RegistrationNumber [string?]    │
│ IsActive [bool]                 │
│ ... (other Identity fields)     │
└─────────────────────────────────┘
         ▲                ▲
         │                │
         │  UserId=FK     │
         │                │
         └────────────────┘
                 │
                 │ (One-to-One)
                 │
         ┌───────▼──────────────────────────┐
         │      SystemUser                  │  (Subscription tracking)
         ├───────────────────────────────────┤
         │ Id (PK) [Guid]                    │
         │ UserId (FK) [string, unique]      │
         │ UserType [RegisterType]           │
         │ FirstName [string]                │
         │ LastName [string]                 │
         │ Email [string, indexed]           │
         │ CompanyName [string?]             │
         ├──────────────────────────────────┤
         │ Subscription Details:             │
         │ - Subscription [SubscriptionType] │
         │ - SubscriptionExpiryDate [DateTime?]
         │ - SubscriptionStartDate [DateTime?]
         ├──────────────────────────────────┤
         │ Status Tracking:                  │
         │ - Status [UserAccountStatus]      │
         │ - StatusReason [string?]          │
         │ - RegisteredDate [DateTime]       │
         │ - LastLoginDate [DateTime?]       │
         │ - IsEmailVerified [bool]          │
         ├──────────────────────────────────┤
         │ Audit Fields:                     │
         │ - CreatedAt [DateTime]            │
         │ - UpdatedAt [DateTime?]           │
         └───────────────────────────────────┘
```

## Data Flow During Registration

```
┌──────────────────┐
│   User Registers │
│ (Lawyer/Corporate)
└────────┬─────────┘
         │
         ▼
┌────────────────────────────────────┐
│ 1. Create ApplicationUser          │
│    - UserName = Email              │
│    - Email (unique)                │
│    - Password (hashed)             │
│    - UserType (Lawyer/Corporate)   │
│    - FirstName/LastName/CompanyName│
│    - IsActive = false              │
│    └─ Creates user in Auth table   │
└────────┬───────────────────────────┘
         │
         ▼
┌────────────────────────────────────┐
│ 2. Assign Role (Lawyer/Corporate)  │
│    - Add to role via UserManager   │
└────────┬───────────────────────────┘
         │
         ▼
┌────────────────────────────────────────────┐
│ 3. Create SystemUser Record                │
│    - UserId = ApplicationUser.Id (FK)      │
│    - UserType = Request.UserType           │
│    - Email = ApplicationUser.Email         │
│    - FirstName/LastName/CompanyName        │
│    - Subscription = FREE                   │
│    - SubscriptionExpiryDate = Now + 1 month
│    - SubscriptionStartDate = Now           │
│    - Status = Pending (awaiting approval)  │
│    - RegisteredDate = Now                  │
│    └─ Creates record in SystemUsers table  │
└────────┬───────────────────────────────────┘
         │
         ▼
┌────────────────────────────────────────┐
│ 4. Send Registration Email             │
│    - Subject: Free plan access for 1mo │
│    - Body: Includes expiry date         │
│    - To: ApplicationUser.Email          │
│    └─ Fire-and-forget (won't fail reg) │
└────────┬───────────────────────────────┘
         │
         ▼
┌────────────────────────────┐
│ 5. Return ApplicationUser.Id│
│    Registration Complete!   │
└─────────────────────────────┘
```

## Subscription Status Timeline

```
Registration          Expiry Date (1 month)    After Expiry
     │                        │                     │
     ▼                        ▼                     ▼
┌──────────────────┐  ┌──────────────────┐  ┌──────────────────┐
│ Status: Pending  │  │ Status: Active   │  │ Status: Inactive │
│ Sub: Free        │  │ Sub: Free        │  │ Sub: Free        │
│ Days Left: 30    │  │ Days Left: 0     │  │ Days Left: -X    │
│                  │  │ ⚠️  Last day!    │  │ 🔒 Account Locked│
│ ✉️  Email sent   │  │ ⚠️  Renewal?     │  │ 🚫 No access     │
└──────────────────┘  └──────────────────┘  └──────────────────┘
     │
     └─ Admin approves
        Status → Active
```

## Querying SystemUsers

```csharp
// Get all registered users waiting for approval
var pendingUsers = await _systemUserService.GetAllRegisteredUsersAsync(
    userType: null,
    status: UserAccountStatus.Pending,
    pageNumber: 1,
    pageSize: 10
);

// Get all Lawyers
var lawyers = await _systemUserService.GetAllRegisteredUsersAsync(
    userType: RegisterType.Lawyer,
    status: null
);

// Get active Corporates
var activeCorporates = await _systemUserService.GetAllRegisteredUsersAsync(
    userType: RegisterType.Corporate,
    status: UserAccountStatus.Active
);

// Get specific user by ApplicationUser.Id
var userDetails = await _systemUserService.GetSystemUserByIdAsync("applicationUserId");

// Count users by type
var counts = await _systemUserService.GetUserCountByTypeAsync();

// Count active users
var activeCount = await _systemUserService.GetActiveUsersCountAsync();
```

## Updating Subscription

```csharp
// Admin upgrades user to Premium plan
var updateRequest = new UpdateSystemUserRequest
{
    Subscription = SubscriptionType.Monthly, // or Yearly
    Status = UserAccountStatus.Active,
    StatusReason = "Manual upgrade from Free to Premium"
};

bool success = await _systemUserService.UpdateSystemUserAsync(
    userId: "applicationUserId",
    request: updateRequest
);
```

## Scheduled Job for Expiry (Recommended Implementation)

```csharp
// Run daily or hourly
public async Task ProcessSubscriptionExpiryAsync()
{
    var now = DateTime.UtcNow;

    // Find users with expired free plan
    var expiredUsers = await _identityContext.SystemUsers
        .Where(x => 
            x.SubscriptionExpiryDate <= now &&
            x.Subscription == SubscriptionType.Free &&
            x.Status != UserAccountStatus.Inactive
        )
        .ToListAsync();

    foreach (var systemUser in expiredUsers)
    {
        // Lock the account
        systemUser.Status = UserAccountStatus.Inactive;
        systemUser.StatusReason = "Free plan expired - no paid subscription";
        systemUser.UpdatedAt = DateTime.UtcNow;

        // Also lock in ApplicationUser
        var appUser = await _userManager.FindByIdAsync(systemUser.UserId);
        if (appUser != null)
        {
            appUser.IsActive = false;
            await _userManager.UpdateAsync(appUser);

            // Optionally: Lock the account
            await _userManager.SetLockoutEnabledAsync(appUser, true);
            await _userManager.SetLockoutEndDateAsync(appUser, DateTimeOffset.MaxValue);
        }

        // Send expiry notification email
        await SendExpiryNotificationEmail(systemUser, appUser);
    }

    await _identityContext.SaveChangesAsync();
}
```

## Benefits of This Structure

✅ **Separation of Concerns**
- ApplicationUser: Identity & Authentication
- SystemUser: Subscription & Billing

✅ **Easy Querying**
- Get all pending registrations
- Get expiring subscriptions
- Filter by status or type

✅ **Audit Trail**
- When was user registered? (RegisteredDate)
- When did they last login? (LastLoginDate)
- When did they subscribe? (SubscriptionStartDate)
- When does it expire? (SubscriptionExpiryDate)

✅ **Flexibility**
- Users can have multiple subscriptions over time
- Easy to add billing history tables later
- Payment records can link to SystemUser

✅ **Performance**
- Indexed on UserId, Email, Status, Type, RegisteredDate
- Fast filtering and pagination
- No N+1 queries needed
