using CourtApp.Application.Common;
using CourtApp.Application.Features.Case;
using CourtApp.Application.Features.CaseDetails;
using CourtApp.Application.Features.UserCase;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CourtApp.Api.Controllers
{
    public sealed class CaseDetailController : BaseController
    {
        /// <summary>Create a new case</summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCaseCommand command)
        {
            command.LinkedIds = new List<string> { UserId };
            Result<Guid> result = await Mediator.Send(command, RequestAborted);
            return FromResult(result, successCode: 201);
        }

        /// <summary>Get case details by ID</summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var query = new GetUserCaseDetailByIdQuery { CaseId = id, LinkedIds = new List<string> { UserId } };
            var result = await Mediator.Send(query, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Update an existing case</summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateCaseDetailCommand command)
        {
            command.Id = id;
            Result<Guid> result = await Mediator.Send(command, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Delete a case</summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var command = new DeleteCaseDetailCommand { Id = id };
            Result<Guid> result = await Mediator.Send(command, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Get case history</summary>
        [HttpGet("{id:guid}/history")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetCaseHistoryAsync(Guid id)
        {
            var query = new GetCaseHistoryQuery { CaseId = id };
            var result = await Mediator.Send(query, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Get case info summary</summary>
        [HttpGet("{id:guid}/info")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetCaseInfoAsync(Guid id)
        {
            var query = new GetCaseInfoQuery { LinkedIds = new List<string> { UserId }, PageNumber = 1, PageSize = 10 };
            var result = await Mediator.Send(query, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Create case document</summary>
        [HttpPost("{id:guid}/documents")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreateCaseDocumentAsync(Guid id, [FromBody] CaseDocsCreateCommand command)
        {
            command.CaseId = id;
            Result<Guid> result = await Mediator.Send(command, RequestAborted);
            return FromResult(result, successCode: 201);
        }

        /// <summary>Get case documents</summary>
        [HttpGet("{id:guid}/documents")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetCaseDocumentsAsync(Guid id)
        {
            var query = new CaseDocumentQuery { CaseId = id };
            var result = await Mediator.Send(query, RequestAborted);
            return FromResult(result);
        }
    }
}
