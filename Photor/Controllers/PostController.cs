using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Photor.Core.Contracts;
using Photor.Core.Extensions;
using Photor.Core.Models;
using Photor.Core.Models.Post;
using Photor.Core.Parsers;
using Photor.Extensions;
using static Photor.Infrastructure.Data.Constants.Image;
using static Photor.Infrastructure.Data.Constants.PaginationConstants;

namespace Photor.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly IFriendService friendService;

        public PostController(IPostService postService, IFriendService friendService)
        {
            this.postService = postService;
            this.friendService = friendService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddPostViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPostViewModel model)
        {
            if ((model.Image?.ImageFormatIsValid() ?? true) == false)
            {
                ModelState.AddModelError("Image", $"{AllowedFormats} extensions are allowed.");
            }

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            model.UserId = User.Id();

            var id = await postService.AddPostAsync(model);

            Console.WriteLine(id);

            return RedirectToAction("View", "Post", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var post = await postService
                .GetPostAsync(id);

            if (post == null)
            {
                throw new Exception("There is no such post existing.");
            }

            if (post.UserId != User.Id())
            {
                throw new Exception("User has no right to edit this post.");
            }

            var model = new EditPostViewModel()
            {
                Id = post.Id,
                UserId = post.ApplicationUser.Id,
                Description = post.Description,
                FriendsOnly = post.FriendsOnly,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPostViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            if (User.Id() != model.UserId)
            {
                throw new Exception("User has no right to edit this post.");
            }

            await postService
                .EditPostAsync(model);

            return RedirectToAction("View", "Post", new { id = model.Id });
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var post = await postService
                .GetPostAsync(id.ToString());

            if (post == null)
            {
                throw new Exception("Cannot find such post.");
            }

            if (User.Id() != post.UserId)
            {
                throw new Exception("Cannot delete a post which isn't yours.");
            }

            await postService
                .DeletePostAsync(id);

            return RedirectToAction("Account", "User", new { id = User.Id() });
        }

        public async Task<IActionResult> View(string id, string? commentFieldValue, string? errorMessage)
        {
            var post = await postService.GetPostAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            var userId = User.Id();
            var userIsAdmin = User.IsInRole("Administrator");
            var userIsModerator = User.IsInRole("Moderator");
            var areFriends = (await friendService.FindUserFriendAsync(post.UserId, userId)) != null;

            var postAccess = await postService.AccessibleAsync(post, userId);

            if (postAccess == false &&
                userIsAdmin == false &&
                userIsModerator == false)
            {
                throw new Exception("Cannot access this post.");
            }

            ViewBag.Disabled = false;
            if (postAccess == false)
            {
                ViewBag.Disabled = true;
            }

            var model = new ViewPostViewModel()
            {
                ImageUrl = post.ImageUrl,
                Description = post.Description,
                Id = post.Id,
                User = post.ApplicationUser.ParseToViewModel(),
            };

            if (String.IsNullOrEmpty(commentFieldValue) == false)
            {
                model.CommentValue = commentFieldValue;
            }

            if (String.IsNullOrEmpty(errorMessage) == false)
            {
                ModelState.AddModelError("", errorMessage);
            }

            ViewBag.FriendsOnly = post.FriendsOnly;
            ViewBag.DateTimeAgo = post.DateTimeOfCreation.GetDateTimeDifferenceText();

            if (post.DateTimeOfLastEdit != null)
            {
                ViewBag.LastEdited = post.DateTimeOfLastEdit.Value.GetDateTimeDifferenceText();
            }

            return View(model);
        }

        public async Task<IActionResult> All(int? page)
        {
            if (page == null || page < 1)
            {
                return RedirectToAction(nameof(All), new { page = 1 });
            }

            var id = User.Id();

            var paginationModel = new PostsPaginationViewModel();
            paginationModel.UserId = id;
            paginationModel.Page = page.Value;
            paginationModel.AllPostsCount = await postService.GetAllPostsCountAsync();

            var posts = (await postService.GetAllPostsAsync(page.Value))
                .ToList();

            var lastPage = Math.Ceiling((double)paginationModel.AllPostsCount / PostsPerPage);

            if ((posts?.Count() ?? 0) == 0 && page.Value > 1)
            {
                return RedirectToAction(nameof(All), new { page = lastPage });
            }

            if (posts != null)
            {
                paginationModel.Posts = posts.ToList();
            }

            ViewBag.ReturnUrl = $"/Post/All?page={page}";
            ViewBag.LastPage = lastPage;
            ViewBag.PaginationModel = paginationModel;
            ViewBag.PreviousPage = page - 1;
            ViewBag.NextPage = page + 1;
            ViewBag.Action = nameof(All);
            ViewBag.Controller = "Post";
            ViewBag.NoDataText = "No posts available.";

            return View();
        }
    }
}
