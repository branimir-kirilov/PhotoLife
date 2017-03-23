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
    public class GetNewsById_Should
    {
        [TestCase(1)]
        [TestCase(101)]
        public void _Call_NewsRepository_GetNewsById(int id)
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
            newsService.GetNewsById(id);

            //Assert
            mockedNewsRepository.Verify(r => r.GetById(id), Times.Once);
        }

        [TestCase(1)]
        [TestCase(101)]
        public void _Return_Correctly(int id)
        {
            //Arrange
            var mockedNews = new Mock<News>();

            var mockedNewsRepository = new Mock<IRepository<News>>();
            mockedNewsRepository.Setup(r => r.GetById(It.IsAny<object>())).Returns(mockedNews.Object);

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
            var res = newsService.GetNewsById(id);

            //Assert
            Assert.AreSame(mockedNews.Object, res);
        }
    }
}
