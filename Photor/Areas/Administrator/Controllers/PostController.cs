using Microsoft.AspNetCore.Mvc;
using Photor.Core.Contracts;

namespace Photor.Areas.Administrator.Controllers
{
    public class PostController : BaseController
    {
        private readonly IPostService postService;
        private readonly IReportService reportService;

        public PostController(IPostService postService, IReportService reportService)
        {
            this.postService = postService;
            this.reportService = reportService;
        }

        public async Task<IActionResult> Delete(Guid id, string? returnUrl)
        {
            var post = await postService
                .GetPostAsync(id.ToString());

            if (post == null)
            {
                throw new Exception("Post not found.");
            }

            await postService
                .DeletePostAsync(id);

            if (String.IsNullOrEmpty(returnUrl) == false)
            {
                return Redirect(returnUrl);
            }

            return Redirect("/Admin/Report/Manage");
        }

        public async Task<IActionResult> IsOkay(Guid id, string? returnUrl)
        {
            var report = await reportService
                .GetReportAsync(id);

            if (report == null)
            {
                throw new Exception("Report not found.");
            }

            await reportService
                .DeleteReportAsync(id);

            if (String.IsNullOrEmpty(returnUrl) == false)
            {
                return Redirect(returnUrl);
            }

            return Redirect("/Administrator/Report/Manage");
        }
    }
}
