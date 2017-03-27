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

namespace PhotoLife.Services.Tests.CommentServiceTests
{

    [TestFixture]
    public class AddCommentToNews_Should
    {
        [TestCase("Coment text", 1, "user id")]
        [TestCase("Coment texasdasdt", 5, "userId&8")]
        public void _Call_DateTimeProvider_GetCurrentDate(string text, int newsId, string userId)
        {
            //Arrange
            var mockedPostService = new Mock<IPostService>();
            var mockedNewsService = new Mock<INewsService>();
            var mockedUserService = new Mock<IUserService>();
            var mockedCommentFactory = new Mock<ICommentFactory>();
            var mockedRepository = new Mock<IRepository<Comment>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var commentService = new CommentService(
                mockedPostService.Object,
                mockedNewsService.Object,
                mockedCommentFactory.Object,
                mockedUserService.Object,
                mockedRepository.Object,
                mockedDateTimeProvider.Object,
                mockedUnitOfWork.Object);


            //Act
            commentService.AddCommentToNews(text, newsId, userId);

            //Assert
            mockedDateTimeProvider.Verify(d => d.GetCurrentDate(), Times.Once);
        }

        [TestCase("Coment text", 1, "user id")]
        [TestCase("Coment texasdasdt", 5, "userId&8")]
        public void _Call_UserService_GetUserById(string text, int newsId, string userId)
        {
            //Arrange
            var mockedPostService = new Mock<IPostService>();
            var mockedNewsService = new Mock<INewsService>();
            var mockedUserService = new Mock<IUserService>();
            var mockedCommentFactory = new Mock<ICommentFactory>();
            var mockedRepository = new Mock<IRepository<Comment>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var commentService = new CommentService(
                mockedPostService.Object,
                mockedNewsService.Object,
                mockedCommentFactory.Object,
                mockedUserService.Object,
                mockedRepository.Object,
                mockedDateTimeProvider.Object,
                mockedUnitOfWork.Object);


            //Act
            commentService.AddCommentToNews(text, newsId, userId);

            //Assert
            mockedUserService.Verify(d => d.GetUserById(userId), Times.Once);
        }

        [TestCase("Coment text", 1, "user id")]
        [TestCase("Coment texasdasdt", 5, "userId&8")]
        public void _Call_CommentFactory_CreateComment(string text, int newsId, string userId)
        {
            //Arrange
            var user = new User();
            var date = new DateTime();

            var mockedPostService = new Mock<IPostService>();
            var mockedNewsService = new Mock<INewsService>();

            var mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(u => u.GetUserById(It.IsAny<string>())).Returns(user);

            var mockedCommentFactory = new Mock<ICommentFactory>();
            var mockedRepository = new Mock<IRepository<Comment>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            mockedDateTimeProvider.Setup(d => d.GetCurrentDate()).Returns(date);

            var commentService = new CommentService(
                mockedPostService.Object,
                mockedNewsService.Object,
                mockedCommentFactory.Object,
                mockedUserService.Object,
                mockedRepository.Object,
                mockedDateTimeProvider.Object,
                mockedUnitOfWork.Object);


            //Act
            commentService.AddCommentToNews(text, newsId, userId);

            //Assert
            mockedCommentFactory.Verify(d => d.CreateComment(user, date, text), Times.Once);
        }

        [TestCase("Coment text", 1, "user id")]
        [TestCase("Coment texasdasdt", 5, "userId&8")]
        public void _Call_NewsService_AddComment(string text, int newsId, string userId)
        {
            //Arrange
            var user = new User();
            var date = new DateTime();
            var comment = new Comment();

            var mockedPostService = new Mock<IPostService>();
            var mockedNewsService = new Mock<INewsService>();

            var mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(u => u.GetUserById(It.IsAny<string>())).Returns(user);

            var mockedCommentFactory = new Mock<ICommentFactory>();
            mockedCommentFactory.Setup(c => c.CreateComment(It.IsAny<User>(), It.IsAny<DateTime>(), It.IsAny<string>()))
                .Returns(comment);

            var mockedRepository = new Mock<IRepository<Comment>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            mockedDateTimeProvider.Setup(d => d.GetCurrentDate()).Returns(date);

            var commentService = new CommentService(
                mockedPostService.Object,
                mockedNewsService.Object,
                mockedCommentFactory.Object,
                mockedUserService.Object,
                mockedRepository.Object,
                mockedDateTimeProvider.Object,
                mockedUnitOfWork.Object);


            //Act
            commentService.AddCommentToNews(text, newsId, userId);

            //Assert
            mockedNewsService.Verify(d => d.AddComment(newsId, comment), Times.Once);
        }
    }
}
