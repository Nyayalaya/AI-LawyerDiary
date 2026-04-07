using CourtApp.Application.Common;
using CourtApp.Application.DTOs.Location;
using CourtApp.Application.Features.Location;
using Microsoft.AspNetCore.Mvc;

namespace CourtApp.Api.Controllers
{
    public class LocationController : BaseController
    {
        [HttpGet("{stateId}")]
        [ProducesResponseType(typeof(ApiResponse<List<LocationResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync(int stateId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetLocationQuery { StateId = stateId, PageNumber = pageNumber, PageSize = pageSize };
            var result = await Mediator.Send(query);
            return result.Succeeded ? FromPaginated(result.Data) : FromResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(CreateLocationCommand command)
        {
            var result = await Mediator.Send(command);
            return result.Succeeded ? Created(result.Data) : FromResult(result);
        }

        [HttpGet("detail/{id}")]
        [ProducesResponseType(typeof(ApiResponse<LocationByIdResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var query = new GetLocationByIdQuery { Id = id };
            var result = await Mediator.Send(query);
            return result.Succeeded ? FromResult(result) : FromResult(result, 404);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateLocationCommand command)
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
            var command = new DeleteLocationCommand { Id = id };
            var result = await Mediator.Send(command);
            return result.Succeeded ? Success(result.Data) : FromResult(result);
        }

        [HttpGet("cache/{stateId}")]
        [ProducesResponseType(typeof(ApiResponse<List<LocationResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCachedByStateAsync(int stateId)
        {
            var query = new GetLocationCacheQuery { StateId = stateId };
            var result = await Mediator.Send(query);
            return result.Succeeded ? Success(result.Data) : FromResult(result);
        }
    }
}
