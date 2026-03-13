using CourtApp.Application.Features.ProceedingHead;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.LawyerDiary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Controllers
{
    public class CaseWorkController : BaseController<CaseWorkController>
    {
        public IActionResult Index()
        {
            var model = new CaseWorkViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetProceedingHeadQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<CaseWorkViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }
    }
}
