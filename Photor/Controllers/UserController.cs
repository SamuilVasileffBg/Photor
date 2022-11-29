using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Photor.Core.Contracts;
using Photor.Core.Models.Identity;
using Photor.Core.Models.User;
using Photor.Core.Parsers;
using Photor.Extensions;
using Photor.Infrastructure.Data;
using Photor.Infrastructure.Data.Models;

namespace Photor.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;
        private readonly IUserService userService;
        private readonly IPostService postService;
        private readonly IFriendService friendService;

        public UserController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            IUserService userService,
            IPostService postService,
            IFriendService friendService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.context = context;
            this.userService = userService;
            this.postService = postService;
            this.friendService = friendService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new RegisterViewModel();

            return View(model);
        }

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            var user = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Login));
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid login!");

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            var data = await context
                .Users
                .Select(u => u.ParseToViewModel())
                .ToListAsync();

            ViewBag.ReturnUrl = "/User/All";

            return View(data);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Search(string? searchValue)
        {
            var model = new UserSearchViewModel();
            model.SearchValue = searchValue;

            var users = await userService.SearchUsersAsync(searchValue);

            if (users != null)
            {
                model.Users = users.ToList();
            }

            ViewBag.ReturnUrl = $"/User/Search?searchValue={searchValue}";

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult SearchUsersById(UserSearchViewModel model)
        {
            return RedirectToAction(nameof(Search), nameof(User), new { searchValue = model.SearchValue });
        }

        public async Task<IActionResult> Account(string id)
        {
            var model = (await userService.GetUserByIdAsync(id)).ParseToViewModel();

            var friendOnlyAccess = (await friendService.FindUserFriendAsync(id, User.Id()) != null ? true : false) || User.Id() == id;

            model.Posts = (await postService.GetUserPostsAsync(id))
                .Where(p => p.FriendsOnly == false ||
                (p.FriendsOnly == true && friendOnlyAccess))
                .ToList();

            ViewBag.ReturnUrl = $"User/Account/{id}";

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = (await userService
                .GetUserByIdAsync(id))
                .ParseToViewModel();

            if (User.Id() != id)
            {
                throw new Exception("Cannot edit someone else' account.");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if (User.Id() != model.Id)
            {
                throw new Exception("Cannot edit someone else' account.");
            }

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await userService.EditAccountAsync(model);

            return RedirectToAction(nameof(Account), nameof(User), new { id = model.Id });
        }
    }
}
