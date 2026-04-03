using CourtApp.Application.Common;
using CourtApp.Application.Features.CourtType.Command;
using CourtApp.Application.Features.CourtType.Query;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace CourtApp.Api.Controllers
{
    public sealed class CourtTypeController : BaseController
    {
        /// <summary>Create a new court type</summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCourtTypeCommand command)
        {
            Result<string> result = await Mediator.Send(command, RequestAborted);
            return FromResult(result, successCode: 201);
        }

        /// <summary>Get court type by ID</summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<GetCourtTypeResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            Result<GetCourtTypeResponse> result = await Mediator.Send(
                new GetCourtTypeByIdQuery { Id = id }, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Get all court types — paginated</summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<GetCourtTypeResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllCourtTypesQuery query)
        {
            PaginatedResult<GetCourtTypeResponse> result = await Mediator.Send(query, RequestAborted);
            return FromPaginated(result);
        }

        /// <summary>Update an existing court type</summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateAsync(
            Guid id, [FromBody] UpdateCourtTypeCommand command)
        {
            command.Id = id;
            Result result = await Mediator.Send(command, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Delete a court type</summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            Result result = await Mediator.Send(
                new DeleteCourtTypeCommand { Id = id }, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Search court types by keyword</summary>
        [HttpGet("search")]
        [ProducesResponseType(typeof(ApiResponse<List<GetCourtTypeResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> SearchAsync([FromQuery] SearchCourtTypesQuery query)
        {
            PaginatedResult<GetCourtTypeResponse> result = await Mediator.Send(query, RequestAborted);
            return FromPaginated(result);
        }
    }
}