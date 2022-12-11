using Photor.Core.Services;
using Photor.Infrastructure.Data.Common;
using Photor.Infrastructure.Data.Models;

namespace Photor.UnitTests
{
    public class ReportServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetReportShouldWorkCorrectlyWhenPassedValidParameters()
        {
            var reportId = Guid.Parse("04070102-c875-4e9d-a943-a584173bb65b");
            var postId = Guid.Parse("04070102-c875-4e9d-a943-a584173bb123");

            var report = new UserPostReport()
            {
                Id = reportId,
                UserId = "Gosho",
                PostId = postId,
            };

            var reportList = new List<UserPostReport>()
            {
                report,
            };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<UserPostReport>())
                .Returns(reportList.BuildMock());
            mockRepo
                .Setup(r => r.AddAsync(It.IsAny<UserPostReport>()))
                .Callback((UserPostReport report) => reportList.Add(report));

            var reportService = new ReportService(mockRepo.Object);

            var returnedReport = await reportService.GetReportAsync(reportId);

            Assert.That(report, Is.SameAs(returnedReport));
        }

        [Test]
        public async Task GetAllReportsShouldWorkCorrectlyWhenPassedValidParameters()
        {
            var reportId = Guid.Parse("04070102-c875-4e9d-a943-a584173bb65b");
            var postId = Guid.Parse("04070102-c875-4e9d-a943-a584173bb123");

            var report = new UserPostReport()
            {
                Id = reportId,
                UserId = "Gosho",
                PostId = postId,
            };

            var reportList = new List<UserPostReport>()
            {
                new UserPostReport()
                {
                    UserId = "1",
                    PostId = postId,
                    DateTime = DateTime.Now,
                },
                new UserPostReport()
                {
                    UserId = "2",
                    PostId = postId,
                    DateTime = DateTime.Now,
                },
                new UserPostReport()
                {
                    UserId = "3",
                    PostId = postId,
                    DateTime = DateTime.Now,
                },
                new UserPostReport()
                {
                    UserId = "4",
                    PostId = postId,
                    DateTime = DateTime.Now,
                },
                new UserPostReport()
                {
                    UserId = "5",
                    PostId = postId,
                    DateTime = DateTime.Now,
                },
                new UserPostReport()
                {
                    UserId = "6",
                    PostId = postId,
                    DateTime = DateTime.Now,
                },
            };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<UserPostReport>())
                .Returns(reportList.BuildMock());
            mockRepo
                .Setup(r => r.AddAsync(It.IsAny<UserPostReport>()))
                .Callback((UserPostReport report) => reportList.Add(report));

            var reportService = new ReportService(mockRepo.Object);

            var firstPageNewest = await reportService.GetReportsAsync(1, true);
            var secondPageNewest = await reportService.GetReportsAsync(2, true);
            var thirdPageNewest = await reportService.GetReportsAsync(3, true);

            Assert.Multiple(() =>
            {
                Assert.That(firstPageNewest, Is.Not.Null);
                Assert.That(secondPageNewest, Is.Not.Null);
                Assert.That(thirdPageNewest, Is.Not.Null);
            });
            Assert.Multiple(() =>
            {
                Assert.That(firstPageNewest.UserId, Is.EqualTo("6"));
                Assert.That(secondPageNewest.UserId, Is.EqualTo("5"));
                Assert.That(thirdPageNewest.UserId, Is.EqualTo("4"));
            });

            var firstPageOldest = await reportService.GetReportsAsync(1, false);
            var secondPageOldest = await reportService.GetReportsAsync(2, false);
            var thirdPageOldest = await reportService.GetReportsAsync(3, false);

            Assert.Multiple(() =>
            {
                Assert.That(firstPageOldest, Is.Not.Null);
                Assert.That(secondPageOldest, Is.Not.Null);
                Assert.That(thirdPageOldest, Is.Not.Null);
            });
            Assert.Multiple(() =>
            {
                Assert.That(firstPageOldest.UserId, Is.EqualTo("1"));
                Assert.That(secondPageOldest.UserId, Is.EqualTo("2"));
                Assert.That(thirdPageOldest.UserId, Is.EqualTo("3"));
            });
        }

        [Test]
        public async Task GetAllReportsCountShouldWorkCorrectlyWhenPassedValidParameters()
        {
            var reportId = Guid.Parse("04070102-c875-4e9d-a943-a584173bb65b");
            var postId = Guid.Parse("04070102-c875-4e9d-a943-a584173bb123");

            var report = new UserPostReport()
            {
                Id = reportId,
                UserId = "Gosho",
                PostId = postId,
            };

            var reportList = new List<UserPostReport>()
            {
                new UserPostReport()
                {
                    UserId = "1",
                    PostId = postId,
                    DateTime = DateTime.Now,
                },
                new UserPostReport()
                {
                    UserId = "2",
                    PostId = postId,
                    DateTime = DateTime.Now,
                },
                new UserPostReport()
                {
                    UserId = "3",
                    PostId = postId,
                    DateTime = DateTime.Now,
                },
                new UserPostReport()
                {
                    UserId = "4",
                    PostId = postId,
                    DateTime = DateTime.Now,
                },
                new UserPostReport()
                {
                    UserId = "5",
                    PostId = postId,
                    DateTime = DateTime.Now,
                },
                new UserPostReport()
                {
                    UserId = "6",
                    PostId = postId,
                    DateTime = DateTime.Now,
                },
            };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<UserPostReport>())
                .Returns(reportList.BuildMock());
            mockRepo
                .Setup(r => r.AddAsync(It.IsAny<UserPostReport>()))
                .Callback((UserPostReport report) => reportList.Add(report));

            var reportService = new ReportService(mockRepo.Object);

            var count = await reportService.AllReportsCountAsync();
            
            Assert.That(reportList, Has.Count.EqualTo(count));
        }

        [Test]
        public async Task ReportPostShouldWorkCorrectly()
        {
            var reportId = Guid.Parse("04070102-c875-4e9d-a943-a584173bb65b");
            var postId = Guid.Parse("04070102-c875-4e9d-a943-a584173bb123");

            var report = new UserPostReport()
            {
                Id = reportId,
                UserId = "Gosho",
                PostId = postId,
            };

            var reportList = new List<UserPostReport>()
            {
            };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<UserPostReport>())
                .Returns(reportList.BuildMock());
            mockRepo
                .Setup(r => r.AddAsync(It.IsAny<UserPostReport>()))
                .Callback((UserPostReport report) => reportList.Add(report));

            var reportService = new ReportService(mockRepo.Object);

            await reportService.ReportPost(postId, "Pesho", "I don't like this post");

            Assert.That(reportList, Has.Count.EqualTo(1));
        }

        [Test]
        public async Task DeletePostShouldWorkCorrectlyWhenPassedValidParameters()
        {
            var reportId = Guid.Parse("04070102-c875-4e9d-a943-a584173bb65b");
            var postId = Guid.Parse("04070102-c875-4e9d-a943-a584173bb123");

            var report = new UserPostReport()
            {
                Id = reportId,
                UserId = "Gosho",
                PostId = postId,
            };

            var reportList = new List<UserPostReport>()
            {
                report,
            };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<UserPostReport>())
                .Returns(reportList.BuildMock());
            mockRepo
                .Setup(r => r.AddAsync(It.IsAny<UserPostReport>()))
                .Callback((UserPostReport report) => reportList.Add(report));

            var reportService = new ReportService(mockRepo.Object);

            await reportService.DeleteReportAsync(reportId);

            Assert.That(report.IsDeleted, Is.True);
        }

        [Test]
        public async Task DeletePostShouldThrowAnExceptionWhenTryingToDeleteAnUnexistingReport()
        {
            var reportId = Guid.Parse("04070102-c875-4e9d-a943-a584173bb65b");
            var postId = Guid.Parse("04070102-c875-4e9d-a943-a584173bb123");

            var report = new UserPostReport()
            {
                Id = reportId,
                UserId = "Gosho",
                PostId = postId,
            };

            var reportList = new List<UserPostReport>()
            {
            };

            var mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(r => r.All<UserPostReport>())
                .Returns(reportList.BuildMock());
            mockRepo
                .Setup(r => r.AddAsync(It.IsAny<UserPostReport>()))
                .Callback((UserPostReport report) => reportList.Add(report));

            var reportService = new ReportService(mockRepo.Object);

            Assert.ThrowsAsync(typeof(Exception), async () => await reportService.DeleteReportAsync(reportId));
        }
    }
}