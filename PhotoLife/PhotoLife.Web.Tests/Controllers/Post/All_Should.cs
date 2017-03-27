using System.Web.Mvc;
using CloudinaryDotNet;
using Moq;
using NUnit.Framework;
using PhotoLife.Authentication.Providers;
using PhotoLife.Controllers;
using PhotoLife.Factories;
using PhotoLife.Services.Contracts;

namespace PhotoLife.Web.Tests.Controllers.Post
{
    public class All_Should
    {
        [TestCase("fake", "fake", "fake")]
        public void _Call_PostService_GetAll(string cloud, string apiKey, string apiSecret)
        {
            //Arrange
            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            var mockedPostService = new Mock<IPostService>();
            var mockedViewModelFactory = new Mock<IViewModelFactory>();

            var mockedAcc = new CloudinaryDotNet.Account(cloud, apiKey, apiSecret);
            var mockedCloudinary = new Mock<Cloudinary>(mockedAcc);

            var postController = new PostController(mockedAuthProvider.Object, mockedPostService.Object,
                mockedViewModelFactory.Object, mockedCloudinary.Object);

            //Act
            postController.All();

            //Assert
            mockedPostService.Verify(s => s.GetAll(), Times.Once);
        }

        [TestCase("fake", "fake", "fake")]
        public void _Return_PartialView(string cloud, string apiKey, string apiSecret)
        {
            //Arrange
            var expectedPartialName = "_PagedPostListPartial";

            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            var mockedPostService = new Mock<IPostService>();
            var mockedViewModelFactory = new Mock<IViewModelFactory>();

            var mockedAcc = new CloudinaryDotNet.Account(cloud, apiKey, apiSecret);
            var mockedCloudinary = new Mock<Cloudinary>(mockedAcc);

            var postController = new PostController(mockedAuthProvider.Object, mockedPostService.Object,
                mockedViewModelFactory.Object, mockedCloudinary.Object);

            //Act
            var res = postController.All() as PartialViewResult;

            //Assert
            Assert.AreEqual(expectedPartialName, res.ViewName);

        }
    }
}
