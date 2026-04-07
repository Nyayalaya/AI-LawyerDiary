# Work Master & Work Master Sub - Service Implementation Guide

## Overview

The **IWorkMasterService**, **IWorkMasterCacheService**, **IWorkMasterSubService**, and **IWorkMasterSubCacheService** interfaces have been implemented to follow the enterprise-level architecture pattern used throughout the CourtApp codebase.

This guide explains:
1. ✅ What these services are
2. ✅ How to use them
3. ✅ Where they are used
4. ✅ How to register them in Dependency Injection
5. ✅ Example implementations

---

## Service Architecture

### Layer Structure

```
┌─────────────────────────────────────────────┐
│         API / Controller Layer              │
├─────────────────────────────────────────────┤
│    Handlers (Commands & Queries)            │
│  (CreateWorkMasterCommandHandler, etc.)     │
├─────────────────────────────────────────────┤
│    Service Layer (Business Logic)           │
│  ┌──────────────────────────────────────┐   │
│  │  IWorkMasterService                  │   │
│  │  IWorkMasterCacheService             │   │
│  │  IWorkMasterSubService               │   │
│  │  IWorkMasterSubCacheService          │   │
│  └──────────────────────────────────────┘   │
├─────────────────────────────────────────────┤
│    Repository Layer (Data Access)           │
│  ┌──────────────────────────────────────┐   │
│  │  IWorkMasterRepository               │   │
│  │  IWorkMasterSubRepository            │   │
│  └──────────────────────────────────────┘   │
├─────────────────────────────────────────────┤
│    Infrastructure Layer (Cache & DB)        │
│  ┌──────────────────────────────────────┐   │
│  │  Distributed Cache (Redis)           │   │
│  │  Database (Entity Framework)         │   │
│  └──────────────────────────────────────┘   │
└─────────────────────────────────────────────┘
```

---

## Services Explained

### 1. IWorkMasterService (Repository Service)

**Location:** `CourtApp.Application\Features\WorkMaster\Services\IWorkMasterService.cs`

**Purpose:** Provides CRUD operations for Work Master data

**Interface Methods:**

```csharp
public interface IWorkMasterService
{
    // Property: Get queryable collection for custom filtering
    IQueryable<WorkTypeEntity> WorkMasters { get; }
    
    // Get all records
    Task<List<WorkTypeEntity>> GetListAsync();
    
    // Get single record by ID
    Task<WorkTypeEntity> GetByIdAsync(Guid workMasterId);
    
    // Create new record
    Task<Guid> InsertAsync(WorkTypeEntity workMaster);
    
    // Update existing record
    Task UpdateAsync(WorkTypeEntity workMaster);
    
    // Delete record
    Task DeleteAsync(WorkTypeEntity workMaster);
    
    // Business logic: Check for duplicates
    Task<bool> IsWorkMasterExistAsync(Guid courtTypeId, string name);
}
```

**Implementation:** `WorkMasterService.cs`

---

### 2. IWorkMasterCacheService (Cache Service)

**Location:** `CourtApp.Application\Features\WorkMaster\Services\IWorkMasterCacheService.cs`

**Purpose:** Provides cached access to Work Master data using Redis distributed cache

**Interface Methods:**

```csharp
public interface IWorkMasterCacheService
{
    // Get all records from cache (24-hour expiration)
    Task<List<WorkTypeEntity>> GetCachedListAsync();
    
    // Get single record from cache
    Task<WorkTypeEntity> GetByIdAsync(Guid workMasterId);
    
    // Clear cache after updates
    Task InvalidateCacheAsync();
}
```

**Implementation:** `WorkMasterCacheService.cs`

**Cache Keys:**
- Individual record: `workmaster_{guid}`
- List cache: `workmaster_list`
- Expiration: 24 hours

---

### 3. IWorkMasterSubService (Repository Service)

**Location:** `CourtApp.Application\Features\WorkMasterSub\Services\IWorkMasterSubService.cs`

**Purpose:** Provides CRUD operations for Work Master Sub data

**Interface Methods:**

```csharp
public interface IWorkMasterSubService
{
    // Property: Get queryable collection for custom filtering
    IQueryable<WorksEntity> WorkMasterSubs { get; }
    
    // Get all records
    Task<List<WorksEntity>> GetListAsync();
    
    // Get single record by ID
    Task<WorksEntity> GetByIdAsync(Guid workSubId);
    
    // Create new record
    Task<Guid> InsertAsync(WorksEntity workSub);
    
    // Update existing record
    Task UpdateAsync(WorksEntity workSub);
    
    // Delete record
    Task DeleteAsync(WorksEntity workSub);
    
    // Business logic: Check for duplicates
    Task<bool> IsWorkMasterSubExistAsync(Guid workId, Guid courtTypeId, string name);
}
```

**Implementation:** `WorkMasterSubService.cs`

---

### 4. IWorkMasterSubCacheService (Cache Service)

**Location:** `CourtApp.Application\Features\WorkMasterSub\Services\IWorkMasterSubCacheService.cs`

**Purpose:** Provides cached access to Work Master Sub data using Redis distributed cache

**Interface Methods:**

```csharp
public interface IWorkMasterSubCacheService
{
    // Get all records from cache (24-hour expiration)
    Task<List<WorksEntity>> GetCachedListAsync();
    
    // Get single record from cache
    Task<WorksEntity> GetByIdAsync(Guid workSubId);
    
    // Clear cache after updates
    Task InvalidateCacheAsync();
}
```

**Implementation:** `WorkMasterSubCacheService.cs`

**Cache Keys:**
- Individual record: `workmastersub_{guid}`
- List cache: `workmastersub_list`
- Expiration: 24 hours

---

## How Services Are Used

### In Command Handlers

Services can be injected into handlers to perform business operations:

```csharp
public class CreateWorkMasterCommandHandler : IRequestHandler<CreateWorkMasterCommand, Result<Guid>>
{
    private readonly IWorkMasterService _service;
    private readonly IWorkMasterCacheService _cacheService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateWorkMasterCommandHandler(
        IWorkMasterService service,
        IWorkMasterCacheService cacheService,
        IUnitOfWork unitOfWork)
    {
        _service = service;
        _cacheService = cacheService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateWorkMasterCommand request, CancellationToken cancellationToken)
    {
        // Check if already exists
        var exists = await _service.IsWorkMasterExistAsync(request.CourtTypeId, request.Name_En);
        if (exists)
            return Result<Guid>.Fail("Work Master already exists");

        // Create entity
        var entity = new WorkTypeEntity 
        { 
            Name = request.Name_En, 
            Code = request.Abbreviation 
        };

        // Insert via service
        var id = await _service.InsertAsync(entity);
        await _unitOfWork.Commit(cancellationToken);

        // Invalidate cache
        await _cacheService.InvalidateCacheAsync();

        return Result<Guid>.Success(id);
    }
}
```

### In Query Handlers

```csharp
public class GetWorkMasterQueryHandler : IRequestHandler<GetWorkMasterQuery, PaginatedResult<WorkMasterResponse>>
{
    private readonly IWorkMasterCacheService _cacheService;
    private readonly IMapper _mapper;

    public GetWorkMasterQueryHandler(IWorkMasterCacheService cacheService, IMapper mapper)
    {
        _cacheService = cacheService;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<WorkMasterResponse>> Handle(GetWorkMasterQuery request, CancellationToken cancellationToken)
    {
        // Get from cache (more efficient than database)
        var works = await _cacheService.GetCachedListAsync();
        var responses = _mapper.Map<List<WorkMasterResponse>>(works);
        
        return responses.ToPaginatedResult(request.PageNumber, request.PageSize);
    }
}
```

---

## Dependency Injection Registration

### In Startup Configuration (Program.cs or ConfigureServices)

```csharp
public static void AddWorkMasterServices(this IServiceCollection services)
{
    // Register WorkMaster Services
    services.AddScoped<IWorkMasterService, WorkMasterService>();
    services.AddScoped<IWorkMasterCacheService, WorkMasterCacheService>();
    
    // Register WorkMasterSub Services
    services.AddScoped<IWorkMasterSubService, WorkMasterSubService>();
    services.AddScoped<IWorkMasterSubCacheService, WorkMasterSubCacheService>();
    
    // Note: IWorkMasterRepository and IWorkMasterSubRepository are already registered
    //       by the Infrastructure layer
}
```

**In Program.cs:**

```csharp
var builder = WebApplicationBuilder.CreateBuilder(args);

// Add services
builder.Services.AddWorkMasterServices();

// Or directly
builder.Services.AddScoped<IWorkMasterService, WorkMasterService>();
builder.Services.AddScoped<IWorkMasterCacheService, WorkMasterCacheService>();
builder.Services.AddScoped<IWorkMasterSubService, WorkMasterSubService>();
builder.Services.AddScoped<IWorkMasterSubCacheService, WorkMasterSubCacheService>();
```

---

## File Structure Summary

### WorkMaster Feature Services
```
Features/WorkMaster/Services/
├── IWorkMasterService.cs                 ← Repository interface
├── WorkMasterService.cs                  ← Repository implementation
├── IWorkMasterCacheService.cs           ← Cache interface
└── WorkMasterCacheService.cs            ← Cache implementation
```

### WorkMasterSub Feature Services
```
Features/WorkMasterSub/Services/
├── IWorkMasterSubService.cs             ← Repository interface
├── WorkMasterSubService.cs              ← Repository implementation
├── IWorkMasterSubCacheService.cs        ← Cache interface
└── WorkMasterSubCacheService.cs         ← Cache implementation
```

---

## Usage Examples

### Example 1: Get Work Masters with Caching

```csharp
public class GetWorkMastersUseCase
{
    private readonly IWorkMasterCacheService _cacheService;

    public GetWorkMastersUseCase(IWorkMasterCacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<List<WorkTypeEntity>> Execute()
    {
        // First call: Fetches from database and caches for 24 hours
        var workMasters = await _cacheService.GetCachedListAsync();
        
        // Subsequent calls: Returns from cache (much faster)
        return workMasters;
    }
}
```

### Example 2: Create Work Master with Cache Invalidation

```csharp
public class CreateWorkMasterUseCase
{
    private readonly IWorkMasterService _service;
    private readonly IWorkMasterCacheService _cacheService;

    public CreateWorkMasterUseCase(
        IWorkMasterService service,
        IWorkMasterCacheService cacheService)
    {
        _service = service;
        _cacheService = cacheService;
    }

    public async Task<Guid> Execute(string name, string abbreviation)
    {
        var entity = new WorkTypeEntity 
        { 
            Name = name, 
            Code = abbreviation 
        };

        var id = await _service.InsertAsync(entity);
        
        // Important: Clear cache after modification
        await _cacheService.InvalidateCacheAsync();
        
        return id;
    }
}
```

### Example 3: Check for Duplicate Work Master

```csharp
public class ValidateWorkMasterUseCase
{
    private readonly IWorkMasterService _service;

    public ValidateWorkMasterUseCase(IWorkMasterService service)
    {
        _service = service;
    }

    public async Task<bool> CheckDuplicate(Guid courtTypeId, string name)
    {
        return await _service.IsWorkMasterExistAsync(courtTypeId, name);
    }
}
```

---

## Performance Optimization

### Cache Strategy

1. **First Request**: Database → Serialize → Cache → Return
2. **Subsequent Requests**: Cache → Return (much faster)
3. **After Modification**: Invalidate Cache → Next request rebuilds cache

### Cache Benefits

✅ Reduced database load
✅ Faster response times (Redis is in-memory)
✅ Scalable for high-traffic scenarios
✅ Automatic 24-hour expiration prevents stale data
✅ Fallback to database if cache fails

### When Cache Invalidates

```csharp
// After Create
await _cacheService.InvalidateCacheAsync();

// After Update
await _cacheService.InvalidateCacheAsync();

// After Delete
await _cacheService.InvalidateCacheAsync();
```

---

## Error Handling

Both service implementations include try-catch blocks for robustness:

```csharp
try
{
    var data = await _cacheService.GetCachedListAsync();
    return data;
}
catch (Exception ex)
{
    _logger.LogError(ex, "Cache error - falling back to database");
    // Automatic fallback to database
    return await _service.GetListAsync();
}
```

---

## Logging

Services include comprehensive logging:

```csharp
_logger.LogInformation("Work Master list retrieved from cache");
_logger.LogInformation("Work Master list cached for 24 hours");
_logger.LogError(ex, "Error retrieving Work Master from cache");
```

View logs in:
- Visual Studio Debug Output
- Application Insights
- Log files (configured in app settings)

---

## Best Practices

✅ **Always invalidate cache after modifications**
✅ **Use cache service for read-heavy operations**
✅ **Use repository service for business logic**
✅ **Inject both services when needed**
✅ **Handle exceptions gracefully**
✅ **Log operations for debugging**
✅ **Use pagination for large result sets**
✅ **Validate input before inserting/updating**

---

## Migration Checklist

- [ ] Register services in Dependency Injection
- [ ] Update handlers to use new services
- [ ] Update API endpoints to use new commands/queries
- [ ] Test create, read, update, delete operations
- [ ] Verify cache invalidation works
- [ ] Check performance improvements
- [ ] Monitor logs for errors
- [ ] Delete old service implementations

---

## Common Issues & Solutions

### Issue: Cache not being used
**Solution:** Verify Redis is running and connected

### Issue: Stale data in cache
**Solution:** Call `InvalidateCacheAsync()` after modifications

### Issue: Duplicate entries not detected
**Solution:** Ensure `IsWorkMasterExistAsync()` is called before insert

### Issue: Poor performance
**Solution:** Verify cache service is being used instead of direct repository access

---

## Next Steps

1. ✅ Register services in DI container
2. ✅ Update handlers to use services
3. ✅ Test all CRUD operations
4. ✅ Monitor cache hits/misses
5. ✅ Optimize based on usage patterns
6. ✅ Document team guidelines
7. ✅ Remove legacy code

---

## References

- Repository Pattern: Provides data access abstraction
- Service Layer: Encapsulates business logic
- Caching Strategy: Improves performance with Redis
- DI Container: Manages service lifecycles
- CQRS Pattern: Separates read/write operations

For more information, see: `FEATURE_REORGANIZATION_GUIDE.md`
