using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using PhotoLife.Authentication.Providers;
using PhotoLife.Controllers;
using PhotoLife.Factories;

namespace PhotoLife.Web.Tests.Controllers.Account
{
    [TestFixture]
    public class Login_Should
    {
        [TestCase("/home")]
        [TestCase("/profile")]
        public void _Return_View(string url)
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();
            var mockedFactory = new Mock<IUserFactory>();

            var controller = new AccountController(mockedProvider.Object, mockedFactory.Object);

            //Act
            var result = controller.Login(url);

            //Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [TestCase("/home")]
        [TestCase("/profile")]
        public void _Return_ShouldSet_CorrectViewBag(string url)
        {
            //Arrange
            var mockedProvider = new Mock<IAuthenticationProvider>();
            var mockedFactory = new Mock<IUserFactory>();

            var controller = new AccountController(mockedProvider.Object, mockedFactory.Object);

            //Act
            ViewResult result = controller.Login(url) as ViewResult;

            //Assert
            Assert.AreEqual(url, result.ViewBag.ReturnUrl);
        }
    }
}
