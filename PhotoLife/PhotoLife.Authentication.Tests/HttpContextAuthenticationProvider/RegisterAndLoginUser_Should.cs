using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Moq;
using NUnit.Framework;
using PhotoLife.Authentication.ApplicationManagers;
using PhotoLife.Models;
using PhotoLife.Providers.Contracts;

namespace PhotoLife.Authentication.Tests.HttpContextAuthenticationProvider
{
    [TestFixture]
    public class RegisterAndLoginUser_Should
    {
        [TestCase("fakePassword", true, true)]
        [TestCase("fakePassword", false, false)]
        [TestCase("fakePassword", true, false)]
        [TestCase("fakePassword", false, true)]
        public void _Call_UserManager_Create_Method(string password, bool isPersistant, bool rememberMe)
        {
            //Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var mockedUserStore = new Mock<IUserStore<User>>();

            var mockedUserManager = new Mock<ApplicationUserManager>(mockedUserStore.Object);
            mockedUserManager.Setup(man => man.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(new IdentityResult());

            var mockedHttpContextProvider = new Mock<IHttpContextProvider>();
            mockedHttpContextProvider.Setup(p => p.GetUserManager<ApplicationUserManager>())
                .Returns(mockedUserManager.Object);

            var provider = new Providers.HttpContextAuthenticationProvider(mockedHttpContextProvider.Object, mockedDateTimeProvider.Object);

            var user = new User();

            //Act
            provider.RegisterAndLoginUser(user, password, isPersistant, rememberMe);

            //Assert
            mockedUserManager.Verify(m => m.CreateAsync(user, password), Times.Once);
        }


        [TestCase("fakePassword", true, true)]
        [TestCase("fakePassword", false, false)]
        [TestCase("fakePassword", true, false)]
        [TestCase("fakePassword", false, true)]
        public void _Call_SignInManager_SignIn_Method(string password, bool isPersistant, bool rememberMe)
        {
            //Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var mockedUserStore = new Mock<IUserStore<User>>();
            var mockedUserManager = new Mock<ApplicationUserManager>(mockedUserStore.Object);
            mockedUserManager.Setup(man => man.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);


            var mockedAuthenticationManager = new Mock<IAuthenticationManager>();
            var mockedSignInManager = new Mock<ApplicationSignInManager>(
                mockedUserManager.Object,
                mockedAuthenticationManager.Object);

            var mockedHttpContextProvider = new Mock<IHttpContextProvider>();
            mockedHttpContextProvider.Setup(p => p.GetUserManager<ApplicationUserManager>())
                .Returns(mockedUserManager.Object);
            mockedHttpContextProvider.Setup(p => p.GetUserManager<ApplicationSignInManager>())
                .Returns(mockedSignInManager.Object);

            var provider = new Providers.HttpContextAuthenticationProvider(mockedHttpContextProvider.Object, mockedDateTimeProvider.Object);

            var user = new User();

            //Act
            provider.RegisterAndLoginUser(user, password, isPersistant, rememberMe);

            //Assert
            mockedSignInManager.Verify(m => m.SignInAsync(user, isPersistant, rememberMe), Times.Once);
        }

        [TestCase("fakePassword", true, true)]
        [TestCase("fakePassword", false, false)]
        [TestCase("fakePassword", true, false)]
        [TestCase("fakePassword", false, true)]
        public void _Return_IdentityResult(string password, bool isPersistant, bool rememberMe)
        {
            //Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var expected = new IdentityResult();

            var mockedUserStore = new Mock<IUserStore<User>>();
            var mockedUserManager = new Mock<ApplicationUserManager>(mockedUserStore.Object);
            mockedUserManager.Setup(man => man.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(expected);


            var mockedAuthenticationManager = new Mock<IAuthenticationManager>();
            var mockedSignInManager = new Mock<ApplicationSignInManager>(
                mockedUserManager.Object,
                mockedAuthenticationManager.Object);

            var mockedHttpContextProvider = new Mock<IHttpContextProvider>();
            mockedHttpContextProvider.Setup(p => p.GetUserManager<ApplicationUserManager>())
                .Returns(mockedUserManager.Object);
            mockedHttpContextProvider.Setup(p => p.GetUserManager<ApplicationSignInManager>())
                .Returns(mockedSignInManager.Object);

            var provider = new Providers.HttpContextAuthenticationProvider(mockedHttpContextProvider.Object, mockedDateTimeProvider.Object);

            var user = new User();

            //Act
            var result = provider.RegisterAndLoginUser(user, password, isPersistant, rememberMe);

            //Assert
            Assert.AreSame(expected, result);

        }
    }
}
