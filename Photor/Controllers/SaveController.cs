﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Photor.Core.Contracts;
using Photor.Core.Models.Post;
using Photor.Extensions;
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

        public async Task<IActionResult> Delete(ViewPostViewModel model, string? returnUrl)
        {
            await saveService
                .DeleteSaveAsync(model.Id, User.Id());

            if (String.IsNullOrEmpty(returnUrl) == false)
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("View", "Post", new { id = model.Id, commentFieldValue = model.CommentValue });
        }

        public async Task<IActionResult> List()
        {
            var userId = User.Id();

            var savedPosts = (await saveService
                .GetSavedPostsAsync(userId))
                .Select(usp => usp.Post)
                .ToList();

            List<Post> model = new List<Post>();

            foreach (var post in savedPosts)
            {
                if (post.FriendsOnly == false)
                {
                    model.Add(post);
                    continue;
                }

                var friendOnlyAccess = (await friendService.FindUserFriendAsync(post.UserId, User.Id()) != null ? true : false) || User.Id() == post.UserId;

                if (friendOnlyAccess == true)
                {
                    model.Add(post);
                }
            }

            ViewBag.ReturnUrl = "/Save/List";

            return View(model);
        }
    }
}