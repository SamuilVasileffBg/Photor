using Microsoft.AspNetCore.Http;
using Photor.Core.Contracts;
using Photor.Core.Models.User;
using Photor.Core.Services;
using Photor.Infrastructure.Data.Common;
using Photor.Infrastructure.Data.Models;

namespace Photor.UnitTests
{
    public class UserServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task SearchUsersAsyncShouldWorkCorrectly()
        {
            var userList = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "SamiAdmin",
                FirstName = "Samuil",
                LastName = "Vasilev",
                NormalizedUserName = "SAMIADMIN",
                Email = "samiadmin@mail.com",
                NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                },
                new ApplicationUser()
                {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "Samuil",
                FirstName = "Samuil",
                LastName = "Vasilev",
                NormalizedUserName = "SAMIADMIN",
                Email = "samiadmin@mail.com",
                NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                },
                new ApplicationUser()
                {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "Samuel",
                FirstName = "Samuil",
                LastName = "Vasilev",
                NormalizedUserName = "SAMIADMIN",
                Email = "samiadmin@mail.com",
                NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                },
                new ApplicationUser()
                {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "Samster",
                FirstName = "Samuil",
                LastName = "Vasilev",
                NormalizedUserName = "SAMIADMIN",
                Email = "samiadmin@mail.com",
                NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                },
                new ApplicationUser()
                {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "Samit0o",
                FirstName = "Samuil",
                LastName = "Vasilev",
                NormalizedUserName = "SAMIADMIN",
                Email = "samiadmin@mail.com",
                NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                },
                new ApplicationUser()
                {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "GotinSami",
                FirstName = "Samuil",
                LastName = "Vasilev",
                NormalizedUserName = "SAMIADMIN",
                Email = "samiadmin@mail.com",
                NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                },
                new ApplicationUser()
                {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "samuilsamuil",
                FirstName = "Samuil",
                LastName = "Vasilev",
                NormalizedUserName = "SAMIADMIN",
                Email = "samiadmin@mail.com",
                NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                },
        };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<ApplicationUser>())
                .Returns(userList.BuildMock());
            mockRepo
                .Setup(r => r.AddAsync(It.IsAny<ApplicationUser>()))
                .Callback((ApplicationUser user) => userList.Add(user));

            var mockGoogleDriveService = new Mock<IGoogleDriveService>();
            mockGoogleDriveService
                .Setup(s => s.UploadImageAsync(It.IsAny<IFormFile>()))
                .Returns(Task.FromResult("https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey"));

            var userService = new UserService(mockGoogleDriveService.Object, mockRepo.Object);

            var searchValue = "Sam";

            var firstPageResult = await userService.SearchUsersAsync(searchValue, 1);
            var secondPageResult = await userService.SearchUsersAsync(searchValue, 2);
            var thirdPageResult = await userService.SearchUsersAsync(searchValue, 3);
            var fourthResult = await userService.SearchUsersAsync(null, 1);

            Assert.Multiple(() =>
            {
                Assert.That(firstPageResult.Count, Is.EqualTo(5));
                Assert.That(secondPageResult.Count, Is.EqualTo(2));
                Assert.That(thirdPageResult.Count, Is.EqualTo(0));
                Assert.That(fourthResult, Is.Null);
            });
        }

        [Test]
        public async Task SearchUsersCountAsyncShouldWorkCorrectly()
        {
            var userList = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "SamiAdmin",
                FirstName = "Samuil",
                LastName = "Vasilev",
                NormalizedUserName = "SAMIADMIN",
                Email = "samiadmin@mail.com",
                NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                },
                new ApplicationUser()
                {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "Samuil",
                FirstName = "Samuil",
                LastName = "Vasilev",
                NormalizedUserName = "SAMIADMIN",
                Email = "samiadmin@mail.com",
                NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                },
                new ApplicationUser()
                {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "Samuel",
                FirstName = "Samuil",
                LastName = "Vasilev",
                NormalizedUserName = "SAMIADMIN",
                Email = "samiadmin@mail.com",
                NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                },
                new ApplicationUser()
                {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "Samster",
                FirstName = "Samuil",
                LastName = "Vasilev",
                NormalizedUserName = "SAMIADMIN",
                Email = "samiadmin@mail.com",
                NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                },
                new ApplicationUser()
                {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "Samit0o",
                FirstName = "Samuil",
                LastName = "Vasilev",
                NormalizedUserName = "SAMIADMIN",
                Email = "samiadmin@mail.com",
                NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                },
                new ApplicationUser()
                {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "GotinSami",
                FirstName = "Samuil",
                LastName = "Vasilev",
                NormalizedUserName = "SAMIADMIN",
                Email = "samiadmin@mail.com",
                NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                },
                new ApplicationUser()
                {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "samuilsamuil",
                FirstName = "Samuil",
                LastName = "Vasilev",
                NormalizedUserName = "SAMIADMIN",
                Email = "samiadmin@mail.com",
                NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                },
        };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<ApplicationUser>())
                .Returns(userList.BuildMock());
            mockRepo
                .Setup(r => r.AddAsync(It.IsAny<ApplicationUser>()))
                .Callback((ApplicationUser user) => userList.Add(user));

            var mockGoogleDriveService = new Mock<IGoogleDriveService>();
            mockGoogleDriveService
                .Setup(s => s.UploadImageAsync(It.IsAny<IFormFile>()))
                .Returns(Task.FromResult("https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey"));

            var userService = new UserService(mockGoogleDriveService.Object, mockRepo.Object);

            var firstSearchValue = "Sam";
            var secondSearchValue = "invalidsearchvalue";

            var firstCount = await userService.SearchUsersCountAsync(firstSearchValue);
            var secondCount = await userService.SearchUsersCountAsync(secondSearchValue);
            var thirdCount = await userService.SearchUsersCountAsync(null);

            Assert.Multiple(() =>
            {
                Assert.That(userList, Has.Count.EqualTo(firstCount));
                Assert.That(secondCount, Is.EqualTo(0));
                Assert.That(thirdCount, Is.EqualTo(0));
            });
        }

        [Test]
        public async Task GetUserByIdAsyncShouldWorkCorrectly()
        {
            var userId = "dea12856-c198-4129-b3f3-b893d8395082";
            var user = new ApplicationUser()
            {
                Id = userId,
                UserName = "SamiAdmin",
                FirstName = "Samuil",
                LastName = "Vasilev",
                NormalizedUserName = "SAMIADMIN",
                Email = "samiadmin@mail.com",
                NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
            };

            var userList = new List<ApplicationUser>()
            {
                user,
            };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<ApplicationUser>())
                .Returns(userList.BuildMock());
            mockRepo
                .Setup(r => r.AddAsync(It.IsAny<ApplicationUser>()))
                .Callback((ApplicationUser user) => userList.Add(user));

            var mockGoogleDriveService = new Mock<IGoogleDriveService>();
            mockGoogleDriveService
                .Setup(s => s.UploadImageAsync(It.IsAny<IFormFile>()))
                .Returns(Task.FromResult("https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey"));

            var userService = new UserService(mockGoogleDriveService.Object, mockRepo.Object);

            var searchValue = "Sam";

            var validResult = await userService.GetUserByIdAsync(userId);
            var nullResult = await userService.GetUserByIdAsync("Gosho");

            Assert.Multiple(() =>
            {
                Assert.That(validResult, Is.SameAs(user));
                Assert.That(nullResult, Is.Null);
            });
        }

        [Test]
        public async Task EditAccountShouldWorkCorrectly()
        {
            var userId = "dea12856-c198-4129-b3f3-b893d8395082";
            var user = new ApplicationUser()
            {
                Id = userId,
                UserName = "SamiAdmin",
                FirstName = "Samuil",
                LastName = "Vasilev",
                NormalizedUserName = "SAMIADMIN",
                Email = "samiadmin@mail.com",
                NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
            };

            var userList = new List<ApplicationUser>()
            {
                user,
            };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<ApplicationUser>())
                .Returns(userList.BuildMock());
            mockRepo
                .Setup(r => r.AddAsync(It.IsAny<ApplicationUser>()))
                .Callback((ApplicationUser user) => userList.Add(user));

            var mockFormFile = new Mock<IFormFile>();

            var editedImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_eyEDITEDURL";
            var editedFirstName = "SamuilEDITED";
            var editedLastName = "VasilevEDITED";

            var mockGoogleDriveService = new Mock<IGoogleDriveService>();
            mockGoogleDriveService
                .Setup(s => s.UploadImageAsync(It.IsAny<IFormFile>()))
                .Returns(Task.FromResult(editedImageUrl));

            var userService = new UserService(mockGoogleDriveService.Object, mockRepo.Object);

            var userViewModel = new UserViewModel()
            {
                Id = userId,
                UserName = "SamiAdmin",
                FirstName = editedFirstName,
                LastName = editedLastName,
                Image = mockFormFile.Object,
            };

            await userService.EditAccountAsync(userViewModel);

            Assert.Multiple(() =>
            {
                Assert.That(user.FirstName, Is.EqualTo(editedFirstName));
                Assert.That(user.LastName, Is.EqualTo(editedLastName));
                Assert.That(user.ImageUrl, Is.EqualTo(editedImageUrl));
            });
        }

        [Test]
        public async Task EditAccountShouldThrowAnExceptionWhenTryingToEditAnUnexistingUser()
        {
            var userId = "dea12856-c198-4129-b3f3-b893d8395082";
            var user = new ApplicationUser()
            {
                Id = userId,
                UserName = "SamiAdmin",
                FirstName = "Samuil",
                LastName = "Vasilev",
                NormalizedUserName = "SAMIADMIN",
                Email = "samiadmin@mail.com",
                NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
            };

            var userList = new List<ApplicationUser>()
            {
            };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<ApplicationUser>())
                .Returns(userList.BuildMock());
            mockRepo
                .Setup(r => r.AddAsync(It.IsAny<ApplicationUser>()))
                .Callback((ApplicationUser user) => userList.Add(user));

            var mockFormFile = new Mock<IFormFile>();

            var editedImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_eyEDITEDURL";
            var editedFirstName = "SamuilEDITED";
            var editedLastName = "VasilevEDITED";

            var mockGoogleDriveService = new Mock<IGoogleDriveService>();
            mockGoogleDriveService
                .Setup(s => s.UploadImageAsync(It.IsAny<IFormFile>()))
                .Returns(Task.FromResult(editedImageUrl));

            var userService = new UserService(mockGoogleDriveService.Object, mockRepo.Object);

            var userViewModel = new UserViewModel()
            {
                Id = userId,
                UserName = "SamiAdmin",
                FirstName = editedFirstName,
                LastName = editedLastName,
                Image = mockFormFile.Object,
            };

            Assert
                .ThrowsAsync(typeof(ArgumentNullException),
                async () => await userService.EditAccountAsync(userViewModel));
        }

        [Test]
        public async Task EditAccountShouldThrowAnExceptionWhenImageUploadFailsAndReturnsNullOrEmpty()
        {
            var userId = "dea12856-c198-4129-b3f3-b893d8395082";
            var user = new ApplicationUser()
            {
                Id = userId,
                UserName = "SamiAdmin",
                FirstName = "Samuil",
                LastName = "Vasilev",
                NormalizedUserName = "SAMIADMIN",
                Email = "samiadmin@mail.com",
                NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
            };

            var userList = new List<ApplicationUser>()
            {
                user,
            };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<ApplicationUser>())
                .Returns(userList.BuildMock());
            mockRepo
                .Setup(r => r.AddAsync(It.IsAny<ApplicationUser>()))
                .Callback((ApplicationUser user) => userList.Add(user));

            var mockFormFile = new Mock<IFormFile>();

            var editedImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_eyEDITEDURL";
            var editedFirstName = "SamuilEDITED";
            var editedLastName = "VasilevEDITED";

            var mockGoogleDriveService = new Mock<IGoogleDriveService>();
            mockGoogleDriveService
                .Setup(s => s.UploadImageAsync(It.IsAny<IFormFile>()))
                .Returns(Task.FromResult(String.Empty));

            var userService = new UserService(mockGoogleDriveService.Object, mockRepo.Object);

            var userViewModel = new UserViewModel()
            {
                Id = userId,
                UserName = "SamiAdmin",
                FirstName = editedFirstName,
                LastName = editedLastName,
                Image = mockFormFile.Object,
            };

            Assert
                .ThrowsAsync(typeof(Exception),
                async () => await userService.EditAccountAsync(userViewModel));
        }
    }
}