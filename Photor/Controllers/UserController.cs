﻿using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpPost]
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

        [AllowAnonymous]
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

        [AllowAnonymous]
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
        public async Task<IActionResult> Search(string? searchValue, int? page)
        {
            var model = new UserSearchViewModel();
            model.SearchValue = searchValue;
            model.Page = page;
            model.AllMatchesCount = await userService.SearchUsersCountAsync(searchValue);

            if (page == null || page < 1)
            {
                return RedirectToAction(nameof(Search), new { searchValue, page = 1 });
            }

            var users = await userService.SearchUsersAsync(searchValue, page.Value);

            if ((users?.Count() ?? 0) == 0 && page.Value > 1)
            {
                return RedirectToAction(nameof(Search), new { searchValue, page = Math.Ceiling((double)model.AllMatchesCount / 5) });
            }

            if (users != null)
            {
                model.Users = users.ToList();
            }

            ViewBag.ReturnUrl = $"/User/Search?searchValue={searchValue}&page={page}";

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Search(UserSearchViewModel model)
        {
            return RedirectToAction(nameof(Search), nameof(User), new { searchValue = model.SearchValue, page = model.Page });
        }

        [Authorize]
        public async Task<IActionResult> Account(string id)
        {
            var model = (await userService.GetUserByIdAsync(id)).ParseToViewModel();

            var friendOnlyAccess = (await friendService.FindUserFriendAsync(id, User.Id()) != null ? true : false) || User.Id() == id;

            model.Posts = (await postService.GetUserPostsAsync(id))
                .Where(p => p.FriendsOnly == false ||
                (p.FriendsOnly == true && friendOnlyAccess))
                .ToList();

            ViewBag.ReturnUrl = $"/User/Account/{id}";

            return View(model);
        }

        [Authorize]
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
