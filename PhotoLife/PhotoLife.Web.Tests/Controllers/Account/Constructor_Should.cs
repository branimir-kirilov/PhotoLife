using System;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using PhotoLife.Authentication.Providers;
using PhotoLife.Factories;
using PhotoLife.Controllers;

namespace PhotoLife.Web.Tests.Controllers.Account
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void _Throw_ArgumentNullException_IfAuthenticationProvider_IsNull()
        {
            //Arrange
            var mockedFactory = new Mock<IUserFactory>();
            var mockedCloudinaryFactory = new Mock<ICloudinaryFactory>();
            //Act & Assert 
            Assert.Throws<ArgumentNullException>(() => new AccountController(null, mockedFactory.Object, mockedCloudinaryFactory.Object));

        }

        [Test]
        public void _Throw_ArgumentNullException_IfUserFactory_IsNull()
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();
            var mockedCloudinaryFactory = new Mock<ICloudinaryFactory>();

            //Act & Assert 
            Assert.Throws<ArgumentNullException>(() => new AccountController(mockedProvider.Object, null, mockedCloudinaryFactory.Object));

        }

        [Test]
        public void _NotThrow_IfEveryting_PassedCorrectly()
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();
            var mockedFactory = new Mock<IUserFactory>();
            var mockedCloudinaryFactory = new Mock<ICloudinaryFactory>();


            //Act & Assert 
            Assert.DoesNotThrow(() => new AccountController(mockedProvider.Object, mockedFactory.Object, mockedCloudinaryFactory.Object));

        }


        [Test]
        public void _Initialize_NotNull_IfEveryting_PassedCorrectly()
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();
            var mockedFactory = new Mock<IUserFactory>();
            var mockedCloudinaryFactory = new Mock<ICloudinaryFactory>();

            //Act 
            var controller = new AccountController(mockedProvider.Object, mockedFactory.Object, mockedCloudinaryFactory.Object);

            //Assert
            Assert.IsNotNull(controller);
        }

        [Test]
        public void _Is_CorrectInstance_IfEveryting_PassedCorrectly()
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();
            var mockedFactory = new Mock<IUserFactory>();
            var mockedCloudinaryFactory = new Mock<ICloudinaryFactory>();

            //Act 
            var controller = new AccountController(mockedProvider.Object, mockedFactory.Object, mockedCloudinaryFactory.Object);

            //Assert
            Assert.IsInstanceOf<Controller>(controller);
        }
    }
}
