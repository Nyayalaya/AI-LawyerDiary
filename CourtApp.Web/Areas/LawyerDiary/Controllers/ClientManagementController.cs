using CourtApp.Web.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Controllers
{
    [Area("LawyerDiary")]
    public class ClientManagementController : BaseController<ClientManagementController>
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
