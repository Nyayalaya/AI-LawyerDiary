# SystemUser Feature - Visual Diagrams

## 1. Registration Flow Diagram

```
┌─────────────────────────────────────┐
│   User Submits Registration         │
│   - Email, Password, UserType       │
│   - Name/Company Info               │
│   - Phone Number                    │
└──────────┬──────────────────────────┘
           │
           ▼
┌─────────────────────────────────────┐
│   Validation & Normalization        │
│   - Email validation                │
│   - Password strength check         │
│   - Phone formatting                │
└──────────┬──────────────────────────┘
           │
           ▼
┌─────────────────────────────────────┐
│   Create ApplicationUser            │
│   (Identity table)                  │
│   ✓ Id = New GUID                   │
│   ✓ Email (unique)                  │
│   ✓ Password (hashed)               │
│   ✓ UserType                        │
│   ✓ FirstName/LastName/CompanyName  │
└──────────┬──────────────────────────┘
           │
           ▼
┌─────────────────────────────────────┐
│   Assign Role                       │
│   ✓ Lawyer or Corporate             │
└──────────┬──────────────────────────┘
           │
           ▼
┌─────────────────────────────────────┐
│   Create SystemUser Record          │
│   (Subscription table)              │
│   ✓ Id = New GUID                   │
│   ✓ UserId = ApplicationUser.Id (FK)│
│   ✓ Subscription = FREE             │
│   ✓ SubExpiryDate = Now + 1 month   │
│   ✓ Status = Pending                │
│   ✓ RegisteredDate = Now            │
└──────────┬──────────────────────────┘
           │
           ▼
┌─────────────────────────────────────┐
│   Send Registration Email           │
│   - Subject: Free plan access       │
│   - Body: Includes expiry date      │
│   - Fire & forget (async)           │
└──────────┬──────────────────────────┘
           │
           ▼
┌─────────────────────────────────────┐
│   Return Success                    │
│   ✓ User created                    │
│   ✓ Email sent                      │
│   ✓ Awaiting admin approval         │
└─────────────────────────────────────┘
```

## 2. Admin Approval Workflow

```
┌──────────────────────────────┐
│  Pending Registrations       │
│  (Status = Pending)          │
│  - 20 new registrations      │
│  - 15 Lawyers                │
│  - 5 Corporates              │
└──────────┬───────────────────┘
           │
           ▼ Admin Reviews
        ╔══════╗
        ║      ║
   ┌────▼─┐  ┌─▼────┐
   │      │  │      │
   ▼      ▼  ▼      ▼
┌────────────┐ ┌──────────────┐
│ APPROVED   │ │ REJECTED     │
├────────────┤ ├──────────────┤
│ Status → 1 │ │ Status → 2   │
│ (Active)   │ │ (Inactive)   │
│            │ │              │
│ User gains │ │ Registration │
│ full access│ │ is denied    │
└────────────┘ └──────────────┘
   │                  │
   └────────┬─────────┘
            │
            ▼
    Account Updated
    (SystemUser record)
```

## 3. Subscription Lifecycle

```
DAY 0 (Registration)
├─ Status: Pending
├─ Subscription: Free
└─ Email: Welcome + Free plan info
   │
   ▼ (Admin approves)
DAY 0-1 (After Approval)
├─ Status: Active
├─ Subscription: Free (still)
└─ Can use all features
   │
   ├─ ✓ Option 1: Continue on free plan
   │  └─ User enjoys features for 1 month
   │
   └─ ✓ Option 2: Upgrade to paid
      └─ Status: Active
         Subscription: Monthly/Yearly
         SubExpiryDate: Extended
   │
   ▼ (Days 1-29)
DAY 1-29
├─ Status: Active
├─ Subscription: Free or Paid
├─ Full feature access
└─ Usage continues normally
   │
   ▼ (Day 23 - Email reminder)
DAY 23
├─ Status: Active
├─ Subscription: Free
├─ Days Left: 7
└─ Email: "Expires in 7 days"
   │
   ▼ (Day 27 - Email reminder)
DAY 27
├─ Status: Active
├─ Subscription: Free
├─ Days Left: 3
└─ Email: "Expires in 3 days"
   │
   ▼ (Day 29 - Email reminder)
DAY 29
├─ Status: Active
├─ Subscription: Free
├─ Days Left: 1
└─ Email: "Expires tomorrow"
   │
   ├─ ✓ Option 1: Upgrade before expiry
   │  └─ Continue with paid plan
   │
   └─ ✗ Option 2: Don't upgrade
      │
      ▼ (Day 30)
      DAY 30 (Expiry)
      ├─ Background job checks expiry
      ├─ Status: Inactive (locked)
      ├─ Subscription: Free (expired)
      └─ Email: "Account locked"
         │
         ▼
         USER CANNOT LOGIN
         └─ Account is locked
```

## 4. Database Schema Relationships

```
┌────────────────────────────────────────┐
│          IDENTITY SCHEMA               │
├────────────────────────────────────────┤
│                                        │
│  ┌──────────────────────────────────┐  │
│  │    ApplicationUser (Users)       │  │
│  ├──────────────────────────────────┤  │
│  │ PK: Id (string, GUID)            │  │
│  │ UK: Email (unique)               │  │
│  │ UK: UserName (unique)            │  │
│  │ ├─ PhoneNumber                   │  │
│  │ ├─ UserType [0=Lawyer, 1=Corp]  │  │
│  │ ├─ FirstName / LastName          │  │
│  │ ├─ CompanyName                   │  │
│  │ ├─ IsActive [bool]               │  │
│  │ └─ ... (other identity fields)   │  │
│  │                                  │  │
│  │ Roles: Lawyer, Corporate         │  │
│  │ Claims: Permissions              │  │
│  └──────┬───────────────────────────┘  │
│         │                              │
│         │ (1 : 0..1)                   │
│         │ FK: UserId                   │
│         │                              │
│  ┌──────▼───────────────────────────┐  │
│  │      SystemUser (Subscriptions)  │  │
│  ├──────────────────────────────────┤  │
│  │ PK: Id (Guid)                    │  │
│  │ FK: UserId (string, unique) ────►│  │
│  │ ├─ UserType [0=Lawyer, 1=Corp] ◄─┘  │
│  │ ├─ FirstName / LastName          │  │
│  │ ├─ Email (indexed)               │  │
│  │ ├─ CompanyName                   │  │
│  │ ├─ Subscription [0=Trial,        │  │
│  │ │   1=Free, 2=Monthly, 3=Yearly] │  │
│  │ ├─ SubscriptionExpiryDate        │  │
│  │ ├─ SubscriptionStartDate         │  │
│  │ ├─ Status [0=Pending,            │  │
│  │ │  1=Active, 2=Inactive, 3=Locked]  │
│  │ ├─ StatusReason                  │  │
│  │ ├─ RegisteredDate (indexed)      │  │
│  │ ├─ LastLoginDate                 │  │
│  │ ├─ IsEmailVerified               │  │
│  │ ├─ CreatedAt (audit)             │  │
│  │ └─ UpdatedAt (audit)             │  │
│  └──────────────────────────────────┘  │
│                                        │
│  Indexes:                              │
│  ├─ UserId (unique, FK)                │
│  ├─ Email (for search)                 │
│  ├─ UserType (for filtering)           │
│  ├─ Status (for querying state)        │
│  ├─ Subscription (for reports)         │
│  └─ RegisteredDate (for sorting)       │
│                                        │
└────────────────────────────────────────┘
```

## 5. API Call Flow

```
Client Request
│
├─ POST /api/auth/register
│  └─ Create ApplicationUser + SystemUser
│     └─ Return UserId
│
├─ GET /api/system-users
│  └─ List registered users
│     └─ Paginated + Filtered results
│
├─ GET /api/system-users/{id}
│  └─ Get specific user details
│     └─ Complete subscription info
│
└─ PUT /api/system-users/{id}
   └─ Update subscription/status
      └─ Audit trail recorded

Layers:
│
├─ API Controller (CourtApp.Api)
│  └─ Validates input
│     └─ Calls Mediator
│
├─ CQRS Handler (CourtApp.Application)
│  └─ Business logic
│     └─ Calls ISystemUserService
│
├─ Service Interface (CourtApp.Application)
│  └─ Abstraction layer
│     └─ Dependency injection
│
├─ Service Implementation (CourtApp.Infrastructure)
│  └─ Database queries
│     └─ EF Core operations
│
└─ Database (PostgreSQL/SQL Server)
   └─ Persistence
      └─ Transactions
```

## 6. Free Plan Expiry Processing

```
                 Automatic (Background Job)
                        │
                        ▼ Runs every 1 hour
        ┌───────────────────────────────────┐
        │ Check All SystemUsers             │
        │ WHERE SubscriptionExpiryDate ≤ NOW
        │ AND Subscription = 'Free'         │
        │ AND Status = 'Active'             │
        └───────────┬───────────────────────┘
                    │
              Found: 5 users
                    │
        ┌───────────▼───────────┐
        │   For Each User:      │
        ├───────────────────────┤
        │ 1. Update SystemUser  │
        │    Status → Inactive  │
        │ 2. Update AppUser     │
        │    IsActive → false   │
        │ 3. Lock Account       │
        │    LockoutEnd → Max   │
        │ 4. Send Email         │
        │    "Account Locked"   │
        │ 5. Log Event          │
        │    Audit trail        │
        └───────────┬───────────┘
                    │
        ┌───────────▼───────────┐
        │ User Receives:        │
        ├───────────────────────┤
        │ 1. Email notification │
        │ 2. Cannot login       │
        │ 3. Account shows      │
        │    as locked          │
        │ 4. Can renew via API  │
        │    or admin           │
        └───────────────────────┘
```

## 7. Status Transition Diagram

```
               ┌─────────────────────┐
               │ New Registration    │
               │ (System creates)    │
               └──────────┬──────────┘
                          │
                          ▼
               ┌─────────────────────┐
       ┌─────►│ PENDING (0)         │◄─────┐
       │      │ Awaiting approval   │      │
       │      └──────────┬──────────┘      │
       │                 │                  │
       │    Admin Review │                  │
       │                 │                  │
       │        ┌────────┴────────┐        │
       │        │                 │        │
       │        ▼                 ▼        │
       │  ┌──────────────┐  ┌──────────────┐
       │  │ ACTIVE (1)   │  │INACTIVE (2)  │
       │  │ - Can use    │  │ - Denied     │
       │  │ - Full perms │  │              │
       │  └──────┬───────┘  └──────────────┘
       │         │                │
       │         │                │ (Rejection)
       │         │                │
       │      Expiry or            └────────►
       │      Manual Lock
       │         │
       │         ▼
       │  ┌──────────────┐
       │  │ LOCKED (3)   │
       │  │ - Cannot use │
       │  │ - Suspended  │
       │  └──────┬───────┘
       │         │
       │    Reactivate │
       │    by Admin  │
       │         │
       └─────────┘
```

## 8. Component Interaction

```
┌─────────────────────────────────────────────────────────┐
│                  API Layer                              │
│  ┌───────────────────────────────────────────────────┐  │
│  │  SystemUsersController                           │  │
│  │  - GetAll() - GetById() - Update()               │  │
│  └─────────────────┬─────────────────────────────────┘  │
└────────────────────┼──────────────────────────────────────┘
                     │
                     ▼
┌─────────────────────────────────────────────────────────┐
│              Application Layer                          │
│  ┌───────────────────────────────────────────────────┐  │
│  │  CQRS Handlers                                    │  │
│  │  - GetSystemUsersQueryHandler                    │  │
│  │  - GetSystemUserByIdQueryHandler                 │  │
│  │  - UpdateSystemUserCommandHandler                │  │
│  └─────────────────┬─────────────────────────────────┘  │
│                    │                                     │
│  ┌────────────────┴────────────────────────────────┐   │
│  │  ISystemUserService (Interface)                │   │
│  │  - GetAllRegisteredUsersAsync()                │   │
│  │  - GetSystemUserByIdAsync()                    │   │
│  │  - UpdateSystemUserAsync()                     │   │
│  └─────────────────┬─────────────────────────────┘    │
└────────────────────┼──────────────────────────────────────┘
                     │
                     ▼
┌─────────────────────────────────────────────────────────┐
│           Infrastructure Layer                          │
│  ┌───────────────────────────────────────────────────┐  │
│  │  SystemUserService (Implementation)              │  │
│  │  + IdentityContext                               │  │
│  │  + UserManager<ApplicationUser>                  │  │
│  └─────────────────┬─────────────────────────────────┘  │
│                    │                                     │
│  ┌────────────────┴────────────────────────────────┐   │
│  │  RegistrationService                            │   │
│  │  + MailService                                  │   │
│  │  + Creates SystemUser on register               │   │
│  └─────────────────┬─────────────────────────────┘    │
└────────────────────┼──────────────────────────────────────┘
                     │
                     ▼
┌─────────────────────────────────────────────────────────┐
│           Data Access Layer                             │
│  ┌───────────────────────────────────────────────────┐  │
│  │  IdentityContext (DbContext)                     │  │
│  │  - DbSet<ApplicationUser>                        │  │
│  │  - DbSet<SystemUser>                            │  │
│  │  - DbSet<IdentityRole>                          │  │
│  │  - ... other tables                             │  │
│  └─────────────────┬─────────────────────────────────┘  │
└────────────────────┼──────────────────────────────────────┘
                     │
                     ▼
┌─────────────────────────────────────────────────────────┐
│         Database Layer                                  │
│  ┌───────────────────────────────────────────────────┐  │
│  │  PostgreSQL / SQL Server                         │  │
│  │  - Users (ApplicationUser)                       │  │
│  │  - SystemUsers (Subscriptions)                   │  │
│  │  - Roles, RoleClaims, UserClaims                 │  │
│  │  - ... other Identity tables                     │  │
│  └───────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────┘
```

---

**Legend:**
- ►─► = Data flow
- ├─┤ = Structure
- │ = Hierarchy
- ▼ = Process step
- ✓ = Success
- ✗ = Failure
