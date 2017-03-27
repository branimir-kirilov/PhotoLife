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
    public class EditComment_Should
    {
        [TestCase("content", 7)]
        [TestCase("contenzxfsdfsdft", 9)]
        public void _Call_CommentRepository_GetById(string content, int commentId)
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
            commentService.EditComment(content,commentId);

            //Assert
            mockedRepository.Verify(r => r.GetById(commentId), Times.Once);
        }

        [TestCase("content", 7)]
        [TestCase("contenzxfsdfsdft", 9)]
        public void _Call_Repository_Update_IfCommentNotNull(string content, int commentId)
        {
            //Arrange
            var comment = new Comment();

            var mockedPostService = new Mock<IPostService>();
            var mockedNewsService = new Mock<INewsService>();
            var mockedUserService = new Mock<IUserService>();
            var mockedCommentFactory = new Mock<ICommentFactory>();

            var mockedRepository = new Mock<IRepository<Comment>>();
            mockedRepository.Setup(r=>r.GetById(It.IsAny<int>())).Returns(comment);

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
            commentService.EditComment(content, commentId);

            //Assert
            mockedRepository.Verify(r => r.Update(comment), Times.Once);
        }
        [TestCase("content", 7)]
        [TestCase("contenzxfsdfsdft", 9)]
        public void _Call_UnitOfWork_Commit_IfCommentNotNull(string content, int commentId)
        {
            //Arrange
            var comment = new Comment();

            var mockedPostService = new Mock<IPostService>();
            var mockedNewsService = new Mock<INewsService>();
            var mockedUserService = new Mock<IUserService>();
            var mockedCommentFactory = new Mock<ICommentFactory>();

            var mockedRepository = new Mock<IRepository<Comment>>();
            mockedRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(comment);

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
            commentService.EditComment(content, commentId);

            //Assert
            mockedUnitOfWork.Verify(r => r.Commit(), Times.Once);
        }

    }
}
