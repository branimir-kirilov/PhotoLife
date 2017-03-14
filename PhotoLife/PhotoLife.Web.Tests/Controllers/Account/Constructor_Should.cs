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

            //Act & Assert 
            Assert.Throws<ArgumentNullException>(() => new AccountController(null, mockedFactory.Object));

        }

        [Test]
        public void _Throw_ArgumentNullException_IfUserFactory_IsNull()
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();

            //Act & Assert 
            Assert.Throws<ArgumentNullException>(() => new AccountController(mockedProvider.Object, null));

        }

        [Test]
        public void _NotThrow_IfEveryting_PassedCorrectly()
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();
            var mockedFactory = new Mock<IUserFactory>();

            //Act & Assert 
            Assert.DoesNotThrow(() => new AccountController(mockedProvider.Object, mockedFactory.Object));

        }


        [Test]
        public void _Initialize_NotNull_IfEveryting_PassedCorrectly()
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();
            var mockedFactory = new Mock<IUserFactory>();

            //Act 
            var controller = new AccountController(mockedProvider.Object, mockedFactory.Object);

            //Assert
            Assert.IsNotNull(controller);
        }

        [Test]
        public void _Is_CorrectInstance_IfEveryting_PassedCorrectly()
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();
            var mockedFactory = new Mock<IUserFactory>();

            //Act 
            var controller = new AccountController(mockedProvider.Object, mockedFactory.Object);

            //Assert
            Assert.IsInstanceOf<Controller>(controller);
        }
    }
}
