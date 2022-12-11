using Photor.Core.Models.Comment;
using Photor.Core.Services;
using Photor.Infrastructure.Data.Common;
using Photor.Infrastructure.Data.Models;

namespace Photor.UnitTests
{
    public class CommentServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task AddCommentShouldWorkCorrectly()
        {
            var commentList = new List<UserPostComment>()
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
                    PostComments = new List<UserPostComment>(),
                },
            };

            var mockRepository = new Mock<IRepository>();
            mockRepository
                .Setup(r => r.All<UserPostComment>())
                .Returns(commentList.BuildMock());
            mockRepository
                .Setup(r => r.AddAsync(It.IsAny<UserPostComment>()))
                .Callback((UserPostComment comment) => commentList.Add(comment));

            var commentService = new CommentService(mockRepository.Object);

            await commentService.AddCommentAsync(postId, userId, "This is my new comment");

            Assert.That(commentList, Has.Count.EqualTo(1));
        }

        [Test]
        public async Task GetPostCommentsShouldWorkCorrectly()
        {
            var postId = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d795c");
            var userId = "Pesho";

            var commentList = new List<UserPostComment>()
            {
                new UserPostComment()
                {
                    PostId = postId,
                    UserId = userId,
                    Content = "ContentContentContent"
                },
                new UserPostComment()
                {
                    PostId = postId,
                    UserId = userId,
                    Content = "ContentContentContent"
                },
                new UserPostComment()
                {
                    PostId = postId,
                    UserId = userId,
                    Content = "ContentContentContent"
                },
                new UserPostComment()
                {
                    PostId = postId,
                    UserId = userId,
                    Content = "ContentContentContent"
                },
                new UserPostComment()
                {
                    PostId = postId,
                    UserId = userId,
                    Content = "ContentContentContent"
                },
                new UserPostComment()
                {
                    PostId = postId,
                    UserId = userId,
                    Content = "ContentContentContent"
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
                    PostComments = new List<UserPostComment>(),
                },
            };

            var mockRepository = new Mock<IRepository>();
            mockRepository
                .Setup(r => r.All<UserPostComment>())
                .Returns(commentList.BuildMock());
            mockRepository
                .Setup(r => r.AddAsync(It.IsAny<UserPostComment>()))
                .Callback((UserPostComment comment) => commentList.Add(comment));

            var commentService = new CommentService(mockRepository.Object);

            var firstPageComments = await commentService.GetPostCommentsAsync(postId, 1);
            var secondPageComments = await commentService.GetPostCommentsAsync(postId, 2);
            var threePageComments = await commentService.GetPostCommentsAsync(postId, 3);

            Assert.Multiple(() =>
            {
                Assert.That(firstPageComments, Has.Count.EqualTo(5));
                Assert.That(secondPageComments, Has.Count.EqualTo(1));
                Assert.That(threePageComments, Is.Empty);
            });
        }

        [Test]
        public async Task GetPostCommentShouldWorkCorrectly()
        {
            var postId = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d795c");
            var userId = "Pesho";
            var commentId = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d7123");

            var comment = new UserPostComment()
            {
                Id = commentId,
                PostId = postId,
                UserId = userId,
                Content = "ContentContentContent"
            };

            var commentList = new List<UserPostComment>()
            {
                comment,
            };


            var postList = new List<Post>()
            {
                new Post()
                {
                    Id = postId,
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                    PostComments = new List<UserPostComment>(),
                },
            };

            var mockRepository = new Mock<IRepository>();
            mockRepository
                .Setup(r => r.All<UserPostComment>())
                .Returns(commentList.BuildMock());
            mockRepository
                .Setup(r => r.AddAsync(It.IsAny<UserPostComment>()))
                .Callback((UserPostComment comment) => commentList.Add(comment));

            var commentService = new CommentService(mockRepository.Object);

            var commentResult = await commentService.GetCommentAsync(commentId);

            Assert.That(commentResult, Is.SameAs(comment));
        }

        [Test]
        public async Task GetPostCommentsCountShouldWorkCorrectly()
        {
            var postId = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d795c");
            var userId = "Pesho";

            var commentList = new List<UserPostComment>()
            {
                new UserPostComment()
                {
                    PostId = postId,
                    UserId = userId,
                    Content = "ContentContentContent"
                },
                new UserPostComment()
                {
                    PostId = postId,
                    UserId = userId,
                    Content = "ContentContentContent"
                },
                new UserPostComment()
                {
                    PostId = postId,
                    UserId = userId,
                    Content = "ContentContentContent"
                },
                new UserPostComment()
                {
                    PostId = postId,
                    UserId = userId,
                    Content = "ContentContentContent"
                },
                new UserPostComment()
                {
                    PostId = postId,
                    UserId = userId,
                    Content = "ContentContentContent"
                },
                new UserPostComment()
                {
                    PostId = postId,
                    UserId = userId,
                    Content = "ContentContentContent"
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
                    PostComments = new List<UserPostComment>(),
                },
            };

            var mockRepository = new Mock<IRepository>();
            mockRepository
                .Setup(r => r.All<UserPostComment>())
                .Returns(commentList.BuildMock());
            mockRepository
                .Setup(r => r.AddAsync(It.IsAny<UserPostComment>()))
                .Callback((UserPostComment comment) => commentList.Add(comment));

            var commentService = new CommentService(mockRepository.Object);

            var count = await commentService.GetPostCommentsCountAsync(postId);

            Assert.That(commentList, Has.Count.EqualTo(count));
        }

        [Test]
        public async Task DeletePostCommentShouldWorkCorrectly()
        {
            var postId = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d795c");
            var userId = "Pesho";
            var commentId = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d7123");

            var comment = new UserPostComment()
            {
                Id = commentId,
                PostId = postId,
                UserId = userId,
                Content = "ContentContentContent"
            };

            var commentList = new List<UserPostComment>()
            {
                comment,
            };


            var postList = new List<Post>()
            {
                new Post()
                {
                    Id = postId,
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                    PostComments = new List<UserPostComment>(),
                },
            };

            var mockRepository = new Mock<IRepository>();
            mockRepository
                .Setup(r => r.All<UserPostComment>())
                .Returns(commentList.BuildMock());
            mockRepository
                .Setup(r => r.AddAsync(It.IsAny<UserPostComment>()))
                .Callback((UserPostComment comment) => commentList.Add(comment));

            var commentService = new CommentService(mockRepository.Object);

            await commentService.DeleteCommentAsync(commentId);

            Assert.That(comment.IsDeleted, Is.True);
        }

        [Test]
        public async Task DeletePostCommentShouldThrowAnExceptionWhenTryingToDeleteAnUnexistingComment()
        {
            var postId = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d795c");
            var userId = "Pesho";
            var commentId = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d7123");

            var comment = new UserPostComment()
            {
                Id = commentId,
                PostId = postId,
                UserId = userId,
                Content = "ContentContentContent"
            };

            var commentList = new List<UserPostComment>()
            {
            };


            var postList = new List<Post>()
            {
                new Post()
                {
                    Id = postId,
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                    PostComments = new List<UserPostComment>(),
                },
            };

            var mockRepository = new Mock<IRepository>();
            mockRepository
                .Setup(r => r.All<UserPostComment>())
                .Returns(commentList.BuildMock());
            mockRepository
                .Setup(r => r.AddAsync(It.IsAny<UserPostComment>()))
                .Callback((UserPostComment comment) => commentList.Add(comment));

            var commentService = new CommentService(mockRepository.Object);

            Assert.ThrowsAsync(typeof(Exception), async () => await commentService.DeleteCommentAsync(commentId));
        }

        [Test]
        public async Task EditPostCommentShouldWorkCorrectly()
        {
            var postId = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d795c");
            var userId = "Pesho";
            var commentId = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d7123");

            var comment = new UserPostComment()
            {
                Id = commentId,
                PostId = postId,
                UserId = userId,
                Content = "ContentContentContent"
            };

            var commentList = new List<UserPostComment>()
            {
                comment,
            };


            var postList = new List<Post>()
            {
                new Post()
                {
                    Id = postId,
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                    PostComments = new List<UserPostComment>(),
                },
            };

            var mockRepository = new Mock<IRepository>();
            mockRepository
                .Setup(r => r.All<UserPostComment>())
                .Returns(commentList.BuildMock());
            mockRepository
                .Setup(r => r.AddAsync(It.IsAny<UserPostComment>()))
                .Callback((UserPostComment comment) => commentList.Add(comment));

            var commentService = new CommentService(mockRepository.Object);

            var editedContent = "EditedContent";
            var editCommentModel = new EditCommentViewModel()
            {
                Id = commentId,
                PostId = postId,
                UserId = userId,
                Content = editedContent,
            };

            await commentService.EditCommentAsync(editCommentModel);

            Assert.Multiple(() =>
            {
                Assert.That(comment.Content, Is.EqualTo(editedContent));
                Assert.That(comment.DateTimeOfLastEdit, Is.Not.Null);
            });
        }

        [Test]
        public async Task EditPostCommentShouldThrowAnExceptionWhenTryingToEditAnUnexistingComment()
        {
            var postId = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d795c");
            var userId = "Pesho";
            var commentId = Guid.Parse("983e099b-6b96-49cc-a42c-4d5c7d5d7123");

            var comment = new UserPostComment()
            {
                Id = commentId,
                PostId = postId,
                UserId = userId,
                Content = "ContentContentContent"
            };

            var commentList = new List<UserPostComment>()
            {
            };


            var postList = new List<Post>()
            {
                new Post()
                {
                    Id = postId,
                    UserId = "Gosho",
                    ImageUrl = "https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    FriendsOnly = false,
                    PostComments = new List<UserPostComment>(),
                },
            };

            var mockRepository = new Mock<IRepository>();
            mockRepository
                .Setup(r => r.All<UserPostComment>())
                .Returns(commentList.BuildMock());
            mockRepository
                .Setup(r => r.AddAsync(It.IsAny<UserPostComment>()))
                .Callback((UserPostComment comment) => commentList.Add(comment));

            var commentService = new CommentService(mockRepository.Object);

            var editedContent = "EditedContent";
            var editCommentModel = new EditCommentViewModel()
            {
                Id = commentId,
                PostId = postId,
                UserId = userId,
                Content = editedContent,
            };

            Assert.ThrowsAsync(typeof(Exception), async () => await commentService.EditCommentAsync(editCommentModel));
        }
    }
}