# SystemUsers API Quick Reference

## Base URL
```
GET/POST/PUT /api/system-users
```

## Endpoints

### 1. Get All System Users (with pagination & filtering)
```http
GET /api/system-users
```

**Query Parameters:**
```
pageNumber=1           (optional, default: 1)
pageSize=10           (optional, default: 10)
userType=Lawyer       (optional, values: Lawyer, Corporate)
status=Pending        (optional, values: Pending, Active, Inactive, Locked)
searchTerm=john       (optional, searches FirstName, LastName, Email, CompanyName)
```

**Example Request:**
```http
GET /api/system-users?pageNumber=1&pageSize=10&userType=Lawyer&status=Pending
```

**Success Response (200 OK):**
```json
{
  "isSuccessful": true,
  "message": "Success",
  "data": {
    "items": [
      {
        "id": "user-uuid-1",
        "userType": 0,
        "firstName": "John",
        "lastName": "Doe",
        "fullName": "John Doe",
        "email": "john@example.com",
        "registeredDate": "2024-01-15T10:30:00Z",
        "subscription": 0,
        "subscriptionExpiryDate": "2024-02-15T10:30:00Z",
        "status": 0,
        "lastLoginDate": null,
        "statusReason": "New registration - Free plan enabled for 1 month"
      }
    ],
    "totalCount": 1,
    "pageNumber": 1,
    "pageSize": 10,
    "totalPages": 1
  }
}
```

### 2. Get Specific System User
```http
GET /api/system-users/{id}
```

**Path Parameters:**
```
id: ApplicationUser.Id (string, required)
```

**Example Request:**
```http
GET /api/system-users/550e8400-e29b-41d4-a716-446655440000
```

**Success Response (200 OK):**
```json
{
  "isSuccessful": true,
  "message": "Success",
  "data": {
    "id": "550e8400-e29b-41d4-a716-446655440000",
    "userType": 0,
    "firstName": "John",
    "lastName": "Doe",
    "fullName": "John Doe",
    "email": "john@example.com",
    "registeredDate": "2024-01-15T10:30:00Z",
    "subscription": 0,
    "subscriptionExpiryDate": "2024-02-15T10:30:00Z",
    "status": 0,
    "lastLoginDate": null,
    "statusReason": "New registration - Free plan enabled for 1 month"
  }
}
```

**Error Response (404 Not Found):**
```json
{
  "isSuccessful": false,
  "message": "System user not found.",
  "errors": ["System user not found."]
}
```

### 3. Update System User (Subscription & Status)
```http
PUT /api/system-users/{id}
```

**Path Parameters:**
```
id: ApplicationUser.Id (string, required)
```

**Request Body:**
```json
{
  "subscription": 1,
  "status": 1,
  "statusReason": "Manual upgrade to monthly plan"
}
```

**Subscription Enum:**
- `0` = Trial
- `1` = Free
- `2` = Monthly
- `3` = Yearly

**Status Enum:**
- `0` = Pending
- `1` = Active
- `2` = Inactive
- `3` = Locked

**Example Request:**
```http
PUT /api/system-users/550e8400-e29b-41d4-a716-446655440000
Content-Type: application/json

{
  "subscription": 2,
  "status": 1,
  "statusReason": "User purchased monthly subscription"
}
```

**Success Response (200 OK):**
```json
{
  "isSuccessful": true,
  "message": "Success",
  "data": true
}
```

**Error Response (404 Not Found):**
```json
{
  "isSuccessful": false,
  "message": "Failed to update system user",
  "errors": ["System user not found"]
}
```

## Curl Examples

### Get all pending registrations
```bash
curl -X GET "http://localhost:5000/api/system-users?status=0" \
  -H "Authorization: Bearer {token}"
```

### Get all lawyer registrations
```bash
curl -X GET "http://localhost:5000/api/system-users?userType=0" \
  -H "Authorization: Bearer {token}"
```

### Search for user by name
```bash
curl -X GET "http://localhost:5000/api/system-users?searchTerm=john" \
  -H "Authorization: Bearer {token}"
```

### Get pending lawyers (status + type filter)
```bash
curl -X GET "http://localhost:5000/api/system-users?userType=0&status=0" \
  -H "Authorization: Bearer {token}"
```

### Upgrade user to monthly subscription
```bash
curl -X PUT "http://localhost:5000/api/system-users/550e8400-e29b-41d4-a716-446655440000" \
  -H "Authorization: Bearer {token}" \
  -H "Content-Type: application/json" \
  -d '{
    "subscription": 2,
    "status": 1,
    "statusReason": "User purchased monthly subscription"
  }'
```

### Approve registration and activate user
```bash
curl -X PUT "http://localhost:5000/api/system-users/550e8400-e29b-41d4-a716-446655440000" \
  -H "Authorization: Bearer {token}" \
  -H "Content-Type: application/json" \
  -d '{
    "status": 1,
    "statusReason": "Approved by admin"
  }'
```

### Lock user account after free plan expiry
```bash
curl -X PUT "http://localhost:5000/api/system-users/550e8400-e29b-41d4-a716-446655440000" \
  -H "Authorization: Bearer {token}" \
  -H "Content-Type: application/json" \
  -d '{
    "status": 3,
    "statusReason": "Account locked - free plan expired"
  }'
```

## Usage in Code

### Getting Users in a Controller
```csharp
[HttpGet("pending-registrations")]
public async Task<IActionResult> GetPendingRegistrations()
{
    var query = new GetSystemUsersQuery
    {
        PageNumber = 1,
        PageSize = 20,
        Status = UserAccountStatus.Pending
    };

    var result = await Mediator.Send(query);
    return FromPaginated(result);
}
```

### In a Handler
```csharp
var users = await _systemUserService.GetAllRegisteredUsersAsync(
    userType: RegisterType.Lawyer,
    status: UserAccountStatus.Active,
    pageNumber: query.PageNumber,
    pageSize: query.PageSize,
    searchTerm: query.SearchTerm
);

return PaginatedResult<SystemUserResponse>.Success(
    data: users,
    totalCount: users.Count,
    pageNumber: query.PageNumber,
    pageSize: query.PageSize
);
```

## Common Workflows

### 1. Approve a Pending Registration
```
GET /api/system-users?status=0     ← View all pending
PUT /api/system-users/{id}          ← Update status to Active
  {
    "status": 1,
    "statusReason": "Approved by admin"
  }
```

### 2. Upgrade Free Plan to Premium
```
PUT /api/system-users/{id}
  {
    "subscription": 2,  ← Monthly
    "statusReason": "Upgraded to monthly plan"
  }
```

### 3. Lock Expired Free Plan Users (Automated)
```
1. Query: SELECT * FROM SystemUsers 
   WHERE SubscriptionExpiryDate <= NOW 
   AND Subscription = 'Free' 
   AND Status != 'Inactive'

2. For each user:
   PUT /api/system-users/{id}
     {
       "status": 3,  ← Locked
       "statusReason": "Free plan expired"
     }
```

## Response Status Codes

- `200 OK` - Request successful
- `400 Bad Request` - Invalid input or validation failed
- `401 Unauthorized` - Missing or invalid authentication token
- `404 Not Found` - Resource not found
- `500 Internal Server Error` - Server error

## Error Response Format
```json
{
  "isSuccessful": false,
  "message": "Error description",
  "errors": [
    "Detailed error 1",
    "Detailed error 2"
  ]
}
```

## Pagination Info
The paginated response includes:
```json
{
  "items": [...],
  "totalCount": 100,
  "pageNumber": 1,
  "pageSize": 10,
  "totalPages": 10
}
```

Calculate pages: `Math.Ceiling(totalCount / pageSize)`
