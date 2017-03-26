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
    }
}
