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
    }
}
