using CourtApp.Application.Common;
using CourtApp.Application.Features.SystemUsers.Commands;
using CourtApp.Application.Features.SystemUsers.DTOs;
using CourtApp.Application.Features.SystemUsers.Queries;
using CourtApp.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CourtApp.Api.Controllers
{
    /// <summary>
    /// Controller for managing system users (Lawyer, Corporate, etc.)
    /// Provides access to directly registered users for role assignment, subscription, and status management
    /// </summary>
    public sealed class SystemUsersController : BaseController
    {
        /// <summary>Get all registered system users with pagination and filtering</summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PaginatedResult<SystemUserResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllAsync(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] RegisterType? userType = null,
            [FromQuery] UserAccountStatus? status = null,
            [FromQuery] string searchTerm = null)
        {
            var query = new GetSystemUsersQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                UserType = userType,
                Status = status,
                SearchTerm = searchTerm
            };

            var result = await Mediator.Send(query, RequestAborted);
            return FromPaginated(result);
        }

        /// <summary>Get a specific system user by ID</summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<SystemUserResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var query = new GetSystemUserByIdQuery(id);
            var result = await Mediator.Send(query, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Update system user subscription and status</summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] UpdateSystemUserRequest request)
        {
            var command = new UpdateSystemUserCommand
            {
                UserId = id,
                Request = request
            };

            var result = await Mediator.Send(command, RequestAborted);
            return FromResult(result);
        }
    }
}
