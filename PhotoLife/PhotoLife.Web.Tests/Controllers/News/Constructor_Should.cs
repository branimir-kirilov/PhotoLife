using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CloudinaryDotNet;
using Moq;
using NUnit.Framework;
using PhotoLife.Authentication.Providers;
using PhotoLife.Controllers;
using PhotoLife.Factories;
using PhotoLife.Services.Contracts;

namespace PhotoLife.Web.Tests.Controllers.News
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void _SetEverythingCorrectly_WhenCorrectDependencies_Provided()
        {
            //Arrange
            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            var mockedNewsService = new Mock<INewsService>();
            var mockedViewModelFactory = new Mock<IViewModelFactory>();

            var fakeAcc = new CloudinaryDotNet.Account("fake", "fake", "fake");
            var mockedCloudinary = new Mock<Cloudinary>(fakeAcc);
            var newsControllerSUT = new NewsController(mockedAuthProvider.Object, mockedNewsService.Object, mockedViewModelFactory.Object, mockedCloudinary.Object);

            //Act & Assert
            Assert.IsNotNull(newsControllerSUT);
        }

        [Test]
        public void _BeCorrectInstance_WhenCorrectDependencies_Provided()
        {
            //Arrange
            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            var mockedNewsService = new Mock<INewsService>();
            var mockedViewModelFactory = new Mock<IViewModelFactory>();

            var fakeAcc = new CloudinaryDotNet.Account("fake", "fake", "fake");
            var mockedCloudinary = new Mock<Cloudinary>(fakeAcc);
            var newsControllerSUT = new NewsController(mockedAuthProvider.Object, mockedNewsService.Object, mockedViewModelFactory.Object, mockedCloudinary.Object);

            //Act & Assert
            Assert.IsInstanceOf<Controller>(newsControllerSUT);
        }

        [Test]
        public void _ShouldThrow_ArgumentNullException_WhenAuthProvider_IsNull()
        {
            //Arrange
            var mockedNewsService = new Mock<INewsService>();
            var mockedViewModelFactory = new Mock<IViewModelFactory>();

            var fakeAcc = new CloudinaryDotNet.Account("fake", "fake", "fake");
            var mockedCloudinary = new Mock<Cloudinary>(fakeAcc);

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new NewsController(null, mockedNewsService.Object, mockedViewModelFactory.Object, mockedCloudinary.Object));
        }

        [Test]
        public void _ShouldThrow_ArgumentNullException_NewsService_IsNull()
        {
            //Arrange
            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            var mockedViewModelFactory = new Mock<IViewModelFactory>();

            var fakeAcc = new CloudinaryDotNet.Account("fake", "fake", "fake");
            var mockedCloudinary = new Mock<Cloudinary>(fakeAcc);

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new NewsController(mockedAuthProvider.Object, null, mockedViewModelFactory.Object, mockedCloudinary.Object));
        }

        [Test]
        public void _ShouldThrow_ArgumentNullException_WhenCloudinary_IsNull()
        {
            //Arrange
            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            var mockedNewsService = new Mock<INewsService>();
            var mockedViewModelFactory = new Mock<IViewModelFactory>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new NewsController(mockedAuthProvider.Object, mockedNewsService.Object, mockedViewModelFactory.Object, null));
        }
    }
}
