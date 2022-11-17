using Microsoft.AspNetCore.Mvc;
using Photor.Infrastructure.Data;
using Photor.Infrastructure.Data.Models;
using Photor.Models;
using System.Diagnostics;

namespace Photor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            //context.UsersFriends.Add(new UserFriend
            //{
            //    UserId = "6af41298-9c9f-4239-a9a8-42b7e00ee7ae",
            //    FriendId = "6af5244d-16c0-4d31-a9d6-9799a62bb2f6",
            //});
            //
            //context.SaveChanges();

            var firstUser = context
                .Users
                .FirstOrDefault(u => u.Id == "6af41298-9c9f-4239-a9a8-42b7e00ee7ae");

            var secondUser = context
                .Users
                .FirstOrDefault(u => u.Id == "6af5244d-16c0-4d31-a9d6-9799a62bb2f6");

            var usersFriends = context
                .UsersFriends
                .ToList();

            Console.WriteLine($"User {firstUser.UserName} has {firstUser.UsersFriends.Count} friends");
            Console.WriteLine($"User {secondUser.UserName} has {secondUser.UsersFriends.Count} friends");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}