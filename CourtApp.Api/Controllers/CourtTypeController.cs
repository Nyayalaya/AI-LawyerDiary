using CourtApp.Application.Features.CourtType.Command;
using CourtApp.Application.Features.CourtType.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace CourtApp.Api.Controllers
{
    public class CourtTypeController : BaseController
    {
        public CourtTypeController(
            IMediator mediator,
            IHttpContextAccessor httpContextAccessor)
            : base(mediator, httpContextAccessor)
        {
        }

        /// <summary>
        /// Create a new court type
        /// </summary>
        /// <param name="command">Court type details with multilingual support</param>
        /// <returns>Returns court type ID if creation is successful</returns>
        [HttpPost("create")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCourtTypeCommand command)
        {
            var result = await Mediator.Send(command, RequestAborted);
            return FromResult(result);
        }

        /// <summary>
        /// Get court type by ID
        /// </summary>
        /// <param name="id">Court type ID</param>
        /// <returns>Returns court type details</returns>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var query = new GetCourtTypeByIdQuery { Id = id };
            var result = await Mediator.Send(query, RequestAborted);
            return FromResult(result);
        }

        /// <summary>
        /// Get all court types with pagination
        /// </summary>
        /// <param name="pageNumber">Page number (default 1)</param>
        /// <param name="pageSize">Page size (default 10)</param>
        /// <returns>Returns paginated list of court types</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetAllCourtTypesQuery { PageNumber = pageNumber, PageSize = pageSize };
            var result = await Mediator.Send(query, RequestAborted);
            return FromResult(result);
        }

        /// <summary>
        /// Update existing court type
        /// </summary>
        /// <param name="id">Court type ID</param>
        /// <param name="command">Updated court type details</param>
        /// <returns>Returns success message</returns>
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateCourtTypeCommand command)
        {
            command.Id = id;
            var result = await Mediator.Send(command, RequestAborted);
            return FromResult(result);
        }

        /// <summary>
        /// Delete court type
        /// </summary>
        /// <param name="id">Court type ID</param>
        /// <returns>Returns success message</returns>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var command = new DeleteCourtTypeCommand { Id = id };
            var result = await Mediator.Send(command, RequestAborted);
            return FromResult(result);
        }

        /// <summary>
        /// Search court types by keyword
        /// </summary>
        /// <param name="keyword">Search keyword</param>
        /// <param name="pageNumber">Page number (default 1)</param>
        /// <param name="pageSize">Page size (default 10)</param>
        /// <returns>Returns matching court types</returns>
        [HttpGet("search/{keyword}")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> SearchAsync(string keyword, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new SearchCourtTypesQuery 
            { 
                Keyword = keyword, 
                PageNumber = pageNumber, 
                PageSize = pageSize 
            };
            var result = await Mediator.Send(query, RequestAborted);
            return FromResult(result);
        }
    }
}