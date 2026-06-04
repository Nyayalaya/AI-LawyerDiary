# 🛡️ Error Handling Guide - CourtApp API

## Overview

This document explains how error handling works in the CourtApp API, specifically for validation errors and exceptions in the CreateClient endpoint and how to apply the same patterns to other endpoints.

---

## 📊 Architecture Flow

```
Client Request
    ↓
API Controller
    ↓
try-catch Block (handles unhandled exceptions)
    ↓
MediatR Pipeline
    ├─ ValidationBehavior (FluentValidation)
    │  ├─ Validates input
    │  ├─ If fails → Returns Result.Fail()
    │  └─ If success → Continues to handler
    │
    └─ Command/Query Handler
       ├─ Business logic execution
       ├─ Returns Result.Success() or Result.Fail()
       └─ Sends back to controller
    ↓
Result Processing (FromResult method)
    ├─ If Succeeded = true → 200/201 OK
    ├─ If Errors.Count > 0 → 422 Unprocessable Entity
    └─ If generic error → 400 Bad Request
    ↓
ApiResponse Object (JSON to Client)
```

---

## 🔄 Error Handling Layers

### Layer 1: ValidationBehavior (MediatR Pipeline)
**Location**: `CourtApp.Application/Common/ValidationBehavior.cs`

✅ **What it does:**
- Runs all registered validators for the request
- Collects validation errors
- Returns `Result.Fail()` instead of throwing exceptions
- Clients receive structured validation error responses

**Example Result from Validation Failure:**
```csharp
Result<Guid>.Fail(
    message: "Validation failed",
    errors: new List<string> {
        "Client name is required",
        "Mobile number must be 10 digits",
        "A client with this name already exists in the system"
    }
)
```

---

### Layer 2: Controller Error Handling (Try-Catch)
**Location**: `CourtApp.Api/Controllers/ClientController.cs`

✅ **What it handles:**
- **Null command** - Invalid request body
- **OperationCanceledException** - Timeout/cancellation
- **ArgumentException** - Invalid arguments
- **General Exception** - Any unhandled exceptions

**Example:**
```csharp
try
{
    if (command == null)
        return ValidationError(new List<string> { "Request body is required" });

    command.UserId = UserId;
    Result<Guid> result = await Mediator.Send(command, RequestAborted);

    // ... handle result
}
catch (Exception ex)
{
    return StatusCode(500, ApiResponse<object>.ServerError(ex.Message));
}
```

---

### Layer 3: Result Processing (FromResult)
**Location**: `CourtApp.Api/Controllers/BaseController.cs`

✅ **What it does:**
- Converts `Result<T>` to `ApiResponse<T>`
- Sets appropriate HTTP status codes
- Includes error details in response

**Code:**
```csharp
protected IActionResult FromResult<T>(Result<T> result, int successCode = 200)
{
    var response = ApiResponse<T>.FromResult(result, successCode);
    return StatusCode(response.StatusCode, response);
}
```

---

## 📨 Response Examples

### ✅ Success Response (201 Created)
```json
{
  "status": true,
  "message": "Client created successfully",
  "statusCode": 201,
  "timestamp": "2024-01-15T10:30:45.123Z",
  "data": "550e8400-e29b-41d4-a716-446655440000",
  "errors": null
}
```

### ⚠️ Validation Error Response (422 Unprocessable Entity)
```json
{
  "status": false,
  "message": "Validation failed",
  "statusCode": 422,
  "timestamp": "2024-01-15T10:30:45.123Z",
  "data": null,
  "errors": [
    "Client name is required",
    "Mobile number must be 10 digits",
    "A client with this name already exists in the system"
  ]
}
```

### ❌ Generic Error Response (400 Bad Request)
```json
{
  "status": false,
  "message": "Failed to create client",
  "statusCode": 400,
  "timestamp": "2024-01-15T10:30:45.123Z",
  "data": null,
  "errors": null
}
```

### 🔴 Server Error Response (500 Internal Server Error)
```json
{
  "status": false,
  "message": "An unexpected error occurred while creating the client",
  "statusCode": 500,
  "timestamp": "2024-01-15T10:30:45.123Z",
  "data": null,
  "errors": ["NullReferenceException: Object reference not set..."]
}
```

---

## 🎯 HTTP Status Codes Used

| Status | Code | When Used |
|--------|------|-----------|
| **Created** | 201 | Resource successfully created |
| **Bad Request** | 400 | Generic error, no specific errors |
| **Unauthorized** | 401 | User not authenticated |
| **Unprocessable Entity** | 422 | Validation/business logic errors |
| **Internal Server Error** | 500 | Unexpected server error |
| **Request Timeout** | 408 | Request cancelled/timed out |

---

## 🔧 How to Apply to Other Endpoints

### Step 1: Add Try-Catch to Your Controller Method

```csharp
[HttpPost]
public async Task<IActionResult> YourMethodAsync([FromBody] YourCommand command)
{
    try
    {
        if (command == null)
            return ValidationError(new List<string> { "Request body is required" });

        // Your logic here
        var result = await Mediator.Send(command, RequestAborted);

        // Handle result
        if (result.Succeeded)
            return FromResult(result, successCode: 201);
        else if (result.Errors?.Count > 0)
            return StatusCode(422, 
                ApiResponse<object>.Failure(result.Message ?? "Validation failed", 422, result.Errors));
        else
            return StatusCode(400, 
                ApiResponse<object>.Failure(result.Message ?? "Operation failed", 400));
    }
    catch (OperationCanceledException ex)
    {
        return StatusCode(408, 
            ApiResponse<object>.Failure("Request cancelled", 408, new List<string> { ex.Message }));
    }
    catch (Exception ex)
    {
        return StatusCode(500, 
            ApiResponse<object>.ServerError("An unexpected error occurred", new List<string> { ex.Message }));
    }
}
```

### Step 2: Update ProducesResponseType Attributes

```csharp
[HttpPost]
[ProducesResponseType(typeof(ApiResponse<YourType>), 201)]        // Success
[ProducesResponseType(typeof(ApiResponse<object>), 400)]          // Bad Request
[ProducesResponseType(typeof(ApiResponse<object>), 422)]          // Validation Error
[ProducesResponseType(typeof(ApiResponse<object>), 401)]          // Unauthorized
[ProducesResponseType(typeof(ApiResponse<object>), 500)]          // Server Error
public async Task<IActionResult> YourMethodAsync(...)
```

---

## 🎓 Best Practices

### ✅ DO

1. **Always validate input** - Check null/empty values first
2. **Use specific exception types** - Catch `ArgumentException`, `OperationCanceledException` separately
3. **Return appropriate status codes** - Use 422 for validation, 400 for bad requests, 500 for server errors
4. **Include error details** - Provide meaningful error messages to clients
5. **Log errors** - Use correlation ID for tracing (production recommendation)
6. **Document response types** - Use `[ProducesResponseType]` attributes

### ❌ DON'T

1. ❌ Don't throw exceptions for validation - Use Result.Fail()
2. ❌ Don't return generic "Error occurred" messages - Be specific
3. ❌ Don't ignore OperationCanceledException - Handle gracefully
4. ❌ Don't expose sensitive data in error messages
5. ❌ Don't return 200 OK for failures - Use proper status codes

---

## 🧪 Testing the Error Handling

### Test 1: Validation Error
```bash
curl -X POST https://localhost:7001/api/v1/client \
  -H "Content-Type: application/json" \
  -d '{
    "name": "",
    "email": "invalid-email",
    "mobile": "123",
    "clientType": "Individual"
  }'

# Expected Response: 422 Unprocessable Entity
# With errors: ["Client name is required", "Mobile number must be 10 digits", ...]
```

### Test 2: Duplicate Client
```bash
curl -X POST https://localhost:7001/api/v1/client \
  -H "Content-Type: application/json" \
  -d '{
    "name": "John Doe",
    "email": "john@example.com",
    "mobile": "9876543210",
    "clientType": "Individual"
  }'

# If "John Doe" already exists:
# Expected Response: 422 Unprocessable Entity
# Error: "A client with this name already exists in the system"
```

### Test 3: Missing Request Body
```bash
curl -X POST https://localhost:7001/api/v1/client \
  -H "Content-Type: application/json"

# Expected Response: 422 Unprocessable Entity
# Error: "Request body is required"
```

### Test 4: Success
```bash
curl -X POST https://localhost:7001/api/v1/client \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Jane Doe",
    "email": "jane@example.com",
    "mobile": "9876543211",
    "clientType": "Individual"
  }'

# Expected Response: 201 Created
# With data: "550e8400-e29b-41d4-a716-446655440000"
```

---

## 📝 Summary

| Component | Responsibility |
|-----------|-----------------|
| **ValidationBehavior** | Validates requests, returns Result.Fail() |
| **Controller Try-Catch** | Catches unhandled exceptions |
| **FromResult()** | Converts Result to ApiResponse with proper status codes |
| **ApiResponse** | Serializes to JSON with status, message, data, errors |
| **Client** | Receives structured error response with clear guidance |

---

## 🚀 Next Steps

1. Apply this pattern to all your endpoints
2. Add logging with correlation IDs
3. Set up error monitoring (e.g., Sentry, Application Insights)
4. Create unit tests for error scenarios
5. Document all validation rules in your API documentation

---

**Last Updated**: January 2024
**Version**: 1.0
