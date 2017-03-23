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

namespace PhotoLife.Services.Tests.NewsServiceTests
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
            var mockedNewsFactory = new Mock<INewsFactory>();
            var mockedCategoryService = new Mock<ICategoryService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act & Asser
            Assert.Throws<ArgumentNullException>(() => new NewsService(
                null,
                mockedUserService.Object,
                mockedUnitOfWork.Object,
                mockedNewsFactory.Object,
                mockedCategoryService.Object,
                mockedDateTimeProvider.Object));
        }

        [Test]
        public void _Throw_ArgumentNullException_When_UserService_IsNull()
        {
            //Arrange
            var mockedNewsRepository = new Mock<IRepository<News>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedNewsFactory = new Mock<INewsFactory>();
            var mockedCategoryService = new Mock<ICategoryService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new NewsService(
                mockedNewsRepository.Object,
                null,
                mockedUnitOfWork.Object,
                mockedNewsFactory.Object,
                mockedCategoryService.Object,
                mockedDateTimeProvider.Object));
        }

        [Test]
        public void _Throw_ArgumentNullException_When_UnitOfWork_IsNull()
        {
            //Arrange
            var mockedNewsRepository = new Mock<IRepository<News>>();
            var mockedUserService = new Mock<IUserService>();
            var mockedNewsFactory = new Mock<INewsFactory>();
            var mockedCategoryService = new Mock<ICategoryService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new NewsService(
                mockedNewsRepository.Object,
                mockedUserService.Object,
                null,
                mockedNewsFactory.Object,
                mockedCategoryService.Object,
                mockedDateTimeProvider.Object));
        }

        [Test]
        public void _Throw_ArgumentNullException_When_NewsFactory_IsNull()
        {
            //Arrange
            var mockedNewsRepository = new Mock<IRepository<News>>();
            var mockedUserService = new Mock<IUserService>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedCategoryService = new Mock<ICategoryService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new NewsService(
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
            var mockedNewsRepository = new Mock<IRepository<News>>();
            var mockedUserService = new Mock<IUserService>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedNewsFactory = new Mock<INewsFactory>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new NewsService(
                mockedNewsRepository.Object,
                mockedUserService.Object,
                mockedUnitOfWork.Object,
                mockedNewsFactory.Object,
                null,
                mockedDateTimeProvider.Object));
        }

        [Test]
        public void _Throw_ArgumentNullException_When_DateTimeProvider_IsNull()
        {
            //Arrange
            var mockedNewsRepository = new Mock<IRepository<News>>();
            var mockedUserService = new Mock<IUserService>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedNewsFactory = new Mock<INewsFactory>();
            var mockedCategoryService = new Mock<ICategoryService>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new NewsService(
                mockedNewsRepository.Object,
                mockedUserService.Object,
                mockedUnitOfWork.Object,
                mockedNewsFactory.Object,
                mockedCategoryService.Object,
                null));
        }
    }
}
