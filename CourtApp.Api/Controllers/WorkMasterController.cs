using CourtApp.Application.Common;
using CourtApp.Application.Features.WorkMaster.Commands;
using CourtApp.Application.Features.WorkMaster.Dtos;
using CourtApp.Application.Features.WorkMaster.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CourtApp.Api.Controllers
{
    public sealed class WorkMasterController : BaseController
    {
        /// <summary>Create a new work master</summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateWorkMasterCommand command)
        {
            Result<Guid> result = await Mediator.Send(command, RequestAborted);
            return FromResult(result, successCode: 201);
        }

        /// <summary>Get work master by ID</summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<WorkMasterByIdResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            Result<WorkMasterByIdResponse> result = await Mediator.Send(
                new GetWorkMasterByIdQuery { Id = id }, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Get all work masters — paginated</summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<WorkMasterResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetWorkMasterQuery query)
        {
            PaginatedResult<WorkMasterResponse> result = await Mediator.Send(query, RequestAborted);
            return FromPaginated(result);
        }

        /// <summary>Update an existing work master</summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateAsync(
            Guid id, [FromBody] UpdateWorkMasterCommand command)
        {
            command.Id = id;
            Result result = await Mediator.Send(command, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Delete a work master</summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            Result result = await Mediator.Send(
                new DeleteWorkMasterCommand { Id = id }, RequestAborted);
            return FromResult(result);
        }
    }
}
