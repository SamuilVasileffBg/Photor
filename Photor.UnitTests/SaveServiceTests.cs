using Photor.Core.Services;
using Photor.Infrastructure.Data.Common;
using Photor.Infrastructure.Data.Models;

namespace Photor.UnitTests
{
    public class SaveServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task AddSaveShouldWorkCorrectlyWhenPassedValidParameters()
        {
            var saveList = new List<UserSavedPost>()
            {
            };

            var postId = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d795c");
            var userId = "Pesho";

            var postList = new List<Post>()
            {
                new Post()
                {
                    Id = postId,
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                },
            };

            var mockRepository = new Mock<IRepository>();
            mockRepository
                .Setup(r => r.All<UserSavedPost>())
                .Returns(saveList.BuildMock());
            mockRepository
                .Setup(r => r.AddAsync(It.IsAny<UserSavedPost>()))
                .Callback((UserSavedPost userSavedPost) => saveList.Add(userSavedPost));

            var saveService = new SaveService(mockRepository.Object);

            await saveService.AddSaveAsync(postId, userId);
            Assert.That(saveList.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task AddSaveShouldThrowAnExceptionWhenTryingToSaveAnAlreadySavedPost()
        {

            var postId = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d795c");
            var userId = "Pesho";

            var saveList = new List<UserSavedPost>()
            {
                new UserSavedPost()
                {
                    PostId = postId,
                    UserId = userId,
                },
            };

            var postList = new List<Post>()
            {
                new Post()
                {
                    Id = postId,
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                },
            };

            var mockRepository = new Mock<IRepository>();
            mockRepository
                .Setup(r => r.All<UserSavedPost>())
                .Returns(saveList.BuildMock());
            mockRepository
                .Setup(r => r.AddAsync(It.IsAny<UserSavedPost>()))
                .Callback((UserSavedPost userSavedPost) => saveList.Add(userSavedPost));

            var saveService = new SaveService(mockRepository.Object);

            Assert.ThrowsAsync(typeof(Exception), async () => await saveService.AddSaveAsync(postId, userId));
        }

        [Test]
        public async Task GetSavedPostsAsyncShouldWorkCorrectlyWhenPassedValidParameters()
        {
            var postId = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d795c");
            var userId = "Pesho";

            var saveList = new List<UserSavedPost>()
            {
                new UserSavedPost()
                {
                    PostId = postId,
                    UserId = userId,
                },
                new UserSavedPost()
                {
                    PostId = postId,
                    UserId = userId,
                },
                new UserSavedPost()
                {
                    PostId = postId,
                    UserId = userId,
                },
                new UserSavedPost()
                {
                    PostId = postId,
                    UserId = userId,
                },
                new UserSavedPost()
                {
                    PostId = postId,
                    UserId = userId,
                },
                new UserSavedPost()
                {
                    PostId = postId,
                    UserId = userId,
                },
            };

            var postList = new List<Post>()
            {
                new Post()
                {
                    Id = postId,
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                },
            };

            var mockRepository = new Mock<IRepository>();
            mockRepository
                .Setup(r => r.All<UserSavedPost>())
                .Returns(saveList.BuildMock());
            mockRepository
                .Setup(r => r.AddAsync(It.IsAny<UserSavedPost>()))
                .Callback((UserSavedPost userSavedPost) => saveList.Add(userSavedPost));

            var saveService = new SaveService(mockRepository.Object);

            var firstPageResult = await saveService.GetSavedPostsAsync(userId, 1);
            var secondPageResult = await saveService.GetSavedPostsAsync(userId, 2);
            var thirdPageResult = await saveService.GetSavedPostsAsync(userId, 3);

            Assert.That(firstPageResult.Count, Is.EqualTo(4));
            Assert.That(secondPageResult.Count, Is.EqualTo(2));
            Assert.That(thirdPageResult.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetSavedPostsCountAsyncShouldWorkCorrectlyWhenPassedValidParameters()
        {
            var postId = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d795c");
            var userId = "Pesho";

            var saveList = new List<UserSavedPost>()
            {
                new UserSavedPost()
                {
                    PostId = postId,
                    UserId = userId,
                },
                new UserSavedPost()
                {
                    PostId = postId,
                    UserId = userId,
                },
                new UserSavedPost()
                {
                    PostId = postId,
                    UserId = userId,
                },
                new UserSavedPost()
                {
                    PostId = postId,
                    UserId = userId,
                },
                new UserSavedPost()
                {
                    PostId = postId,
                    UserId = userId,
                },
                new UserSavedPost()
                {
                    PostId = postId,
                    UserId = userId,
                },
            };

            var postList = new List<Post>()
            {
                new Post()
                {
                    Id = postId,
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                },
            };

            var mockRepository = new Mock<IRepository>();
            mockRepository
                .Setup(r => r.All<UserSavedPost>())
                .Returns(saveList.BuildMock());
            mockRepository
                .Setup(r => r.AddAsync(It.IsAny<UserSavedPost>()))
                .Callback((UserSavedPost userSavedPost) => saveList.Add(userSavedPost));

            var saveService = new SaveService(mockRepository.Object);

            var result = await saveService.GetSavedPostsCountAsync(userId);

            Assert.That(saveList, Has.Count.EqualTo(result));
        }

        [Test]
        public async Task DeleteSavedPostShouldWorkCorrectlyWhenPassedValidParameters()
        {
            var postId = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d795c");
            var userId = "Pesho";

            var save = new UserSavedPost()
            {
                PostId = postId,
                UserId = userId,
            };

            var saveList = new List<UserSavedPost>()
            {
                save,
            };

            var postList = new List<Post>()
            {
                new Post()
                {
                    Id = postId,
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                },
            };

            var mockRepository = new Mock<IRepository>();
            mockRepository
                .Setup(r => r.All<UserSavedPost>())
                .Returns(saveList.BuildMock());
            mockRepository
                .Setup(r => r.AddAsync(It.IsAny<UserSavedPost>()))
                .Callback((UserSavedPost userSavedPost) => saveList.Add(userSavedPost));

            var saveService = new SaveService(mockRepository.Object);

            await saveService.DeleteSaveAsync(postId, userId);

            Assert.That(save.IsDeleted, Is.True);
        }

        [Test]
        public async Task DeleteSavedPostShouldShouldThrowAnExceptionWhenTryingToDeleteAnUnexistingSave()
        {
            var postId = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d795c");
            var userId = "Pesho";


            var saveList = new List<UserSavedPost>()
            {
            };

            var postList = new List<Post>()
            {
            };

            var mockRepository = new Mock<IRepository>();
            mockRepository
                .Setup(r => r.All<UserSavedPost>())
                .Returns(saveList.BuildMock());
            mockRepository
                .Setup(r => r.AddAsync(It.IsAny<UserSavedPost>()))
                .Callback((UserSavedPost userSavedPost) => saveList.Add(userSavedPost));

            var saveService = new SaveService(mockRepository.Object);

            Assert.ThrowsAsync(typeof(Exception), async () => await saveService.DeleteSaveAsync(postId, userId));
        }
    }
}