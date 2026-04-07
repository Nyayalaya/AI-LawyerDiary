using CourtApp.Application.Common;
using CourtApp.Application.Features.Court;
using CourtApp.Application.Features.Court.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Api.Controllers
{
    public class CourtController : BaseController
    {
        [HttpGet("{locationId}")]
        [ProducesResponseType(typeof(ApiResponse<List<CourtResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync(Guid locationId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetCourtQuery { LocationId = locationId, PageNumber = pageNumber, PageSize = pageSize };
            var result = await Mediator.Send(query);
            return result.Succeeded ? FromPaginated(result.Data) : FromResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(CreateCourtCommand command)
        {
            var result = await Mediator.Send(command);
            return result.Succeeded ? Created(result.Data) : FromResult(result);
        }

        [HttpGet("detail/{id}")]
        [ProducesResponseType(typeof(ApiResponse<CourtByIdResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var query = new GetCourtByIdQuery { Id = id };
            var result = await Mediator.Send(query);
            return result.Succeeded ? FromResult(result) : FromResult(result, 404);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateCourtCommand command)
        {
            command.Id = id;
            var result = await Mediator.Send(command);
            return result.Succeeded ? Success(result.Data) : FromResult(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var command = new DeleteCourtCommand { Id = id };
            var result = await Mediator.Send(command);
            return result.Succeeded ? Success(result.Data) : FromResult(result);
        }

        [HttpGet("cache/{locationId}")]
        [ProducesResponseType(typeof(ApiResponse<List<CourtResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCachedByLocationAsync(Guid locationId)
        {
            var query = new GetCourtCacheQuery { LocationId = locationId };
            var result = await Mediator.Send(query);
            return result.Succeeded ? Success(result.Data) : FromResult(result);
        }
    }
}

