using System;
using Moq;
using NUnit.Framework;
using PhotoLife.Data.Contracts;
using PhotoLife.Factories;
using PhotoLife.Models;
using PhotoLife.Providers.Contracts;
using PhotoLife.Services.Contracts;

namespace PhotoLife.Services.Tests.CommentServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void _Initialize_NotNull_WhenEverythingPassedCorrectly()
        {
            //Arrange
            var mockedPostService = new Mock<IPostService>();
            var mockedNewsService = new Mock<INewsService>();
            var mockedUserService = new Mock<IUserService>();
            var mockedCommentFactory = new Mock<ICommentFactory>();
            var mockedRepository = new Mock<IRepository<Comment>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act
            var commentService = new CommentService(
                mockedPostService.Object,
                mockedNewsService.Object,
                mockedCommentFactory.Object,
                mockedUserService.Object,
                mockedRepository.Object,
                mockedDateTimeProvider.Object,
                mockedUnitOfWork.Object);

            //Assert
            Assert.IsNotNull(commentService);

        }

        [Test]
        public void _Throws_ArgumentNullException_WhenPostService_IsNull()
        {
            //Arrange
            var mockedNewsService = new Mock<INewsService>();
            var mockedUserService = new Mock<IUserService>();
            var mockedCommentFactory = new Mock<ICommentFactory>();
            var mockedRepository = new Mock<IRepository<Comment>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new CommentService(
                null,
                mockedNewsService.Object,
                mockedCommentFactory.Object,
                mockedUserService.Object,
                mockedRepository.Object,
                mockedDateTimeProvider.Object,
                mockedUnitOfWork.Object));
        }

        [Test]
        public void _Throws_ArgumentNullException_WhenNewsService_IsNull()
        {
            //Arrange
            var mockedPostService = new Mock<IPostService>();
            var mockedUserService = new Mock<IUserService>();
            var mockedCommentFactory = new Mock<ICommentFactory>();
            var mockedRepository = new Mock<IRepository<Comment>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new CommentService(
                mockedPostService.Object,
                null,
                mockedCommentFactory.Object,
                mockedUserService.Object,
                mockedRepository.Object,
                mockedDateTimeProvider.Object,
                mockedUnitOfWork.Object));
        }

        [Test]
        public void _Throws_ArgumentNullException_WhenUserService_IsNull()
        {
            //Arrange
            var mockedPostService = new Mock<IPostService>();
            var mockedNewsService = new Mock<INewsService>();
            var mockedCommentFactory = new Mock<ICommentFactory>();
            var mockedRepository = new Mock<IRepository<Comment>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new CommentService(
                mockedPostService.Object,
                mockedNewsService.Object,
                mockedCommentFactory.Object,
                null,
                mockedRepository.Object,
                mockedDateTimeProvider.Object,
                mockedUnitOfWork.Object));
        }

        [Test]
        public void _Throws_ArgumentNullException_WhenCommentFactory_IsNull()
        {
            //Arrange
            var mockedPostService = new Mock<IPostService>();
            var mockedNewsService = new Mock<INewsService>();
            var mockedUserService = new Mock<IUserService>();
            var mockedRepository = new Mock<IRepository<Comment>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new CommentService(
                mockedPostService.Object,
                mockedNewsService.Object,
                null,
                mockedUserService.Object,
                mockedRepository.Object,
                mockedDateTimeProvider.Object,
                mockedUnitOfWork.Object));
        }

        [Test]
        public void _Throws_ArgumentNullException_WhenRepositoryIsNull()
        {
            //Arrange
            var mockedPostService = new Mock<IPostService>();
            var mockedNewsService = new Mock<INewsService>();
            var mockedUserService = new Mock<IUserService>();
            var mockedCommentFactory = new Mock<ICommentFactory>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new CommentService(
                mockedPostService.Object,
                mockedNewsService.Object,
                mockedCommentFactory.Object,
                mockedUserService.Object,
                null,
                mockedDateTimeProvider.Object,
                mockedUnitOfWork.Object));
        }

        [Test]
        public void _Throws_ArgumentNullException_WhenPUnitOfWork_IsNull()
        {
            //Arrange
            var mockedPostService = new Mock<IPostService>();
            var mockedNewsService = new Mock<INewsService>();
            var mockedUserService = new Mock<IUserService>();
            var mockedCommentFactory = new Mock<ICommentFactory>();
            var mockedRepository = new Mock<IRepository<Comment>>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new CommentService(
                mockedPostService.Object,
                mockedNewsService.Object,
                mockedCommentFactory.Object,
                mockedUserService.Object,
                mockedRepository.Object,
                mockedDateTimeProvider.Object,
                null));
        }

        [Test]
        public void _Throws_ArgumentNullException_WhenDateTimeProvider_IsNull()
        {
            //Arrange
            var mockedPostService = new Mock<IPostService>();
            var mockedNewsService = new Mock<INewsService>();
            var mockedUserService = new Mock<IUserService>();
            var mockedCommentFactory = new Mock<ICommentFactory>();
            var mockedRepository = new Mock<IRepository<Comment>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new CommentService(
                mockedPostService.Object,
                mockedNewsService.Object,
                mockedCommentFactory.Object,
                mockedUserService.Object,
                mockedRepository.Object,
                null,
                mockedUnitOfWork.Object));
        }
    }
}
