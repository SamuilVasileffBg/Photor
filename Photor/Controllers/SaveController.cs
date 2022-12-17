using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Photor.Core.Contracts;
using Photor.Core.Models.Post;
using Photor.Extensions;
using static Photor.Infrastructure.Data.Constants.PaginationConstants;
using Photor.Infrastructure.Data.Models;

namespace Photor.Controllers
{
    [Authorize]
    public class SaveController : Controller
    {
        private readonly ISaveService saveService;
        private readonly IPostService postService;
        private readonly IFriendService friendService;

        public SaveController(ISaveService saveService,
            IPostService postService,
            IFriendService friendService)
        {
            this.saveService = saveService;
            this.postService = postService;
            this.friendService = friendService;
        }

        public async Task<IActionResult> Add(ViewPostViewModel model, string? returnUrl)
        {
            try
            {
                var post = await postService
                    .GetPostAsync(model.Id.ToString());

                if (post == null)
                {
                    throw new Exception("Post doesn't exist.");
                }

                var userId = User.Id();

                if (post.FriendsOnly == true &&
                    post.UserId != userId &&
                    await friendService.FindUserFriendAsync(userId, post.UserId) == null)
                {
                    throw new Exception("User has no right to save this post.");
                }

                await saveService
                    .AddSaveAsync(model.Id, userId);

                if (String.IsNullOrEmpty(returnUrl) == false)
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("View", "Post", new { id = model.Id, commentFieldValue = model.CommentValue });
            }
            catch (Exception e)
            {
                return View("Error", e.Message);
            }
        }

        public async Task<IActionResult> Delete(ViewPostViewModel model, string? returnUrl)
        {
            try
            {
                await saveService
                    .DeleteSaveAsync(model.Id, User.Id());

                if (String.IsNullOrEmpty(returnUrl) == false)
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("View", "Post", new { id = model.Id, commentFieldValue = model.CommentValue });
            }
            catch (Exception e)
            {
                return View("Error", e.Message);
            }

        }

        public async Task<IActionResult> List(int? page)
        {
            try
            {
                var userId = User.Id();

                ViewBag.ReturnUrl = "/Save/List";

                //return View(model);


                if (page == null || page < 1)
                {
                    return RedirectToAction(nameof(List), new { page = 1 });
                }

                var model = new PostsPaginationViewModel();
                model.UserId = userId;
                model.Page = page.Value;
                model.AllPostsCount = await saveService.GetSavedPostsCountAsync(userId);

                var posts = (await saveService
                    .GetSavedPostsAsync(userId, page.Value))
                    .Select(usp => usp.Post)
                    .ToList();

                var lastPage = Math.Ceiling((double)model.AllPostsCount / PostsPerPage);

                if ((posts?.Count() ?? 0) == 0 && page.Value > 1)
                {
                    return RedirectToAction(nameof(List), new { page = lastPage });
                }

                if (posts != null)
                {
                    model.Posts = posts.ToList();
                }

                ViewBag.ReturnUrl = $"/Save/List?page={page}";
                ViewBag.LastPage = lastPage;

                return View(model);
            }
            catch (Exception e)
            {
                return View("Error", e.Message);
            }
        }
    }
}
