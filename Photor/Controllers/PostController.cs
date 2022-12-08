using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Photor.Core.Contracts;
using Photor.Core.Models;
using Photor.Core.Models.Post;
using Photor.Core.Parsers;
using Photor.Extensions;

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
            var areFriends = (await friendService.FindUserFriendAsync(post.UserId, userId)) != null;

            if (post.FriendsOnly == true &&
                post.UserId != userId &&
                areFriends == false &&
                userIsAdmin == false)
            {
                throw new Exception("Cannot access this post.");
            }

            ViewBag.Disabled = false;
            if (areFriends == false && userIsAdmin == true)
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

            return View(model);
        }
    }
}
