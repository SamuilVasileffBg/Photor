using Photor.Core.Contracts;
using Photor.Core.Services;
using Photor.Infrastructure.Data.Common;
using Photor.Infrastructure.Data.Models;

namespace Photor.UnitTests
{
    public class LikeServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task AddLikeShouldWorkCorrectlyWhenPassedValidParameters()
        {
            var postId = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d795c");

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

            var likeList = new List<UserLikedPost>()
            {
            };

            var mockRepository = new Mock<IRepository>();

            mockRepository
                .Setup(r => r.All<UserLikedPost>())
                .Returns(likeList.BuildMock());
            mockRepository
                .Setup(r => r.All<Post>())
                .Returns(postList.BuildMock());
            mockRepository
                .Setup(r => r.AllReadonly<UserLikedPost>())
                .Returns(likeList.BuildMock());
            mockRepository
                .Setup(r => r.AllReadonly<Post>())
                .Returns(postList.BuildMock());
            mockRepository
                .Setup(r => r.AddAsync(It.IsAny<UserLikedPost>()))
                .Callback((UserLikedPost userLikedPost) => likeList.Add(userLikedPost));

            var mockPostService = new Mock<IPostService>();

            var likeService = new LikeService(mockRepository.Object, mockPostService.Object);

            await likeService.AddLikeAsync(postId, "Gosho");

            Assert.That(likeList, Has.Count.EqualTo(1));
        }

        [Test]
        public async Task AddLikeShouldThrowAnExceptionWhenTryingToLikeAnAlreadyLikedPost()
        {
            var postId = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d795c");

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

            var likeList = new List<UserLikedPost>()
            {
                new UserLikedPost()
                {
                    UserId = "Pesho",
                    PostId = postId,
                },
            };

            var mockRepository = new Mock<IRepository>();

            mockRepository
                .Setup(r => r.All<UserLikedPost>())
                .Returns(likeList.BuildMock());
            mockRepository
                .Setup(r => r.All<Post>())
                .Returns(postList.BuildMock());
            mockRepository
                .Setup(r => r.AllReadonly<UserLikedPost>())
                .Returns(likeList.BuildMock());
            mockRepository
                .Setup(r => r.AllReadonly<Post>())
                .Returns(postList.BuildMock());
            mockRepository
                .Setup(r => r.AddAsync(It.IsAny<UserLikedPost>()))
                .Callback((UserLikedPost userLikedPost) => likeList.Add(userLikedPost));

            var mockPostService = new Mock<IPostService>();

            var likeService = new LikeService(mockRepository.Object, mockPostService.Object);

            Assert.ThrowsAsync(typeof(Exception), async () => await likeService.AddLikeAsync(postId, "Pesho"));
        }

        [Test]
        public async Task DeleteLikeShouldWorkCorrectlyWhenPassedValidParameters()
        {
            var postId = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d795c");

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

            var likedUserId = "Pesho";

            var like = new UserLikedPost()
            {
                UserId = likedUserId,
                PostId = postId,
            };

            var likeList = new List<UserLikedPost>()
            {
                like,
            };

            var mockRepository = new Mock<IRepository>();

            mockRepository
                .Setup(r => r.All<UserLikedPost>())
                .Returns(likeList.BuildMock());
            mockRepository
                .Setup(r => r.All<Post>())
                .Returns(postList.BuildMock());
            mockRepository
                .Setup(r => r.AllReadonly<UserLikedPost>())
                .Returns(likeList.BuildMock());
            mockRepository
                .Setup(r => r.AllReadonly<Post>())
                .Returns(postList.BuildMock());
            mockRepository
                .Setup(r => r.AddAsync(It.IsAny<UserLikedPost>()))
                .Callback((UserLikedPost userLikedPost) => likeList.Add(userLikedPost));

            var mockPostService = new Mock<IPostService>();

            var likeService = new LikeService(mockRepository.Object, mockPostService.Object);

            await likeService.DeleteLikeAsync(postId, likedUserId);

            Assert.That(like.IsDeleted, Is.True);
        }

        [Test]
        public async Task DeleteLikeShouldThrowAnExceptionWhenPostIsNtLiked()
        {
            var postId = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d795c");

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

            var likeList = new List<UserLikedPost>()
            {
            };

            var mockRepository = new Mock<IRepository>();

            mockRepository
                .Setup(r => r.All<UserLikedPost>())
                .Returns(likeList.BuildMock());
            mockRepository
                .Setup(r => r.All<Post>())
                .Returns(postList.BuildMock());
            mockRepository
                .Setup(r => r.AllReadonly<UserLikedPost>())
                .Returns(likeList.BuildMock());
            mockRepository
                .Setup(r => r.AllReadonly<Post>())
                .Returns(postList.BuildMock());
            mockRepository
                .Setup(r => r.AddAsync(It.IsAny<UserLikedPost>()))
                .Callback((UserLikedPost userLikedPost) => likeList.Add(userLikedPost));

            var mockPostService = new Mock<IPostService>();

            var likeService = new LikeService(mockRepository.Object, mockPostService.Object);

            Assert.ThrowsAsync(typeof(Exception), async () => await likeService.DeleteLikeAsync(postId, "Pesho"));
        }

        [Test]
        public async Task GetPostLikesCountShouldWorkCorrectly()
        {
            var firstPostId = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d7951");
            var secondPostId = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d7952");
            var thirdPostId = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d7953");

            var postList = new List<Post>()
            {
                new Post()
                {
                    Id = firstPostId,
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                },
                new Post()
                {
                    Id = secondPostId,
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                },
                new Post()
                {
                    Id = thirdPostId,
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                },
            };

            var likeList = new List<UserLikedPost>()
            {
               new UserLikedPost()
               {
                   UserId = "1",
                   PostId = firstPostId,
               },
               new UserLikedPost()
               {
                   UserId = "2",
                   PostId = firstPostId,
               },
               new UserLikedPost()
               {
                   UserId = "3",
                   PostId = firstPostId,
               },
               new UserLikedPost()
               {
                   UserId = "4",
                   PostId = firstPostId,
               },
               new UserLikedPost()
               {
                   UserId = "1",
                   PostId = secondPostId,
               },
            };

            var mockRepository = new Mock<IRepository>();

            mockRepository
                .Setup(r => r.All<UserLikedPost>())
                .Returns(likeList.BuildMock());
            mockRepository
                .Setup(r => r.All<Post>())
                .Returns(postList.BuildMock());
            mockRepository
                .Setup(r => r.AllReadonly<UserLikedPost>())
                .Returns(likeList.BuildMock());
            mockRepository
                .Setup(r => r.AllReadonly<Post>())
                .Returns(postList.BuildMock());
            mockRepository
                .Setup(r => r.AddAsync(It.IsAny<UserLikedPost>()))
                .Callback((UserLikedPost userLikedPost) => likeList.Add(userLikedPost));

            var mockPostService = new Mock<IPostService>();

            var likeService = new LikeService(mockRepository.Object, mockPostService.Object);

            var firstPostLikesCount = await likeService.GetPostLikesCountAsync(firstPostId);
            var secondPostLikesCount = await likeService.GetPostLikesCountAsync(secondPostId);
            var thirdPostLikesCount = await likeService.GetPostLikesCountAsync(thirdPostId);

            Assert.Multiple(() =>
            {
                Assert.That(firstPostLikesCount, Is.EqualTo(4));
                Assert.That(secondPostLikesCount, Is.EqualTo(1));
                Assert.That(thirdPostLikesCount, Is.EqualTo(0));
            });
        }
    }
}