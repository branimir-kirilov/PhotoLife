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

namespace PhotoLife.Services.Tests.NewsServiceTests
{
    [TestFixture]
    public class Get_All_Should
    {
        [Test]
        public void _Call_NewsRepository_GetAll()
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
            newsService.GetAll();

            //Assert
            mockedNewsRepository.Verify(r => r.GetAll, Times.Once);
        }

        [Test]
        public void _Return_Correctly()
        {
            //Arrange
            var mockedNews = new List<News>()
            {
                new Mock<News>() { }.Object,
                new Mock<News>() { }.Object
            }.AsQueryable();

            var mockedNewsRepository = new Mock<IRepository<News>>();
            mockedNewsRepository.Setup(r => r.GetAll).Returns(mockedNews);

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
            var res = newsService.GetAll();

            //Assert
            Assert.AreEqual(mockedNews, res);
        }

    }
}
