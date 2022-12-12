using Microsoft.AspNetCore.Mvc;
using Photor.Core.Contracts;
using Photor.Core.Models.Report;
using Photor.Extensions;

namespace Photor.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService reportService;
        private readonly IPostService postService;

        public ReportController(IReportService reportService, IPostService postService)
        {
            this.reportService = reportService;
            this.postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> ReportPost(Guid id)
        {
            var post = await postService.GetPostAsync(id.ToString());
            var userId = User.Id();

            if (post == null)
            {
                throw new Exception("Post not found.");
            }

            if (await postService.AccessibleAsync(post, userId) == false)
            {
                throw new Exception("No access.");
            }

            ViewBag.Post = post;

            var model = new ReportPostViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ReportPost(ReportPostViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            var post = await postService.GetPostAsync(model.PostId.ToString());
            var userId = User.Id();

            if (post == null)
            {
                throw new Exception("Post not found.");
            }

            if (await postService.AccessibleAsync(post, userId) == false)
            {
                throw new Exception("No access.");
            }

            await reportService
                .ReportPost(model.PostId, userId, model.Reason);

            return RedirectToAction("View", "Post", new { id = model.PostId });
        }
    }
}
