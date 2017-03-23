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
using PhotoLife.Models.Enums;
using PhotoLife.Providers.Contracts;
using PhotoLife.Services.Contracts;

namespace PhotoLife.Services.Tests.NewsServiceTests
{
    [TestFixture]
    public class CreateNews_Should
    {
        [TestCase("userid8", "Title", "text of the news which is quite big actually fuck this", "www.cloudinary.net/somePic9", CategoryEnum.Celebrity)]
        [TestCase("userid7", "Title", "<h1>text</h1>", "www.cloudinary.net/somePic9", CategoryEnum.Celebrity)]
        public void _Call_UserService_GetById(
            string userId, 
            string title, 
            string text,
            string coverPicture,
            CategoryEnum categoryEnum)
        {
            //Arrange
            var mockedNewsRepository = new Mock<IRepository<News>>();
            var mockedUserService = new Mock<IUserService>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedNewsFactory = new Mock<INewsFactory>();
            var mockedCategoryService = new Mock<ICategoryService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var newsService = new NewsService(
                  mockedNewsRepository.Object,
                  mockedUserService.Object,
                  mockedUnitOfWork.Object,
                  mockedNewsFactory.Object,
                  mockedCategoryService.Object,
                  mockedDateTimeProvider.Object);

            //Act
            newsService.CreateNews(userId, title, text, coverPicture, categoryEnum);

            //Assert
            mockedUserService.Verify(r => r.GetUserById(userId), Times.Once);

        }

        [TestCase("userid8", "Title", "text of the news which is quite big actually fuck this", "www.cloudinary.net/somePic9", CategoryEnum.Celebrity)]
        [TestCase("userid7", "Title", "<h1>text</h1>", "www.cloudinary.net/somePic9", CategoryEnum.Celebrity)]
        public void _Call_DateTimeProvider_GetCurrentDate(
            string userId,
            string title,
            string text,
            string coverPicture,
            CategoryEnum categoryEnum)
        {
            //Arrange
            var mockedNewsRepository = new Mock<IRepository<News>>();
            var mockedUserService = new Mock<IUserService>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedNewsFactory = new Mock<INewsFactory>();
            var mockedCategoryService = new Mock<ICategoryService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var newsService = new NewsService(
                  mockedNewsRepository.Object,
                  mockedUserService.Object,
                  mockedUnitOfWork.Object,
                  mockedNewsFactory.Object,
                  mockedCategoryService.Object,
                  mockedDateTimeProvider.Object);

            //Act
            newsService.CreateNews(userId, title, text, coverPicture, categoryEnum);

            //Assert
            mockedDateTimeProvider.Verify(r => r.GetCurrentDate(), Times.Once);
        }

        [TestCase("userid8", "Title", "text of the news which is quite big actually fuck this", "www.cloudinary.net/somePic9", CategoryEnum.Celebrity)]
        [TestCase("userid7", "Title", "<h1>text</h1>", "www.cloudinary.net/somePic9", CategoryEnum.Celebrity)]
        public void _Call_CategoryService_GetCategoryByName(
            string userId,
            string title,
            string text,
            string coverPicture,
            CategoryEnum categoryEnum)
        {
            //Arrange
            var mockedNewsRepository = new Mock<IRepository<News>>();
            var mockedUserService = new Mock<IUserService>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedNewsFactory = new Mock<INewsFactory>();
            var mockedCategoryService = new Mock<ICategoryService>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var newsService = new NewsService(
                  mockedNewsRepository.Object,
                  mockedUserService.Object,
                  mockedUnitOfWork.Object,
                  mockedNewsFactory.Object,
                  mockedCategoryService.Object,
                  mockedDateTimeProvider.Object);

            //Act
            newsService.CreateNews(userId, title, text, coverPicture, categoryEnum);

            //Assert
            mockedCategoryService.Verify(c => c.GetCategoryByName(categoryEnum), Times.Once);
        }

        [TestCase("userid8", "Title", "text of the news which is quite big actually fuck this", "www.cloudinary.net/somePic9", CategoryEnum.Celebrity)]
        [TestCase("userid7", "Title", "<h1>text</h1>", "www.cloudinary.net/somePic9", CategoryEnum.Celebrity)]
        public void _Call_NewsFactory_CreateNews(
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

            var mockedNewsRepository = new Mock<IRepository<News>>();

            var mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(m => m.GetUserById(It.IsAny<string>())).Returns(user);

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedNewsFactory = new Mock<INewsFactory>();

            var mockedCategoryService = new Mock<ICategoryService>();
            mockedCategoryService.Setup(c => c.GetCategoryByName(categoryEnum)).Returns(category);

            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            mockedDateTimeProvider.Setup(d => d.GetCurrentDate()).Returns(date);

            var newsService = new NewsService(
                  mockedNewsRepository.Object,
                  mockedUserService.Object,
                  mockedUnitOfWork.Object,
                  mockedNewsFactory.Object,
                  mockedCategoryService.Object,
                  mockedDateTimeProvider.Object);

            //Act
            newsService.CreateNews(userId, title, text, coverPicture, categoryEnum);

            //Assert
            mockedNewsFactory.Verify(c => c.CreateNews(title, text ,coverPicture, user, category, date), Times.Once);
        }

        [TestCase("userid8", "Title", "text of the news which is quite big actually fuck this", "www.cloudinary.net/somePic9", CategoryEnum.Celebrity)]
        [TestCase("userid7", "Title", "<h1>text</h1>", "www.cloudinary.net/somePic9", CategoryEnum.Celebrity)]
        public void _Call_NewsRepsitory_Add(
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

            var mockedNewsRepository = new Mock<IRepository<News>>();

            var mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(m => m.GetUserById(It.IsAny<string>())).Returns(user);

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedNewsFactory = new Mock<INewsFactory>();

            var mockedCategoryService = new Mock<ICategoryService>();
            mockedCategoryService.Setup(c => c.GetCategoryByName(categoryEnum)).Returns(category);

            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            mockedDateTimeProvider.Setup(d => d.GetCurrentDate()).Returns(date);

            var newsService = new NewsService(
                  mockedNewsRepository.Object,
                  mockedUserService.Object,
                  mockedUnitOfWork.Object,
                  mockedNewsFactory.Object,
                  mockedCategoryService.Object,
                  mockedDateTimeProvider.Object);

            //Act
            var news = newsService.CreateNews(userId, title, text, coverPicture, categoryEnum);

            //Assert
            mockedNewsRepository.Verify(c => c.Add(news), Times.Once);
        }
    }
}
