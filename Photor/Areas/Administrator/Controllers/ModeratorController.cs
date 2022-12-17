using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Photor.Core.Contracts;
using Photor.Infrastructure.Data.Models;

namespace Photor.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ModeratorController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;

        public ModeratorController(UserManager<ApplicationUser> userManager, IUserService userService)
        {
            this.userManager = userManager;
            this.userService = userService;
        }

        public async Task<IActionResult> Become(string id, string? returnUrl)
        {
            try
            {
                var user = await userManager.FindByIdAsync(id);

                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                var isModerator = await userManager.IsInRoleAsync(user, "Moderator");
                var isAdmin = await userManager.IsInRoleAsync(user, "Administrator");

                if (isModerator == true)
                {
                    throw new Exception("User is already a moderator.");
                }

                if (isAdmin == true)
                {
                    throw new Exception("Administrators cannot become moderators.");
                }

                await userManager.AddToRoleAsync(user, "Moderator");

                if (String.IsNullOrEmpty(returnUrl) == false)
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return View("Error", e.Message);
            }
        }

        public async Task<IActionResult> Remove(string id, string? returnUrl)
        {
            try
            {
                var user = await userManager.FindByIdAsync(id);

                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                var isModerator = await userManager.IsInRoleAsync(user, "Moderator");

                if (isModerator == false)
                {
                    throw new Exception("User is not a moderator.");
                }

                await userManager.RemoveFromRoleAsync(user, "Moderator");

                if (String.IsNullOrEmpty(returnUrl) == false)
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return View("Error", e.Message);
            }
        }
    }
}
