using System.Web.Mvc;
using CloudinaryDotNet;
using Moq;
using NUnit.Framework;
using PhotoLife.Authentication.Providers;
using PhotoLife.Controllers;
using PhotoLife.Factories;

namespace PhotoLife.Web.Tests.Controllers.Account
{
    [TestFixture]
    public class Register_Should
    {
        [Test]
        public void _Return_NotNull()
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();
            var mockedFactory = new Mock<IUserFactory>();

            var fakeAcc = new CloudinaryDotNet.Account("sdfsdfsd", "sdfsdfsdf", "sdfsdfsdf");
            var mockedCloudinary = new Mock<Cloudinary>(fakeAcc);

            var controller = new AccountController(mockedProvider.Object, mockedFactory.Object, mockedCloudinary.Object);

            //Act
            var result = controller.Register();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void _Return_View()
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();
            var mockedFactory = new Mock<IUserFactory>();

            var fakeAcc = new CloudinaryDotNet.Account("sdfsdfsd", "sdfsdfsdf", "sdfsdfsdf");
            var mockedCloudinary = new Mock<Cloudinary>(fakeAcc);

            var controller = new AccountController(mockedProvider.Object, mockedFactory.Object, mockedCloudinary.Object);

            //Act
            var result = controller.Register();

            //Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }
    }
}
