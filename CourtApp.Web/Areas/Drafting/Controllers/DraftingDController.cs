using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.Drafting.Models;
using CourtApp.Web.Areas.LawyerDiary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using static CourtApp.Application.Constants.Permissions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.Drafting.Controllers
{
    [Area("Drafting")]
    public class DraftingDController : BaseController<DraftingDController>
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
                var ViewModel = new DeathViewModel();                
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", ViewModel) });
            }
            return null;
        }
    }
}
