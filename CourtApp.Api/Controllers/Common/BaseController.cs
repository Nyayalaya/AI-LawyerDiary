
using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CourtApp.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        // ── Lazy service resolution — no constructor needed in subclasses
        private IMediator? _mediator;
        private ICurrentUserService? _currentUserService;

        protected IMediator Mediator
            => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

        private ICurrentUserService CurrentUserService
            => _currentUserService ??= HttpContext.RequestServices
                .GetRequiredService<ICurrentUserService>();

        protected string RequestOrigin
    => $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";

        // ── Request ───────────────────────────────────────────────────
        protected CancellationToken RequestAborted => HttpContext.RequestAborted;
        protected string CorrelationId => HttpContext.TraceIdentifier;

        // ── User ──────────────────────────────────────────────────────
        protected UserContextInfo CurrentUser => CurrentUserService.GetCurrentUser();
        protected string UserId => CurrentUser.UserId;
        protected string? UserEmail => CurrentUser.Email;
        protected string? UserName => CurrentUser.UserName;
        protected List<string> Roles => CurrentUser.Roles;

        protected bool HasRole(string role) => CurrentUserService.HasRole(role);
        protected bool HasAnyRole(params string[] r) => CurrentUserService.HasAnyRole(r);

        // ── Result-based responses ────────────────────────────────────
        protected IActionResult FromResult<T>(Result<T> result, int successCode = 200)
        {
            var response = ApiResponse<T>.FromResult(result, successCode);
            return StatusCode(response.StatusCode, response);
        }

        protected IActionResult FromResult(Result result, int successCode = 200)
        {
            var response = ApiResponse<object>.FromResult(result, successCode);
            return StatusCode(response.StatusCode, response);
        }

        protected IActionResult FromPaginated<T>(PaginatedResult<T> result)
        {
            var response = ApiResponse<List<T>>.FromPaginated(result);
            return StatusCode(response.StatusCode, response);
        }

        // ── Direct response helpers ───────────────────────────────────
        protected IActionResult Success<T>(T data, string message = "Success")
            => Ok(ApiResponse<T>.Success(data, message));

        protected IActionResult Created<T>(T data, string message = "Created successfully")
            => StatusCode(201, ApiResponse<T>.Success(data, message, 201));

        protected IActionResult Failure(string message, int statusCode = 400,
            List<string>? errors = null)
            => StatusCode(statusCode,
                ApiResponse<object>.Failure(message, statusCode, errors));

        protected IActionResult ValidationError(List<string> errors,
            string message = "Validation failed")
            => StatusCode(422, ApiResponse<object>.Failure(message, 422, errors));

        protected IActionResult NotFoundResponse(string message = "Resource not found")
            => StatusCode(404, ApiResponse<object>.Failure(message, 404));

        protected IActionResult UnauthorizedResponse(string message = "Unauthorized")
            => StatusCode(401, ApiResponse<object>.Failure(message, 401));

        protected IActionResult ForbiddenResponse(string message = "Forbidden")
            => StatusCode(403, ApiResponse<object>.Failure(message, 403));

        protected IActionResult ServerError(string message = "An unexpected error occurred")
            => StatusCode(500, ApiResponse<object>.ServerError(message));
    }
}
