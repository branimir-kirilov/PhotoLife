using System.Web.Mvc;
using CloudinaryDotNet;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using PhotoLife.Authentication.Providers;
using PhotoLife.Controllers;
using PhotoLife.Factories;
using PhotoLife.Models;
using PhotoLife.Models.Account;

namespace PhotoLife.Web.Tests.Controllers.Account
{
    [TestFixture]
    public class After_Register_Should
    {
        [TestCase("fakeMail@fakeService.fakeDomain", "branimiri", "Branimir", "fakeDescription", "fakePassword", "fakeUrl")]
        public void _Call_AuthenticationProvider_RegisterAndLoginUser_Correctly(
            string email,
            string username,
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
            var mockedCloudinaryFactory = new Mock<ICloudinaryFactory>();

            var fakeAcc = new CloudinaryDotNet.Account("sdfsdfsd", "sdfsdfsdf", "sdfsdfsdf");
            var mockedCloudinary = new Cloudinary(fakeAcc);
           

            var model = new RegisterViewModel(mockedCloudinary)
            {
                Email = email,
                UserName = username,
                Name = name,
                Password = password,
                Description = description,
                ProfilePicUrl = profilePicUrl
            };

            var controller = new AccountController(mockedAuthenticationProvider.Object, mockedFactory.Object, mockedCloudinaryFactory.Object);

            //Act
            controller.Register(model);

            //Assert
            mockedFactory.Verify(mf => mf.CreateUser(username, email, name, description, profilePicUrl), Times.Once);
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

            var mockedCloudinaryFactory = new Mock<ICloudinaryFactory>();

            var fakeAcc = new CloudinaryDotNet.Account("sdfsdfsd", "sdfsdfsdf", "sdfsdfsdf");
            var mockedCloudinary = new Cloudinary(fakeAcc);

            var model = new RegisterViewModel(mockedCloudinary)
            {
                Email = email,
                Name = name,
                Password = password,
                Description = description,
                ProfilePicUrl = profilePicUrl
            };

            var controller = new AccountController(mockedAuthenticationProvider.Object, mockedFactory.Object, mockedCloudinaryFactory.Object);
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
            var mockedCloudinaryFactory = new Mock<ICloudinaryFactory>();

            var fakeAcc = new CloudinaryDotNet.Account("sdfsdfsd", "sdfsdfsdf", "sdfsdfsdf");
            var mockedCloudinary = new Cloudinary(fakeAcc);

            var model = new RegisterViewModel(mockedCloudinary)
            {
                Email = email,
                Name = name,
                Password = password,
                Description = description,
                ProfilePicUrl = profilePicUrl
            };

            var controller = new AccountController(mockedAuthenticationProvider.Object, mockedFactory.Object, mockedCloudinaryFactory.Object);

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

            var mockedCloudinaryFactory = new Mock<ICloudinaryFactory>();

            var fakeAcc = new CloudinaryDotNet.Account("sdfsdfsd", "sdfsdfsdf", "sdfsdfsdf");
            var mockedCloudinary = new Cloudinary(fakeAcc);

            var model = new RegisterViewModel(mockedCloudinary)
            {
                Email = email,
                Name = name,
                Password = password,
                Description = description,
                ProfilePicUrl = profilePicUrl
            };

            var controller = new AccountController(mockedAuthenticationProvider.Object, mockedFactory.Object, mockedCloudinaryFactory.Object);

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

            var mockedCloudinaryFactory = new Mock<ICloudinaryFactory>();

            var fakeAcc = new CloudinaryDotNet.Account("sdfsdfsd", "sdfsdfsdf", "sdfsdfsdf");
            var mockedCloudinary = new Cloudinary(fakeAcc);

            var model = new RegisterViewModel(mockedCloudinary)
            {
                Email = email,
                Name = name,
                Password = password,
                Description = description,
                ProfilePicUrl = profilePicUrl
            };

            var controller = new AccountController(mockedAuthenticationProvider.Object, mockedFactory.Object, mockedCloudinaryFactory.Object);

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

            var mockedCloudinaryFactory = new Mock<ICloudinaryFactory>();

            var fakeAcc = new CloudinaryDotNet.Account("sdfsdfsd", "sdfsdfsdf", "sdfsdfsdf");
            var mockedCloudinary = new Cloudinary(fakeAcc);

            var model = new RegisterViewModel(mockedCloudinary)
            {
                Email = email,
                Name = name,
                Password = password,
                Description = description,
                ProfilePicUrl = profilePicUrl
            };

            var controller = new AccountController(mockedAuthenticationProvider.Object, mockedFactory.Object, mockedCloudinaryFactory.Object);
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

            var mockedCloudinaryFactory = new Mock<ICloudinaryFactory>();

            var fakeAcc = new CloudinaryDotNet.Account("sdfsdfsd", "sdfsdfsdf", "sdfsdfsdf");
            var mockedCloudinary = new Cloudinary(fakeAcc);

            var model = new RegisterViewModel(mockedCloudinary)
            {
                Email = email,
                Name = name,
                Password = password,
                Description = description,
                ProfilePicUrl = profilePicUrl
            };

            var controller = new AccountController(mockedAuthenticationProvider.Object, mockedFactory.Object, mockedCloudinaryFactory.Object);
            controller.ModelState.AddModelError("error", "error-message");

            //Act
            var res = controller.Register(model) as ViewResult;

            //Assert
            Assert.AreSame(model, res.Model);
        }
    }
}
