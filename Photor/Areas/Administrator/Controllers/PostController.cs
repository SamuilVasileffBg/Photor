using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Photor.Core.Contracts;
using Photor.Infrastructure.Data.Common;
using Photor.Infrastructure.Data.Models;

namespace Photor.Areas.Administrator.Controllers
{
    public class PostController : BaseController
    {
        private readonly IPostService postService;
        private readonly IReportService reportService;
        private readonly IRepository repository;

        public PostController(IPostService postService, IReportService reportService, IRepository repository)
        {
            this.postService = postService;
            this.reportService = reportService;
            this.repository = repository;
        }

        public async Task<IActionResult> Delete(Guid id, string? returnUrl)
        {
            try
            {
                var post = await postService
                    .GetPostAsync(id.ToString());

                if (post == null)
                {
                    throw new Exception("Post not found.");
                }

                var anyReport = await repository
                    .All<UserPostReport>()
                    .AnyAsync(upr => upr.PostId == id && upr.IsDeleted == false);

                if (anyReport == false)
                {
                    throw new Exception("Post has no existing reports.");
                }

                await postService
                    .DeletePostAsync(id);

                if (String.IsNullOrEmpty(returnUrl) == false)
                {
                    return Redirect(returnUrl);
                }

                return Redirect("/Admin/Report/Manage");
            }
            catch (Exception e)
            {
                return View("Error", e.Message);
            }
        }

        public async Task<IActionResult> IsOkay(Guid id, string? returnUrl)
        {
            try
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
            catch (Exception e)
            {
                return View("Error", e.Message);
            }
        }
    }
}
