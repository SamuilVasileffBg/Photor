using Microsoft.AspNetCore.Http;
using MockQueryable.Moq;
using Photor.Core.Contracts;
using Photor.Core.Models.Post;
using Photor.Core.Services;
using Photor.Infrastructure.Data.Common;
using Photor.Infrastructure.Data.Models;
using System.Text;

namespace Photor.UnitTests
{
    public class PostServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task AddPostShouldWorkCorrectly()
        {
            var list = new List<Post>();

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.AddAsync(It.IsAny<Post>()))
                .Callback((Post post) => list.Add(post));

            var mockFriendService = new Mock<IFriendService>();
            
            var mockGoogleDriveService = new Mock<IGoogleDriveService>();

            mockGoogleDriveService
                .Setup(s => s.UploadImageAsync(It.IsAny<IFormFile>()))
                .Returns(Task.FromResult("https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey"));

            var postService = new PostService(mockRepo.Object, mockFriendService.Object, mockGoogleDriveService.Object);

            var postViewModel = new AddPostViewModel()
            {
                Description = "Some cool description",
                FriendsOnly = true,
                Image = new Mock<IFormFile>().Object,
                UserId = "Pesho",
            };

            await postService.AddPostAsync(postViewModel);

            Assert.That(list.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task DeletePostShouldWorkCorrectly()
        {
            var postId = Guid.Parse("a15b8156-7aa3-429d-992d-bc1df50e56b1");

            var likeList = new List<UserLikedPost>()
            {
                new UserLikedPost()
                {
                    UserId = "Pesho",
                    PostId = postId,
                    IsDeleted = false,
                    DateTime = DateTime.UtcNow,
                },
                new UserLikedPost()
                {
                    UserId = "Tosho",
                    PostId = postId,
                    IsDeleted = false,
                    DateTime = DateTime.UtcNow,
                },
                new UserLikedPost()
                {
                    UserId = "Gosho",
                    PostId = postId,
                    IsDeleted = false,
                    DateTime = DateTime.UtcNow,
                },
            };

            var reportList = new List<UserPostReport>()
            {
                new UserPostReport()
                {
                    PostId = postId,
                    DateTime = DateTime.UtcNow,
                    IsDeleted = false,
                    UserId = "Gosho",
                    Reason = "reasonreasonreason",
                },
                new UserPostReport()
                {
                    PostId = postId,
                    DateTime = DateTime.UtcNow,
                    IsDeleted = false,
                    UserId = "Pesho",
                    Reason = "reasonreasonreason",
                },
                new UserPostReport()
                {
                    PostId = postId,
                    DateTime = DateTime.UtcNow,
                    IsDeleted = false,
                    UserId = "Tosho",
                    Reason = "reasonreasonreason",
                },
            };

            var saveList = new List<UserSavedPost>()
            {
                new UserSavedPost()
                {
                    PostId = postId,
                    UserId = "Pesho",
                },
                new UserSavedPost()
                {
                    PostId = postId,
                    UserId = "Tosho",
                },
                new UserSavedPost()
                {
                    PostId = postId,
                    UserId = "Gosho",
                },
            };

            var commentList = new List<UserPostComment>()
            {
                new UserPostComment()
                {
                    PostId = postId,
                    Content = "contentcontentcontent",
                    UserId = "Pesho",
                },
                new UserPostComment()
                {
                    PostId = postId,
                    Content = "contentcontentcontent",
                    UserId = "Tosho",
                },
                new UserPostComment()
                {
                    PostId = postId,
                    Content = "contentcontentcontent",
                    UserId = "Gosho",
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
                    PostLikes = likeList,
                    PostComments = commentList,
                    PostReports = reportList,
                    PostSaves = saveList,
                },
            };

            var mockRepository = new Mock<IRepository>();
            mockRepository
                .Setup(r => r.All<Post>())
                .Returns(postList.BuildMock());

            var mockFriendService = new Mock<IFriendService>();
            var mockGoogleDriveService = new Mock<IGoogleDriveService>();

            var postService = new PostService(mockRepository.Object, mockFriendService.Object, mockGoogleDriveService.Object);

            await postService.DeletePostAsync(postId);

            Assert.That(postList.First().IsDeleted, Is.True);
            Assert.That(postList.First().PostLikes.Any(pl => pl.IsDeleted == false), Is.False);
            Assert.That(postList.First().PostComments.Any(pc => pc.IsDeleted == false), Is.False);
            Assert.That(postList.First().PostSaves.Any(ps => ps.IsDeleted == false), Is.False);
            Assert.That(postList.First().PostReports.Any(pr => pr.IsDeleted == false), Is.False);
        }

        [Test]
        public async Task DeletePostShouldThrowAnExceptionWhenPassedAnInvalidPostId()
        {
            var postList = new List<Post>();

            var mockRepository = new Mock<IRepository>();
            mockRepository
                .Setup(r => r.All<Post>())
                .Returns(postList.BuildMock());

            var mockFriendService = new Mock<IFriendService>();
            var mockGoogleDriveService = new Mock<IGoogleDriveService>();

            var postService = new PostService(mockRepository.Object, mockFriendService.Object, mockGoogleDriveService.Object);

            Assert
                .ThrowsAsync(typeof(Exception),
                async () => await postService.DeletePostAsync(Guid.Parse("6f03c524-9104-467f-a882-7793cdb78972")));
        }

        [Test]
        public async Task AccessibleShouldReturnTrueIfPostIsNotFriendOnly()
        {
            var mockRepository = new Mock<IRepository>();
            var mockFriendService = new Mock<IFriendService>();
            var mockGoogleDriveService = new Mock<IGoogleDriveService>();

            var postService = new PostService(mockRepository.Object, mockFriendService.Object, mockGoogleDriveService.Object);

            var post = new Post()
            {
                ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                UserId = "Pesho",
                FriendsOnly = false,
            };

            var result = await postService.Accessible(post, "Gosho");

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task AccessibleShouldReturnTrueIfPostIsFriendsOnlyAndUserIsAccessingTheirOwnPost()
        {
            var mockRepository = new Mock<IRepository>();
            var mockFriendService = new Mock<IFriendService>();
            var mockGoogleDriveService = new Mock<IGoogleDriveService>();

            var postService = new PostService(mockRepository.Object, mockFriendService.Object, mockGoogleDriveService.Object);

            var post = new Post()
            {
                ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                UserId = "Pesho",
                FriendsOnly = true,
            };

            var result = await postService.Accessible(post, "Pesho");

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task AccessibleShouldReturnTrueIfPostIsFriendsOnlyAndThereIsAnExistingFriendshipBetweenUsers()
        {
            var mockRepository = new Mock<IRepository>();

            var userFriend = new UserFriend()
            {
                UserId = "Pesho",
                FriendId = "Gosho",
            };

            var mockFriendService = new Mock<IFriendService>();
            mockFriendService
                .Setup(s => s.FindUserFriendAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(userFriend));

            var mockGoogleDriveService = new Mock<IGoogleDriveService>();

            var postService = new PostService(mockRepository.Object, mockFriendService.Object, mockGoogleDriveService.Object);

            var post = new Post()
            {
                ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                UserId = "Pesho",
                FriendsOnly = true,
            };

            var result = await postService.Accessible(post, "Gosho");

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task AccessibleShouldReturnFalseIfPostIsFriendsOnlyAndThereIsNotAnExistingFriendshipBetweenUsers()
        {
            var mockRepository = new Mock<IRepository>();

            UserFriend? userFriend = null;

            var mockFriendService = new Mock<IFriendService>();
            mockFriendService
                .Setup(s => s.FindUserFriendAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(userFriend));

            var mockGoogleDriveService = new Mock<IGoogleDriveService>();

            var postService = new PostService(mockRepository.Object, mockFriendService.Object, mockGoogleDriveService.Object);

            var post = new Post()
            {
                ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                UserId = "Pesho",
                FriendsOnly = true,
            };

            var result = await postService.Accessible(post, "Gosho");

            Assert.That(result, Is.False);
        }
    }
}