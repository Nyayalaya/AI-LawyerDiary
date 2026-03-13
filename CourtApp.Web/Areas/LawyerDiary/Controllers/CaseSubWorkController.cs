using Microsoft.AspNetCore.Mvc;

namespace CourtApp.Web.Areas.LawyerDiary.Controllers
{
    public class CaseSubWorkController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
