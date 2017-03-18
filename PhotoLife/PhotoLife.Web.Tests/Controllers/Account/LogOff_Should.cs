using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using PhotoLife.Authentication.Providers;
using PhotoLife.Controllers;
using PhotoLife.Factories;

namespace PhotoLife.Web.Tests.Controllers.Account
{
    [TestFixture]
    public class LogOff_Should
    {
        [Test]
        public void _ShouldCall_Provider_LogOout()
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();
            var mockedFactory = new Mock<IUserFactory>();
            var mockedCloudinaryFactory = new Mock<ICloudinaryFactory>();

             var controller = new AccountController(mockedProvider.Object, mockedFactory.Object, mockedCloudinaryFactory.Object);
          
            //Act
            controller.LogOff();

            //Assert
            mockedProvider.Verify(p => p.SignOut(), Times.Once);
        }

        [Test]
        public void _Should_NotReturn_Null()
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();
            var mockedFactory = new Mock<IUserFactory>();
            var mockedCloudinaryFactory = new Mock<ICloudinaryFactory>();

            var controller = new AccountController(mockedProvider.Object, mockedFactory.Object, mockedCloudinaryFactory.Object);

            //Act
            var res = controller.LogOff();

            //Assert
            Assert.IsNotNull(res);
        }

        [Test]
        public void _Should_Redirect_ToAction()
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();
            var mockedFactory = new Mock<IUserFactory>();
            var mockedCloudinaryFactory = new Mock<ICloudinaryFactory>();

            var controller = new AccountController(mockedProvider.Object, mockedFactory.Object, mockedCloudinaryFactory.Object);

            //Act
            var res = controller.LogOff();

            //Assert
            Assert.IsInstanceOf<RedirectToRouteResult>(res);
        }
    }
}
