using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Photor.Core.Contracts;
using Photor.Core.Models.Comment;
using Photor.Core.Models.Post;
using Photor.Extensions;

namespace Photor.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly IFriendService friendService;
        private readonly IPostService postService;
        private readonly ICommentService commentService;

        public CommentController(IFriendService friendService, IPostService postService, ICommentService commentService)
        {
            this.friendService = friendService;
            this.postService = postService;
            this.commentService = commentService;
        }

        public async Task<IActionResult> Add(ViewPostViewModel model)
        {
            var post = await postService.GetPostAsync(model.Id.ToString());

            if (post == null)
            {
                throw new Exception("Cannot comment an unexisting post");
            }

            var userId = User.Id();

            if (userId == null)
            {
                throw new Exception("No right to comment this post.");
            }

            if (post.FriendsOnly == true &&
                post.UserId != userId &&
                (await friendService.FindUserFriendAsync(post.UserId, userId)) == null)
            {
                throw new Exception("No right to comment this post.");
            }

            if (model.CommentValue == null)
            {
                return RedirectToAction("View", "Post", new { id = model.Id, errorMessage = "Commend field is required." });
            }

            if (model.CommentValue.Length > 1000)
            {
                return RedirectToAction("View", "Post", new { id = model.Id, commentFieldValue = model.CommentValue, errorMessage = "Your comment shouldn't be longer than 1000 characters." });
                }

            //if (ModelState.IsValid == false)
            //{
            //    return View("View", model);
            //}

            await commentService
                .AddCommentAsync(post.Id, userId, model.CommentValue);

            return RedirectToAction("View", "Post", new { id = model.Id });
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var comment = await commentService.GetCommentAsync(id);

            if (comment == null)
            {
                throw new Exception("Comment not found.");
            }

            if (comment.UserId != User.Id() && comment.Post.UserId != User.Id())
            {
                throw new Exception("You have no right to delete this comment.");
            }

            await commentService.DeleteCommentAsync(id);

            return RedirectToAction("View", "Post", new { id = comment.PostId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var comment = await commentService.GetCommentAsync(id);

            if (comment == null)
            {
                throw new Exception("Comment not found.");
            }

            if (comment.UserId != User.Id())
            {
                throw new Exception("You have no right to edit this comment.");
            }

            var model = new EditCommentViewModel()
            {
                Id = comment.Id,
                PostId = comment.PostId,
                UserId = comment.UserId,
                Content = comment.Content,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCommentViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            if (model.UserId != User.Id())
            {
                throw new Exception("You have no right to edit this comment.");
            }

            await commentService
                .EditCommentAsync(model);

            return RedirectToAction("View", "Post", new { id = model.PostId });
        }
    }
}
