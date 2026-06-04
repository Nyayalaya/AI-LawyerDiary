# 📚 Error Handling - Practical Code Examples

## Quick Reference: Copy-Paste Templates

### Template 1: Basic Endpoint with Error Handling

```csharp
[HttpPost]
[ProducesResponseType(typeof(ApiResponse<Guid>), 201)]
[ProducesResponseType(typeof(ApiResponse<object>), 400)]
[ProducesResponseType(typeof(ApiResponse<object>), 422)]
[ProducesResponseType(typeof(ApiResponse<object>), 500)]
public async Task<IActionResult> CreateAsync([FromBody] YourCommand command)
{
    try
    {
        // 1. Null check
        if (command == null)
            return ValidationError(new List<string> { "Request body is required" });

        // 2. Set user context
        command.UserId = UserId;

        // 3. Send through pipeline
        var result = await Mediator.Send(command, RequestAborted);

        // 4. Handle result
        if (result.Succeeded)
            return FromResult(result, successCode: 201);

        if (result.Errors?.Count > 0)
            return StatusCode(422,
                ApiResponse<object>.Failure(
                    result.Message ?? "Validation failed",
                    422,
                    result.Errors));

        return StatusCode(400,
            ApiResponse<object>.Failure(
                result.Message ?? "Operation failed",
                400));
    }
    catch (OperationCanceledException ex)
    {
        return StatusCode(408,
            ApiResponse<object>.Failure(
                "Request was cancelled",
                408,
                new List<string> { ex.Message }));
    }
    catch (Exception ex)
    {
        return StatusCode(500,
            ApiResponse<object>.ServerError(
                "An unexpected error occurred",
                new List<string> { ex.Message }));
    }
}
```

---

### Template 2: GET Endpoint with Error Handling

```csharp
[HttpGet("{id:guid}")]
[ProducesResponseType(typeof(ApiResponse<YourDto>), 200)]
[ProducesResponseType(typeof(ApiResponse<object>), 404)]
[ProducesResponseType(typeof(ApiResponse<object>), 500)]
public async Task<IActionResult> GetByIdAsync(Guid id)
{
    try
    {
        // 1. Validate ID
        if (id == Guid.Empty)
            return ValidationError(new List<string> { "Invalid ID" });

        // 2. Create query
        var query = new GetYourQuery { Id = id };

        // 3. Send query
        var result = await Mediator.Send(query, RequestAborted);

        // 4. Handle result
        if (result.Succeeded)
            return FromResult(result);

        // 5. Check if not found
        if (result.Message?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
            return NotFoundResponse(result.Message);

        return StatusCode(400,
            ApiResponse<object>.Failure(result.Message ?? "Failed to retrieve", 400));
    }
    catch (Exception ex)
    {
        return StatusCode(500,
            ApiResponse<object>.ServerError(
                "An unexpected error occurred",
                new List<string> { ex.Message }));
    }
}
```

---

### Template 3: PUT Endpoint with Error Handling

```csharp
[HttpPut("{id:guid}")]
[ProducesResponseType(typeof(ApiResponse<object>), 200)]
[ProducesResponseType(typeof(ApiResponse<object>), 400)]
[ProducesResponseType(typeof(ApiResponse<object>), 404)]
[ProducesResponseType(typeof(ApiResponse<object>), 422)]
[ProducesResponseType(typeof(ApiResponse<object>), 500)]
public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateYourCommand command)
{
    try
    {
        // 1. Validate ID
        if (id == Guid.Empty)
            return ValidationError(new List<string> { "Invalid ID" });

        // 2. Null check
        if (command == null)
            return ValidationError(new List<string> { "Request body is required" });

        // 3. Set ID and user
        command.Id = id;
        command.UserId = UserId;

        // 4. Send command
        var result = await Mediator.Send(command, RequestAborted);

        // 5. Handle result
        if (result.Succeeded)
            return Ok(ApiResponse<object>.Success(null, "Updated successfully"));

        if (result.Errors?.Count > 0)
            return StatusCode(422,
                ApiResponse<object>.Failure(
                    result.Message ?? "Validation failed",
                    422,
                    result.Errors));

        if (result.Message?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
            return NotFoundResponse(result.Message);

        return StatusCode(400,
            ApiResponse<object>.Failure(result.Message ?? "Update failed", 400));
    }
    catch (Exception ex)
    {
        return StatusCode(500,
            ApiResponse<object>.ServerError(
                "An unexpected error occurred",
                new List<string> { ex.Message }));
    }
}
```

---

### Template 4: DELETE Endpoint with Error Handling

```csharp
[HttpDelete("{id:guid}")]
[ProducesResponseType(typeof(ApiResponse<object>), 200)]
[ProducesResponseType(typeof(ApiResponse<object>), 404)]
[ProducesResponseType(typeof(ApiResponse<object>), 500)]
public async Task<IActionResult> DeleteAsync(Guid id)
{
    try
    {
        // 1. Validate ID
        if (id == Guid.Empty)
            return ValidationError(new List<string> { "Invalid ID" });

        // 2. Create command
        var command = new DeleteYourCommand { Id = id };

        // 3. Send command
        var result = await Mediator.Send(command, RequestAborted);

        // 4. Handle result
        if (result.Succeeded)
            return Ok(ApiResponse<object>.Success(null, "Deleted successfully"));

        if (result.Message?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
            return NotFoundResponse(result.Message);

        return StatusCode(400,
            ApiResponse<object>.Failure(result.Message ?? "Delete failed", 400));
    }
    catch (Exception ex)
    {
        return StatusCode(500,
            ApiResponse<object>.ServerError(
                "An unexpected error occurred",
                new List<string> { ex.Message }));
    }
}
```

---

## Real-World Scenario: Complete Example

### Scenario: Create a Case with Full Error Handling

```csharp
[HttpPost]
[ProducesResponseType(typeof(ApiResponse<Guid>), 201)]
[ProducesResponseType(typeof(ApiResponse<object>), 400)]
[ProducesResponseType(typeof(ApiResponse<object>), 422)]
[ProducesResponseType(typeof(ApiResponse<object>), 500)]
public async Task<IActionResult> CreateCaseAsync([FromBody] CreateCaseCommand command)
{
    try
    {
        // STEP 1: Validate request body exists
        if (command == null)
            return ValidationError(
                new List<string> { "Request body is required" },
                "Invalid request");

        // STEP 2: Set audit information
        command.UserId = UserId;
        command.CreatedAt = DateTime.UtcNow;

        // STEP 3: Log request (optional - for audit trail)
        // _logger.LogInformation($"Creating case for user {UserId}. Correlation: {CorrelationId}");

        // STEP 4: Send through MediatR pipeline
        // This is where ValidationBehavior runs
        Result<Guid> result = await Mediator.Send(command, RequestAborted);

        // STEP 5: Handle different result scenarios

        // Scenario A: Success
        if (result.Succeeded)
        {
            return FromResult(result, successCode: 201);
            /* Returns:
            {
              "status": true,
              "message": "Case created successfully",
              "statusCode": 201,
              "data": "550e8400-e29b-41d4-a716-446655440000"
            }
            */
        }

        // Scenario B: Validation/Business Logic Errors (has error list)
        if (result.Errors?.Count > 0)
        {
            return StatusCode((int)HttpStatusCode.UnprocessableEntity,
                ApiResponse<object>.Failure(
                    result.Message ?? "Validation failed",
                    422,
                    result.Errors));
            /* Returns:
            {
              "status": false,
              "message": "Validation failed",
              "statusCode": 422,
              "errors": [
                "Case number already exists",
                "Filing date cannot be in future"
              ]
            }
            */
        }

        // Scenario C: Generic error (no error list)
        return StatusCode((int)HttpStatusCode.BadRequest,
            ApiResponse<object>.Failure(
                result.Message ?? "Failed to create case",
                400));
        /* Returns:
        {
          "status": false,
          "message": "Failed to create case",
          "statusCode": 400
        }
        */
    }
    catch (OperationCanceledException ex)
    {
        // Request timeout
        return StatusCode((int)HttpStatusCode.RequestTimeout,
            ApiResponse<object>.Failure(
                "Request was cancelled or timed out",
                408,
                new List<string> { ex.Message }));
        /* Returns:
        {
          "status": false,
          "message": "Request was cancelled or timed out",
          "statusCode": 408,
          "errors": ["The operation was cancelled."]
        }
        */
    }
    catch (ArgumentException ex)
    {
        // Invalid arguments
        return ValidationError(
            new List<string> { ex.Message },
            "Argument validation failed");
        /* Returns:
        {
          "status": false,
          "message": "Argument validation failed",
          "statusCode": 422,
          "errors": ["Argument was invalid"]
        }
        */
    }
    catch (InvalidOperationException ex)
    {
        // Business logic violation
        return StatusCode((int)HttpStatusCode.BadRequest,
            ApiResponse<object>.Failure(
                "Invalid operation",
                400,
                new List<string> { ex.Message }));
    }
    catch (Exception ex)
    {
        // Unexpected error
        // In production: Log this with full stack trace and correlation ID
        return StatusCode((int)HttpStatusCode.InternalServerError,
            ApiResponse<object>.ServerError(
                "An unexpected error occurred while creating the case",
                new List<string> { ex.Message }));
        /* Returns:
        {
          "status": false,
          "message": "An unexpected error occurred while creating the case",
          "statusCode": 500,
          "errors": ["NullReferenceException: ..."]
        }
        */
    }
}
```

---

## Test Cases with Expected Responses

### Test 1: Null Body
```csharp
[Fact]
public async Task CreateCase_WithNullBody_Returns422()
{
    // Arrange
    CreateCaseCommand? command = null;

    // Act
    var result = await _client.PostAsJsonAsync("/api/v1/case", command);

    // Assert
    Assert.Equal(HttpStatusCode.UnprocessableEntity, result.StatusCode);
    var content = await result.Content.ReadAsAsync<ApiResponse<object>>();
    Assert.False(content.Status);
    Assert.Contains("Request body is required", content.Errors?[0]);
}
```

### Test 2: Validation Error
```csharp
[Fact]
public async Task CreateCase_WithInvalidData_Returns422()
{
    // Arrange
    var command = new CreateCaseCommand
    {
        CaseNumber = "", // Empty
        FilingDate = DateTime.UtcNow.AddDays(1) // Future date
    };

    // Act
    var result = await _client.PostAsJsonAsync("/api/v1/case", command);

    // Assert
    Assert.Equal(HttpStatusCode.UnprocessableEntity, result.StatusCode);
    var content = await result.Content.ReadAsAsync<ApiResponse<object>>();
    Assert.False(content.Status);
    Assert.Contains("Case number is required", content.Errors?[0]);
    Assert.Contains("Filing date cannot be in future", content.Errors?[1]);
}
```

### Test 3: Success
```csharp
[Fact]
public async Task CreateCase_WithValidData_Returns201()
{
    // Arrange
    var command = new CreateCaseCommand
    {
        CaseNumber = "2024/001",
        FilingDate = DateTime.UtcNow.AddDays(-1)
    };

    // Act
    var result = await _client.PostAsJsonAsync("/api/v1/case", command);

    // Assert
    Assert.Equal(HttpStatusCode.Created, result.StatusCode);
    var content = await result.Content.ReadAsAsync<ApiResponse<Guid>>();
    Assert.True(content.Status);
    Assert.NotEqual(Guid.Empty, content.Data);
}
```

### Test 4: Server Error
```csharp
[Fact]
public async Task CreateCase_WhenDatabaseFails_Returns500()
{
    // Arrange
    _mockRepository.Setup(x => x.InsertAsync(It.IsAny<CaseEntity>()))
        .ThrowsAsync(new SqlException("Connection failed"));

    var command = new CreateCaseCommand { /* valid data */ };

    // Act
    var result = await _client.PostAsJsonAsync("/api/v1/case", command);

    // Assert
    Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    var content = await result.Content.ReadAsAsync<ApiResponse<object>>();
    Assert.False(content.Status);
}
```

---

## Common Mistakes & How to Fix Them

### ❌ WRONG: Not handling null body
```csharp
public async Task<IActionResult> CreateAsync([FromBody] CreateClientCommand command)
{
    // Will crash if command is null!
    var result = await Mediator.Send(command, RequestAborted);
}
```

### ✅ CORRECT: Validate null body
```csharp
public async Task<IActionResult> CreateAsync([FromBody] CreateClientCommand command)
{
    if (command == null)
        return ValidationError(new List<string> { "Request body is required" });

    var result = await Mediator.Send(command, RequestAborted);
}
```

---

### ❌ WRONG: Not catching exceptions
```csharp
public async Task<IActionResult> CreateAsync([FromBody] CreateClientCommand command)
{
    var result = await Mediator.Send(command, RequestAborted);
    return FromResult(result, successCode: 201);
    // If exception thrown, returns 500 unstructured error
}
```

### ✅ CORRECT: Proper exception handling
```csharp
try
{
    var result = await Mediator.Send(command, RequestAborted);
    return FromResult(result, successCode: 201);
}
catch (Exception ex)
{
    return StatusCode(500,
        ApiResponse<object>.ServerError("Error occurred", new List<string> { ex.Message }));
}
```

---

### ❌ WRONG: Ignoring validation errors
```csharp
var result = await Mediator.Send(command, RequestAborted);
return FromResult(result); // Returns 400, but should be 422 for validation
```

### ✅ CORRECT: Proper status code for validation
```csharp
var result = await Mediator.Send(command, RequestAborted);
if (result.Errors?.Count > 0)
    return StatusCode(422, 
        ApiResponse<object>.Failure(result.Message, 422, result.Errors));
```

---

## Checklist: Before Deploying Your Endpoint

- [ ] ✅ Null body check implemented
- [ ] ✅ Try-catch block for unhandled exceptions
- [ ] ✅ Specific exception handling (OperationCanceledException, etc.)
- [ ] ✅ Proper status codes (201/200 for success, 422 for validation, 400 for error, 500 for server error)
- [ ] ✅ Error list included in response when available
- [ ] ✅ ProducesResponseType attributes added for all scenarios
- [ ] ✅ Logging added (at least for errors in production)
- [ ] ✅ Unit tests for success and error scenarios
- [ ] ✅ Integration tests for API responses

