using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Photor.Core.Contracts;
using Photor.Core.Models;
using Photor.Core.Parsers;
using Photor.Extensions;

namespace Photor.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddPostViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPostViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            model.UserId = User.Id();

            var id = await postService.AddPostAsync(model);

            Console.WriteLine(id);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> View(string id)
        {
            var post = await postService.GetPostAsync(id);

            var model = new ViewPostViewModel()
            {
                ImageUrl = post.ImageUrl,
                Description = post.Description,
                Id = post.Id,
                User = post.ApplicationUser.ParseToViewModel()
            };

            return View(model);
        }
    }
}
