using CourtApp.Application.Common;
using CourtApp.Application.Features.CaseStages.Query;
using CourtApp.Application.Features.State.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CourtApp.Api.Controllers
{
   
    public class CaseStageController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<GetStateMasterResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllAsync([FromQuery] CaseStageCacheAllQuery query)
        {
            PaginatedResult<CaseStageCacheAllQueryResponse> result = await Mediator.Send(query, RequestAborted);
            return FromPaginated(result);
        }
    }
}
