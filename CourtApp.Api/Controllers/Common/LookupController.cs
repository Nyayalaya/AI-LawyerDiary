using CourtApp.Application.Common;
using CourtApp.Application.DTOs.DropdownDtos;
using CourtApp.Application.Features.Cadre.Queries;
using CourtApp.Application.Features.CaseCategory.Query;
using CourtApp.Application.Features.CaseStage.Query;
using CourtApp.Application.Features.CaseType.Query;
using CourtApp.Application.Features.Clients.Queries.GetAllCached;
using CourtApp.Application.Features.DOType;
using CourtApp.Application.Features.Matter.Queries;
using CourtApp.Application.Features.State.Query;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CourtApp.Api.Controllers.Common
{
    public class LookupController : BaseController
    {
        [HttpGet("master/states")]
        public async Task<IActionResult> GetStates()
        {  
            var result = await Mediator.Send(new GetStateLookupQuery(), RequestAborted);
            return FromResult(result);
        }

        [HttpGet("master/cadres")]
        public async Task<IActionResult> GetCadres()
        {
            var result = await Mediator.Send(new GetCadreQueryLookup(), RequestAborted);
            return FromResult(result);
        }

        [HttpGet("master/casecategories")]
        public async Task<IActionResult> GetCaseCategories(Guid CourtTypeId)
        {
            var result = await Mediator.Send(new GetCaseCategoryLookupQuery() { CourtTypeId = CourtTypeId }, RequestAborted);
            return FromResult(result);
        }

        [HttpGet("userclients")]
        public async Task<IActionResult> GetUserClients()
        {
            var result = await Mediator.Send(new GetUserClientLookUpQuery() { UserId=UserId}, RequestAborted);
            return FromResult(result);
        }

        [HttpGet("master/casetypes")]
        public async Task<IActionResult> GetCaseTypes()
        {
            var result = await Mediator.Send(new GetCaseTypeLookupQuery(), RequestAborted);
            return FromResult(result);
        }

        [HttpGet("master/casestages")]
        public async Task<IActionResult> GetCaseStages()
        {
            var result = await Mediator.Send(new GetCaseStageLookupQuery(), RequestAborted);
            return FromResult(result);
        }

        [HttpGet("master/documenttypes")]
        public async Task<IActionResult> GetDocumentTypes(int TypeId)
        {
            var result = await Mediator.Send(new GetDocumentTypeLookupQuery() { TypeId = TypeId }, RequestAborted);
            return FromResult(result);
        }

        [HttpGet("matter/types")]
        [ProducesResponseType(typeof(ApiResponse<List<DdlGuidStringDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetMatterTypeDropdown()
        {
            var result = await Mediator.Send(new GetMatterTypeDropdownQuery(), RequestAborted);
            return FromResult(result);
        }
        [HttpGet("matter/categories")]
        [ProducesResponseType(typeof(ApiResponse<List<DdlGuidStringDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMatterCategoryDropdown(
            [FromQuery] Guid matterTypeId)
        {
            var result = await Mediator.Send(new GetMatterCategoryDropdownQuery
            {
                MatterTypeId = matterTypeId
            }, RequestAborted);
            return FromResult(result);
        }
        [HttpGet("matter/subcategories")]
        [ProducesResponseType(typeof(ApiResponse<List<DdlGuidStringDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMatterSubCategoryDropdown(
            [FromQuery] Guid matterCategoryId)
        {
            var result = await Mediator.Send(new GetMatterSubCategoryDropdownQuery
            {
                MatterCategoryId = matterCategoryId
            }, RequestAborted);
            return FromResult(result);
        }
    }
}
