using CourtApp.Application.Common;
using CourtApp.Application.DTOs.CourtComplex;
using CourtApp.Application.Features.CourtComplex;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CourtApp.Api.Controllers
{
    public class CourtComplexController : BaseController
    {
        /// <summary>Get all court complexes with pagination</summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<CourtComplexResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetCourtComplexQuery query)
        {
            PaginatedResult<CourtComplexResponse> result = await Mediator.Send(query, RequestAborted);
            return FromPaginated(result);
        }

        /// <summary>Create a new court complex</summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCourtComplexCommand command)
        {
            Result<Guid> result = await Mediator.Send(command, RequestAborted);
            return FromResult(result, successCode: 201);
        }

        /// <summary>Get court complex by ID</summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<CourtComplexByIdResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            Result<CourtComplexByIdResponse> result = await Mediator.Send(
                new GetCourtComplexByIdQuery { Id = id }, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Update an existing court complex</summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateCourtComplexCommand command)
        {
            command.Id = id;
            Result<Guid> result = await Mediator.Send(command, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Delete a court complex</summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            Result<Guid> result = await Mediator.Send(
                new DeleteCourtComplexCommand { Id = id }, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Get cached court complexes for a district</summary>
        [HttpGet("cached-by-district/{courtDistrictId:guid}")]
        [ProducesResponseType(typeof(ApiResponse<List<CourtComplexResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetCachedByDistrictAsync(Guid courtDistrictId)
        {
            Result<List<CourtComplexResponse>> result = await Mediator.Send(
                new GetCourtComplexCacheQuery { CourtDistrictId = courtDistrictId }, RequestAborted);
            return FromResult(result);
        }
    }
}
