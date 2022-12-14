using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Photor.Infrastructure.Data.Models;

namespace Photor.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Route("Admin/[controller]/[Action]/{id?}")]
    [Authorize(Roles = "Administrator,Moderator")]

    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
