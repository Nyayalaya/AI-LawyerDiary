using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseCategory.Commands;
using CourtApp.Application.Features.CaseCategory.Dto;
using CourtApp.Application.Features.CaseCategory.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CourtApp.Api.Controllers
{

    public sealed class CourtCategoryController : BaseController
    {
        // No constructor needed — Mediator resolved lazily from base

        /// <summary>
        /// Create a new court category
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreateAsync([FromBody] CaseCategoryCreateCommand command)
        {
            Result<string> result = await Mediator.Send(command, RequestAborted);
            return FromResult(result, successCode: 201);
        }

        /// <summary>
        /// Get court category by ID
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<CaseCategoryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            //Result<CaseCategoryResponse> result = await Mediator.Send(
            //    new GetQueryByIdCaseCategory { Id = id }, RequestAborted);
            Result<CaseCategoryResponse> result=new();
            return FromResult(result);
        }

        /// <summary>
        /// Get all court categories — paginated
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<CaseCategoryResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetQueryCaseCategory query)
        {
            PaginatedResult<CaseCategoryResponse> result = await Mediator.Send(query, RequestAborted);
            return FromPaginated(result);
        }

        /// <summary>
        /// Update an existing court category
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateAsync(
            Guid id, [FromBody] CaseCategoryUpdateCommand command)
        {
            command.Id = id;
            Result result = await Mediator.Send(command, RequestAborted);
            return FromResult(result);
        }

        /// <summary>
        /// Delete a court category
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            Result result = await Mediator.Send(
                new CaseCategoryDeleteCommand(id), RequestAborted);
            return FromResult(result);
        }
    }
}

