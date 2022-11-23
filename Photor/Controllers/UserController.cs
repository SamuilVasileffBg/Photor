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
    public class UserController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;
        private readonly IUserService userService;

        public UserController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            IUserService userService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.context = context;
            this.userService = userService;
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
