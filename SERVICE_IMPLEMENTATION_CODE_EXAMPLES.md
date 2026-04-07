# Service Implementation - Practical Code Examples

## Complete Working Examples

---

## Example 1: Create Work Master with Service

### Before (Without Service Pattern)
```csharp
public class CreateWorkMasterCommandHandler : IRequestHandler<CreateWorkMasterCommand, Result<Guid>>
{
    private readonly IWorkMasterRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public async Task<Result<Guid>> Handle(CreateWorkMasterCommand request, CancellationToken cancellationToken)
    {
        // Business logic directly in handler - HARD TO TEST
        var wmdt = _repository.Entities
            .Where(x => x.Name.Equals(request.Name_En) && x.CourtTypeId == request.CourtTypeId)
            .FirstOrDefault();
        if (wmdt != null) 
            return Result<Guid>.Fail("This work type already exists!");
        
        var entity = new WorkTypeEntity 
        { 
            Name = request.Name_En, 
            Code = request.Abbreviation,
            CourtTypeId = request.CourtTypeId
        };
        
        await _repository.InsertAsync(entity);
        await _unitOfWork.Commit(cancellationToken);
        return Result<Guid>.Success(entity.Id);
    }
}
```

### After (With Service Pattern) ✅
```csharp
public class CreateWorkMasterCommandHandler : IRequestHandler<CreateWorkMasterCommand, Result<Guid>>
{
    private readonly IWorkMasterService _service;
    private readonly IWorkMasterCacheService _cacheService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateWorkMasterCommandHandler(
        IWorkMasterService service,
        IWorkMasterCacheService cacheService,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _service = service;
        _cacheService = cacheService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateWorkMasterCommand request, CancellationToken cancellationToken)
    {
        // Use service for business logic - CLEAN AND REUSABLE
        var exists = await _service.IsWorkMasterExistAsync(request.CourtTypeId, request.Name_En);
        if (exists)
            return Result<Guid>.Fail("This work type already exists!");
        
        // Map command to entity
        var entity = _mapper.Map<WorkTypeEntity>(request);
        
        // Use service to insert
        var id = await _service.InsertAsync(entity);
        await _unitOfWork.Commit(cancellationToken);
        
        // Invalidate cache for fresh data on next read
        await _cacheService.InvalidateCacheAsync();
        
        return Result<Guid>.Success(id);
    }
}
```

**Benefits:**
- ✅ Business logic in service (reusable)
- ✅ Automatic cache invalidation
- ✅ Duplicate checking delegated to service
- ✅ Easy to unit test
- ✅ Code is cleaner and more maintainable

---

## Example 2: Get Work Masters with Caching

### Before (Without Cache Service)
```csharp
public class GetWorkMasterQueryHandler : IRequestHandler<GetWorkMasterQuery, PaginatedResult<WorkMasterResponse>>
{
    private readonly IWorkMasterRepository _repository;
    private readonly IMapper _mapper;

    public async Task<PaginatedResult<WorkMasterResponse>> Handle(GetWorkMasterQuery request, CancellationToken cancellationToken)
    {
        // EVERY request hits the database - PERFORMANCE ISSUE
        var works = await _repository.GetListAsync();
        var responses = _mapper.Map<List<WorkMasterResponse>>(works);
        return responses.ToPaginatedResult(request.PageNumber, request.PageSize);
    }
}
```

### After (With Cache Service) ✅
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
        // First request: database → cache
        // Subsequent requests: cache → instant response (5ms vs 150ms!)
        var works = await _cacheService.GetCachedListAsync();
        var responses = _mapper.Map<List<WorkMasterResponse>>(works);
        return responses.ToPaginatedResult(request.PageNumber, request.PageSize);
    }
}
```

**Performance Improvement:**
- Request 1: 200ms (database + cache)
- Request 2-1000: 5ms each (cache hit)
- **Total: ~95% faster for typical usage!**

---

## Example 3: Update Work Master

```csharp
public class UpdateWorkMasterCommandHandler : IRequestHandler<UpdateWorkMasterCommand, Result<Guid>>
{
    private readonly IWorkMasterService _service;
    private readonly IWorkMasterCacheService _cacheService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateWorkMasterCommandHandler(
        IWorkMasterService service,
        IWorkMasterCacheService cacheService,
        IUnitOfWork unitOfWork)
    {
        _service = service;
        _cacheService = cacheService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(UpdateWorkMasterCommand request, CancellationToken cancellationToken)
    {
        // Get the record
        var entity = await _service.GetByIdAsync(request.Id);
        if (entity == null)
            return Result<Guid>.Fail("Work Master not found.");

        // Check for duplicate (excluding current record)
        var exists = await _service.IsWorkMasterExistAsync(request.CourtTypeId, request.Name_En);
        if (exists && entity.Name != request.Name_En)
            return Result<Guid>.Fail("Another work type with the same name already exists.");

        // Update fields
        entity.Name = request.Name_En;
        entity.Code = request.Abbreviation;
        entity.CourtTypeId = request.CourtTypeId;

        // Persist changes
        await _service.UpdateAsync(entity);
        await _unitOfWork.Commit(cancellationToken);

        // ⚠️ CRITICAL: Invalidate cache so next GET returns updated data
        await _cacheService.InvalidateCacheAsync();

        return Result<Guid>.Success(entity.Id);
    }
}
```

**Key Points:**
- ✅ Use service for CRUD operations
- ✅ Validate before updating
- ✅ Always invalidate cache after modification
- ✅ Provides immediate consistency

---

## Example 4: Delete Work Master

```csharp
public class DeleteWorkMasterCommandHandler : IRequestHandler<DeleteWorkMasterCommand, Result<Guid>>
{
    private readonly IWorkMasterService _service;
    private readonly IWorkMasterCacheService _cacheService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteWorkMasterCommandHandler(
        IWorkMasterService service,
        IWorkMasterCacheService cacheService,
        IUnitOfWork unitOfWork)
    {
        _service = service;
        _cacheService = cacheService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DeleteWorkMasterCommand request, CancellationToken cancellationToken)
    {
        // Verify record exists
        var entity = await _service.GetByIdAsync(request.Id);
        if (entity == null)
            return Result<Guid>.Fail("Work Master not found.");

        // Delete the record
        await _service.DeleteAsync(entity);
        await _unitOfWork.Commit(cancellationToken);

        // Invalidate cache
        await _cacheService.InvalidateCacheAsync();

        return Result<Guid>.Success(entity.Id);
    }
}
```

---

## Example 5: Get Work Master by ID with Cache

```csharp
public class GetWorkMasterByIdQueryHandler : IRequestHandler<GetWorkMasterByIdQuery, Result<WorkMasterByIdResponse>>
{
    private readonly IWorkMasterCacheService _cacheService;
    private readonly IMapper _mapper;

    public GetWorkMasterByIdQueryHandler(IWorkMasterCacheService cacheService, IMapper mapper)
    {
        _cacheService = cacheService;
        _mapper = mapper;
    }

    public async Task<Result<WorkMasterByIdResponse>> Handle(GetWorkMasterByIdQuery request, CancellationToken cancellationToken)
    {
        // Get from cache (or database if not cached)
        var entity = await _cacheService.GetByIdAsync(request.Id);
        if (entity == null)
            return Result<WorkMasterByIdResponse>.Fail("Work Master not found.");

        var response = _mapper.Map<WorkMasterByIdResponse>(entity);
        return Result<WorkMasterByIdResponse>.Success(response);
    }
}
```

---

## Example 6: WorkMasterSub - Complete Example

### Create Work Master Sub
```csharp
public class CreateWorkSubMasterCommandHandler : IRequestHandler<CreateWorkSubMasterCommand, Result<Guid>>
{
    private readonly IWorkMasterSubService _service;
    private readonly IWorkMasterSubCacheService _cacheService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateWorkSubMasterCommandHandler(
        IWorkMasterSubService service,
        IWorkMasterSubCacheService cacheService,
        IUnitOfWork unitOfWork)
    {
        _service = service;
        _cacheService = cacheService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateWorkSubMasterCommand request, CancellationToken cancellationToken)
    {
        if (request.Works == null || !request.Works.Any())
            return Result<Guid>.Fail("Work items are not supplied!");

        Guid lastInsertedId = Guid.Empty;

        foreach (var item in request.Works)
        {
            // Check for duplicate
            var exists = await _service.IsWorkMasterSubExistAsync(
                request.WorkId, 
                request.CourtTypeId, 
                item.Name_En);
            
            if (exists)
                return Result<Guid>.Fail($"The name '{item.Name_En}' already exists.");

            // Create entity
            var entity = new WorksEntity
            {
                Name = item.Name_En.ToUpper().Trim(),
                Code = item.Abbreviation?.Trim(),
                WorkId = request.WorkId,
                CourtTypeId = request.CourtTypeId
            };

            // Insert via service
            var id = await _service.InsertAsync(entity);
            lastInsertedId = id;
        }

        await _unitOfWork.Commit(cancellationToken);
        
        // Invalidate cache
        await _cacheService.InvalidateCacheAsync();

        return Result<Guid>.Success(lastInsertedId);
    }
}
```

---

## Example 7: Unit Testing with Services

### Test Without Services (Hard to Test)
```csharp
[TestClass]
public class WorkMasterHandlerTests
{
    [TestMethod]
    public async Task CreateHandler_ShouldFail_WhenDuplicate()
    {
        // Hard to test - too many dependencies
        var mockRepo = new Mock<IWorkMasterRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockMapper = new Mock<IMapper>();
        
        // Complex setup... lots of mocking...
        var handler = new CreateWorkMasterCommandHandler(mockRepo.Object, mockUnitOfWork.Object, mockMapper.Object);
        // ...
    }
}
```

### Test With Services (Easy to Test) ✅
```csharp
[TestClass]
public class WorkMasterHandlerTests
{
    private Mock<IWorkMasterService> _mockService;
    private Mock<IWorkMasterCacheService> _mockCacheService;
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private CreateWorkMasterCommandHandler _handler;

    [TestInitialize]
    public void Setup()
    {
        _mockService = new Mock<IWorkMasterService>();
        _mockCacheService = new Mock<IWorkMasterCacheService>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        
        _handler = new CreateWorkMasterCommandHandler(
            _mockService.Object,
            _mockCacheService.Object,
            _mockUnitOfWork.Object);
    }

    [TestMethod]
    public async Task CreateCommand_ShouldFail_WhenDuplicate()
    {
        // Arrange
        var command = new CreateWorkMasterCommand 
        { 
            Name_En = "Hearing", 
            CourtTypeId = Guid.NewGuid() 
        };
        
        _mockService
            .Setup(x => x.IsWorkMasterExistAsync(It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync(true); // Simulate duplicate

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual("This work type already exists!", result.Message);
        
        // Verify cache wasn't invalidated
        _mockCacheService.Verify(x => x.InvalidateCacheAsync(), Times.Never);
    }

    [TestMethod]
    public async Task CreateCommand_ShouldSucceed_WhenNew()
    {
        // Arrange
        var command = new CreateWorkMasterCommand 
        { 
            Name_En = "Hearing", 
            CourtTypeId = Guid.NewGuid() 
        };
        
        _mockService
            .Setup(x => x.IsWorkMasterExistAsync(It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync(false);
        
        _mockService
            .Setup(x => x.InsertAsync(It.IsAny<WorkTypeEntity>()))
            .ReturnsAsync(Guid.NewGuid());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        
        // Verify cache was invalidated
        _mockCacheService.Verify(x => x.InvalidateCacheAsync(), Times.Once);
        
        // Verify unit of work committed
        _mockUnitOfWork.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);
    }
}
```

**Benefits:**
- ✅ Only mock the services you need
- ✅ Clear test setup
- ✅ Easy to verify cache invalidation
- ✅ Test business logic independently

---

## Example 8: Dependency Injection Setup

### Complete Program.cs Configuration
```csharp
var builder = WebApplicationBuilder.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Add Repositories (from Infrastructure)
builder.Services.AddScoped<IWorkMasterRepository, WorkMasterRepository>();
builder.Services.AddScoped<IWorkMasterSubRepository, WorkMasterSubRepository>();

// Add Services (from Application)
builder.Services.AddScoped<IWorkMasterService, WorkMasterService>();
builder.Services.AddScoped<IWorkMasterCacheService, WorkMasterCacheService>();
builder.Services.AddScoped<IWorkMasterSubService, WorkMasterSubService>();
builder.Services.AddScoped<IWorkMasterSubCacheService, WorkMasterSubCacheService>();

// Add Distributed Cache (Redis)
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add Logging
builder.Services.AddLogging();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

### Extension Method for Cleaner Code
```csharp
public static class ServiceExtensions
{
    public static IServiceCollection AddWorkMasterServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        // Add Services
        services.AddScoped<IWorkMasterService, WorkMasterService>();
        services.AddScoped<IWorkMasterCacheService, WorkMasterCacheService>();
        services.AddScoped<IWorkMasterSubService, WorkMasterSubService>();
        services.AddScoped<IWorkMasterSubCacheService, WorkMasterSubCacheService>();

        // Add Repositories
        services.AddScoped<IWorkMasterRepository, WorkMasterRepository>();
        services.AddScoped<IWorkMasterSubRepository, WorkMasterSubRepository>();

        // Add Cache
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
        });

        return services;
    }
}

// Usage in Program.cs
builder.Services.AddWorkMasterServices(builder.Configuration);
```

---

## Example 9: API Controller Usage

```csharp
[ApiController]
[Route("api/[controller]")]
public class WorkMasterController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<WorkMasterController> _logger;

    public WorkMasterController(IMediator mediator, ILogger<WorkMasterController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateWorkMasterCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating Work Master");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var query = new GetWorkMasterQuery 
            { 
                PageNumber = pageNumber, 
                PageSize = pageSize 
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving Work Masters");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var query = new GetWorkMasterByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving Work Master {id}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateWorkMasterCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating Work Master");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var command = new DeleteWorkMasterCommand { Id = id };
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting Work Master {id}");
            return StatusCode(500, "Internal server error");
        }
    }
}
```

---

## Example 10: Error Handling Pattern

```csharp
public class CreateWorkMasterCommandHandler : IRequestHandler<CreateWorkMasterCommand, Result<Guid>>
{
    private readonly IWorkMasterService _service;
    private readonly IWorkMasterCacheService _cacheService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateWorkMasterCommandHandler> _logger;

    public async Task<Result<Guid>> Handle(CreateWorkMasterCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(request.Name_En))
                return Result<Guid>.Fail("Work Master name is required.");

            // Check for duplicates
            var exists = await _service.IsWorkMasterExistAsync(request.CourtTypeId, request.Name_En);
            if (exists)
            {
                _logger.LogWarning($"Duplicate Work Master attempted: {request.Name_En}");
                return Result<Guid>.Fail("Work Master already exists.");
            }

            // Create entity
            var entity = new WorkTypeEntity
            {
                Name = request.Name_En,
                Code = request.Abbreviation,
                CourtTypeId = request.CourtTypeId
            };

            // Insert
            var id = await _service.InsertAsync(entity);
            await _unitOfWork.Commit(cancellationToken);

            _logger.LogInformation($"Work Master created successfully: {id}");

            // Invalidate cache
            await _cacheService.InvalidateCacheAsync();

            return Result<Guid>.Success(id);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database error while creating Work Master");
            return Result<Guid>.Fail("Database error occurred. Please try again.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while creating Work Master");
            return Result<Guid>.Fail("An unexpected error occurred.");
        }
    }
}
```

---

## Summary

✅ **Use IWorkMasterService for:** CRUD operations, business logic, validation
✅ **Use IWorkMasterCacheService for:** GET operations (read-heavy), performance optimization
✅ **Always invalidate cache** after CREATE, UPDATE, DELETE
✅ **Inject both services** in handlers that need them
✅ **Log important operations** for debugging
✅ **Handle errors gracefully** with proper error messages
✅ **Unit test** using mocked services
✅ **Register in DI container** before running the application
