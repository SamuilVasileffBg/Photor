using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Photor.Core.Contracts;
using Photor.Core.Parsers;
using Photor.Extensions;
using System.Security.Claims;

namespace Photor.Controllers
{
    [Authorize]
    public class FriendController : Controller
    {
        private readonly IFriendService friendService;

        private readonly IUserService userService;

        public FriendController(IFriendService friendService, IUserService userService)
        {
            this.friendService = friendService;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SendFriendInvitation(string receiverId)
        {
            string? senderId = GetUserId();

            if (senderId == null)
            {
                throw new ArgumentNullException(nameof(senderId));
            }

            await friendService.SendFriendInvitationAsync(senderId, receiverId);

            return RedirectToAction("Search", "User");
        }

        public async Task<IActionResult> DeleteFriendInvitation(string id)
        {
            await friendService.DeleteFriendInvitationAsync(User.Id(), id);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> DeleteFriendship(string id)
        {
            string? userId = GetUserId();

            if (userId == null)
            {
                throw new Exception("User id is null.");
            }

            await friendService.RemoveUserFriendAsync(userId, id);

            return RedirectToAction(nameof(FriendList), new { id = userId });
        }

        public async Task<IActionResult> Invitations()
        {
            var userId = GetUserId();

            if (userId == null)
            {
                RedirectToAction("Login", "User");
            }

            var model = await friendService.GetReceivedFriendInvitationsAsync(userId);

            return View(model);
        }

        public async Task<IActionResult> AcceptFriendInvitation(string senderId)
        {
            await friendService.AcceptFriendInvitationAsync(senderId, GetUserId());

            return RedirectToAction(nameof(Invitations));
        }

        public async Task<IActionResult> RejectFriendInvitation(string senderId)
        {
            await friendService.RejectFriendInvitationAsync(senderId, GetUserId());

            return RedirectToAction(nameof(Invitations));
        }

        public async Task<IActionResult> FriendList(string id)
        {
            var user = await userService.GetUserByIdAsync(id);

            if (user == null)
            {
                throw new Exception("There is no user with such id.");
            }

            ViewBag.Title = $"Friends of {user.UserName}";

            var model = (await friendService
                .GetUserFriendsAsync(id))
                .Select(u => u.ParseToViewModel())
                .ToList();

            return View(model);
        }

        private string? GetUserId() => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}
