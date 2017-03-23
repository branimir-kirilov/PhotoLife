using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using PhotoLife.Data.Contracts;
using PhotoLife.Factories;
using PhotoLife.Models;
using PhotoLife.Providers.Contracts;
using PhotoLife.Services.Contracts;

namespace PhotoLife.Services.Tests.PostsServiceTests
{
    public class GetAll_Should
    {
        [Test]
        public void _Call_PostRepository_GetAll()
        {
            //Arrange
            var mockedPostRepository = new Mock<IRepository<Post>>();
            var mockedUserService = new Mock<IUserService>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedPostFactory = new Mock<IPostFactory>();
            var mockedCategoryService = new Mock<ICategoryService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var postService = new PostsService(
                  mockedPostRepository.Object,
                  mockedUserService.Object,
                  mockedUnitOfWork.Object,
                  mockedPostFactory.Object,
                  mockedCategoryService.Object,
                  mockedDateTimeProvider.Object);

            //Act
            postService.GetAll();

            //Assert
            mockedPostRepository.Verify(r => r.GetAll, Times.Once);
        }

        [Test]
        public void _Return_Correctly()
        {
            //Arrange
            var mockedPost = new List<Post>()
            {
                new Mock<Post>() { }.Object,
                new Mock<Post>() { }.Object
            }.AsQueryable();

            var mockedPostRepository = new Mock<IRepository<Post>>();
            mockedPostRepository.Setup(r => r.GetAll).Returns(mockedPost);

            var mockedUserService = new Mock<IUserService>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedPostFactory = new Mock<IPostFactory>();
            var mockedCategoryService = new Mock<ICategoryService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var postService = new PostsService(
                  mockedPostRepository.Object,
                  mockedUserService.Object,
                  mockedUnitOfWork.Object,
                  mockedPostFactory.Object,
                  mockedCategoryService.Object,
                  mockedDateTimeProvider.Object);

            //Act
            var res = postService.GetAll();

            //Assert
            Assert.AreEqual(mockedPost, res);
        }
    }
}
