using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Photor.Core.Contracts;
using Photor.Core.Models.Post;
using Photor.Extensions;

namespace Photor.Controllers
{
    [Authorize]
    public class LikeController : Controller
    {
        private readonly ILikeService likeService;
        private readonly IPostService postService;
        private readonly IFriendService friendService;

        public LikeController(ILikeService likeService,
            IPostService postService,
            IFriendService friendService)
        {
            this.likeService = likeService;
            this.postService = postService;
            this.friendService = friendService;
        }

        public async Task<IActionResult> Add(ViewPostViewModel model)
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
                    throw new Exception("User has no right to like this post.");
                }

                await likeService
                    .AddLikeAsync(model.Id, userId);

                return RedirectToAction("View", "Post", new { id = model.Id, commentFieldValue = model.CommentValue });
            }
            catch (Exception e)
            {
                return View("Error", e.Message);
            }

        }

        public async Task<IActionResult> DeleteLike(ViewPostViewModel model)
        {
            try
            {
                await likeService
                    .DeleteLikeAsync(model.Id, User.Id());

                return RedirectToAction("View", "Post", new { id = model.Id, commentFieldValue = model.CommentValue });
            }
            catch (Exception e)
            {
                return View("Error", e.Message);
            }
        }
    }
}
