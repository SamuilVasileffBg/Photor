using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Photor.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Route("Admin/[controller]/[Action]/{id?}")]
    [Authorize(Roles = "Administrator")]

    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
