using Moq;
using NUnit.Framework;
using PhotoLife.Data.Contracts;
using PhotoLife.Models;

namespace PhotoLife.Services.Tests.UserServiceTests
{
    [TestFixture]
    public class GetById_Should
    {
        [TestCase("some id")]
        [TestCase("other id")]
        public void _CallRepository_GetByIdMethod(string id)
        {
            //Arrange
            var mockedRepository = new Mock<IRepository<User>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var userService = new UserService(mockedRepository.Object, mockedUnitOfWork.Object);

            //Act
            userService.GetUserById(id);

            //Assert            
            mockedRepository.Verify(r => r.GetById(id), Times.Once);
        }
    }
}
