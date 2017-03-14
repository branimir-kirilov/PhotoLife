using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Moq;
using NUnit.Framework;
using PhotoLife.Authentication.Providers;
using PhotoLife.Controllers;
using PhotoLife.Factories;
using PhotoLife.Models;

namespace PhotoLife.Web.Tests.Controllers.Account
{
    [TestFixture]
    public class After_Login_Should
    {
        [TestCase("fakeMail@fakeMailService.fakeDomain", "fakePassword", true, "/home")]
        [TestCase("fakeMail@fakeMailService.fakeDomain", "fakePassword", false, "/home")]

        public void _Call_Provider_SignInWithPassword_ModelState_IsValid(
           string email,
           string password,
           bool rememberMe,
           string returnUrl)
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();
            var mockedFactory = new Mock<IUserFactory>();

            var loginViewModel = new LoginViewModel()
            {
                Email = email,
                Password = password,
                RememberMe = rememberMe
            };

            var controller = new AccountController(mockedProvider.Object, mockedFactory.Object);

            //Act
            controller.Login(loginViewModel, returnUrl);

            //Assert
            mockedProvider.Verify(
                p => p
                .SignInWithPassword(
                    email,
                    password,
                    rememberMe,
                    It.IsAny<bool>()),
                Times.Once);
        }

        [TestCase("fakeMail@fakeMailService.fakeDomain", "fakePassword", true, "/home")]
        [TestCase("fakeMail@fakeMailService.fakeDomain", "fakePassword", false, "/home")]

        public void _Return_ViewWithModel_If_ModelState_NotValid(
            string email,
            string password,
            bool rememberMe,
            string returnUrl)
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();
            var mockedFactory = new Mock<IUserFactory>();

            var loginViewModel = new LoginViewModel()
            {
                Email = email,
                Password = password,
                RememberMe = rememberMe
            };

            var controller = new AccountController(mockedProvider.Object, mockedFactory.Object);
            controller.ModelState.AddModelError("key", "error");

            //Act
            var res = controller.Login(loginViewModel, returnUrl) as ViewResult;

            //Assert
            Assert.AreSame(loginViewModel, res.Model);
        }

        [TestCase("fakeMail@fakeMailService.fakeDomain", "fakePassword", true, "/home")]
        [TestCase("fakeMail@fakeMailService.fakeDomain", "fakePassword", false, "/home")]

        public void _Return_RedirectResult_WithCorrect_Url_WhenProvider_ReturnsSuccess(
          string email,
          string password,
          bool rememberMe,
          string returnUrl)
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();
            var mockedFactory = new Mock<IUserFactory>();

            var loginViewModel = new LoginViewModel()
            {
                Email = email,
                Password = password,
                RememberMe = rememberMe
            };

            var controller = new AccountController(mockedProvider.Object, mockedFactory.Object);

            //Act
            var result = controller.Login(loginViewModel, returnUrl) as RedirectResult;

            //Assert
            Assert.AreEqual(returnUrl, result.Url);
        }

        [TestCase("fakeMail@fakeMailService.fakeDomain", "fakePassword", true, null)]
        [TestCase("fakeMail@fakeMailService.fakeDomain", "fakePassword", false, null)]

        public void _Return_RedirectResult_Home_WhenProvider_Returns_EmptyReturnUrl(
         string email,
         string password,
         bool rememberMe,
         string returnUrl)
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();
            mockedProvider.Setup(
                    p =>
                        p.SignInWithPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(),
                            It.IsAny<bool>()))
                .Returns(SignInStatus.Success);

            var mockedFactory = new Mock<IUserFactory>();

            var loginViewModel = new LoginViewModel()
            {
                Email = email,
                Password = password,
                RememberMe = rememberMe
            };

            var controller = new AccountController(mockedProvider.Object, mockedFactory.Object);

            //Act
            var result = controller.Login(loginViewModel, returnUrl) as RedirectResult;

            //Assert
            Assert.AreEqual("/Home/Index", result.Url);
        }

        [TestCase("fakeMail@fakeMailService.fakeDomain", "fakePassword", true, null)]
        [TestCase("fakeMail@fakeMailService.fakeDomain", "fakePassword", false, null)]

        public void _Return_LockoutView_WhenProvider_ReturnLockedOutStatus(
        string email,
        string password,
        bool rememberMe,
        string returnUrl)
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();
            mockedProvider.Setup(
                    p =>
                        p.SignInWithPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(),
                            It.IsAny<bool>()))
                .Returns(SignInStatus.LockedOut);

            var mockedFactory = new Mock<IUserFactory>();

            var loginViewModel = new LoginViewModel()
            {
                Email = email,
                Password = password,
                RememberMe = rememberMe
            };

            var controller = new AccountController(mockedProvider.Object, mockedFactory.Object);

            //Act
            var result = controller.Login(loginViewModel, returnUrl) as ViewResult;

            //Assert
            Assert.AreEqual("Lockout", result.ViewName);
        }

        [TestCase("fakeMail@fakeMailService.fakeDomain", "fakePassword", true, null)]
        [TestCase("fakeMail@fakeMailService.fakeDomain", "fakePassword", false, null)]

        public void _Set_ModelState_NotValid_WhenProvider_ReturnsFail(
        string email,
        string password,
        bool rememberMe,
        string returnUrl)
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();
            mockedProvider.Setup(
                    p =>
                        p.SignInWithPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(),
                            It.IsAny<bool>()))
                .Returns(SignInStatus.Failure);

            var mockedFactory = new Mock<IUserFactory>();

            var loginViewModel = new LoginViewModel()
            {
                Email = email,
                Password = password,
                RememberMe = rememberMe
            };

            var controller = new AccountController(mockedProvider.Object, mockedFactory.Object);

            //Act
            controller.Login(loginViewModel, returnUrl);

            //Assert
            Assert.IsFalse(controller.ModelState.IsValid);
        }

        [TestCase("fakeMail@fakeMailService.fakeDomain", "fakePassword", true, null)]
        [TestCase("fakeMail@fakeMailService.fakeDomain", "fakePassword", false, null)]

        public void _Return_ViewWithModel_WhenProvider_ReturnsFail(
        string email,
        string password,
        bool rememberMe,
        string returnUrl)
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();
            mockedProvider.Setup(
                    p =>
                        p.SignInWithPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(),
                            It.IsAny<bool>()))
                .Returns(SignInStatus.Failure);

            var mockedFactory = new Mock<IUserFactory>();

            var loginViewModel = new LoginViewModel()
            {
                Email = email,
                Password = password,
                RememberMe = rememberMe
            };

            var controller = new AccountController(mockedProvider.Object, mockedFactory.Object);

            //Act
            var res = controller.Login(loginViewModel, returnUrl) as ViewResult;

            //Assert
            Assert.AreEqual(loginViewModel, res.Model);
        }
    }
}
