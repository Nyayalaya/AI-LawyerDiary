# RoleManager Feature - Folder Structure

```
D:\EnterpriseApps\DairyApps\LawyerDiary- AI\Service\
│
├── CourtApp.Application/
│   └── Features/
│       └── RoleManager/                          [NEW FOLDER]
│           ├── Commands/                         [NEW FOLDER]
│           │   ├── CreateRoleCommand.cs
│           │   ├── UpdateRoleCommand.cs
│           │   ├── DeleteRoleCommand.cs
│           │   └── UpdateRolePermissionsCommand.cs
│           │
│           ├── Queries/                          [NEW FOLDER]
│           │   ├── GetRoleQuery.cs
│           │   ├── GetRoleByIdQuery.cs
│           │   └── GetRolePermissionsQuery.cs
│           │
│           ├── Handlers/                         [NEW FOLDER]
│           │   ├── CreateRoleCommandHandler.cs
│           │   ├── UpdateRoleCommandHandler.cs
│           │   ├── DeleteRoleCommandHandler.cs
│           │   ├── UpdateRolePermissionsCommandHandler.cs
│           │   ├── GetRoleQueryHandler.cs
│           │   ├── GetRoleByIdQueryHandler.cs
│           │   └── GetRolePermissionsQueryHandler.cs
│           │
│           └── Validators/                       [NEW FOLDER]
│               ├── CreateRoleValidator.cs
│               ├── UpdateRoleValidator.cs
│               └── UpdateRolePermissionsValidator.cs
│
├── CourtApp.Api/
│   ├── Controllers/
│   │   ├── RoleManagerController.cs              [NEW]
│   │   ├── CadreController.cs
│   │   └── ... (other controllers)
│   │
│   ├── Postman/
│   │   └── RoleManager-API.postman_collection.json [NEW]
│   │
│   └── RoleManager-README.md                     [NEW]
│
├── Program.cs                                    [UNCHANGED - auto-registers via AddInfrastructure()]
│
└── ROLEMANAGER_IMPLEMENTATION_SUMMARY.md        [NEW - Root level]
```

## Total Files Created: 20

### Commands (4 files)
1. CreateRoleCommand.cs
2. UpdateRoleCommand.cs
3. DeleteRoleCommand.cs
4. UpdateRolePermissionsCommand.cs

### Queries (3 files)
1. GetRoleQuery.cs
2. GetRoleByIdQuery.cs
3. GetRolePermissionsQuery.cs

### Handlers (7 files)
1. CreateRoleCommandHandler.cs
2. UpdateRoleCommandHandler.cs
3. DeleteRoleCommandHandler.cs
4. UpdateRolePermissionsCommandHandler.cs
5. GetRoleQueryHandler.cs
6. GetRoleByIdQueryHandler.cs
7. GetRolePermissionsQueryHandler.cs

### Validators (3 files)
1. CreateRoleValidator.cs
2. UpdateRoleValidator.cs
3. UpdateRolePermissionsValidator.cs

### Controller (1 file)
1. RoleManagerController.cs

### Documentation & Tests (2 files)
1. RoleManager-API.postman_collection.json
2. RoleManager-README.md

---

## DI Registration - AUTOMATIC ✅

No manual DI registration needed! The following is already handled by the existing infrastructure:

```csharp
// In Program.cs
builder.Services.AddInfrastructure(builder.Configuration);
    ↓
// In InfrastructureServiceExtensions.cs
AddIdentityLayer()  // Registers RoleManager<IdentityRole>
    ↓
// In ServiceCollectionExtensions.cs
services.AddIdentity<ApplicationUser, IdentityRole>(...)
    .AddEntityFrameworkStores<IdentityContext>()
    .AddDefaultTokenProviders();
```

**Result:** 
- ✅ `RoleManager<IdentityRole>` auto-registered
- ✅ `UserManager<IdentityUser>` auto-registered
- ✅ Validators auto-discovered by MediatR
- ✅ Handlers auto-registered with MediatR

---

## Quick Start

### 1. Build & Run
```bash
cd "D:\EnterpriseApps\DairyApps\LawyerDiary- AI\Service"
# Stop debugger first (Shift+F5)
dotnet build
dotnet run
```

### 2. Get JWT Token
```bash
POST /api/auth/login
```

### 3. Import Postman Collection
- File: `CourtApp.Api/Postman/RoleManager-API.postman_collection.json`
- Set variables: baseUrl, accessToken, roleId

### 4. Test Endpoints
Start with POST `/api/rolemanager` to create a role, then test other endpoints.

---

## File Dependencies

```
RoleManagerController.cs
├── CreateRoleCommand (MediatR)
├── UpdateRoleCommand (MediatR)
├── DeleteRoleCommand (MediatR)
├── UpdateRolePermissionsCommand (MediatR)
├── GetRoleQuery (MediatR)
├── GetRoleByIdQuery (MediatR)
└── GetRolePermissionsQuery (MediatR)
    │
    ├── Handlers (Execute commands/queries)
    │   ├── RoleManager<IdentityRole> (DI)
    │   ├── UserManager<IdentityUser> (DI)
    │   └── Database Context (inherited)
    │
    └── Validators (Validate input)
        ├── RoleManager<IdentityRole> (DI)
        └── FluentValidation pipeline
```

---

## Configuration - NO CHANGES NEEDED ✅

The existing Program.cs and infrastructure setup already includes:

- ✅ Identity configuration
- ✅ MediatR registration
- ✅ FluentValidation pipeline
- ✅ Base controller setup
- ✅ API response formatting

No modifications to Program.cs or configuration files required!

---

## Validation Flow

```
Request
  ↓
RoleManagerController.CreateAsync()
  ↓
MediatR.Send(CreateRoleCommand)
  ↓
ValidationBehavior<CreateRoleCommand>
  ├── CreateRoleValidator.Validate()
  │   ├── Name required? ✓
  │   ├── Name 3-100 chars? ✓
  │   ├── Unique in DB? ✓ (MustAsync)
  │   └── Description max 500? ✓
  │
  └─ If valid → CreateRoleCommandHandler
                 ├── Create IdentityRole
                 ├── RoleManager.CreateAsync()
                 ├── Return Result<Guid>.Success()
                 └─ 201 Created Response

  └─ If invalid → ValidationBehavior
                  └─ Return Result<Guid>.Fail()
                     400 Bad Request
```

---

## Testing Checklist

- [ ] Application builds successfully
- [ ] Debugger stopped (Shift+F5)
- [ ] Application runs (dotnet run)
- [ ] Authentication endpoint works (get JWT token)
- [ ] POST /api/rolemanager (Create role)
- [ ] GET /api/rolemanager (List roles - paginated)
- [ ] GET /api/rolemanager/{id} (Get role details)
- [ ] PUT /api/rolemanager/{id} (Update role)
- [ ] GET /api/rolemanager/{roleId}/permissions (Get permissions)
- [ ] POST /api/rolemanager/{roleId}/permissions (Update permissions)
- [ ] DELETE /api/rolemanager/{id} (Delete role)
- [ ] Duplicate role name validation works
- [ ] Pagination with searchTerm filter works
- [ ] Permission activate/deactivate works

---

## Support & Documentation

- **Full Documentation:** See `RoleManager-README.md`
- **Testing Collection:** Import `RoleManager-API.postman_collection.json`
- **Implementation Summary:** See `ROLEMANAGER_IMPLEMENTATION_SUMMARY.md`

---

**Status:** ✅ All files created and ready to test!
