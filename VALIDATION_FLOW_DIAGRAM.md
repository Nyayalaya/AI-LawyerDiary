# 🔍 Validation Error Handling Flow - Visual Guide

## Complete Request/Response Flow

```
┌─────────────────────────────────────────────────────────────────────┐
│                        CLIENT REQUEST                              │
│  POST /api/v1/client                                               │
│  {                                                                  │
│    "name": "",                                                      │
│    "mobile": "123",                                                │
│    "email": "invalid"                                               │
│  }                                                                  │
└──────────────────────────┬──────────────────────────────────────────┘
                           │
                           ▼
┌─────────────────────────────────────────────────────────────────────┐
│               API CONTROLLER (ClientController)                    │
│                                                                     │
│  [HttpPost] CreateAsync()                                          │
│  ├─ try {                                                           │
│  │  ├─ Check if command != null                                    │
│  │  │  └─ Returns ValidationError() if null                        │
│  │  │                                                               │
│  │  ├─ Set UserId                                                  │
│  │  │                                                               │
│  │  └─ Send through MediatR pipeline ──────────────────────┐       │
│  │                                                         │       │
│  └─ catch { ... }                                          │       │
└─────────────────────────────────────────────────────────────┼───────┘
                                                              │
                                                              ▼
                            ┌─────────────────────────────────────────┐
                            │   MEDIATR PIPELINE                      │
                            │                                         │
                            │  ┌──────────────────────────────┐      │
                            │  │ ValidationBehavior           │      │
                            │  │ (IPipelineBehavior)          │      │
                            │  │                              │      │
                            │  │ 1. Get validators            │      │
                            │  │ 2. Run all async             │      │
                            │  │ 3. Collect errors            │      │
                            │  │                              │      │
                            │  │ IF errors found:             │      │
                            │  │ ├─ Create error list         │      │
                            │  │ └─ RETURN Result.Fail()      │      │
                            │  │                              │      │
                            │  │ IF no errors:                │      │
                            │  │ └─ Continue to handler       │      │
                            │  └──────────────────────────────┘      │
                            │              │                        │
                            │              ▼ (if validation passes) │
                            │  ┌──────────────────────────────┐      │
                            │  │ CreateClientCommandHandler   │      │
                            │  │                              │      │
                            │  │ 1. Business logic            │      │
                            │  │ 2. Database operations       │      │
                            │  │ 3. Return Result             │      │
                            │  │    ├─ Success()              │      │
                            │  │    └─ Fail()                 │      │
                            │  └──────────────────────────────┘      │
                            │              │                        │
                            └──────────────┼────────────────────────┘
                                           │
                                           ▼
┌─────────────────────────────────────────────────────────────────────┐
│              RESULT PROCESSING (Controller)                        │
│                                                                     │
│  result = Result<Guid> {                                           │
│    Succeeded = false                                               │
│    Message = "Validation failed"                                   │
│    Errors = [                                                      │
│      "Client name is required",                                    │
│      "Mobile number must be 10 digits",                            │
│      "Email format is invalid"                                     │
│    ]                                                               │
│  }                                                                 │
│                                                                     │
│  Check: if (result.Succeeded) ──────────────> FALSE ──────────┐   │
│         else if (result.Errors?.Count > 0)    ──> TRUE (3)    │   │
│                                                                 │   │
│         Return 422 Unprocessable Entity                         │   │
└─────────────────────────────────────────────────────────────────┼───┘
                                                                  │
                                                                  ▼
┌─────────────────────────────────────────────────────────────────────┐
│                     API RESPONSE BUILDER                           │
│                                                                     │
│  ApiResponse<object>.Failure(                                      │
│    message: "Validation failed",                                   │
│    statusCode: 422,                                                │
│    errors: [...]                                                   │
│  )                                                                 │
│                                                                     │
│  Result:                                                           │
│  {                                                                 │
│    "status": false,                                               │
│    "message": "Validation failed",                                │
│    "statusCode": 422,                                             │
│    "timestamp": "2024-01-15T10:30:45.123Z",                       │
│    "data": null,                                                  │
│    "errors": [                                                    │
│      "Client name is required",                                   │
│      "Mobile number must be 10 digits",                           │
│      "Email format is invalid"                                    │
│    ]                                                              │
│  }                                                                │
└──────────────────────────┬───────────────────────────────────────┘
                           │
                           ▼
┌─────────────────────────────────────────────────────────────────────┐
│                    HTTP RESPONSE (422)                              │
│                                                                     │
│  Headers:                                                          │
│  ├─ Content-Type: application/json                               │
│  ├─ HTTP/1.1 422 Unprocessable Entity                            │
│  └─ Date: Mon, 15 Jan 2024 10:30:45 GMT                          │
│                                                                     │
│  Body:                                                             │
│  {                                                                 │
│    "status": false,                                               │
│    "message": "Validation failed",                                │
│    "statusCode": 422,                                             │
│    "timestamp": "2024-01-15T10:30:45.123Z",                       │
│    "data": null,                                                  │
│    "errors": [                                                    │
│      "Client name is required",                                   │
│      "Mobile number must be 10 digits",                           │
│      "Email format is invalid"                                    │
│    ]                                                              │
│  }                                                                │
└──────────────────────────┬───────────────────────────────────────┘
                           │
                           ▼
┌─────────────────────────────────────────────────────────────────────┐
│                      CLIENT RECEIVES                               │
│                                                                     │
│  Now the client knows:                                             │
│  ✓ What went wrong (3 specific validation errors)                 │
│  ✓ Status code (422 = unprocessable)                              │
│  ✓ Message (clear indication of failure)                          │
│  ✓ Timestamp (when the error occurred)                            │
│                                                                     │
│  Client can:                                                       │
│  1. Display errors to user                                        │
│  2. Highlight invalid fields                                      │
│  3. Suggest corrections                                           │
│  4. Retry with corrected data                                     │
└─────────────────────────────────────────────────────────────────────┘
```

---

## Validation Scenarios

### Scenario 1: Valid Request ✅

```
CLIENT REQUEST
│
├─ Name: "John Doe" ✓
├─ Email: "john@example.com" ✓
├─ Mobile: "9876543210" ✓
└─ ClientType: "Individual" ✓
│
▼
VALIDATION BEHAVIOR
└─ All validators pass ✓
│
▼
COMMAND HANDLER
├─ Check uniqueness ✓
├─ Insert into DB ✓
└─ Return Result.Success(guid)
│
▼
CONTROLLER RESPONSE
└─ 201 Created with data
│
▼
CLIENT RECEIVES
{
  "status": true,
  "message": "Client created successfully",
  "statusCode": 201,
  "data": "550e8400-e29b-41d4-a716-446655440000"
}
```

---

### Scenario 2: Validation Error - Format Issues ❌

```
CLIENT REQUEST
│
├─ Name: "" ❌ (empty)
├─ Email: "invalid-email" ❌ (bad format)
├─ Mobile: "123" ❌ (only 3 digits, need 10)
└─ ClientType: "Individual" ✓
│
▼
VALIDATION BEHAVIOR
│
├─ Name validation
│  └─ "Client name is required" ❌
│
├─ Email validation
│  └─ "Email format is invalid" ❌
│
├─ Mobile validation
│  └─ "Mobile number must be 10 digits" ❌
│
└─ Errors found! RETURN Result.Fail()
│
▼
CONTROLLER RESULT PROCESSING
└─ result.Errors.Count > 0 → TRUE
   └─ Return 422 Unprocessable Entity
│
▼
CLIENT RECEIVES 422
{
  "status": false,
  "message": "Validation failed",
  "statusCode": 422,
  "errors": [
    "Client name is required",
    "Email format is invalid",
    "Mobile number must be 10 digits"
  ]
}
```

---

### Scenario 3: Business Logic Error - Duplicate ❌

```
CLIENT REQUEST
│
├─ Name: "John Doe" (already exists)
├─ Email: "john.new@example.com" ✓
├─ Mobile: "9876543211" ✓
└─ ClientType: "Individual" ✓
│
▼
VALIDATION BEHAVIOR
│
├─ Name format validation ✓
├─ Email format validation ✓
├─ Mobile format validation ✓
└─ Database uniqueness check
   └─ "A client with this name already exists" ❌

RETURN Result.Fail()
│
▼
CONTROLLER RESULT PROCESSING
└─ result.Errors.Count > 0 → TRUE
   └─ Return 422 Unprocessable Entity
│
▼
CLIENT RECEIVES 422
{
  "status": false,
  "message": "Validation failed",
  "statusCode": 422,
  "errors": [
    "A client with this name already exists in the system"
  ]
}
```

---

### Scenario 4: Server Exception 🔴

```
CLIENT REQUEST
│
├─ All data valid ✓
├─ Passes validation ✓
│
▼
COMMAND HANDLER
│
├─ Try to access database ✓
├─ Database connection lost ❌
│  └─ SqlException thrown
│
▼
CONTROLLER TRY-CATCH
│
catch (Exception ex)
│
└─ 500 Internal Server Error
│
▼
CLIENT RECEIVES 500
{
  "status": false,
  "message": "An unexpected error occurred while creating the client",
  "statusCode": 500,
  "errors": [
    "The connection string is not initialized."
  ]
}
```

---

## Decision Tree: What Status Code to Return?

```
                           ┌─── Was request body null/empty?
                           │         Yes → 422 (ValidationError)
                           │         No ↓
                           │
                    ┌──────▼──────────┐
                    │ Proceed with    │
                    │ MediatR         │
                    └──────┬──────────┘
                           │
                    ┌──────▼──────────┐
                    │ Validation      │
                    │ Behavior        │
                    └──────┬──────────┘
                           │
                    ┌──────▼──────────────────┐
                    │ Errors found?           │
                    └──────┬──────────────┬───┘
                           │              │
                        Yes │              │ No
                           │              │
                        422 │              └──────────────┐
                   Unproc   │                             │
                   Entity   │                    ┌────────▼────────┐
                            │                    │ Command Handler │
                            │                    └────────┬────────┘
                            │                            │
                            │                    ┌───────▼────────────┐
                            │                    │ Success?           │
                            │                    └───┬─────────────┬───┘
                            │                   Yes  │             │ No
                            │                        │             │
                            │                     201 │             │
                            │                  Created│      ┌──────▼─────────┐
                            │                        │      │ Has errors?     │
                            │                        │      └────┬─────────┬───┘
                            │                        │      Yes  │         │ No
                            │                        │           │         │
                            │                        │        422 │      400
                            │                        │    Unproc  │     Bad Req
                            │                        │      Entity│
                            └────────────┬───────────┴───────┬────┴────┬────┘
                                         │                   │         │
                                         ▼                   ▼         ▼
                                    201 SUCCESS        422 VALIDATION 400 ERROR

────────────────────────────────────────────────────────────────────────────

CATCH BLOCKS:

        Exception Thrown
                │
        ┌───────┴────────────┐
        │                    │
   OperationCanceledException  Other Exception
        │                    │
    408 TIMEOUT          500 SERVER ERROR
```

---

## Exception Handling Chain

```
┌─────────────────────────────────────────┐
│      Exception Thrown in Handler        │
└──────────────┬──────────────────────────┘
               │
               ▼ (Bubbles up to Controller)
┌─────────────────────────────────────────┐
│    Controller Try-Catch Block           │
│                                         │
│  catch (OperationCanceledException)    │
│  ├─ Status: 408 Request Timeout        │
│  └─ Message: "Request was cancelled"   │
│                                         │
│  catch (ArgumentException)              │
│  ├─ Status: 422 Unprocessable          │
│  └─ Message: From exception            │
│                                         │
│  catch (Exception)                      │
│  ├─ Status: 500 Internal Server Error  │
│  └─ Message: "Unexpected error"        │
└─────────────────────────────────────────┘
               │
               ▼
    ┌──────────────────────┐
    │  ApiResponse Created │
    │                      │
    │  JSON Response Sent  │
    │  to Client          │
    └──────────────────────┘
```

---

## Key Takeaways

| Component | Handles | Returns |
|-----------|---------|---------|
| **ValidationBehavior** | Format/business validation | `Result.Fail()` (422) |
| **Controller Try-Catch** | Unhandled exceptions | `ApiResponse.ServerError()` (500) |
| **Controller Logic** | Null checks | `ValidationError()` (422) |
| **Command Handler** | Business logic | `Result.Success()` or `Result.Fail()` |
| **FromResult()** | Response conversion | `ApiResponse<T>` |

✅ **Result**: Clients ALWAYS receive structured, meaningful error responses!

