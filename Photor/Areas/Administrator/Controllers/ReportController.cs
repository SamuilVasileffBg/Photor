using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Photor.Core.Contracts;
using Photor.Core.Models.Report;

namespace Photor.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Route("Admin/[controller]/[Action]/{id?}")]
    [Authorize(Roles = "Administrator")]
    public class ReportController : BaseController
    {
        private readonly IReportService reportService;
        private readonly IUserService userService;
        private readonly IPostService postService;

        public ReportController(IReportService reportService, IUserService userService, IPostService postService)
        {
            this.reportService = reportService;
            this.userService = userService;
            this.postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> Manage(int? page, bool? newest)
        {
            if (newest == null)
            {
                newest = false;
            }

            if (page == null || page < 1)
            {
                return Redirect($"/Admin/Report/Manage?page=1&newest={newest}");
            }

            //return RedirectToAction("Index", "Base", new { Area = "Administrator" });

            var model = new ManageReportViewModel();
            model.AllMatchesCount = await reportService.AllReportsCountAsync();

            model.Page = page.Value;
            model.Newest = newest.Value;
            model.AllMatchesCount = await reportService.AllReportsCountAsync();

            var report = await reportService.GetReportsAsync(page.Value, newest.Value);

            var lastPage = Math.Ceiling((double)model.AllMatchesCount);

            if (report == null && page.Value > 1)
            {
                return Redirect($"/Admin/Report/Manage?page={lastPage}&newest={newest}");
            }

            if (report != null)
            {
                model.Report = report;
            }

            ViewBag.ReturnUrl = $"/Admin/Report/Manage?page={page}&newest={newest}";
            ViewBag.LastPage = lastPage;
            ViewBag.PreviousPage = page - 1;
            ViewBag.NextPage = page + 1;

            return View(model);
        }
    }
}
