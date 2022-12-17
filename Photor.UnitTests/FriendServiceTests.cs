using Photor.Core.Contracts;
using Photor.Core.Services;
using Photor.Infrastructure.Data.Common;
using Photor.Infrastructure.Data.Models;

namespace Photor.UnitTests
{
    public class FriendServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SendFriendInvitationShouldThrowAnExceptionWhenReceiverIdAndSenderIdAreSame()
        {
            var mockRepo = new Mock<IRepository>();
            var mockUserService = new Mock<IUserService>();

            var friendService = new FriendService(mockUserService.Object, mockRepo.Object);

            Assert
                .ThrowsAsync(typeof(ArgumentException),
                async () => await friendService.SendFriendInvitationAsync("Pesho", "Pesho"));
        }

        [Test]
        public void SendFriendInvitationShouldThrowAnExceptionWhenThereIsAnExistingFriendship()
        {
            var userFriendList = new List<UserFriend>()
            {
                new UserFriend()
                {
                    FriendId = "Gosho",
                    UserId = "Pesho",
                },
            };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<UserFriend>())
                .Returns(userFriendList.BuildMock());

            var mockUserService = new Mock<IUserService>();

            var friendService = new FriendService(mockUserService.Object, mockRepo.Object);

            Assert
                .ThrowsAsync(typeof(ArgumentException),
                async () => await friendService.SendFriendInvitationAsync("Pesho", "Gosho"));

            Assert
                .ThrowsAsync(typeof(ArgumentException),
                async () => await friendService.SendFriendInvitationAsync("Gosho", "Pesho"));
        }

        [Test]
        public void SendFriendInvitationShouldThrowAnExceptionWhenThereIsAnAlreadySentInvitation()
        {
            var friendInvitationList = new List<FriendInvitation>()
            {
                new FriendInvitation()
                {
                    SenderId = "Gosho",
                    ReceiverId = "Pesho",
                },
            };

            var userFriendList = new List<UserFriend>()
            {
            };


            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<FriendInvitation>())
                .Returns(friendInvitationList.BuildMock());

            mockRepo
                .Setup(r => r.All<UserFriend>())
                .Returns(userFriendList.BuildMock());

            var mockUserService = new Mock<IUserService>();

            var friendService = new FriendService(mockUserService.Object, mockRepo.Object);

            Assert
                .ThrowsAsync(typeof(ArgumentException),
                async () => await friendService.SendFriendInvitationAsync("Pesho", "Gosho"));

            Assert
                .ThrowsAsync(typeof(ArgumentException),
                async () => await friendService.SendFriendInvitationAsync("Gosho", "Pesho"));
        }

        [Test]
        public void SendFriendInvitationShouldThrowAnExceptionWhenReceiverIdIsInvalid()
        {
            var friendInvitationList = new List<FriendInvitation>()
            {
            };

            var userFriendList = new List<UserFriend>()
            {
            };

            var applicationUserList = new List<ApplicationUser>()
            {
            };


            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<FriendInvitation>())
                .Returns(friendInvitationList.BuildMock());

            mockRepo
                .Setup(r => r.All<UserFriend>())
                .Returns(userFriendList.BuildMock());

            mockRepo
                .Setup(r => r.All<ApplicationUser>())
                .Returns(applicationUserList.BuildMock());

            var mockUserService = new Mock<IUserService>();

            var friendService = new FriendService(mockUserService.Object, mockRepo.Object);

            Assert
                .ThrowsAsync(typeof(ArgumentNullException),
                async () => await friendService.SendFriendInvitationAsync("Pesho", "Gosho"));

            Assert
                .ThrowsAsync(typeof(ArgumentNullException),
                async () => await friendService.SendFriendInvitationAsync("Gosho", "Pesho"));
        }

        [Test]
        public void SendFriendInvitationShouldThrowAnExceptionWhenSenderIdIsInvalid()
        {
            var friendInvitationList = new List<FriendInvitation>()
            {
            };

            var userFriendList = new List<UserFriend>()
            {
            };

            var applicationUserList = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = "Gosho",
                    UserName = "SamiAdmin",
                    FirstName = "Samuil",
                    LastName = "Vasilev",
                    NormalizedUserName = "SAMIADMIN",
                    Email = "samiadmin@mail.com",
                    NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                    ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                }
            };


            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<FriendInvitation>())
                .Returns(friendInvitationList.BuildMock());

            mockRepo
                .Setup(r => r.All<UserFriend>())
                .Returns(userFriendList.BuildMock());

            mockRepo
                .Setup(r => r.All<ApplicationUser>())
                .Returns(applicationUserList.BuildMock());

            var mockUserService = new Mock<IUserService>();

            var friendService = new FriendService(mockUserService.Object, mockRepo.Object);

            Assert
                .ThrowsAsync(typeof(ArgumentNullException),
                async () => await friendService.SendFriendInvitationAsync("Pesho", "Gosho"));
        }

        [Test]
        public async Task SendFriendInvitationShouldWorkCorrectlyWhenPassedValidParameters()
        {
            var friendInvitationList = new List<FriendInvitation>()
            {
            };

            var userFriendList = new List<UserFriend>()
            {
            };

            var applicationUserList = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = "Gosho",
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
                    Id = "Pesho",
                    UserName = "SamiAdmin",
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
                .Setup(r => r.All<FriendInvitation>())
                .Returns(friendInvitationList.BuildMock());

            mockRepo
                .Setup(r => r.All<UserFriend>())
                .Returns(userFriendList.BuildMock());

            mockRepo
                .Setup(r => r.All<ApplicationUser>())
                .Returns(applicationUserList.BuildMock());

            mockRepo
                .Setup(r => r.AddAsync(It.IsAny<FriendInvitation>()))
                .Callback((FriendInvitation friendInvitation) => friendInvitationList.Add(friendInvitation));

            var mockUserService = new Mock<IUserService>();

            var friendService = new FriendService(mockUserService.Object, mockRepo.Object);
            await friendService.SendFriendInvitationAsync("Pesho", "Gosho");

            Assert.That(friendInvitationList, Has.Count.EqualTo(1));
        }

        [Test]
        public void AddUserFriendShouldThrowAnExceptionWhenReceiverIdAndSenderIdAreSame()
        {
            var mockRepo = new Mock<IRepository>();
            var mockUserService = new Mock<IUserService>();

            var friendService = new FriendService(mockUserService.Object, mockRepo.Object);

            Assert
                .ThrowsAsync(typeof(ArgumentException),
                async () => await friendService.AddUserFriendAsync("Pesho", "Pesho"));
        }

        [Test]
        public void AddFriendShouldThrowAnExceptionWhenThereIsAnExistingFriendship()
        {
            var friendInvitationList = new List<FriendInvitation>()
            {
            };

            var userFriendList = new List<UserFriend>()
            {
                new UserFriend()
                {
                    FriendId = "Pesho",
                    UserId = "Gosho",
                },
                new UserFriend()
                {
                    FriendId = "Stoyan",
                    UserId = "Samuil",
                }
            };


            var mockRepo = new Mock<IRepository>();

            mockRepo
                .Setup(r => r.All<UserFriend>())
                .Returns(userFriendList.BuildMock());

            var mockUserService = new Mock<IUserService>();

            var friendService = new FriendService(mockUserService.Object, mockRepo.Object);

            Assert
                .ThrowsAsync(typeof(ArgumentException),
                async () => await friendService.AddUserFriendAsync("Pesho", "Gosho"));

            Assert
                .ThrowsAsync(typeof(ArgumentException),
                async () => await friendService.AddUserFriendAsync("Stoyan", "Samuil"));
        }

        [Test]
        public async Task AddFriendShouldWorkCorrectlyWhenPassedValidParameters()
        {
            var userFriendList = new List<UserFriend>()
            {
            };

            var applicationUserList = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = "Gosho",
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
                    Id = "Pesho",
                    UserName = "SamiAdmin",
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
                .Setup(r => r.All<UserFriend>())
                .Returns(userFriendList.BuildMock());

            mockRepo
                .Setup(r => r.All<ApplicationUser>())
                .Returns(applicationUserList.BuildMock());

            mockRepo
                .Setup(r => r.AddAsync(It.IsAny<UserFriend>()))
                .Callback((UserFriend userFriend) => userFriendList.Add(userFriend));

            var mockUserService = new Mock<IUserService>();

            var friendService = new FriendService(mockUserService.Object, mockRepo.Object);
            await friendService.AddUserFriendAsync("Pesho", "Gosho");

            Assert.That(userFriendList, Has.Count.EqualTo(1));
        }

        [Test]
        public async Task AcceptFriendInvitationShouldWorkCorrectlyWhenPassedValidParameters()
        {
            var senderId = "Gosho";
            var receiverId = "Pesho";

            var friendInvitationList = new List<FriendInvitation>()
            {
                new FriendInvitation()
                {
                    SenderId = senderId,
                    ReceiverId = receiverId,
                },
            };

            var userFriendList = new List<UserFriend>()
            {
            };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<FriendInvitation>())
                .Returns(friendInvitationList.BuildMock());

            mockRepo
               .Setup(r => r.All<UserFriend>())
               .Returns(userFriendList.BuildMock());

            mockRepo
                .Setup(r => r.AddAsync(It.IsAny<UserFriend>()))
                .Callback((UserFriend userFriend) => userFriendList.Add(userFriend));

            var mockUserService = new Mock<IUserService>();

            var friendService = new FriendService(mockUserService.Object, mockRepo.Object);

            await friendService.AcceptFriendInvitationAsync(senderId, receiverId);

            Assert.That(userFriendList, Has.Count.EqualTo(1));
        }

        [Test]
        public async Task AcceptFriendInvitationShouldThrowAnExceptionWhenThereIsNoInvitationSent()
        {
            var senderId = "Gosho";
            var receiverId = "Pesho";

            var friendInvitationList = new List<FriendInvitation>()
            {
            };

            var userFriendList = new List<UserFriend>()
            {
            };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<FriendInvitation>())
                .Returns(friendInvitationList.BuildMock());

            mockRepo
               .Setup(r => r.All<UserFriend>())
               .Returns(userFriendList.BuildMock());

            mockRepo
                .Setup(r => r.AddAsync(It.IsAny<UserFriend>()))
                .Callback((UserFriend userFriend) => userFriendList.Add(userFriend));

            var mockUserService = new Mock<IUserService>();

            var friendService = new FriendService(mockUserService.Object, mockRepo.Object);

            Assert.ThrowsAsync(typeof(Exception), async () => await friendService.AcceptFriendInvitationAsync(senderId, receiverId));
        }

        [Test]
        public async Task RemoveUserFriendShouldThrowAnExceptionWhenThereIsNoExistingFriendship()
        {
            var senderId = "Gosho";
            var receiverId = "Pesho";

            var friendInvitationList = new List<FriendInvitation>()
            {
            };

            var userFriendList = new List<UserFriend>()
            {
            };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<FriendInvitation>())
                .Returns(friendInvitationList.BuildMock());

            mockRepo
               .Setup(r => r.All<UserFriend>())
               .Returns(userFriendList.BuildMock());

            var mockUserService = new Mock<IUserService>();

            var friendService = new FriendService(mockUserService.Object, mockRepo.Object);

            Assert.ThrowsAsync(typeof(Exception), async () => await friendService.RemoveUserFriendAsync(senderId, receiverId));
        }

        [Test]
        public async Task RemoveUserFriendShouldWorkCorrectlyWhenPassedValidParameters()
        {
            var userId = "Gosho";
            var friendId = "Pesho";

            var userId2 = "Simeon";
            var friendId2 = "Petar";

            var friendInvitationList = new List<FriendInvitation>()
            {
            };

            var userFriend = new UserFriend()
            {
                UserId = userId,
                FriendId = friendId,
            };
            var userFriend2 = new UserFriend()
            {
                UserId = userId2,
                FriendId = friendId2,
            };

            var userFriendList = new List<UserFriend>()
            {
                userFriend,
                userFriend2,
            };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<FriendInvitation>())
                .Returns(friendInvitationList.BuildMock());

            mockRepo
               .Setup(r => r.All<UserFriend>())
               .Returns(userFriendList.BuildMock());

            var mockUserService = new Mock<IUserService>();

            var friendService = new FriendService(mockUserService.Object, mockRepo.Object);

            await friendService.RemoveUserFriendAsync(userId, friendId);
            await friendService.RemoveUserFriendAsync(friendId2, userId2);

            Assert.That(userFriend.IsDeleted, Is.True);
            Assert.That(userFriend2.IsDeleted, Is.True);
        }

        [Test]
        public async Task RemoveFriendInvitationShouldThrowAnExceptionWhenThereIsNoExistingInvitation()
        {
            var senderId = "Gosho";
            var receiverId = "Pesho";

            var friendInvitationList = new List<FriendInvitation>()
            {
            };

            var userFriendList = new List<UserFriend>()
            {
            };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<FriendInvitation>())
                .Returns(friendInvitationList.BuildMock());

            mockRepo
               .Setup(r => r.All<UserFriend>())
               .Returns(userFriendList.BuildMock());

            var mockUserService = new Mock<IUserService>();

            var friendService = new FriendService(mockUserService.Object, mockRepo.Object);

            Assert.ThrowsAsync(typeof(Exception), async () => await friendService.DeleteFriendInvitationAsync(senderId, receiverId));
        }

        [Test]
        public async Task RemoveFriendInvitationShouldWorkCorrectlyWhenPassedValidParameters()
        {
            var senderId = "Gosho";
            var receiverId = "Pesho";

            var friendInvitation = new FriendInvitation()
            {
                SenderId = senderId,
                ReceiverId = receiverId,
            };

            var friendInvitationList = new List<FriendInvitation>()
            {
                friendInvitation,
            };

            var userFriendList = new List<UserFriend>()
            {
            };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<FriendInvitation>())
                .Returns(friendInvitationList.BuildMock());

            mockRepo
               .Setup(r => r.All<UserFriend>())
               .Returns(userFriendList.BuildMock());

            var mockUserService = new Mock<IUserService>();

            var friendService = new FriendService(mockUserService.Object, mockRepo.Object);

            await friendService.DeleteFriendInvitationAsync(senderId, receiverId);

            Assert.That(friendInvitation.IsDeleted, Is.True);
        }

        [Test]
        public async Task RejectFriendInvitationShouldThrowAnExceptionWhenThereIsNoExistingInvitation()
        {
            var receiverId = "Gosho";
            var senderId = "Pesho";

            var friendInvitationList = new List<FriendInvitation>()
            {
            };

            var userFriendList = new List<UserFriend>()
            {
            };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<FriendInvitation>())
                .Returns(friendInvitationList.BuildMock());

            mockRepo
               .Setup(r => r.All<UserFriend>())
               .Returns(userFriendList.BuildMock());

            var mockUserService = new Mock<IUserService>();

            var friendService = new FriendService(mockUserService.Object, mockRepo.Object);

            Assert.ThrowsAsync(typeof(Exception), async () => await friendService.RejectFriendInvitationAsync(senderId, receiverId));
        }

        [Test]
        public async Task RejectFriendInvitationShouldWorkCorrectlyWhenPassedValidParameters()
        {
            var receiverId = "Gosho";
            var senderId = "Pesho";

            var friendInvitation = new FriendInvitation()
            {
                SenderId = senderId,
                ReceiverId = receiverId,
            };

            var friendInvitationList = new List<FriendInvitation>()
            {
                friendInvitation,
            };

            var userFriendList = new List<UserFriend>()
            {
            };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<FriendInvitation>())
                .Returns(friendInvitationList.BuildMock());

            mockRepo
               .Setup(r => r.All<UserFriend>())
               .Returns(userFriendList.BuildMock());

            var mockUserService = new Mock<IUserService>();

            var friendService = new FriendService(mockUserService.Object, mockRepo.Object);

            await friendService.RejectFriendInvitationAsync(senderId, receiverId);

            Assert.That(friendInvitation.IsDeleted, Is.True);
        }

        [Test]
        public async Task GetUserFriendsShouldWorkCorrectly()
        {
            var userId = "Gosho";

            var friendInvitationList = new List<FriendInvitation>()
            {
            };

            var userFriendList = new List<UserFriend>()
            {
                new UserFriend()
                {
                    UserId = userId,
                    FriendId = "1",
                },
                new UserFriend()
                {
                    FriendId = userId,
                    UserId = "2",
                },
                new UserFriend()
                {
                    UserId = userId,
                    FriendId = "3",
                },
                new UserFriend()
                {
                    FriendId = userId,
                    UserId = "4",
                },
                new UserFriend()
                {
                    UserId = userId,
                    FriendId = "5",
                },
                new UserFriend()
                {
                    UserId = userId,
                    FriendId = "6",
                },
                new UserFriend()
                {
                    UserId = userId,
                    FriendId = "7",
                },
            };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<FriendInvitation>())
                .Returns(friendInvitationList.BuildMock());

            mockRepo
               .Setup(r => r.All<UserFriend>())
               .Returns(userFriendList.BuildMock());

            var mockUserService = new Mock<IUserService>();

            var friendService = new FriendService(mockUserService.Object, mockRepo.Object);

            var pageOneResult = await friendService.GetUserFriendsAsync(userId, 1);
            var pageTwoResult = await friendService.GetUserFriendsAsync(userId, 2);
            var pageThreeResult = await friendService.GetUserFriendsAsync(userId, 3);

            Assert.That(pageOneResult.Count(), Is.EqualTo(5));
            Assert.That(pageTwoResult.Count(), Is.EqualTo(2));
            Assert.That(pageThreeResult.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetUserFriendsWithPaginationCountShouldWorkCorrectly()
        {
            var userId = "Gosho";

            var friendInvitationList = new List<FriendInvitation>()
            {
            };

            var userFriendList = new List<UserFriend>()
            {
                new UserFriend()
                {
                    UserId = userId,
                    FriendId = "1",
                },
                new UserFriend()
                {
                    FriendId = userId,
                    UserId = "2",
                },
                new UserFriend()
                {
                    UserId = userId,
                    FriendId = "3",
                },
                new UserFriend()
                {
                    FriendId = userId,
                    UserId = "4",
                },
                new UserFriend()
                {
                    UserId = userId,
                    FriendId = "5",
                },
                new UserFriend()
                {
                    UserId = userId,
                    FriendId = "6",
                },
                new UserFriend()
                {
                    UserId = userId,
                    FriendId = "7",
                },
            };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<FriendInvitation>())
                .Returns(friendInvitationList.BuildMock());

            mockRepo
               .Setup(r => r.All<UserFriend>())
               .Returns(userFriendList.BuildMock());

            var mockUserService = new Mock<IUserService>();

            var friendService = new FriendService(mockUserService.Object, mockRepo.Object);

            var result = await friendService.GetUserFriendsCountAsync(userId);

            Assert.That(userFriendList, Has.Count.EqualTo(result));
        }

        [Test]
        public async Task GetUserFriendsCountShouldWorkCorrectly()
        {
            var userId = "Gosho";

            var friendInvitationList = new List<FriendInvitation>()
            {
            };

            var userFriendList = new List<UserFriend>()
            {
                new UserFriend()
                {
                    UserId = userId,
                    FriendId = "1",
                },
                new UserFriend()
                {
                    FriendId = userId,
                    UserId = "2",
                },
                new UserFriend()
                {
                    UserId = userId,
                    FriendId = "3",
                },
                new UserFriend()
                {
                    FriendId = userId,
                    UserId = "4",
                },
                new UserFriend()
                {
                    UserId = userId,
                    FriendId = "5",
                },
                new UserFriend()
                {
                    UserId = userId,
                    FriendId = "6",
                },
                new UserFriend()
                {
                    UserId = userId,
                    FriendId = "7",
                },
            };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<FriendInvitation>())
                .Returns(friendInvitationList.BuildMock());

            mockRepo
               .Setup(r => r.All<UserFriend>())
               .Returns(userFriendList.BuildMock());

            var mockUserService = new Mock<IUserService>();

            var friendService = new FriendService(mockUserService.Object, mockRepo.Object);

            var result = await friendService.GetUserFriendsAsync(userId);

            Assert.That(userFriendList, Has.Count.EqualTo(result.Count()));
        }

        [Test]
        public async Task GetUserReceivedFriendInvitationsShouldWorkCorrectly()
        {
            var receiverId = "Gosho";

            var friendInvitationList = new List<FriendInvitation>()
            {
                new FriendInvitation()
                {
                    ReceiverId = receiverId,
                    SenderId = "1",
                },
                new FriendInvitation()
                {
                    ReceiverId = receiverId,
                    SenderId = "2",
                },
                new FriendInvitation()
                {
                    ReceiverId = receiverId,
                    SenderId = "3",
                },
                new FriendInvitation()
                {
                    ReceiverId = receiverId,
                    SenderId = "4",
                },
                new FriendInvitation()
                {
                    ReceiverId = receiverId,
                    SenderId = "5",
                },
                new FriendInvitation()
                {
                    ReceiverId = receiverId,
                    SenderId = "6",
                },
                new FriendInvitation()
                {
                    ReceiverId = receiverId,
                    SenderId = "7",
                },
            };

            var userFriendList = new List<UserFriend>()
            {
            };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<FriendInvitation>())
                .Returns(friendInvitationList.BuildMock());

            var mockUserService = new Mock<IUserService>();

            var friendService = new FriendService(mockUserService.Object, mockRepo.Object);

            var pageOneResult = await friendService.GetReceivedFriendInvitationsAsync(receiverId, 1);
            var pageTwoResult = await friendService.GetReceivedFriendInvitationsAsync(receiverId, 2);
            var pageThreeResult = await friendService.GetReceivedFriendInvitationsAsync(receiverId, 3);

            Assert.That(pageOneResult.Count(), Is.EqualTo(5));
            Assert.That(pageTwoResult.Count(), Is.EqualTo(2));
            Assert.That(pageThreeResult.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetUserReceivedFriendInvitationsCountShouldWorkCorrectly()
        {
            var receiverId = "Gosho";

            var friendInvitationList = new List<FriendInvitation>()
            {
                new FriendInvitation()
                {
                    ReceiverId = receiverId,
                    SenderId = "1",
                },
                new FriendInvitation()
                {
                    ReceiverId = receiverId,
                    SenderId = "2",
                },
                new FriendInvitation()
                {
                    ReceiverId = receiverId,
                    SenderId = "3",
                },
                new FriendInvitation()
                {
                    ReceiverId = receiverId,
                    SenderId = "4",
                },
                new FriendInvitation()
                {
                    ReceiverId = receiverId,
                    SenderId = "5",
                },
                new FriendInvitation()
                {
                    ReceiverId = receiverId,
                    SenderId = "6",
                },
                new FriendInvitation()
                {
                    ReceiverId = receiverId,
                    SenderId = "7",
                },
            };

            var userFriendList = new List<UserFriend>()
            {
            };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<FriendInvitation>())
                .Returns(friendInvitationList.BuildMock());

            var mockUserService = new Mock<IUserService>();

            var friendService = new FriendService(mockUserService.Object, mockRepo.Object);

            var result = await friendService.GetReceivedFriendInvitationsCountAsync(receiverId);

            Assert.That(friendInvitationList, Has.Count.EqualTo(result));
        }

        [Test]
        public async Task GetMutualFriendsCountShouldWorkCorrectly()
        {
            var firstUserId = "Gosho";
            var firstUser = new ApplicationUser()
            {
                Id = firstUserId,
                UserName = "SamiAdmin",
                FirstName = "Samuil",
                LastName = "Vasilev",
                NormalizedUserName = "SAMIADMIN",
                Email = "samiadmin@mail.com",
                NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
            };

            var secondUserId = "Pesho";
            var secondUser = new ApplicationUser()
            {
                Id = secondUserId,
                UserName = "SamiAdmin",
                FirstName = "Samuil",
                LastName = "Vasilev",
                NormalizedUserName = "SAMIADMIN",
                Email = "samiadmin@mail.com",
                NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
            };


            var friendInvitationList = new List<FriendInvitation>()
            {
            };

            var userFriendList = new List<UserFriend>()
            {
                new UserFriend()
                {
                    UserId = firstUserId,
                    FriendId = "1",
                    Friend = new ApplicationUser()
                    {
                        Id = "1",
                        UserName = "TestUserName",
                        FirstName = "Samuil",
                        LastName = "Vasilev",
                        NormalizedUserName = "SAMIADMIN",
                        Email = "samiadmin@mail.com",
                        NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                        ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    },
                    IsDeleted = false,
                },
                new UserFriend()
                {
                    FriendId = secondUserId,
                    UserId = "1",
                    User = new ApplicationUser()
                    {
                        Id = "1",
                        UserName = "TestUserName",
                        FirstName = "Samuil",
                        LastName = "Vasilev",
                        NormalizedUserName = "SAMIADMIN",
                        Email = "samiadmin@mail.com",
                        NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                        ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    },
                    IsDeleted = false,
                },
                new UserFriend()
                {
                    UserId = firstUserId,
                    FriendId = "2",
                    Friend = new ApplicationUser()
                    {
                        Id = "2",
                        UserName = "TestUserName",
                        FirstName = "Samuil",
                        LastName = "Vasilev",
                        NormalizedUserName = "SAMIADMIN",
                        Email = "samiadmin@mail.com",
                        NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                        ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    },
                    IsDeleted = false,
                },
                new UserFriend()
                {
                    FriendId = secondUserId,
                    UserId = "2",
                    User = new ApplicationUser()
                    {
                        Id = "2",
                        UserName = "TestUserName",
                        FirstName = "Samuil",
                        LastName = "Vasilev",
                        NormalizedUserName = "SAMIADMIN",
                        Email = "samiadmin@mail.com",
                        NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                        ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    },
                    IsDeleted = false,
                },
                new UserFriend()
                {
                    UserId = firstUserId,
                    FriendId = "5",
                    Friend = new ApplicationUser()
                    {
                        Id = "5",
                        UserName = "TestUserName",
                        FirstName = "Samuil",
                        LastName = "Vasilev",
                        NormalizedUserName = "SAMIADMIN",
                        Email = "samiadmin@mail.com",
                        NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                        ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    },
                    IsDeleted = false,
                },
                new UserFriend()
                {
                    UserId = secondUserId,
                    FriendId = "6",
                    Friend = new ApplicationUser()
                    {
                        Id = "6",
                        UserName = "TestUserName",
                        FirstName = "Samuil",
                        LastName = "Vasilev",
                        NormalizedUserName = "SAMIADMIN",
                        Email = "samiadmin@mail.com",
                        NormalizedEmail = "samiadmin@mail.com".ToUpper(),
                        ImageUrl = @"https://lh3.googleusercontent.com/d/1Lf2T40cLdGd8GuGPEBuFCoPCPNQHz_ey",
                    },
                    IsDeleted = false,
                },
                new UserFriend()
                {
                    UserId = firstUserId,
                    User = firstUser,
                    FriendId = secondUserId,
                    Friend = secondUser,
                    IsDeleted = false,
                },
            };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<FriendInvitation>())
                .Returns(friendInvitationList.BuildMock());

            mockRepo
               .Setup(r => r.All<UserFriend>())
               .Returns(userFriendList.BuildMock());

            var mockUserService = new Mock<IUserService>();

            var friendService = new FriendService(mockUserService.Object, mockRepo.Object);

            var result = await friendService.GetMutualFriendsCountAsync(firstUserId, secondUserId);

            Assert.That(result, Is.EqualTo(2));
        }
    }
}