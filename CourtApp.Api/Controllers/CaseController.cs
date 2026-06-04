using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseDetails;
using CourtApp.Application.Features.CaseDetails.Commands;
using CourtApp.Application.Features.CaseDetails.Queries;
using CourtApp.Application.Features.UserCase;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CourtApp.Api.Controllers
{
    public sealed class CaseController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetCasesQuery query)
        {
            var result = await Mediator.Send(query, RequestAborted);
            return FromPaginated(result);
        }
       
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

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCaseCommand command)
        {
            command.Case.LinkedIds = new List<string> { UserId };
            Result<string> result = await Mediator.Send(command, RequestAborted);
            return FromResult(result, successCode: 201);
        }
        
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateCaseCommand command)
        {
            command.Id = id;
            Result<string> result = await Mediator.Send(command, RequestAborted);
            return FromResult(result);
        }

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
