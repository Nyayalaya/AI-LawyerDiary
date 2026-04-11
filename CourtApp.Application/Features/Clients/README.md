# Clients Feature - CQRS Architecture

## Overview
The Clients feature is organized following the CQRS (Command Query Responsibility Segregation) pattern with clear separation of concerns.

## Directory Structure

```
Features/Clients/
├── Commands/
│   ├── CreateClient/
│   │   └── CreateClientCommand.cs
│   ├── UpdateClient/
│   │   └── UpdateClientCommand.cs
│   └── DeleteClient/
│       └── DeleteClientCommand.cs
├── Queries/
│   ├── GetClientById/
│   │   └── GetClientByIdQuery.cs
│   ├── GetAllClients/
│   │   └── GetAllClientsQuery.cs
│   └── SearchClients/
│       └── SearchClientsQuery.cs
├── Handlers/
│   ├── CreateClient/
│   │   └── CreateClientCommandHandler.cs
│   ├── UpdateClient/
│   │   └── UpdateClientCommandHandler.cs
│   ├── DeleteClient/
│   │   └── DeleteClientCommandHandler.cs
│   ├── GetClientById/
│   │   └── GetClientByIdQueryHandler.cs
│   ├── GetAllClients/
│   │   └── GetAllClientsQueryHandler.cs
│   └── SearchClients/
│       └── SearchClientsQueryHandler.cs
├── Validators/
│   ├── CreateClient/
│   │   └── CreateClientCommandValidator.cs
│   ├── UpdateClient/
│   │   └── UpdateClientCommandValidator.cs
│   ├── DeleteClient/
│   │   └── DeleteClientCommandValidator.cs
│   └── SearchClients/
│       └── SearchClientsQueryValidator.cs
├── DTOs/
│   ├── ClientCreateDto.cs
│   ├── ClientUpdateDto.cs
│   ├── ClientResponseDto.cs
│   └── ClientListDto.cs
├── Interfaces/
│   └── IClientService.cs
├── Mappings/
│   └── ClientMappingProfile.cs
└── README.md (this file)
```

## Components

### Commands
- **CreateClientCommand**: Creates a new client
- **UpdateClientCommand**: Updates an existing client
- **DeleteClientCommand**: Deletes a client

### Queries
- **GetClientByIdQuery**: Retrieves a single client by ID
- **GetAllClientsQuery**: Retrieves all clients
- **SearchClientsQuery**: Searches clients with pagination and filtering

### Handlers
Each command/query has a corresponding handler that implements the business logic:
- **CreateClientCommandHandler**: Validates and creates the client
- **UpdateClientCommandHandler**: Validates and updates the client
- **DeleteClientCommandHandler**: Validates and deletes the client
- **GetClientByIdQueryHandler**: Retrieves and maps client data
- **GetAllClientsQueryHandler**: Retrieves and maps all clients
- **SearchClientsQueryHandler**: Searches and paginates results

### Validators
Using FluentValidation for input validation:
- **CreateClientCommandValidator**: Validates create command
- **UpdateClientCommandValidator**: Validates update command
- **DeleteClientCommandValidator**: Validates delete command
- **SearchClientsQueryValidator**: Validates search query

### DTOs (Data Transfer Objects)
- **ClientCreateDto**: Request DTO for creating clients
- **ClientUpdateDto**: Request DTO for updating clients
- **ClientResponseDto**: Response DTO with full client details
- **ClientListDto**: Response DTO for list views

### Interfaces
- **IClientService**: Defines the contract for client-related operations

## Usage Example

### Creating a Client
```csharp
var command = new CreateClientCommand
{
    Name = "John Doe",
    Email = "john@example.com",
    Mobile = "9876543210",
    // ... other properties
};

var result = await mediator.Send(command);
```

### Updating a Client
```csharp
var command = new UpdateClientCommand
{
    Id = clientId,
    Name = "Jane Doe",
    // ... other properties
};

var result = await mediator.Send(command);
```

### Retrieving a Client
```csharp
var query = new GetClientByIdQuery { Id = clientId };
var result = await mediator.Send(query);
```

### Searching Clients
```csharp
var query = new SearchClientsQuery
{
    SearchTerm = "John",
    PageNumber = 1,
    PageSize = 10
};

var result = await mediator.Send(query);
```

## Validation Rules

### CreateClientCommand
- Name: Required, 2-255 characters
- Email: Required, valid email format
- Mobile: Required, 10 digits
- ClientType: Required

### UpdateClientCommand
- Id: Required (non-empty Guid)
- Name: Required, 2-255 characters
- Email: Required, valid email format
- Mobile: Required, 10 digits
- ClientType: Required

### DeleteClientCommand
- Id: Required (non-empty Guid)

### SearchClientsQuery
- PageNumber: Must be > 0
- PageSize: Must be > 0 and ≤ 100
- SearchTerm: Optional, max 100 characters

## Result Types

All handlers return `Result<T>` which includes:
- `IsSuccess`: Boolean indicating success/failure
- `Data`: The result data
- `Message`: Status message
- `StatusCode`: HTTP status code (201 for created, 404 for not found, etc.)

## Error Handling

All handlers implement try-catch blocks and return appropriate error messages:
- 404 Not Found: When client doesn't exist
- 400 Bad Request: Invalid input data
- 500 Server Error: Unexpected exceptions

## Dependencies

- MediatR: For CQRS pattern implementation
- AutoMapper: For DTO mapping
- FluentValidation: For input validation
- Entity Framework Core: For data access
