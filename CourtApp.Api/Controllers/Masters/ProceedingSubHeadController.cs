using CourtApp.Application.Common;
using CourtApp.Application.DTOs.ProcSubHead;
using CourtApp.Application.Features.ProceedingSubHead;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CourtApp.Api.Controllers
{
    public class ProceedingSubHeadController : BaseController
    {
        /// <summary>Get all proceeding sub heads with pagination</summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<GetProcSubHeadResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetProceedingSubHeadQuery query)
        {
            PaginatedResult<GetProcSubHeadResponse> result = await Mediator.Send(query, RequestAborted);
            return FromPaginated(result);
        }

        /// <summary>Create a new proceeding sub head</summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateProcSubHeadCommand command)
        {
            Result<Guid> result = await Mediator.Send(command, RequestAborted);
            return FromResult(result, successCode: 201);
        }

        /// <summary>Get proceeding sub head by ID</summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<GetProcSubHeadByIdResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            Result<GetProcSubHeadByIdResponse> result = await Mediator.Send(
                new GetProceedingSubHeadGetByIdQuery { Id = id }, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Update an existing proceeding sub head</summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateAsync(
            Guid id, [FromBody] UpdateProcSubHeadCommand command)
        {
            command.Id = id;
            Result<Guid> result = await Mediator.Send(command, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Delete a proceeding sub head</summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            Result<Guid> result = await Mediator.Send(
                new DeleteProcSubHeadCommand { Id = id }, RequestAborted);
            return FromResult(result);
        }
    }
}
