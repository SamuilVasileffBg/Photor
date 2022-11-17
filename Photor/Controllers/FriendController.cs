using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Photor.Core.Contracts;
using System.Security.Claims;

namespace Photor.Controllers
{
    [Authorize]
    public class FriendController : Controller
    {
        private readonly IFriendService friendService;

        public FriendController(IFriendService friendService)
        {
            this.friendService = friendService;
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

        private string? GetUserId() => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}
