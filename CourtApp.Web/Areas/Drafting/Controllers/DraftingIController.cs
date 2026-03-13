using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.Drafting.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace CourtApp.Web.Areas.Drafting.Controllers
{
    [Area("Drafting")]
    public class DraftingIController : BaseController<DraftingIController>
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> OnGetCreateOrEdit(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                var ViewModel = new InjuryViewModel();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
            }
            return null;
        }
    }
}
