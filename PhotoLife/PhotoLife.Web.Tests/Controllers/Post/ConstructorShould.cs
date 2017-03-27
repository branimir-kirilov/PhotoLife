using System;
using CloudinaryDotNet;
using Moq;
using NUnit.Framework;
using PhotoLife.Authentication.Providers;
using PhotoLife.Controllers;
using PhotoLife.Factories;
using PhotoLife.Services.Contracts;

namespace PhotoLife.Web.Tests.Controllers.Post
{
    [TestFixture]
    public class ConstructorShould
    {
        [TestCase("fake", "fake", "fake")]
        public void _InitialieNotNull_WhenEverythingPassedCorrectly(string cloud, string apiKey, string apiSecret)
        {
            //Arrange
            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            var mockedPostService = new Mock<IPostService>();
            var mockedViewModelFactory = new Mock<IViewModelFactory>();

            var mockedAcc = new CloudinaryDotNet.Account(cloud, apiKey, apiSecret);
            var mockedCloudinary = new Mock<Cloudinary>(mockedAcc);

            //Act
            var postController = new PostController(mockedAuthProvider.Object, mockedPostService.Object,
                mockedViewModelFactory.Object, mockedCloudinary.Object);

            //Assert
            Assert.IsNotNull(postController);
        }

        [TestCase("fake", "fake", "fake")]
        public void _Throws_ArgumentNullException_When_AuthProvider_IsNull(string cloud, string apiKey, string apiSecret)
        {
            //Arrange
            var mockedPostService = new Mock<IPostService>();
            var mockedViewModelFactory = new Mock<IViewModelFactory>();

            var mockedAcc = new CloudinaryDotNet.Account(cloud, apiKey, apiSecret);
            var mockedCloudinary = new Mock<Cloudinary>(mockedAcc);

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new PostController(null, mockedPostService.Object,
                mockedViewModelFactory.Object, mockedCloudinary.Object));
        }

        [TestCase("fake", "fake", "fake")]
        public void _Throws_ArgumentNullException_When_PostService_IsNull(string cloud, string apiKey, string apiSecret)
        {
            //Arrange
            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            var mockedViewModelFactory = new Mock<IViewModelFactory>();

            var mockedAcc = new CloudinaryDotNet.Account(cloud, apiKey, apiSecret);
            var mockedCloudinary = new Mock<Cloudinary>(mockedAcc);

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new PostController(
                mockedAuthProvider.Object, 
                null,
                mockedViewModelFactory.Object,
                mockedCloudinary.Object));
        }

        [TestCase("fake", "fake", "fake")]
        public void _Throws_ArgumentNullException_When_ViewModelFactory_IsNull(string cloud, string apiKey, string apiSecret)
        {
            //Arrange
            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            var mockedPostService = new Mock<IPostService>();

            var mockedAcc = new CloudinaryDotNet.Account(cloud, apiKey, apiSecret);
            var mockedCloudinary = new Mock<Cloudinary>(mockedAcc);

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new PostController(
                mockedAuthProvider.Object,
                mockedPostService.Object,
                null,
                mockedCloudinary.Object));
        }


        [TestCase("fake", "fake", "fake")]
        public void _Throws_ArgumentNullException_WhenCloudinary_IsNull(string cloud, string apiKey, string apiSecret)
        {
            //Arrange
            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            var mockedPostService = new Mock<IPostService>();
            var mockedViewModelFactory = new Mock<IViewModelFactory>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new PostController(
                mockedAuthProvider.Object,
                mockedPostService.Object,
                mockedViewModelFactory.Object,
                null
                ));
        }
    }
}
