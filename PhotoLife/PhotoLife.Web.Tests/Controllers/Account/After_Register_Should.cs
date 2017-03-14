using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using PhotoLife.Authentication.Providers;
using PhotoLife.Controllers;
using PhotoLife.Factories;
using PhotoLife.Models;

namespace PhotoLife.Web.Tests.Controllers.Account
{
    [TestFixture]
    public class After_Register_Should
    {
        [TestCase("fakeMail@fakeService.fakeDomain", "branimir", "fakeDescription", "fakePassword", "fakeUrl")]
        public void _Call_AuthenticationProvider_RegisterAndLoginUser_Correctly(
            string email,
            string name,
            string description,
            string password,
            string profilePicUrl)
        {
            //Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(
                    p =>
                        p.RegisterAndLoginUser(
                            It.IsAny<User>(),
                            It.IsAny<string>(),
                            It.IsAny<bool>(),
                            It.IsAny<bool>()))
                .Returns(IdentityResult.Success);

            var mockedFactory = new Mock<IUserFactory>();

            var model = new RegisterViewModel()
            {
                Email = email,
                Name = name,
                Password = password,
                Description = description,
                ProfilePicUrl = profilePicUrl
            };

            var controller = new AccountController(mockedAuthenticationProvider.Object, mockedFactory.Object);

            //Act
            controller.Register(model);

            //Assert
            mockedFactory.Verify(mf => mf.CreateUser(email, email, name, description, profilePicUrl), Times.Once);
        }


        [TestCase("fakeMail@fakeService.fakeDomain", "branimir", "fakeDescription", "fakePassword", "fakeUrl")]
        public void _Call_UserFactory_CreateUser_WhenStateIsValid(
            string email,
            string name,
            string description,
            string password,
            string profilePicUrl)
        {
            //Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(
                    p =>
                        p.RegisterAndLoginUser(
                            It.IsAny<User>(),
                            It.IsAny<string>(),
                            It.IsAny<bool>(),
                            It.IsAny<bool>()))
                .Returns(IdentityResult.Success);

            var user = new User();

            var mockedFactory = new Mock<IUserFactory>();
            mockedFactory.Setup(f => f.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                 .Returns(user);

            var model = new RegisterViewModel()
            {
                Email = email,
                Name = name,
                Password = password,
                Description = description,
                ProfilePicUrl = profilePicUrl
            };

            var controller = new AccountController(mockedAuthenticationProvider.Object, mockedFactory.Object);

            //Act
            controller.Register(model);

            //Assert
            mockedAuthenticationProvider.Setup(ap => ap.RegisterAndLoginUser(user, password, It.IsAny<bool>(), It.IsAny<bool>()));
        }

        [TestCase("fakeMail@fakeService.fakeDomain", "branimir", "fakeDescription", "fakePassword", "fakeUrl")]
        public void _Return_RedirectToRouteResult_WhenIdentityResult_Success(
            string email,
            string name,
            string description,
            string password,
            string profilePicUrl)
        {
            //Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(
                    p =>
                        p.RegisterAndLoginUser(
                            It.IsAny<User>(),
                            It.IsAny<string>(),
                            It.IsAny<bool>(),
                            It.IsAny<bool>()))
                .Returns(IdentityResult.Success);

            var user = new User();

            var mockedFactory = new Mock<IUserFactory>();
            mockedFactory.Setup(f => f.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                 .Returns(user);

            var model = new RegisterViewModel()
            {
                Email = email,
                Name = name,
                Password = password,
                Description = description,
                ProfilePicUrl = profilePicUrl
            };

            var controller = new AccountController(mockedAuthenticationProvider.Object, mockedFactory.Object);

            //Act
            var res = controller.Register(model);

            //Assert
            Assert.IsInstanceOf<RedirectToRouteResult>(res);
        }

        [TestCase("fakeMail@fakeService.fakeDomain", "branimir", "fakeDescription", "fakePassword", "fakeUrl")]
        public void _Return__WhenIdentityResult_NotSuccess(
            string email,
            string name,
            string description,
            string password,
            string profilePicUrl)
        {
            //Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(
                    p =>
                        p.RegisterAndLoginUser(
                            It.IsAny<User>(),
                            It.IsAny<string>(),
                            It.IsAny<bool>(),
                            It.IsAny<bool>()))
                .Returns(IdentityResult.Failed());

            var user = new User();

            var mockedFactory = new Mock<IUserFactory>();
            mockedFactory.Setup(f => f.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                 .Returns(user);

            var model = new RegisterViewModel()
            {
                Email = email,
                Name = name,
                Password = password,
                Description = description,
                ProfilePicUrl = profilePicUrl
            };

            var controller = new AccountController(mockedAuthenticationProvider.Object, mockedFactory.Object);

            //Act
            var res = controller.Register(model);

            //Assert
            Assert.IsInstanceOf<ViewResult>(res);
        }

        [TestCase("fakeMail@fakeService.fakeDomain", "branimir", "fakeDescription", "fakePassword", "fakeUrl")]
        public void _Set_ViewModel_Correctly_WhenIdentityResult_NotSuccess(
            string email,
            string name,
            string description,
            string password,
            string profilePicUrl)
        {
            //Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(
                    p =>
                        p.RegisterAndLoginUser(
                            It.IsAny<User>(),
                            It.IsAny<string>(),
                            It.IsAny<bool>(),
                            It.IsAny<bool>()))
                .Returns(IdentityResult.Failed());

            var user = new User();

            var mockedFactory = new Mock<IUserFactory>();
            mockedFactory.Setup(f => f.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                 .Returns(user);

            var model = new RegisterViewModel()
            {
                Email = email,
                Name = name,
                Password = password,
                Description = description,
                ProfilePicUrl = profilePicUrl
            };

            var controller = new AccountController(mockedAuthenticationProvider.Object, mockedFactory.Object);

            //Act
            var res = controller.Register(model) as ViewResult;

            //Assert
            Assert.AreSame(model, res.Model);
        }

        [TestCase("fakeMail@fakeService.fakeDomain", "branimir", "fakeDescription", "fakePassword", "fakeUrl")]
        public void _Return_View_WhenIdentityResult_Success_ModelState_NotValid(
          string email,
          string name,
          string description,
          string password,
          string profilePicUrl)
        {
            //Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(
                    p =>
                        p.RegisterAndLoginUser(
                            It.IsAny<User>(),
                            It.IsAny<string>(),
                            It.IsAny<bool>(),
                            It.IsAny<bool>()))
                .Returns(IdentityResult.Failed());

            var user = new User();

            var mockedFactory = new Mock<IUserFactory>();
            mockedFactory.Setup(f => f.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                 .Returns(user);

            var model = new RegisterViewModel()
            {
                Email = email,
                Name = name,
                Password = password,
                Description = description,
                ProfilePicUrl = profilePicUrl
            };

            var controller = new AccountController(mockedAuthenticationProvider.Object, mockedFactory.Object);
            controller.ModelState.AddModelError("error", "error-message");

            //Act
            var res = controller.Register(model);

            //Assert
            Assert.IsInstanceOf<ViewResult>(res);
        }

        [TestCase("fakeMail@fakeService.fakeDomain", "branimir", "fakeDescription", "fakePassword", "fakeUrl")]
        public void _Set_ViewModel_Correctly_WhenModelState_NotValid(
          string email,
          string name,
          string description,
          string password,
          string profilePicUrl)
        {
            //Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.Setup(
                    p =>
                        p.RegisterAndLoginUser(
                            It.IsAny<User>(),
                            It.IsAny<string>(),
                            It.IsAny<bool>(),
                            It.IsAny<bool>()))
                .Returns(IdentityResult.Failed());

            var user = new User();

            var mockedFactory = new Mock<IUserFactory>();
            mockedFactory.Setup(f => f.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                 .Returns(user);

            var model = new RegisterViewModel()
            {
                Email = email,
                Name = name,
                Password = password,
                Description = description,
                ProfilePicUrl = profilePicUrl
            };

            var controller = new AccountController(mockedAuthenticationProvider.Object, mockedFactory.Object);
            controller.ModelState.AddModelError("error", "error-message");

            //Act
            var res = controller.Register(model) as ViewResult;

            //Assert
            Assert.AreSame(model, res.Model);
        }
    }
}
