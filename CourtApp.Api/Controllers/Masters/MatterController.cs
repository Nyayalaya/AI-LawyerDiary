using CourtApp.Application.Common;
using CourtApp.Application.DTOs.DropdownDtos;
using CourtApp.Application.Features.Matter.Commands;
using CourtApp.Application.Features.Matter.Commands.MatterCategory.Create;
using CourtApp.Application.Features.Matter.Commands.MatterCategory.Delete;
using CourtApp.Application.Features.Matter.Commands.MatterCategory.Update;
using CourtApp.Application.Features.Matter.Dtos;
using CourtApp.Application.Features.Matter.Queries;
using CourtApp.Application.Features.Matter.Queries.MatterCategory.GetById;
using CourtApp.Application.Features.Matter.Queries.MatterSubCategory.GetById;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CourtApp.Api.Controllers.Masters
{
   
    public sealed class MatterController : BaseController
    {
        #region Matter Type

        [HttpGet("types")]
        [ProducesResponseType(typeof(ApiResponse<List<MatterTypeDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllMatterTypes(
            [FromQuery] GetAllMatterTypeQuery query)
        {
            var result = await Mediator.Send(query);
            return FromPaginated(result);
        }

        [HttpGet("types/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<MatterTypeDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetMatterTypeById(Guid id)
        {
            return Ok(await Mediator.Send(new GetMatterTypeByIdQuery(id)));
        }

        [HttpPost("types")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreateMatterType(
            CreateMatterTypeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("types")]
        [ProducesResponseType(typeof(ApiResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateMatterType(
            UpdateMatterTypeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("types/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteMatterType(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteMatterTypeCommand(id)));
        }

        #endregion

        #region Matter Category

        [HttpGet("categories")]
        [ProducesResponseType(typeof(ApiResponse<List<MatterCategoryDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllMatterCategories(
            [FromQuery] GetAllMatterCategoryQuery query)
        {
            var result = await Mediator.Send(query);
            return FromPaginated(result);
        }

        [HttpGet("categories/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<MatterCategoryDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetMatterCategoryById(Guid id)
        {
            return Ok(await Mediator.Send(new GetMatterCategoryByIdQuery(id)));
        }

        

        [HttpPost("categories")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateMatterCategory(
            CreateMatterCategoryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("categories")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateMatterCategory(
            UpdateMatterCategoryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("categories/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteMatterCategory(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteMatterCategoryCommand(id)));
        }

        #endregion

        #region Matter Sub Category

        [HttpGet("subcategories")]
        [ProducesResponseType(typeof(ApiResponse<List<MatterSubCategoryDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllMatterSubCategories(
            [FromQuery] GetAllMatterSubCategoryQuery query)
        {
            var result = await Mediator.Send(query);
            return FromPaginated(result);
        }

        [HttpGet("subcategories/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<MatterSubCategoryDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetMatterSubCategoryById(Guid id)
        {
            return Ok(await Mediator.Send(new GetMatterSubCategoryByIdQuery(id)));
        }

        

        [HttpPost("subcategories")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateMatterSubCategory(
            CreateMatterSubCategoryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("subcategories")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateMatterSubCategory(
            UpdateMatterSubCategoryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("subcategories/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteMatterSubCategory(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteMatterSubCategoryCommand(id)));
        }

        #endregion
    }
}
