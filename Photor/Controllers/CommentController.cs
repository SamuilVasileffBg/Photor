﻿using Microsoft.AspNetCore.Mvc;
using Photor.Core.Contracts;
using Photor.Core.Models.Post;
using Photor.Extensions;

namespace Photor.Controllers
{
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
            if (model.CommentValue == null)
            {
                ModelState.AddModelError("", "Commend field is required.");
                return View("../Post/View", model);
            }

            //if (ModelState.IsValid == false)
            //{
            //    return View("View", model);
            //}

            var post = await postService.GetPostAsync(model.Id.ToString());

            if (post == null)
            {
                throw new Exception("Cannot comment an unexisting post");
            }

            var userId = User.Id();

            if (post.FriendsOnly == true &&
                post.UserId != userId &&
                (await friendService.FindUserFriendAsync(post.UserId, userId)) == null)
            {
                throw new Exception("No right to comment this post.");
            }

            await commentService
                .AddCommentAsync(post.Id, userId, model.CommentValue);

            return RedirectToAction("View", "Post", new { id = model.Id });
        }
    }
}