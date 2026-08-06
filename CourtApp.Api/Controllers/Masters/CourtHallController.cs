using CourtApp.Application.Common;
using CourtApp.Application.Features.CourtHall;
using CourtApp.Application.Features.CourtHall.Commands;
using CourtApp.Application.Features.CourtHall.DTOs;
using CourtApp.Application.Features.CourtHall.Query;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CourtApp.Api.Controllers
{
    public class CourtHallController : BaseController
    {
        /// <summary>Get all court halls with pagination</summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<CourtHallResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetCourtHallQuery query)
        {
            PaginatedResult<CourtHallResponse> result = await Mediator.Send(query, RequestAborted);
            return FromPaginated(result);
        }

        /// <summary>Create new court hall(s)</summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCourtHallCommand command)
        {
            Result<Guid> result = await Mediator.Send(command, RequestAborted);
            return FromResult(result, successCode: 201);
        }

        /// <summary>Get court hall by ID</summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<CourtHallByIdResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            Result<CourtHallByIdResponse> result = await Mediator.Send(
                new GetCourtHallByIdQuery { Id = id }, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Update an existing court hall</summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateCourtHallCommand command)
        {
            command.Id = id;
            Result<Guid> result = await Mediator.Send(command, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Delete a court hall</summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            Result<Guid> result = await Mediator.Send(
                new DeleteCourtHallCommand { Id = id }, RequestAborted);
            return FromResult(result);
        }

        /// <summary>Get cached court halls for a complex</summary>
        [HttpGet("cached-by-complex/{courtComplexId:guid}")]
        [ProducesResponseType(typeof(ApiResponse<List<CourtHallResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetCachedByComplexAsync(Guid courtComplexId)
        {
            Result<List<CourtHallResponse>> result = await Mediator.Send(
                new GetCourtHallCacheQuery { CourtComplexId = courtComplexId }, RequestAborted);
            return FromResult(result);
        }
    }
}
