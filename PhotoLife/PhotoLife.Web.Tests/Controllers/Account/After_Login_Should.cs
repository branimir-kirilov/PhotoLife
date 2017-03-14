using System.Web.Mvc;
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
    }
}
