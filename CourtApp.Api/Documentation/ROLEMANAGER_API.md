# RoleManager API Documentation

## Overview

The RoleManager API provides comprehensive role management functionality with CRUD operations and permission assignment capabilities. It integrates with ASP.NET Identity for secure role and permission management.

## Base URL
```
/api/rolemanager
```

## Authentication
All endpoints require Bearer token authentication via JWT.

---

## Endpoints

### 1. Get All Roles (Paginated)

**Endpoint:** `GET /api/rolemanager`

**Description:** Retrieve a paginated list of all roles with optional search filtering.

**Query Parameters:**
- `pageNumber` (int, default: 1): Page number for pagination
- `pageSize` (int, default: 10): Number of items per page
- `searchTerm` (string, optional): Search roles by name

**Example Request:**
```http
GET /api/rolemanager?pageNumber=1&pageSize=10&searchTerm=admin HTTP/1.1
Authorization: Bearer {token}
```

**Example Response:**
```json
{
  "succeeded": true,
  "message": "Roles retrieved successfully",
  "data": [
    {
      "id": "550e8400-e29b-41d4-a716-446655440000",
      "name": "Admin",
      "description": "",
      "permissionCount": 25,
      "userCount": 5
    },
    {
      "id": "6ba7b810-9dad-11d1-80b4-00c04fd430c8",
      "name": "Editor",
      "description": "",
      "permissionCount": 12,
      "userCount": 8
    }
  ],
  "paginationData": {
    "totalCount": 2,
    "pageNumber": 1,
    "pageSize": 10,
    "totalPages": 1
  }
}
```

**Status Codes:**
- `200 OK`: Success
- `401 Unauthorized`: Invalid or missing token

---

### 2. Get Role by ID

**Endpoint:** `GET /api/rolemanager/{id}`

**Description:** Retrieve detailed information about a specific role.

**Path Parameters:**
- `id` (string, required): Role ID

**Example Request:**
```http
GET /api/rolemanager/550e8400-e29b-41d4-a716-446655440000 HTTP/1.1
Authorization: Bearer {token}
```

**Example Response:**
```json
{
  "succeeded": true,
  "message": "Role retrieved successfully",
  "data": {
    "id": "550e8400-e29b-41d4-a716-446655440000",
    "name": "Admin",
    "description": ""
  }
}
```

**Status Codes:**
- `200 OK`: Success
- `404 Not Found`: Role not found
- `401 Unauthorized`: Invalid or missing token

---

### 3. Create Role

**Endpoint:** `POST /api/rolemanager`

**Description:** Create a new role with a unique name.

**Request Body:**
```json
{
  "name": "string (required, 3-100 characters, must be unique)",
  "description": "string (optional, max 500 characters)"
}
```

**Example Request:**
```http
POST /api/rolemanager HTTP/1.1
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "Moderator",
  "description": "Role for content moderation"
}
```

**Example Response (Success):**
```json
{
  "succeeded": true,
  "message": "Role 'Moderator' created successfully",
  "data": "550e8400-e29b-41d4-a716-446655440001"
}
```

**Example Response (Validation Error):**
```json
{
  "succeeded": false,
  "message": "A role with this name already exists",
  "errors": ["A role with this name already exists"]
}
```

**Validation Rules:**
- Name is required
- Name must be between 3-100 characters
- Name must be unique across the system
- Description must not exceed 500 characters

**Status Codes:**
- `201 Created`: Role created successfully
- `400 Bad Request`: Validation error
- `401 Unauthorized`: Invalid or missing token

---

### 4. Update Role

**Endpoint:** `PUT /api/rolemanager/{id}`

**Description:** Update an existing role's information.

**Path Parameters:**
- `id` (string, required): Role ID

**Request Body:**
```json
{
  "id": "string (required, must match path)",
  "name": "string (required, 3-100 characters, must be unique or same as original)",
  "description": "string (optional, max 500 characters)"
}
```

**Example Request:**
```http
PUT /api/rolemanager/550e8400-e29b-41d4-a716-446655440000 HTTP/1.1
Authorization: Bearer {token}
Content-Type: application/json

{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "name": "Senior Admin",
  "description": "Senior administrator role"
}
```

**Example Response:**
```json
{
  "succeeded": true,
  "message": "Role 'Senior Admin' updated successfully",
  "data": true
}
```

**Validation Rules:**
- Role must exist
- New name must be unique (or same as current name)
- Name must be 3-100 characters
- Description must not exceed 500 characters

**Status Codes:**
- `200 OK`: Role updated successfully
- `400 Bad Request`: Validation error
- `404 Not Found`: Role not found
- `401 Unauthorized`: Invalid or missing token

---

### 5. Delete Role

**Endpoint:** `DELETE /api/rolemanager/{id}`

**Description:** Delete a role from the system.

**Path Parameters:**
- `id` (string, required): Role ID

**Example Request:**
```http
DELETE /api/rolemanager/550e8400-e29b-41d4-a716-446655440000 HTTP/1.1
Authorization: Bearer {token}
```

**Example Response:**
```json
{
  "succeeded": true,
  "message": "Role 'Editor' deleted successfully",
  "data": true
}
```

**Status Codes:**
- `200 OK`: Role deleted successfully
- `404 Not Found`: Role not found
- `401 Unauthorized`: Invalid or missing token

---

### 6. Get Role Permissions

**Endpoint:** `GET /api/rolemanager/{roleId}/permissions`

**Description:** Retrieve all permissions assigned to a specific role.

**Path Parameters:**
- `roleId` (string, required): Role ID

**Example Request:**
```http
GET /api/rolemanager/550e8400-e29b-41d4-a716-446655440000/permissions HTTP/1.1
Authorization: Bearer {token}
```

**Example Response:**
```json
{
  "succeeded": true,
  "message": "Role permissions retrieved successfully",
  "data": [
    {
      "id": "CREATE_CASE",
      "name": "CREATE_CASE",
      "description": "",
      "isActive": true
    },
    {
      "id": "EDIT_CASE",
      "name": "EDIT_CASE",
      "description": "",
      "isActive": true
    },
    {
      "id": "DELETE_CASE",
      "name": "DELETE_CASE",
      "description": "",
      "isActive": false
    }
  ]
}
```

**Status Codes:**
- `200 OK`: Success
- `404 Not Found`: Role not found
- `401 Unauthorized`: Invalid or missing token

---

### 7. Update Role Permissions (Activate/Deactivate)

**Endpoint:** `POST /api/rolemanager/{roleId}/permissions`

**Description:** Update role permissions by activating or deactivating specific permissions.

**Path Parameters:**
- `roleId` (string, required): Role ID

**Request Body:**
```json
[
  {
    "permissionId": "string (required)",
    "isActive": "boolean (required)"
  }
]
```

**Example Request:**
```http
POST /api/rolemanager/550e8400-e29b-41d4-a716-446655440000/permissions HTTP/1.1
Authorization: Bearer {token}
Content-Type: application/json

[
  {
    "permissionId": "CREATE_CASE",
    "isActive": true
  },
  {
    "permissionId": "EDIT_CASE",
    "isActive": true
  },
  {
    "permissionId": "DELETE_CASE",
    "isActive": false
  },
  {
    "permissionId": "VIEW_REPORTS",
    "isActive": true
  }
]
```

**Example Response:**
```json
{
  "succeeded": true,
  "message": "Role permissions updated successfully",
  "data": true
}
```

**Validation Rules:**
- Role must exist
- At least one permission must be provided
- Each permission must have a valid permissionId
- isActive must be a boolean

**Status Codes:**
- `200 OK`: Permissions updated successfully
- `400 Bad Request`: Validation error
- `404 Not Found`: Role not found
- `401 Unauthorized`: Invalid or missing token

---

## Error Handling

### Error Response Format
```json
{
  "succeeded": false,
  "message": "Human-readable error message",
  "errors": [
    "Detailed error 1",
    "Detailed error 2"
  ]
}
```

### Common Error Messages
- **Role not found**: The specified role ID does not exist
- **A role with this name already exists**: Role name must be unique
- **At least one permission must be provided**: Permission list cannot be empty
- **Unable to resolve service**: DI container issue (contact administrator)

---

## Permission Management

### Permission Structure
Permissions are stored as claims attached to roles. Each permission has:
- `PermissionId`: Unique identifier (e.g., "CREATE_CASE")
- `IsActive`: Boolean flag indicating activation status

### Activation/Deactivation
When updating role permissions:
- **Activate**: Set `isActive: true` to grant the permission
- **Deactivate**: Set `isActive: false` to revoke the permission
- **Remove**: Omit the permission from the request body to remove it

---

## Integration with ASP.NET Identity

The RoleManager API uses:
- **RoleManager<IdentityRole>**: For role management operations
- **IdentityRole**: Standard ASP.NET Identity role entity
- **IdentityRoleClaim**: For storing role permissions as claims

## Architecture

### CQRS Pattern
- **Commands**: CreateRoleCommand, UpdateRoleCommand, DeleteRoleCommand, UpdateRolePermissionsCommand
- **Queries**: GetRoleQuery, GetRoleByIdQuery, GetRolePermissionsQuery
- **Handlers**: Corresponding handlers for each command/query
- **Validators**: FluentValidation validators with async database checks

### Validation Pipeline
1. FluentValidation validators execute asynchronously
2. Unique name checks query the database
3. Role existence checks confirm ID validity
4. Permission existence validation

---

## Best Practices

1. **Always use pagination** for list endpoints to improve performance
2. **Cache role information** on the client to reduce API calls
3. **Validate permissions locally** before making requests
4. **Handle 404 responses** gracefully in UI
5. **Log all role changes** for audit purposes
6. **Use meaningful permission IDs** that describe the action

---

## Example: Complete Workflow

```
1. Create a new role:
   POST /api/rolemanager
   Body: { "name": "Reviewer", "description": "Content reviewer" }

2. Get the newly created role ID from response
   Response: roleId = "550e8400-e29b-41d4-a716-446655440002"

3. Assign permissions to the role:
   POST /api/rolemanager/550e8400-e29b-41d4-a716-446655440002/permissions
   Body: [
     { "permissionId": "VIEW_CASE", "isActive": true },
     { "permissionId": "EDIT_CASE", "isActive": true },
     { "permissionId": "DELETE_CASE", "isActive": false }
   ]

4. Get role permissions:
   GET /api/rolemanager/550e8400-e29b-41d4-a716-446655440002/permissions

5. Update role name:
   PUT /api/rolemanager/550e8400-e29b-41d4-a716-446655440002
   Body: { "name": "Content Reviewer", "description": "Reviews content" }
```

---

## Rate Limiting
Currently no rate limiting is implemented. Contact the API administrator for rate limit requirements.

---

## Support
For issues or questions, contact the development team or create an issue in the repository.
