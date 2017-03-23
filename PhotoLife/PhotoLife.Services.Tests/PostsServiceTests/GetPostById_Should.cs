using Moq;
using NUnit.Framework;
using PhotoLife.Data.Contracts;
using PhotoLife.Factories;
using PhotoLife.Models;
using PhotoLife.Providers.Contracts;
using PhotoLife.Services.Contracts;

namespace PhotoLife.Services.Tests.PostsServiceTests
{
    [TestFixture]
    public class GetPostById_Should
    {
        [TestCase(1)]
        [TestCase(101)]
        public void _Call_PostRepository_GetPostById(int id)
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
            postService.GetPostById(id);

            //Assert
            mockedPostRepository.Verify(r => r.GetById(id), Times.Once);
        }

        [TestCase(1)]
        [TestCase(101)]
        public void _Return_Correctly(int id)
        {
            //Arrange
            var mockedPost = new Mock<Post>();

            var mockedPostRepository = new Mock<IRepository<Post>>();
            mockedPostRepository.Setup(r => r.GetById(It.IsAny<object>())).Returns(mockedPost.Object);

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
            var res = postService.GetPostById(id);

            //Assert
            Assert.AreSame(mockedPost.Object, res);
        }

        [TestCase(1)]
        [TestCase(101)]
        public void _Return_Correct_Instance(int id)
        {
            //Arrange
            var mockedPost = new Mock<Post>();

            var mockedPostRepository = new Mock<IRepository<Post>>();
            mockedPostRepository.Setup(r => r.GetById(It.IsAny<object>())).Returns(mockedPost.Object);

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
            var res = postService.GetPostById(id);

            //Assert
            Assert.IsInstanceOf<Post>(res);
        }
    }
}
