using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.LawyerDiary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Controllers
{
    [Area("LawyerDiary")]
    public class ExpenseHeadController : BaseController<ExpenseHeadController>
    {
        public IActionResult Index()
        {
            var model = new ExpenseHeadViewModel();
            return View(model);
        }

        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0)
        {
            //var brandsResponse = await _mediator.Send(new GetAllBrandsCachedQuery());

            if (id == 0)
            {
                var ViewModel = new ExpenseHeadViewModel();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
            }
            else
            {
                //var response = await _mediator.Send(new GetBrandByIdQuery() { Id = id });
                //if (response.Succeeded)
                //{
                //    var brandViewModel = _mapper.Map<ViewModel>(response.Data);
                //    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", brandViewModel) });
                //}
                return null;
            }
        }
    }
}
