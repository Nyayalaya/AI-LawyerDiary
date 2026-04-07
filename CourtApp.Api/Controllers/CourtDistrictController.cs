using CourtApp.Application.Common;
using CourtApp.Application.DTOs.CourtDistrict;
using CourtApp.Application.Features.CourtDistrict;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CourtApp.Api.Controllers
{
    public class CourtDistrictController : BaseController
    {
        /// <summary>Get all court districts with pagination</summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<System.Collections.Generic.List<CourtDistrictReponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetCourtDistrictQuery query)
        {
            PaginatedResult<CourtDistrictReponse> result = await Mediator.Send(query, RequestAborted);
            return FromPaginated(result);
        }

        /// <summary>Create a new court district</summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCourtDistrictCommand command)
        {
            Result<Guid> result = await Mediator.Send(command, RequestAborted);
            return FromResult(result, successCode: 201);
        }

        /// <summary>Get court district by ID</summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<CourtDistrictByIdReponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            Result<CourtDistrictByIdReponse> result = await Mediator.Send(
                new GetCourtDistrictQueryById { Id = id }, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Update an existing court district</summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateAsync(
            Guid id, [FromBody] UpdateCourtDistrictCommand command)
        {
            command.Id = id;
            Result<Guid> result = await Mediator.Send(command, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Delete a court district</summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            Result<Guid> result = await Mediator.Send(
                new DeleteCourtDistrictCommand { Id = id }, RequestAborted);
            return FromResult(result);
        }
    }
}
