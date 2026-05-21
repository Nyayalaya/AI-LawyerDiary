using CourtApp.Application.Common;
using CourtApp.Application.Features.RoleManager.Commands;
using CourtApp.Application.Features.RoleManager.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace CourtApp.Api.Controllers
{
    public sealed class RoleManagerController : BaseController
    {
        /// <summary>Create a new role</summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateRoleCommand command)
        {
            Result<Guid> result = await Mediator.Send(command, RequestAborted);
            return FromResult(result, successCode: 201);
        }

        /// <summary>Get role by ID</summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<RoleDetailResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            Result<RoleDetailResponse> result = await Mediator.Send(
                new GetRoleByIdQuery { Id = id }, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Get all roles — paginated</summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<RoleResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetRoleQuery query)
        {
            PaginatedResult<RoleResponse> result = await Mediator.Send(query, RequestAborted);
            return FromPaginated(result);
        }

        /// <summary>Update an existing role</summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateAsync(
            string id, [FromBody] UpdateRoleCommand command)
        {
            command.Id = id;
            Result<bool> result = await Mediator.Send(command, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Delete a role</summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            Result<bool> result = await Mediator.Send(
                new DeleteRoleCommand { Id = id }, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Get role permissions by role ID</summary>
        [HttpGet("{roleId}/permissions")]
        [ProducesResponseType(typeof(ApiResponse<List<RolePermissionResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetPermissionsAsync(string roleId)
        {
            Result<List<RolePermissionResponse>> result = await Mediator.Send(
                new GetRolePermissionsQuery { RoleId = roleId }, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Update role permissions (activate/deactivate)</summary>
        [HttpPost("{roleId}/permissions")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdatePermissionsAsync(
            string roleId, [FromBody] List<RolePermissionItem> permissions)
        {
            var command = new UpdateRolePermissionsCommand
            {
                RoleId = roleId,
                Permissions = permissions
            };

            Result<bool> result = await Mediator.Send(command, RequestAborted);
            return FromResult(result);
        }
    }
}
