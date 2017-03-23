using Moq;
using NUnit.Framework;
using PhotoLife.Data.Contracts;
using PhotoLife.Factories;
using PhotoLife.Models;
using PhotoLife.Models.Enums;
using PhotoLife.Providers.Contracts;
using PhotoLife.Services.Contracts;

namespace PhotoLife.Services.Tests.PostsServiceTests
{
    [TestFixture]
    public class EditPost_Should
    {
        [TestCase(7, "title", "some description", CategoryEnum.Abstract)]
        [TestCase(7, "title", "<h1>some description</h1>", CategoryEnum.Celebrity)]

        public void _Call_PostRepository_GetById(int id, string title, string description, CategoryEnum categoryEnum)
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
            postService.EditPost(id, title, description, categoryEnum);

            //Assert
            mockedPostRepository.Verify(r => r.GetById(id), Times.Once);
        }

        [TestCase(7, "title", "some description",  CategoryEnum.Abstract)]
        [TestCase(7, "title", "<h1>some description</h1>",  CategoryEnum.Celebrity)]

        public void _Call_CategporyService_GetCategoryByName(int id, string title, string description, CategoryEnum categoryEnum)
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
            postService.EditPost(id, title, description, categoryEnum);

            //Assert
            mockedCategoryService.Verify(r => r.GetCategoryByName(categoryEnum), Times.Once);
        }

        [TestCase(7, "title", "some description", CategoryEnum.Abstract)]
        [TestCase(7, "title", "<h1>some description</h1>",  CategoryEnum.Celebrity)]

        public void _Call_PostRepository_Update(int id, string title, string description, CategoryEnum categoryEnum)
        {
            //Arrange
            var Post = new Post();
            var category = new Category();
            var mockedPostRepository = new Mock<IRepository<Post>>();
            mockedPostRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(Post);

            var mockedUserService = new Mock<IUserService>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedPostFactory = new Mock<IPostFactory>();

            var mockedCategoryService = new Mock<ICategoryService>();
            mockedCategoryService.Setup(m => m.GetCategoryByName(It.IsAny<CategoryEnum>())).Returns(category);

            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var postService = new PostsService(
                  mockedPostRepository.Object,
                  mockedUserService.Object,
                  mockedUnitOfWork.Object,
                  mockedPostFactory.Object,
                  mockedCategoryService.Object,
                  mockedDateTimeProvider.Object);

            //Act
            postService.EditPost(id, title, description, categoryEnum);

            //Assert
            mockedPostRepository.Verify(r => r.Update(Post), Times.Once);
        }

        [TestCase(7, "title", "some description", CategoryEnum.Abstract)]
        [TestCase(7, "title", "<h1>some description</h1>", CategoryEnum.Celebrity)]
        public void _Call_UnitOfWork_Commit(int id, string title, string description, CategoryEnum categoryEnum)
        {
            //Arrange
            var Post = new Post();
            var category = new Category();

            var mockedPostRepository = new Mock<IRepository<Post>>();
            mockedPostRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(Post);

            var mockedUserService = new Mock<IUserService>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedPostFactory = new Mock<IPostFactory>();

            var mockedCategoryService = new Mock<ICategoryService>();
            mockedCategoryService.Setup(m => m.GetCategoryByName(It.IsAny<CategoryEnum>())).Returns(category);

            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var postService = new PostsService(
                  mockedPostRepository.Object,
                  mockedUserService.Object,
                  mockedUnitOfWork.Object,
                  mockedPostFactory.Object,
                  mockedCategoryService.Object,
                  mockedDateTimeProvider.Object);

            //Act
            postService.EditPost(id, title, description, categoryEnum);

            //Assert
            mockedUnitOfWork.Verify(r => r.Commit(), Times.Once);
        }

        [TestCase(7, "title", "some description", CategoryEnum.Abstract)]
        [TestCase(7, "title", "<h1>some description</h1>",  CategoryEnum.Celebrity)]
        public void _Should_Set_Title_Correctly(int id, string title, string description, CategoryEnum categoryEnum)
        {
            //Arrange
            var Post = new Post();
            var category = new Category();

            var mockedPostRepository = new Mock<IRepository<Post>>();
            mockedPostRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(Post);

            var mockedUserService = new Mock<IUserService>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedPostFactory = new Mock<IPostFactory>();

            var mockedCategoryService = new Mock<ICategoryService>();
            mockedCategoryService.Setup(m => m.GetCategoryByName(It.IsAny<CategoryEnum>())).Returns(category);

            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var postService = new PostsService(
                  mockedPostRepository.Object,
                  mockedUserService.Object,
                  mockedUnitOfWork.Object,
                  mockedPostFactory.Object,
                  mockedCategoryService.Object,
                  mockedDateTimeProvider.Object);

            //Act
            postService.EditPost(id, title, description, categoryEnum);

            //Assert
            Assert.AreEqual(Post.Title, title);
        }

        [TestCase(7, "title", "some description", CategoryEnum.Abstract)]
        [TestCase(7, "title", "<h1>some description</h1>", CategoryEnum.Celebrity)]
        public void _Should_Set_description_Correctly(int id, string title, string description, CategoryEnum categoryEnum)
        {
            //Arrange
            var Post = new Post();
            var category = new Category();

            var mockedPostRepository = new Mock<IRepository<Post>>();
            mockedPostRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(Post);

            var mockedUserService = new Mock<IUserService>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedPostFactory = new Mock<IPostFactory>();

            var mockedCategoryService = new Mock<ICategoryService>();
            mockedCategoryService.Setup(m => m.GetCategoryByName(It.IsAny<CategoryEnum>())).Returns(category);

            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var postService = new PostsService(
                  mockedPostRepository.Object,
                  mockedUserService.Object,
                  mockedUnitOfWork.Object,
                  mockedPostFactory.Object,
                  mockedCategoryService.Object,
                  mockedDateTimeProvider.Object);

            //Act
            postService.EditPost(id, title, description, categoryEnum);

            //Assert
            Assert.AreEqual(Post.Description, description);
        }

        [TestCase(7, "title", "some description", CategoryEnum.Abstract)]
        [TestCase(7, "title", "<h1>some description</h1>", CategoryEnum.Celebrity)]
        public void _Should_Set_Category_Correctly(int id, string title, string description, CategoryEnum categoryEnum)
        {
            //Arrange
            var Post = new Post();
            var category = new Category();

            var mockedPostRepository = new Mock<IRepository<Post>>();
            mockedPostRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(Post);

            var mockedUserService = new Mock<IUserService>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedPostFactory = new Mock<IPostFactory>();

            var mockedCategoryService = new Mock<ICategoryService>();
            mockedCategoryService.Setup(m => m.GetCategoryByName(It.IsAny<CategoryEnum>())).Returns(category);

            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var postService = new PostsService(
                  mockedPostRepository.Object,
                  mockedUserService.Object,
                  mockedUnitOfWork.Object,
                  mockedPostFactory.Object,
                  mockedCategoryService.Object,
                  mockedDateTimeProvider.Object);

            //Act
            postService.EditPost(id, title, description, categoryEnum);

            //Assert
            Assert.AreSame(Post.Category, category);
        }
    }
}
