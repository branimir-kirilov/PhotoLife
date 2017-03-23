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
    public class EditNews_Should
    {
        [TestCase(7,"title", "some text",  "some.url/somepic/2", CategoryEnum.Abstract)]
        [TestCase(7, "title", "<h1>some text</h1>", "some.url/somepic/2", CategoryEnum.Celebrity)]

        public void _Call_NewsRepository_GetById(int id, string title, string text, string imageUrl, CategoryEnum categoryEnum)
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
            newsService.EditNews(id, title, text, imageUrl, categoryEnum);

            //Assert
            mockedNewsRepository.Verify(r => r.GetById(id), Times.Once);
        }

        [TestCase(7, "title", "some text", "some.url/somepic/2", CategoryEnum.Abstract)]
        [TestCase(7, "title", "<h1>some text</h1>", "some.url/somepic/2", CategoryEnum.Celebrity)]

        public void _Call_CategporyService_GetCategoryByName(int id, string title, string text, string imageUrl, CategoryEnum categoryEnum)
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
            newsService.EditNews(id, title, text, imageUrl, categoryEnum);

            //Assert
            mockedCategoryService.Verify(r => r.GetCategoryByName(categoryEnum), Times.Once);
        }

        [TestCase(7, "title", "some text", "some.url/somepic/2", CategoryEnum.Abstract)]
        [TestCase(7, "title", "<h1>some text</h1>", "some.url/somepic/2", CategoryEnum.Celebrity)]

        public void _Call_NewsRepository_Update(int id, string title, string text, string imageUrl, CategoryEnum categoryEnum)
        {
            //Arrange
            var news = new News();

            var mockedNewsRepository = new Mock<IRepository<News>>();
            mockedNewsRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(news);

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
            newsService.EditNews(id, title, text, imageUrl, categoryEnum);

            //Assert
            mockedNewsRepository.Verify(r => r.Update(news), Times.Once);
        }

        [TestCase(7, "title", "some text", "some.url/somepic/2", CategoryEnum.Abstract)]
        [TestCase(7, "title", "<h1>some text</h1>", "some.url/somepic/2", CategoryEnum.Celebrity)]

        public void _Call_UnitOfWork_Commit(int id, string title, string text, string imageUrl, CategoryEnum categoryEnum)
        {
            //Arrange
            var news = new News();

            var mockedNewsRepository = new Mock<IRepository<News>>();
            mockedNewsRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(news);

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
            newsService.EditNews(id, title, text, imageUrl, categoryEnum);

            //Assert
            mockedUnitOfWork.Verify(r => r.Commit(), Times.Once);
        }
    }
}
