using System;
using System.Web.Mvc;
using CloudinaryDotNet;
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

            var fakeAcc = new CloudinaryDotNet.Account("sdfsdfsd", "sdfsdfsdf", "sdfsdfsdf");
            var mockedCloudinary = new Mock<Cloudinary>(fakeAcc);

            //Act & Assert 
            Assert.Throws<ArgumentNullException>(() => new AccountController(null, mockedFactory.Object, mockedCloudinary.Object));

        }

        [Test]
        public void _Throw_ArgumentNullException_IfUserFactory_IsNull()
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();

            var fakeAcc = new CloudinaryDotNet.Account("sdfsdfsd", "sdfsdfsdf", "sdfsdfsdf");
            var mockedCloudinary = new Mock<Cloudinary>(fakeAcc);

            //Act & Assert 
            Assert.Throws<ArgumentNullException>(() => new AccountController(mockedProvider.Object, null, mockedCloudinary.Object));

        }

        [Test]
        public void _Throw_ArgumentNullException_IfCloudinary_IsNull()
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();
            var mockedFactory = new Mock<IUserFactory>();
            
            //Act & Assert 
            Assert.Throws<ArgumentNullException>(() => new AccountController(mockedProvider.Object, mockedFactory.Object, null));
        }

        [Test]
        public void _NotThrow_IfEveryting_PassedCorrectly()
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();
            var mockedFactory = new Mock<IUserFactory>();

            var fakeAcc = new CloudinaryDotNet.Account("sdfsdfsd", "sdfsdfsdf", "sdfsdfsdf");
            var mockedCloudinary = new Mock<Cloudinary>(fakeAcc);

            //Act & Assert 
            Assert.DoesNotThrow(() => new AccountController(mockedProvider.Object, mockedFactory.Object, mockedCloudinary.Object));

        }


        [Test]
        public void _Initialize_NotNull_IfEveryting_PassedCorrectly()
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();
            var mockedFactory = new Mock<IUserFactory>();

            var fakeAcc = new CloudinaryDotNet.Account("sdfsdfsd", "sdfsdfsdf", "sdfsdfsdf");
            var mockedCloudinary = new Mock<Cloudinary>(fakeAcc);

            //Act 
            var controller = new AccountController(mockedProvider.Object, mockedFactory.Object, mockedCloudinary.Object);

            //Assert
            Assert.IsNotNull(controller);
        }

        [Test]
        public void _Is_CorrectInstance_IfEveryting_PassedCorrectly()
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();
            var mockedFactory = new Mock<IUserFactory>();

            var fakeAcc = new CloudinaryDotNet.Account("sdfsdfsd", "sdfsdfsdf", "sdfsdfsdf");
            var mockedCloudinary = new Mock<Cloudinary>(fakeAcc);

            //Act 
            var controller = new AccountController(mockedProvider.Object, mockedFactory.Object, mockedCloudinary.Object);

            //Assert
            Assert.IsInstanceOf<Controller>(controller);
        }
    }
}
