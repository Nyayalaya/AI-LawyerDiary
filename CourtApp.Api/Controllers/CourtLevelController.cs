using CourtApp.Application.Common;
using CourtApp.Application.Features.State.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CourtApp.Api.Controllers
{
    
    public class CourtLevelController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<GetStateMasterResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetStateMasterQuery query)
        {
            PaginatedResult<GetStateMasterResponse> result = await Mediator.Send(query, RequestAborted);
            return FromPaginated(result);
        }
    }
}
