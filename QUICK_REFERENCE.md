# ⚡ Quick Reference Card - Error Handling

## 🎯 At a Glance

| Aspect | Details |
|--------|---------|
| **Validation Layer** | `ValidationBehavior` (MediatR Pipeline) |
| **Exception Handling** | `try-catch` in Controller |
| **Response Type** | `ApiResponse<T>` with status codes |
| **Status Codes** | 201 (success), 400 (error), 422 (validation), 500 (server error) |
| **Client Experience** | Structured JSON with clear error messages |

---

## 🔄 Flow Diagram (One Page)

```
Request → Controller → MediatR Pipeline → Validators → Handler → Result
                          ↓
                    If Validation Fails:
                    Return Result.Fail()
                          ↓
                    Back to Controller:
                    Convert to ApiResponse
                    with proper status code
                          ↓
                    Client receives JSON
                    with error details
```

---

## 📝 Code Template (Minimal)

```csharp
[HttpPost]
public async Task<IActionResult> CreateAsync([FromBody] YourCommand cmd)
{
    try
    {
        if (cmd == null) return ValidationError(new List<string> { "Required" });
        cmd.UserId = UserId;
        var result = await Mediator.Send(cmd, RequestAborted);

        if (result.Succeeded) return FromResult(result, 201);
        if (result.Errors?.Count > 0) 
            return StatusCode(422, ApiResponse<object>.Failure(result.Message, 422, result.Errors));
        return StatusCode(400, ApiResponse<object>.Failure(result.Message, 400));
    }
    catch (Exception ex)
    {
        return StatusCode(500, ApiResponse<object>.ServerError("Error", new List<string> { ex.Message }));
    }
}
```

---

## ✅/❌ Common Mistakes

| ❌ Wrong | ✅ Correct |
|---------|-----------|
| `throw new ValidationException()` | `return Result.Fail()` |
| `return FromResult(result)` for validation | `return StatusCode(422, ...)` |
| No null check for command | `if (cmd == null) return ValidationError()` |
| No try-catch | `try { } catch { }` |
| Return 200 for errors | Return 400/422/500 |

---

## 🚦 Status Codes Cheat Sheet

| Code | Meaning | When |
|------|---------|------|
| 201 | Created | Success ✅ |
| 400 | Bad Request | Generic error ❌ |
| 422 | Unprocessable | Validation error ⚠️ |
| 500 | Server Error | Exception 🔴 |
| 408 | Timeout | Request cancelled ⏱️ |

---

## 📊 Response Template

### Success
```json
{
  "status": true,
  "statusCode": 201,
  "message": "Created successfully",
  "data": { /* your data */ }
}
```

### Validation Error
```json
{
  "status": false,
  "statusCode": 422,
  "message": "Validation failed",
  "errors": ["error 1", "error 2"]
}
```

### Server Error
```json
{
  "status": false,
  "statusCode": 500,
  "message": "Unexpected error",
  "errors": ["exception message"]
}
```

---

## 🧪 Test Cases (3 Essential)

```csharp
// Test 1: Validation Error
[Fact]
public async Task CreateAsync_InvalidData_Returns422()
{
    var cmd = new CreateCmd { Name = "" }; // Invalid
    var result = await _client.PostAsJsonAsync("/api/v1/endpoint", cmd);
    Assert.Equal(422, result.StatusCode);
}

// Test 2: Success
[Fact]
public async Task CreateAsync_ValidData_Returns201()
{
    var cmd = new CreateCmd { Name = "Valid" }; // Valid
    var result = await _client.PostAsJsonAsync("/api/v1/endpoint", cmd);
    Assert.Equal(201, result.StatusCode);
}

// Test 3: Server Error
[Fact]
public async Task CreateAsync_ExceptionThrown_Returns500()
{
    // Setup to throw exception
    var result = await _client.PostAsJsonAsync("/api/v1/endpoint", validCmd);
    Assert.Equal(500, result.StatusCode);
}
```

---

## 🎓 Key Principles

1. **Never throw exceptions for validation** ↪️ Use `Result.Fail()`
2. **Always check for null** ↪️ Validate input first
3. **Use proper status codes** ↪️ 422 for validation, 400 for error, 500 for exception
4. **Provide error details** ↪️ Include error messages in response
5. **Catch exceptions gracefully** ↪️ Return structured response, not crash

---

## 📚 Where to Find More

| Question | Document |
|----------|----------|
| How does it work? | `ERROR_HANDLING_GUIDE.md` |
| Show me diagrams | `VALIDATION_FLOW_DIAGRAM.md` |
| Code examples? | `ERROR_HANDLING_EXAMPLES.md` |
| What changed? | `IMPLEMENTATION_SUMMARY.md` |

---

## ⚡ One-Minute Overview

**Problem**: Client doesn't know what went wrong with their request

**Solution**: 
1. Validation runs in MediatR pipeline (ValidationBehavior)
2. Returns structured `Result.Fail()` instead of throwing
3. Controller converts to `ApiResponse` with proper HTTP status code
4. Client receives JSON with clear error messages

**Result**: ✅ Happy clients who know exactly what to fix!

---

## 🚀 Deploy Checklist

- [ ] ValidationBehavior returns Result.Fail()
- [ ] Controller has try-catch
- [ ] Null body check present
- [ ] Proper status codes used (201/400/422/500)
- [ ] Error messages included in response
- [ ] ProducesResponseType attributes added
- [ ] Tests written for all scenarios
- [ ] API docs updated

---

**Last Updated**: January 2024 | **Version**: 1.0

