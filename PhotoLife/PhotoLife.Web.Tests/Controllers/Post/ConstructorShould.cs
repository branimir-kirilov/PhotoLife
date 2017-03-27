using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            var mockedAcc = new CloudinaryDotNet.Account(cloud,apiKey, apiSecret);
            var mockedCloudinary = new Mock<Cloudinary>(mockedAcc);

            //Act
            var postController = new PostController(mockedAuthProvider.Object, mockedPostService.Object, mockedViewModelFactory.Object, mockedCloudinary.Object)

            //Assert
            Assert.IsNotNull(postController);
        }
    }
}
