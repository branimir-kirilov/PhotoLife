using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PhotoLife.Data.Contracts;
using PhotoLife.Factories;
using PhotoLife.Models;
using PhotoLife.Providers.Contracts;
using PhotoLife.Services.Contracts;

namespace PhotoLife.Services.Tests.PostServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void _Throw_ArgumentNullException_When_NewsRepositoryIsNull()
        {
            //Arrange
            var mockedUserService = new Mock<IUserService>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedPostFactory = new Mock<IPostFactory>();
            var mockedCategoryService = new Mock<ICategoryService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act & Asser
            Assert.Throws<ArgumentNullException>(() => new PostsService(
                null,
                mockedUserService.Object,
                mockedUnitOfWork.Object,
                mockedPostFactory.Object,
                mockedCategoryService.Object,
                mockedDateTimeProvider.Object));
        }

        [Test]
        public void _Throw_ArgumentNullException_When_UserService_IsNull()
        {
            //Arrange
            var mockedNewsRepository = new Mock<IRepository<Post>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedPostFactory = new Mock<IPostFactory>();
            var mockedCategoryService = new Mock<ICategoryService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PostsService(
                mockedNewsRepository.Object,
                null,
                mockedUnitOfWork.Object,
                mockedPostFactory.Object,
                mockedCategoryService.Object,
                mockedDateTimeProvider.Object));
        }

        [Test]
        public void _Throw_ArgumentNullException_When_UnitOfWork_IsNull()
        {
            //Arrange
            var mockedNewsRepository = new Mock<IRepository<Post>>();
            var mockedUserService = new Mock<IUserService>();
            var mockedPostFactory = new Mock<IPostFactory>();
            var mockedCategoryService = new Mock<ICategoryService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PostsService(
                mockedNewsRepository.Object,
                mockedUserService.Object,
                null,
                mockedPostFactory.Object,
                mockedCategoryService.Object,
                mockedDateTimeProvider.Object));
        }

        [Test]
        public void _Throw_ArgumentNullException_When_NewsFactory_IsNull()
        {
            //Arrange
            var mockedNewsRepository = new Mock<IRepository<Post>>();
            var mockedUserService = new Mock<IUserService>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedCategoryService = new Mock<ICategoryService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PostsService(
                mockedNewsRepository.Object,
                mockedUserService.Object,
                mockedUnitOfWork.Object,
                null,
                mockedCategoryService.Object,
                mockedDateTimeProvider.Object));
        }

        [Test]
        public void _Throw_ArgumentNullException_When_CategoryService_IsNull()
        {
            //Arrange
            var mockedNewsRepository = new Mock<IRepository<Post>>();
            var mockedUserService = new Mock<IUserService>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedPostFactory = new Mock<IPostFactory>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PostsService(
                mockedNewsRepository.Object,
                mockedUserService.Object,
                mockedUnitOfWork.Object,
                mockedPostFactory.Object,
                null,
                mockedDateTimeProvider.Object));
        }

        [Test]
        public void _Throw_ArgumentNullException_When_DateTimeProvider_IsNull()
        {
            //Arrange
            var mockedNewsRepository = new Mock<IRepository<Post>>();
            var mockedUserService = new Mock<IUserService>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedPostFactory = new Mock<IPostFactory>();
            var mockedCategoryService = new Mock<ICategoryService>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PostsService(
                mockedNewsRepository.Object,
                mockedUserService.Object,
                mockedUnitOfWork.Object,
                mockedPostFactory.Object,
                mockedCategoryService.Object,
                null));
        }

        [Test]
        public void _NotThrow_ArgumentNullException_When_EverythingCorrect()
        {
            //Arrange
            var mockedNewsRepository = new Mock<IRepository<Post>>();
            var mockedUserService = new Mock<IUserService>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedPostFactory = new Mock<IPostFactory>();
            var mockedCategoryService = new Mock<ICategoryService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act
            var result = new PostsService(
                  mockedNewsRepository.Object,
                  mockedUserService.Object,
                  mockedUnitOfWork.Object,
                  mockedPostFactory.Object,
                  mockedCategoryService.Object,
                  mockedDateTimeProvider.Object);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void _Initializes_CorrectInstance_WhenEverything_PassedCorrectly()
        {
            //Arrange
            var mockedNewsRepository = new Mock<IRepository<Post>>();
            var mockedUserService = new Mock<IUserService>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedPostFactory = new Mock<IPostFactory>();
            var mockedCategoryService = new Mock<ICategoryService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act
            var result = new PostsService(
                  mockedNewsRepository.Object,
                  mockedUserService.Object,
                  mockedUnitOfWork.Object,
                  mockedPostFactory.Object,
                  mockedCategoryService.Object,
                  mockedDateTimeProvider.Object);

            //Assert
            Assert.IsInstanceOf<IPostService>(result);
        }
    }
}
