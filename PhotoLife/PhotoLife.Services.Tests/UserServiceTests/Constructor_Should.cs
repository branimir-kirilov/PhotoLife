using System;
using Moq;
using NUnit.Framework;
using PhotoLife.Data.Contracts;
using PhotoLife.Models;

namespace PhotoLife.Services.Tests.UserServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {

        [Test]
        public void _Throw_ArgumentNullException_WhenRepository_IsNull()
        {
            //Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new UserService(null, mockedUnitOfWork.Object));
        }

        [Test]
        public void _Throw_ArgumentNullException_WhenUnitOfWork_IsNull()
        {
            //Arrange
            var mockedUserRepository = new Mock<IRepository<User>>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new UserService(mockedUserRepository.Object, null));
        }

        [Test]
        public void _NotBeNull_WhenEverything_PassedCorrectly()
        {
            //Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedUserRepository = new Mock<IRepository<User>>();

            //Act 
            var userService = new UserService(mockedUserRepository.Object, mockedUnitOfWork.Object);

            //Assert
            Assert.IsNotNull(userService);
        }

        [Test]
        public void _Initializes_AsCorrectInstance_WhenEverything_PassedCorrectly()
        {
            //Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedUserRepository = new Mock<IRepository<User>>();

            //Act 
            var userService = new UserService(mockedUserRepository.Object, mockedUnitOfWork.Object);

            //Assert
            Assert.IsInstanceOf<UserService>(userService);
        }
    }
}
