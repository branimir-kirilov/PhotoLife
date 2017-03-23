using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Moq;
using NUnit.Framework;
using PhotoLife.Authentication.ApplicationManagers;
using PhotoLife.Models;
using PhotoLife.Providers.Contracts;

namespace PhotoLife.Authentication.Tests.HttpContextAuthenticationProvider
{
    [TestFixture]
    public class SignInWithPassword_Should
    {
        [TestCase("fakeusername", "fakePassword", true, false)]
        [TestCase("fakeusername", "fakePassword", false, false)]
        public void _Call_SignInManager_PasswordSignIn(string username, string password, bool rememberMe, bool shouldLockout)
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

            //Act
            provider.SignInWithPassword(username, password, rememberMe, shouldLockout);

            //Assert
            mockedSignInManager.Verify(m => m.PasswordSignInAsync(username, password, rememberMe, shouldLockout), Times.Once);

        }

        [TestCase("fakeusername", "fakePassword", true, false, SignInStatus.Success)]
        [TestCase("fakeusername", "fakePassword", true, false, SignInStatus.Failure)]
        [TestCase("fakeusername", "fakePassword", true, false, SignInStatus.LockedOut)]
        [TestCase("fakeusername", "fakePassword", true, false, SignInStatus.RequiresVerification)]
        public void _ReturnCorrectly_(string username, string password, bool rememberMe, bool shouldLockout, SignInStatus signInStatus)
        {
            //Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var mockedUserStore = new Mock<IUserStore<User>>();
            var mockedUserManager = new Mock<ApplicationUserManager>(mockedUserStore.Object);

            var mockedAuthenticationManager = new Mock<IAuthenticationManager>();
            var mockedSignInManager = new Mock<ApplicationSignInManager>(
                mockedUserManager.Object,
                mockedAuthenticationManager.Object);

            mockedSignInManager.Setup(m => m.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(signInStatus);

            var mockedHttpContextProvider = new Mock<IHttpContextProvider>();
            mockedHttpContextProvider.Setup(p => p.GetUserManager<ApplicationUserManager>())
                .Returns(mockedUserManager.Object);
            mockedHttpContextProvider.Setup(p => p.GetUserManager<ApplicationSignInManager>())
                .Returns(mockedSignInManager.Object);

            var provider = new Providers.HttpContextAuthenticationProvider(mockedHttpContextProvider.Object, mockedDateTimeProvider.Object);

            //Act
            var result = provider.SignInWithPassword(username, password, rememberMe, shouldLockout);

            //Assert
            Assert.AreEqual(signInStatus, result);
        }
    }
}
