using Microsoft.AspNetCore.Http;
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

            var result = await postService.AccessibleAsync(post, "Gosho");

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

            var result = await postService.AccessibleAsync(post, "Pesho");

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

            var result = await postService.AccessibleAsync(post, "Gosho");

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

            var result = await postService.AccessibleAsync(post, "Gosho");

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task EditPostShouldChangeDescriptionFriendsOnlyAndDateTimeOfLastEditWhenPassedAnValidId()
        {
            var postId = Guid.Parse("8db8180c-0108-4899-b37e-20c68620603a");

            var post = new Post()
            {
                Id = postId,
                UserId = "Pesho",
                FriendsOnly = false,
                DateTimeOfCreation = DateTime.Now,
                ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                Description = "descriptiondescriptiondescription",
            };

            var postList = new List<Post>()
            {
                post,
            };

            var mockRepository = new Mock<IRepository>();
            mockRepository
                .Setup(r => r.All<Post>())
                .Returns(postList.BuildMock());

            var mockFriendService = new Mock<IFriendService>();

            var mockGoogleDriveService = new Mock<IGoogleDriveService>();

            var postService = new PostService(mockRepository.Object, mockFriendService.Object, mockGoogleDriveService.Object);

            var editedDescription = "editedDescription";
            var editedFriendsOnly = true;

            var editPostViewModel = new EditPostViewModel()
            {
                Id = postId,
                Description = editedDescription,
                FriendsOnly = editedFriendsOnly,
                UserId = "Pesho",
            };

            await postService.EditPostAsync(editPostViewModel);

            Assert.Multiple(() =>
            {
                Assert.That(post.FriendsOnly, Is.EqualTo(editedFriendsOnly));
                Assert.That(post.Description, Is.EqualTo(editedDescription));
                Assert.That(post.DateTimeOfLastEdit, Is.Not.EqualTo(null));
            });
        }

        [Test]
        public async Task EditPostShouldThrowAnExceptionWhenPassedAnInvalidPostId()
        {
            var postId = Guid.Parse("8db8180c-0108-1234-b37e-20c68620603a");
            var invalidPostId = Guid.Parse("8db8180c-0108-1111-b37e-20c68620603a");

            var post = new Post()
            {
                Id = invalidPostId,
                UserId = "Pesho",
                FriendsOnly = false,
                DateTimeOfCreation = DateTime.Now,
                ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                Description = "descriptiondescriptiondescription",
            };

            var postList = new List<Post>()
            {
                post,
            };

            var mockRepository = new Mock<IRepository>();
            mockRepository
                .Setup(r => r.All<Post>())
                .Returns(postList.BuildMock());

            var mockFriendService = new Mock<IFriendService>();

            var mockGoogleDriveService = new Mock<IGoogleDriveService>();

            var postService = new PostService(mockRepository.Object, mockFriendService.Object, mockGoogleDriveService.Object);

            var editedDescription = "editedDescription";
            var editedFriendsOnly = true;

            var editPostViewModel = new EditPostViewModel()
            {
                Id = postId,
                Description = editedDescription,
                FriendsOnly = editedFriendsOnly,
                UserId = "Pesho",
            };



            Assert.ThrowsAsync(typeof(Exception), async () => await postService.EditPostAsync(editPostViewModel));
        }

        [Test]
        public async Task GetUserPostsShouldWorkCorrectly()
        {
            var postList = new List<Post>()
            {
                new Post()
                {
                    Id = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d795c"),
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                },
                new Post()
                {
                    Id = Guid.Parse("d5f8f25b-7c6e-403f-a70e-f540dec6db2a"),
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                },
                new Post()
                {
                    Id = Guid.Parse("b2621127-c055-4b4e-a989-4451954e28b5"),
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                },
                new Post()
                {
                    Id = Guid.Parse("2c187459-df77-4952-9642-5424ad515449"),
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                },
                new Post()
                {
                    Id = Guid.Parse("cc61d12a-048b-4362-907b-2cf563594a75"),
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                },
                new Post()
                {
                    Id = Guid.Parse("b42a2487-a008-4c6f-a11e-955dcfada736"),
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                },
                new Post()
                {
                    Id = Guid.Parse("2b99b2e8-63eb-4bce-b772-2919035efb2e"),
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                },
            };

            var mockRepository = new Mock<IRepository>();
            mockRepository
                .Setup(r => r.All<Post>())
                .Returns(postList.BuildMock());

            var mockFriendService = new Mock<IFriendService>();
            var mockGoogleDriveService = new Mock<IGoogleDriveService>();

            var postService = new PostService(mockRepository.Object, mockFriendService.Object, mockGoogleDriveService.Object);

            var pageOneResult = await postService.GetUserPostsAsync("Gosho", 1);
            var pageTwoResult = await postService.GetUserPostsAsync("Gosho", 2);
            var pageThreeResult = await postService.GetUserPostsAsync("Gosho", 3);

            Assert.Multiple(() =>
            {
                Assert.That(pageOneResult, Has.Count.EqualTo(4));
                Assert.That(pageTwoResult, Has.Count.EqualTo(3));
                Assert.That(pageThreeResult, Is.Empty);
            });
        }

        [Test]
        public async Task GetUserPostsCountShouldWorkCorrectly()
        {
            var postList = new List<Post>()
            {
                new Post()
                {
                    Id = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d795c"),
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                },
                new Post()
                {
                    Id = Guid.Parse("d5f8f25b-7c6e-403f-a70e-f540dec6db2a"),
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                },
                new Post()
                {
                    Id = Guid.Parse("b2621127-c055-4b4e-a989-4451954e28b5"),
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                },
                new Post()
                {
                    Id = Guid.Parse("2c187459-df77-4952-9642-5424ad515449"),
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                },
                new Post()
                {
                    Id = Guid.Parse("cc61d12a-048b-4362-907b-2cf563594a75"),
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                },
                new Post()
                {
                    Id = Guid.Parse("b42a2487-a008-4c6f-a11e-955dcfada736"),
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                },
                new Post()
                {
                    Id = Guid.Parse("2b99b2e8-63eb-4bce-b772-2919035efb2e"),
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                },
            };

            var mockRepository = new Mock<IRepository>();
            mockRepository
                .Setup(r => r.All<Post>())
                .Returns(postList.BuildMock());

            var mockFriendService = new Mock<IFriendService>();
            var mockGoogleDriveService = new Mock<IGoogleDriveService>();

            var postService = new PostService(mockRepository.Object, mockFriendService.Object, mockGoogleDriveService.Object);

            var result = await postService.GetUserPostsCountAsync("Gosho");

            Assert.That(result, Is.EqualTo(postList.Count));
        }
    }
}