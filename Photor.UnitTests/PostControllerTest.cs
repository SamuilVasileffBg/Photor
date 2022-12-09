using Photor.Areas.Administrator.Controllers;
using Photor.Core.Contracts;
using Photor.Core.Models.Post;

namespace Photor.UnitTests
{
    public class PostControllerTest
    {
        private PostController PostController { get; set; }

        [SetUp]
        public void Setup()
        {
            var postService = new Mock<IPostService>();
            var reportService = new Mock<IReportService>();

            var postController = new PostController(postService.Object, reportService.Object);
            var result = postController.Index();

            //result
        }

        [Test]
        public async Task UserShouldAddPostSuccessfully()
        {
        }
    }
}