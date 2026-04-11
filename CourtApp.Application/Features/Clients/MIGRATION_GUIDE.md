# Clients Feature - Migration Guide

## For Developers: How to Update Your Code

If you have existing code that uses the old Clients structure, here's how to migrate:

### Old Way → New Way

#### 1. Creating a Client

**OLD:**
```csharp
var command = new ClientCreateCommand
{
    Name = "John Doe",
    // ... properties
};
var result = await mediator.Send(command);
```

**NEW:**
```csharp
var command = new CreateClientCommand
{
    Name = "John Doe",
    Email = "john@example.com",
    Mobile = "9876543210",
    ClientType = "Individual", // or "Corporation"
    UserId = userId
};

var result = await mediator.Send(command);
if (result.Succeeded)
{
    var clientId = result.Data; // Returns Guid
}
```

#### 2. Updating a Client

**OLD:**
```csharp
var command = new ClientUpdateCommand
{
    Id = clientId,
    ClientType = clientTypeEnum
};
var result = await mediator.Send(command);
```

**NEW:**
```csharp
var command = new UpdateClientCommand
{
    Id = clientId,
    ClientType = "Individual", // String, gets parsed to enum
    // ... all required properties
};

var result = await mediator.Send(command);
if (result.Succeeded)
{
    // Update was successful
}
```

#### 3. Deleting a Client

**OLD:**
```csharp
var command = new ClientDeleteCommand { Id = clientId };
var result = await mediator.Send(command);
```

**NEW:**
```csharp
var command = new DeleteClientCommand { Id = clientId };
var result = await mediator.Send(command);
if (result.Succeeded)
{
    // Deletion was successful
}
```

#### 4. Getting a Client by ID

**OLD:**
```csharp
var query = new GetClientByIdQuery { Id = clientId };
var result = await mediator.Send(query);
var response = result.Data; // Returns GetClientByIdResponse
```

**NEW:**
```csharp
var query = new GetClientByIdQuery { Id = clientId };
var result = await mediator.Send(query);
if (result.Succeeded)
{
    var client = result.Data; // Returns ClientResponseDto
}
```

#### 5. Getting All Clients

**OLD:**
```csharp
var query = new GetAllClientsQuery();
var result = await mediator.Send(query);
var clients = result.Data; // Returns List<T>
```

**NEW:**
```csharp
var query = new GetAllClientsQuery
{
    PageNumber = 1,
    PageSize = 10
};

var result = await mediator.Send(query);
if (result.Succeeded)
{
    var clients = result.Data; // Returns List<ClientListDto>
}
```

#### 6. Searching Clients

**OLD:**
```csharp
// Not available in old structure
```

**NEW:**
```csharp
var query = new SearchClientsQuery
{
    SearchTerm = "John",
    PageNumber = 1,
    PageSize = 10
};

var result = await mediator.Send(query);
if (result.Succeeded)
{
    var paginatedResult = result.Data;
    var clients = paginatedResult.Data; // List of ClientListDto
    var totalCount = paginatedResult.Pagination.TotalCount;
    var pageCount = paginatedResult.Pagination.TotalPages;
}
```

### Result Pattern Changes

**OLD:**
```csharp
public class Result<T>
{
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}
```

**NEW:**
```csharp
public class Result<T>
{
    public bool Succeeded { get; private set; }
    public string Message { get; private set; }
    public T Data { get; private set; }
    
    // Methods:
    // Result<T>.Success(data, message)
    // Result<T>.Fail(message, errors)
}
```

### DTO Changes

**OLD:**
- Direct entity mapping
- Mixed response types

**NEW:**
```csharp
// Request DTOs
public class ClientCreateDto { /* ... */ }
public class ClientUpdateDto { /* ... */ }

// Response DTOs
public class ClientResponseDto { /* full details */ }
public class ClientListDto { /* list view */ }
```

## Validation

The new structure includes automatic validation. Validators are applied through:

```csharp
// In Program.cs or Startup.cs
services.AddValidatorsFromAssembly(typeof(CreateClientCommandValidator).Assembly);
```

## AutoMapper Integration

Register the mapping profile:

```csharp
// In Program.cs or Startup.cs
services.AddAutoMapper(typeof(ClientMappingProfile).Assembly);
```

## Common Issues & Solutions

### Issue 1: "ClientType must be a string"
**Solution:** Change your code to use string for ClientType:
```csharp
command.ClientType = "Individual"; // or "Corporation"
```

### Issue 2: "Property Proprietor doesn't exist"
**Solution:** Use the correct property name:
```csharp
client.Proprietor = "John"; // not Properiter
```

### Issue 3: "InsertAsync doesn't exist"
**Solution:** Use InsertAsync instead of AddAsync:
```csharp
await _clientRepository.InsertAsync(client); // not AddAsync
```

### Issue 4: "Result.Failure doesn't exist"
**Solution:** Use Result.Fail instead:
```csharp
return Result<T>.Fail("Error message"); // not Failure
```

## Checklist for Migration

- [ ] Update command class names (e.g., ClientCreateCommand → CreateClientCommand)
- [ ] Update query class names (e.g., GetClientByIdQuery → GetClientByIdQuery)
- [ ] Change ClientType from enum to string in commands
- [ ] Update Result usage (Fail instead of Failure)
- [ ] Update repository method names (InsertAsync instead of AddAsync)
- [ ] Update DTO usage (ClientResponseDto instead of mixed types)
- [ ] Register validators in DI
- [ ] Register AutoMapper profiles
- [ ] Update API controllers with new command/query classes
- [ ] Test all CRUD operations
- [ ] Run validation tests

## Testing the New Structure

```csharp
[TestClass]
public class ClientCommandTests
{
    private IMediator _mediator;
    
    [TestInitialize]
    public void Setup()
    {
        // Setup mediator with handlers
        _mediator = new Mediator(/* ... */);
    }
    
    [TestMethod]
    public async Task CreateClient_WithValidData_ReturnsSuccess()
    {
        var command = new CreateClientCommand
        {
            Name = "Test Client",
            Email = "test@example.com",
            Mobile = "1234567890",
            ClientType = "Individual",
            UserId = Guid.NewGuid().ToString()
        };
        
        var result = await _mediator.Send(command);
        
        Assert.IsTrue(result.Succeeded);
        Assert.AreNotEqual(Guid.Empty, result.Data);
    }
    
    [TestMethod]
    public async Task CreateClient_WithInvalidEmail_ReturnsFail()
    {
        var command = new CreateClientCommand
        {
            Name = "Test Client",
            Email = "invalid-email",
            Mobile = "1234567890",
            ClientType = "Individual",
            UserId = Guid.NewGuid().ToString()
        };
        
        var result = await _mediator.Send(command);
        
        Assert.IsFalse(result.Succeeded);
    }
}
```

## Support

For issues or questions about the new structure:
1. Check the README.md in the Clients feature folder
2. Review the RESTRUCTURING_SUMMARY.md
3. Check test files for usage examples
4. Consult the CQRS pattern documentation

---

**Version**: 1.0
**Last Updated**: 2024
**Status**: Complete
