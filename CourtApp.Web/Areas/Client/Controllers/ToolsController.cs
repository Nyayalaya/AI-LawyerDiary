using CourtApp.Application.Constants;
using CourtApp.Application.Features.Queries.States;
using CourtApp.Application.Features.Queries.Tools;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.Client.Model;
using CourtApp.Web.Areas.LawyerDiary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.Client.Controllers
{
    [Area("Client")]
    public class ToolsController : BaseController<ToolsController>
    {
        public async Task<IActionResult> AdvalremFeeCal()
        {
            var viewModel = new AdvalremFeeViewModel();
            viewModel.FeeKind = new SelectList(StaticDropDownDictionaries.FeeType(), "Key", "Value");           
            var statelist = (await _mediator.Send(new GetStateMasterQuery())).Data;
            if (statelist != null)
            {
                var stateViewModel = _mapper.Map<List<StateViewModel>>(statelist);
                viewModel.States = new SelectList(stateViewModel, nameof(StateViewModel.Id), nameof(StateViewModel.Name_En), null, null);
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<JsonResult> AdvalremFeeCalResultAsync(int statecode, string feekind, string amount)
        {
            var result = await _mediator.Send(new AdvalremCourtFeeCalQuery() { StateCode = statecode,FeeKind=feekind,CalculativeAmount=Convert.ToDouble(amount) });
            var data = Json(result.Message);
            return data;
        }
    }
}
