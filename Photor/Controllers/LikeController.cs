using Microsoft.AspNetCore.Mvc;
using Photor.Core.Contracts;
using Photor.Extensions;

namespace Photor.Controllers
{
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

        public async Task<IActionResult> Add(Guid id)
        {
            var post = await postService
                .GetPostAsync(id.ToString());

            if (post == null)
            {
                throw new Exception("Post doesn't exist.");
            }

            var userId = User.Id();

            if (post.FriendsOnly == true &&
                await friendService.FindUserFriendAsync(User.Id(), post.UserId) == null)
            {
                throw new Exception("User has no right to like this post.");
            }

            await likeService
                .AddLikeAsync(id, userId);

            return RedirectToAction("View", "Post", new { id });
        }

        public async Task<IActionResult> DeleteLike(Guid id)
        {
            await likeService
                .DeleteLikeAsync(id, User.Id());

            return RedirectToAction("View", "Post", new { id });
        }
    }
}
