using System;
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
    public class CreatePost_Should
    {
        [TestCase("userid8", "Title", "text of the Post which is quite big actually fuck this", "www.cloudinary.net/somePic9", CategoryEnum.Celebrity)]
        [TestCase("userid7", "Title", "description", "www.cloudinary.net/somePic9", CategoryEnum.Celebrity)]
        public void _Call_UserService_GetById(
           string userId,
           string title,
           string text,
           string coverPicture,
           CategoryEnum categoryEnum)
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
            postService.CreatePost(userId, title, text, coverPicture, categoryEnum);

            //Assert
            mockedUserService.Verify(r => r.GetUserById(userId), Times.Once);

        }

        [TestCase("userid8", "Title", "text of the Post which is quite big actually fuck this", "www.cloudinary.net/somePic9", CategoryEnum.Celebrity)]
        [TestCase("userid7", "Title", "description", "www.cloudinary.net/somePic9", CategoryEnum.Celebrity)]
        public void _Call_DateTimeProvider_GetCurrentDate(
            string userId,
            string title,
            string text,
            string coverPicture,
            CategoryEnum categoryEnum)
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
            postService.CreatePost(userId, title, text, coverPicture, categoryEnum);

            //Assert
            mockedDateTimeProvider.Verify(r => r.GetCurrentDate(), Times.Once);
        }

        [TestCase("userid8", "Title", "text of the Post which is quite big actually fuck this", "www.cloudinary.net/somePic9", CategoryEnum.Celebrity)]
        [TestCase("userid7", "Title", "description", "www.cloudinary.net/somePic9", CategoryEnum.Celebrity)]
        public void _Call_CategoryService_GetCategoryByName(
            string userId,
            string title,
            string text,
            string coverPicture,
            CategoryEnum categoryEnum)
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
            postService.CreatePost(userId, title, text, coverPicture, categoryEnum);

            //Assert
            mockedCategoryService.Verify(c => c.GetCategoryByName(categoryEnum), Times.Once);
        }

        [TestCase("userid8", "Title", "text of the Post which is quite big actually fuck this", "www.cloudinary.net/somePic9", CategoryEnum.Celebrity)]
        [TestCase("userid7", "Title", "description", "www.cloudinary.net/somePic9", CategoryEnum.Celebrity)]
        public void _Call_PostFactory_CreatePost(
         string userId,
         string title,
         string text,
         string coverPicture,
         CategoryEnum categoryEnum)
        {
            //Arrange
            var user = new User();
            var date = new DateTime();
            var category = new Category();

            var mockedPostRepository = new Mock<IRepository<Post>>();

            var mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(m => m.GetUserById(It.IsAny<string>())).Returns(user);

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedPostFactory = new Mock<IPostFactory>();

            var mockedCategoryService = new Mock<ICategoryService>();
            mockedCategoryService.Setup(c => c.GetCategoryByName(categoryEnum)).Returns(category);

            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            mockedDateTimeProvider.Setup(d => d.GetCurrentDate()).Returns(date);

            var postService = new PostsService(
                  mockedPostRepository.Object,
                  mockedUserService.Object,
                  mockedUnitOfWork.Object,
                  mockedPostFactory.Object,
                  mockedCategoryService.Object,
                  mockedDateTimeProvider.Object);

            //Act
            postService.CreatePost(userId, title, text, coverPicture, categoryEnum);

            //Assert
            mockedPostFactory.Verify(c => c.CreatePost(title, text, coverPicture, user, category, date), Times.Once);
        }

        [TestCase("userid8", "Title", "text of the Post which is quite big actually fuck this", "www.cloudinary.net/somePic9", CategoryEnum.Celebrity)]
        [TestCase("userid7", "Title", "description", "www.cloudinary.net/somePic9", CategoryEnum.Celebrity)]
        public void _Call_PostRepsitory_Add(
         string userId,
         string title,
         string text,
         string coverPicture,
         CategoryEnum categoryEnum)
        {
            //Arrange
            var user = new User();
            var date = new DateTime();
            var category = new Category();

            var mockedPostRepository = new Mock<IRepository<Post>>();

            var mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(m => m.GetUserById(It.IsAny<string>())).Returns(user);

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedPostFactory = new Mock<IPostFactory>();

            var mockedCategoryService = new Mock<ICategoryService>();
            mockedCategoryService.Setup(c => c.GetCategoryByName(categoryEnum)).Returns(category);

            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            mockedDateTimeProvider.Setup(d => d.GetCurrentDate()).Returns(date);

            var postService = new PostsService(
                  mockedPostRepository.Object,
                  mockedUserService.Object,
                  mockedUnitOfWork.Object,
                  mockedPostFactory.Object,
                  mockedCategoryService.Object,
                  mockedDateTimeProvider.Object);

            //Act
            var Post = postService.CreatePost(userId, title, text, coverPicture, categoryEnum);

            //Assert
            mockedPostRepository.Verify(c => c.Add(Post), Times.Once);
        }

        [TestCase("userid8", "Title", "text of the Post which is quite big actually fuck this", "www.cloudinary.net/somePic9", CategoryEnum.Celebrity)]
        [TestCase("userid7", "Title", "description", "www.cloudinary.net/somePic9", CategoryEnum.Celebrity)]
        public void _Call_UnitOfWork_Commit(
        string userId,
        string title,
        string text,
        string coverPicture,
        CategoryEnum categoryEnum)
        {
            //Arrange
            var user = new User();
            var date = new DateTime();
            var category = new Category();

            var mockedPostRepository = new Mock<IRepository<Post>>();

            var mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(m => m.GetUserById(It.IsAny<string>())).Returns(user);

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedPostFactory = new Mock<IPostFactory>();

            var mockedCategoryService = new Mock<ICategoryService>();
            mockedCategoryService.Setup(c => c.GetCategoryByName(categoryEnum)).Returns(category);

            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            mockedDateTimeProvider.Setup(d => d.GetCurrentDate()).Returns(date);

            var postService = new PostsService(
                  mockedPostRepository.Object,
                  mockedUserService.Object,
                  mockedUnitOfWork.Object,
                  mockedPostFactory.Object,
                  mockedCategoryService.Object,
                  mockedDateTimeProvider.Object);

            //Act
            var Post = postService.CreatePost(userId, title, text, coverPicture, categoryEnum);

            //Assert
            mockedUnitOfWork.Verify(u => u.Commit(), Times.Once);
        }

        [TestCase("userid8", "Title", "text of the Post which is quite big actually fuck this", "www.cloudinary.net/somePic9", CategoryEnum.Celebrity)]
        [TestCase("userid7", "Title", "description", "www.cloudinary.net/somePic9", CategoryEnum.Celebrity)]
        public void _Call_ReturnCorrect_Post_Commit(
           string userId,
           string title,
           string text,
           string coverPicture,
           CategoryEnum categoryEnum)
        {
            //Arrange
            var user = new User();
            var date = new DateTime();
            var category = new Category();
            var Post = new Post();

            var mockedPostRepository = new Mock<IRepository<Post>>();

            var mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(m => m.GetUserById(It.IsAny<string>())).Returns(user);

            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var mockedPostFactory = new Mock<IPostFactory>();
            mockedPostFactory.Setup(f => f.CreatePost(title, text, coverPicture, user, category, date)).Returns(Post);

            var mockedCategoryService = new Mock<ICategoryService>();
            mockedCategoryService.Setup(c => c.GetCategoryByName(categoryEnum)).Returns(category);

            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            mockedDateTimeProvider.Setup(d => d.GetCurrentDate()).Returns(date);

            var postService = new PostsService(
                  mockedPostRepository.Object,
                  mockedUserService.Object,
                  mockedUnitOfWork.Object,
                  mockedPostFactory.Object,
                  mockedCategoryService.Object,
                  mockedDateTimeProvider.Object);

            //Act
            var postRes = postService.CreatePost(userId, title, text, coverPicture, categoryEnum);

            //Assert
            Assert.AreSame(Post, postRes);
        }
    }
}
