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
    }
}
