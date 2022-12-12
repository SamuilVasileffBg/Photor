using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Photor.Core.Contracts;
using Photor.Core.Models.Friend;
using Photor.Core.Models.Post;
using Photor.Core.Parsers;
using Photor.Extensions;
using System.Security.Claims;
using static Photor.Infrastructure.Data.Constants.PaginationConstants;

namespace Photor.Controllers
{
    [Authorize]
    public class FriendController : Controller
    {
        private readonly IFriendService friendService;

        private readonly IUserService userService;

        private readonly IPostService postService;

        public FriendController(IFriendService friendService, IUserService userService, IPostService postService)
        {
            this.friendService = friendService;
            this.userService = userService;
            this.postService = postService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SendFriendInvitation(string receiverId, string? returnUrl)
        {
            string? senderId = User.Id();

            if (senderId == null)
            {
                throw new ArgumentNullException(nameof(senderId));
            }

            if (receiverId == null)
            {
                throw new ArgumentNullException(nameof(receiverId));
            }

            await friendService.SendFriendInvitationAsync(senderId, receiverId);

            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Search", "User");
        }

        public async Task<IActionResult> DeleteFriendInvitation(string id, string? returnUrl)
        {
            var friendInvitation = await friendService.FindFriendInvitationAsync(User.Id(), id);

            if (friendInvitation == null)
            {
                throw new Exception("Friend invitation now found.");
            }

            if (friendInvitation.SenderId != User.Id())
            {
                throw new Exception("No access.");
            }

            await friendService.DeleteFriendInvitationAsync(User.Id(), id);

            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> DeleteFriendship(string id, string? returnUrl)
        {
            string? userId = User.Id();

            if (userId == null)
            {
                throw new Exception("User id is null.");
            }

            await friendService.RemoveUserFriendAsync(userId, id);

            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(List), new { id = userId });
        }

        public async Task<IActionResult> Invitations(int? page)
        {
            var id = User.Id();

            var user = await userService.GetUserByIdAsync(id);

            if (user == null)
            {
                throw new Exception("There is no user with such id.");
            }

            ViewBag.Title = $"{user.UserName}'s received friend invitations";

            var model = new FriendInvitationListViewModel();
            model.UserId = id;
            model.Page = page;
            model.AllInvitationsCount = await friendService.GetReceivedFriendInvitationsCountAsync(id);

            if (page == null || page < 1)
            {
                return RedirectToAction(nameof(Invitations), new { page = 1 });
            }

            var invitations = (await friendService
                .GetReceivedFriendInvitationsAsync(id, page.Value))
                .ToList();

            var lastPage = Math.Ceiling((double)model.AllInvitationsCount / FriendsPerPage);

            if (invitations.Count == 0 && page.Value > 1)
            {
                return RedirectToAction(nameof(Invitations), new { page = lastPage });
            }

            if (invitations != null)
            {
                model.Invitations = invitations.ToList();
            }

            ViewBag.ReturnUrl = $"/Friend/Invitations?page={page}";
            ViewBag.LastPage = lastPage;
            ViewBag.PreviousPage = page - 1;
            ViewBag.NextPage = page + 1;

            return View(model);
        }

        public async Task<IActionResult> AcceptFriendInvitation(string senderId, string? returnUrl)
        {
            var friendInvitation = await friendService.FindFriendInvitationAsync(senderId, User.Id());

            if (friendInvitation == null)
            {
                throw new Exception("Friend invitation now found.");
            }

            if (friendInvitation.ReceiverId != User.Id())
            {
                throw new Exception("No access.");
            }

            await friendService.AcceptFriendInvitationAsync(senderId, User.Id());

            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(Invitations));
        }

        public async Task<IActionResult> RejectFriendInvitation(string senderId, string? returnUrl)
        {
            var friendInvitation = await friendService.FindFriendInvitationAsync(senderId, User.Id());

            if (friendInvitation == null)
            {
                throw new Exception("Friend invitation now found.");
            }

            if (friendInvitation.ReceiverId != User.Id())
            {
                throw new Exception("No access.");
            }

            await friendService.RejectFriendInvitationAsync(senderId, User.Id());

            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(Invitations));
        }

        public async Task<IActionResult> List(string id, int? page)
        {
            var user = await userService.GetUserByIdAsync(id);

            if (user == null)
            {
                throw new Exception("There is no user with such id.");
            }

            ViewBag.Title = $"Friends of {user.UserName}";

            var model = new FriendListViewModel();
            model.UserId = id;
            model.Page = page;
            model.AllFriendsCount = await friendService.GetUserFriendsCountAsync(id);

            if (page == null || page < 1)
            {
                return RedirectToAction(nameof(List), new { id, page = 1 });
            }

            var friends = (await friendService
                .GetUserFriendsAsync(id, page.Value))
                .Select(u => u.ParseToViewModel())
                .ToList();

            if ((friends?.Count() ?? 0) == 0 && page.Value > 1)
            {
                return RedirectToAction(nameof(List), new { id, page = Math.Ceiling((double)model.AllFriendsCount / 5) });
            }

            if (friends != null)
            {
                model.Friends = friends.ToList();
            }

            ViewBag.ReturnUrl = $"/Friend/List/{id}?page={page}";
            ViewBag.LastPage = Math.Ceiling((double)model.AllFriendsCount / 5);

            return View(model);
        }
    }
}
