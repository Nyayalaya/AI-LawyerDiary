using CourtApp.Application.Common;
using CourtApp.Application.Features.CourtLevel.Query;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CourtApp.Api.Controllers
{

    public sealed class CourtLevelController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<GetCourtLevelResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetCourtLevelQuery query)
        {
            PaginatedResult<GetCourtLevelResponse> result = await Mediator.Send(query, RequestAborted);
            return FromPaginated(result);
        }
    }
}
