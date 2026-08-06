using CourtApp.Application.Common;
using CourtApp.Application.Features.DynamicProperty.Commands;
using CourtApp.Application.Features.DynamicProperty.Queries;
using CourtApp.Application.Features.DynamicProperty.Queries.GetById;
using CourtApp.Application.Features.DynamicProperty.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CourtApp.Api.Controllers.Common;

public sealed class DynamicPropertyController : BaseController
{
    [HttpGet("dynamicproperties")]
    [ProducesResponseType(typeof(ApiResponse<List<DynamicPropertyDto>>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAllDynamicProperties([FromQuery] GetAllDynamicPropertyQuery query)
    {
        var result = await Mediator.Send(query);
        return FromResult(result, successCode: 200);
    }

    [HttpGet("dynamicproperties/{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<DynamicPropertyDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetDynamicPropertyById(Guid id)
    {
        return Ok(await Mediator.Send(new GetDynamicPropertyByIdQuery(id)));
    }

    [HttpPost("dynamicproperties")]
    [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateDynamicProperty(CreateDynamicPropertyCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("dynamicproperties")]
    [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateDynamicProperty(UpdateDynamicPropertyCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("dynamicproperties/{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteDynamicProperty(Guid id)
    {
        return Ok(await Mediator.Send(new DeleteDynamicPropertyCommand(id)));
    }
}
