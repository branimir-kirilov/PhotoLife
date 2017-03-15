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

        [TestCase("some id")]
        [TestCase("other id")]
        public void _Return_Correctly(string id)
        {
            //Arrange
            var mockedUser = new Mock<User>();
            var mockedRepository = new Mock<IRepository<User>>();
            mockedRepository.Setup(r=>r.GetById(It.IsAny<string>())).Returns(mockedUser.Object);

            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var userService = new UserService(mockedRepository.Object, mockedUnitOfWork.Object);

            //Act
            var res = userService.GetUserById(id);

            //Assert            
            Assert.AreSame(mockedUser.Object, res);
        }

        [TestCase("some id")]
        [TestCase("other id")]
        public void _Return_CorrectInstance(string id)
        {
            //Arrange
            var mockedUser = new Mock<User>();
            var mockedRepository = new Mock<IRepository<User>>();
            mockedRepository.Setup(r => r.GetById(It.IsAny<string>())).Returns(mockedUser.Object);

            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var userService = new UserService(mockedRepository.Object, mockedUnitOfWork.Object);

            //Act
            var res = userService.GetUserById(id);

            //Assert            
            Assert.IsInstanceOf<User>(res);
        }
    }
}
