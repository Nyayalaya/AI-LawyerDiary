# RoleManager Feature Documentation

## Overview
The RoleManager feature provides comprehensive role and permission management using ASP.NET Identity. It implements the CQRS pattern with MediatR and includes validation using FluentValidation.

## Endpoints

### Role CRUD Operations

#### Create Role
- **Endpoint:** `POST /api/rolemanager`
- **Request Body:**
```json
{
  "name": "Admin",
  "description": "Administrator role"
}
```
- **Response (201 Created):**
```json
{
  "succeeded": true,
  "data": "role-id-guid",
  "message": "Role 'Admin' created successfully"
}
```
- **Validations:**
  - Role name is required (3-100 characters)
  - Role name must be unique
  - Description max 500 characters

---

#### Get All Roles (Paginated)
- **Endpoint:** `GET /api/rolemanager?pageNumber=1&pageSize=10&searchTerm=`
- **Query Parameters:**
  - `pageNumber` (int, default: 1)
  - `pageSize` (int, default: 10)
  - `searchTerm` (string, optional) - Search by role name
- **Response (200 OK):**
```json
{
  "succeeded": true,
  "data": [
    {
      "id": "role-id-1",
      "name": "Admin",
      "description": "Administrator",
      "permissionCount": 25,
      "userCount": 5
    }
  ],
  "pageNumber": 1,
  "pageSize": 10,
  "totalCount": 1,
  "hasPreviousPage": false,
  "hasNextPage": false,
  "message": "Roles retrieved successfully"
}
```

---

#### Get Role by ID
- **Endpoint:** `GET /api/rolemanager/{roleId}`
- **Path Parameters:**
  - `roleId` (string) - The ID of the role to retrieve
- **Response (200 OK):**
```json
{
  "succeeded": true,
  "data": {
    "id": "role-id",
    "name": "Admin",
    "description": "Administrator role"
  },
  "message": "Role retrieved successfully"
}
```
- **Error Response (404 Not Found):**
```json
{
  "succeeded": false,
  "errors": ["Role not found"],
  "message": "Role not found"
}
```

---

#### Update Role
- **Endpoint:** `PUT /api/rolemanager/{roleId}`
- **Path Parameters:**
  - `roleId` (string) - The ID of the role to update
- **Request Body:**
```json
{
  "name": "Admin Updated",
  "description": "Updated administrator role"
}
```
- **Response (200 OK):**
```json
{
  "succeeded": true,
  "data": true,
  "message": "Role 'Admin Updated' updated successfully"
}
```
- **Validations:**
  - Role must exist
  - Role name must be unique (excluding current role)
  - Name and description validation same as Create

---

#### Delete Role
- **Endpoint:** `DELETE /api/rolemanager/{roleId}`
- **Path Parameters:**
  - `roleId` (string) - The ID of the role to delete
- **Response (200 OK):**
```json
{
  "succeeded": true,
  "data": true,
  "message": "Role 'Admin' deleted successfully"
}
```

---

### Role Permission Management

#### Get Role Permissions
- **Endpoint:** `GET /api/rolemanager/{roleId}/permissions`
- **Path Parameters:**
  - `roleId` (string) - The ID of the role
- **Response (200 OK):**
```json
{
  "succeeded": true,
  "data": [
    {
      "id": "CREATE_CASE",
      "name": "CREATE_CASE",
      "description": "",
      "isActive": true
    },
    {
      "id": "DELETE_CASE",
      "name": "DELETE_CASE",
      "description": "",
      "isActive": true
    }
  ],
  "message": "Role permissions retrieved successfully"
}
```

---

#### Update Role Permissions (Activate/Deactivate)
- **Endpoint:** `POST /api/rolemanager/{roleId}/permissions`
- **Path Parameters:**
  - `roleId` (string) - The ID of the role
- **Request Body:**
```json
[
  {
    "permissionId": "CREATE_CASE",
    "isActive": true
  },
  {
    "permissionId": "DELETE_CASE",
    "isActive": false
  },
  {
    "permissionId": "EDIT_CASE",
    "isActive": true
  }
]
```
- **Response (200 OK):**
```json
{
  "succeeded": true,
  "data": true,
  "message": "Role permissions updated successfully"
}
```
- **Validations:**
  - Role must exist
  - At least one permission must be provided
  - Each permission must have a valid PermissionId

---

## Architecture

### Folder Structure
```
CourtApp.Application/Features/RoleManager/
├── Commands/
│   ├── CreateRoleCommand.cs
│   ├── UpdateRoleCommand.cs
│   ├── DeleteRoleCommand.cs
│   └── UpdateRolePermissionsCommand.cs
├── Queries/
│   ├── GetRoleQuery.cs
│   ├── GetRoleByIdQuery.cs
│   └── GetRolePermissionsQuery.cs
├── Handlers/
│   ├── CreateRoleCommandHandler.cs
│   ├── UpdateRoleCommandHandler.cs
│   ├── DeleteRoleCommandHandler.cs
│   ├── UpdateRolePermissionsCommandHandler.cs
│   ├── GetRoleQueryHandler.cs
│   ├── GetRoleByIdQueryHandler.cs
│   └── GetRolePermissionsQueryHandler.cs
└── Validators/
    ├── CreateRoleValidator.cs
    ├── UpdateRoleValidator.cs
    └── UpdateRolePermissionsValidator.cs

CourtApp.Api/Controllers/
└── RoleManagerController.cs
```

### CQRS Pattern
- **Commands:** Create, Update, Delete, UpdatePermissions
- **Queries:** GetAll (paginated), GetById, GetPermissions
- **Handlers:** MediatR request handlers that execute business logic
- **Validators:** FluentValidation validators with async DB checks

### Identity Integration
- Uses `RoleManager<IdentityRole>` from ASP.NET Identity
- Uses `UserManager<IdentityUser>` for user-role associations
- Implements `RoleClaims` for permission management
- Role permissions are stored as "Permission" claims on roles

---

## Key Features

### 1. **Duplicate Role Name Prevention**
The `CreateRoleValidator` uses `MustAsync` to check for duplicate role names in the database before creation.

### 2. **Pagination Support**
The `GetRoleQuery` supports:
- Customizable page size
- Search term filtering
- Total count calculation
- Previous/next page indicators

### 3. **Permission Management**
- Attach/detach permissions (claims) to roles
- Activate/deactivate permissions without deletion
- List all active permissions for a role

### 4. **Error Handling**
All handlers include:
- Try-catch exception handling
- Result<T>.Fail() for validation failures
- Descriptive error messages
- HTTP status code mapping

### 5. **API Response Format**
All responses follow a consistent format using `ApiResponse<T>` and `PaginatedResult<T>`:
```csharp
{
  "succeeded": bool,
  "data": T,
  "errors": List<string>,
  "message": string,
  "pageNumber": int?,
  "pageSize": int?,
  "totalCount": int?,
  "hasNextPage": bool?,
  "hasPreviousPage": bool?
}
```

---

## Usage Examples

### Example 1: Create an Admin Role
```bash
curl -X POST http://localhost:5000/api/rolemanager \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -d '{
    "name": "Admin",
    "description": "Full system access"
  }'
```

### Example 2: Get Paginated Roles with Search
```bash
curl -X GET "http://localhost:5000/api/rolemanager?pageNumber=1&pageSize=5&searchTerm=Admin" \
  -H "Authorization: Bearer YOUR_TOKEN"
```

### Example 3: Assign Permissions to Role
```bash
curl -X POST http://localhost:5000/api/rolemanager/{roleId}/permissions \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -d '[
    {"permissionId": "VIEW_CASES", "isActive": true},
    {"permissionId": "CREATE_CASES", "isActive": true},
    {"permissionId": "DELETE_CASES", "isActive": false}
  ]'
```

---

## Validators

### CreateRoleValidator
Validates role creation with:
- Required name (3-100 chars)
- Unique name check (async DB query)
- Optional description (max 500 chars)

### UpdateRoleValidator
Validates role updates with:
- Role existence check
- Unique name validation (allows current name)
- Name and description constraints

### UpdateRolePermissionsValidator
Validates permission updates with:
- Role existence verification
- At least one permission required
- Valid permission IDs

---

## Dependencies

### NuGet Packages
- `MediatR` - CQRS command/query handling
- `FluentValidation` - Validation framework
- `Microsoft.AspNetCore.Identity` - Role management
- `Microsoft.EntityFrameworkCore` - Database access

### Services Required
- `RoleManager<IdentityRole>` - Role management
- `UserManager<IdentityUser>` - User management
- `IValidator<TCommand>` - FluentValidation

---

## Testing with Postman

1. Import the `RoleManager-API.postman_collection.json` file into Postman
2. Set variables in Postman:
   - `baseUrl`: http://localhost:5000
   - `accessToken`: Your JWT token
   - `roleId`: A valid role ID from the system
3. Execute requests in order

---

## Future Enhancements

- [ ] Bulk role import/export
- [ ] Role hierarchy/inheritance
- [ ] Permission templates
- [ ] Audit logging for role changes
- [ ] Role cloning functionality
- [ ] Permission groups

---

## Troubleshooting

### Issue: RoleManager not registered in DI
**Solution:** Ensure `AddInfrastructure()` is called in Program.cs with `AddIdentityLayer()`

### Issue: Duplicate role name validation not working
**Solution:** Ensure async validators are registered in MediatR pipeline with `ValidationBehavior`

### Issue: 401 Unauthorized on endpoints
**Solution:** Include valid JWT token in `Authorization: Bearer {token}` header

---

## Support
For issues or questions, refer to the main application documentation or contact the development team.
