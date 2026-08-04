using CourtApp.Application.Features.Cadre.Queries;
using CourtApp.Application.Features.CaseCategory.Query;
using CourtApp.Application.Features.CaseStage.Query;
using CourtApp.Application.Features.CaseType.Query;
using CourtApp.Application.Features.Clients.Queries.GetAllCached;
using CourtApp.Application.Features.DOType;
using CourtApp.Application.Features.State.Query;
using Microsoft.AspNetCore.Mvc;

namespace CourtApp.Api.Controllers.Common
{
    public class LookupController : BaseController
    {
        [HttpGet("states")]
        public async Task<IActionResult> GetStates()
        {  
            var result = await Mediator.Send(new GetStateLookupQuery(), RequestAborted);
            return FromResult(result);
        }

        [HttpGet("cadres")]
        public async Task<IActionResult> GetCadres()
        {
            var result = await Mediator.Send(new GetCadreQueryLookup(), RequestAborted);
            return FromResult(result);
        }

        [HttpGet("casecategories")]
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

        [HttpGet("casetypes")]
        public async Task<IActionResult> GetCaseTypes()
        {
            var result = await Mediator.Send(new GetCaseTypeLookupQuery(), RequestAborted);
            return FromResult(result);
        }

        [HttpGet("casestages")]
        public async Task<IActionResult> GetCaseStages()
        {
            var result = await Mediator.Send(new GetCaseStageLookupQuery(), RequestAborted);
            return FromResult(result);
        }

        [HttpGet("documenttypes")]
        public async Task<IActionResult> GetDocumentTypes(int TypeId)
        {
            var result = await Mediator.Send(new GetDocumentTypeLookupQuery() { TypeId = TypeId }, RequestAborted);
            return FromResult(result);
        }
    }
}
