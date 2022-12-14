using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Photor.Core.Contracts;
using Photor.Core.Extensions;
using Photor.Core.Models.Identity;
using Photor.Core.Models.Post;
using Photor.Core.Models.User;
using Photor.Core.Parsers;
using Photor.Extensions;
using Photor.Infrastructure.Data;
using Photor.Infrastructure.Data.Models;
using static Photor.Infrastructure.Data.Constants.PaginationConstants;
using static Photor.Infrastructure.Data.Constants.Image;

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
                ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");

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
        public IActionResult Login(string? ReturnUrl)
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new LoginViewModel();

            ViewBag.returnUrl = ReturnUrl;

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? ReturnUrl)
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
                    if (String.IsNullOrEmpty(ReturnUrl) == false)
                    {
                        return Redirect(ReturnUrl);
                    }

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
        public async Task<IActionResult> Account(string id, int? page)
        {
            var user = await userService.GetUserByIdAsync(id);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var model = user.ParseToViewModel();

            if (page == null || page < 1)
            {
                return RedirectToAction(nameof(Account), new { id, page = 1 });
            }

            var paginationModel = new PostsPaginationViewModel();
            paginationModel.UserId = id;
            paginationModel.Page = page.Value;
            paginationModel.AllPostsCount = await postService.GetUserPostsCountAsync(id);

            var posts = (await postService.GetUserPostsAsync(id, page.Value))
                .ToList();

            var lastPage = Math.Ceiling((double)paginationModel.AllPostsCount / PostsPerPage);

            if ((posts?.Count() ?? 0) == 0 && page.Value > 1)
            {
                return RedirectToAction(nameof(Account), new { id, page = lastPage });
            }

            if (posts != null)
            {
                paginationModel.Posts = posts.ToList();
            }

            ViewBag.ReturnUrl = $"/User/Account/{id}?page={page}";
            ViewBag.LastPage = lastPage;
            ViewBag.PaginationModel = paginationModel;
            ViewBag.PreviousPage = page - 1;
            ViewBag.NextPage = page + 1;
            ViewBag.Action = nameof(Account);
            ViewBag.Controller = "User";
            ViewBag.NoDataText = "User has no posts.";

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(string id, string? returnUrl)
        {
            var user = await userService
                .GetUserByIdAsync(id);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var model = user
                .ParseToViewModel();

            if (User.Id() != id)
            {
                throw new Exception("Cannot edit someone else' account.");
            }

            ViewBag.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model, string? returnUrl)
        {
            if (User.Id() != model.Id)
            {
                throw new Exception("Cannot edit someone else' account.");
            }

            if ((model.Image?.ImageFormatIsValid() ?? true) == false)
            {
                ModelState.AddModelError("Image", $"{AllowedFormats} extensions are allowed.");
            }

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await userService.EditAccountAsync(model);

            if (String.IsNullOrEmpty(returnUrl) == false)
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(Account), nameof(User), new { id = model.Id });
        }
    }
}
