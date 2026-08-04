using CourtApp.Application.Common;
using CourtApp.Application.Features.Clients.Commands.CreateClient;
using CourtApp.Application.Features.Clients.Commands.DeleteClient;
using CourtApp.Application.Features.Clients.Commands.UpdateClient;
using CourtApp.Application.Features.Clients.Queries.GetAllClients;
using CourtApp.Application.Features.Clients.Queries.GetClientById;
using CourtApp.Application.Features.Clients.Queries.SearchClients;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CourtApp.Api.Controllers.clients
{
    public sealed class ClientController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync( [FromBody] CreateClientCommand command,CancellationToken cancellationToken)
        {
            command.UserId = UserId;
            var result = await Mediator.Send(command, cancellationToken);
            return result.Succeeded
                ? FromResult(result, StatusCodes.Status201Created)
                : FromResult(result);
        }

        /// <summary>Get client by ID</summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var query = new GetClientByIdQuery { Id = id };
            var result = await Mediator.Send(query, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Get all clients</summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<object>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllClientsQuery query)
        {
            var result = await Mediator.Send(query, RequestAborted);
            return FromPaginated(result);
        }

        /// <summary>Update an existing client</summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateClientCommand command)
        {
            command.Id = id;
            Result<bool> result = await Mediator.Send(command, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Delete a client</summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var command = new DeleteClientCommand { Id = id };
            Result<bool> result = await Mediator.Send(command, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Search clients by keyword — paginated</summary>
        [HttpGet("search")]
        [ProducesResponseType(typeof(ApiResponse<List<object>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> SearchAsync([FromQuery] SearchClientsQuery query)
        {
            var result = await Mediator.Send(query, RequestAborted);
            if (result.Succeeded && result.Data != null)
            {   
                var genericResult = PaginatedResult<object>.Success(
                    result.Data.Data.Cast<object>().ToList(),
                    result.Data.Pagination.TotalCount,
                    result.Data.Pagination.PageNumber,
                    result.Data.Pagination.PageSize
                );
                return FromPaginated(genericResult);
            }
            return FromResult(Result.Fail(result.Message));
        }
    }
}
