using System.Collections.Generic;
using System.Linq;
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
    public class GetTopNews_Should
    {
        [TestCase(0)]
        [TestCase(15)]
        public void _Call_NewsRepository_GetAll(int topCount)
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
            newsService.GetTopNews(topCount);

            //Assert
            mockedNewsRepository.Verify(r => r.GetAll, Times.Once);
        }

        [TestCase(0)]
        [TestCase(15)]
        public void _Return_Correct_Collection(int topCount)
        {
            //Arrange
            var mockedNewsCollection = new List<News>()
            {
                new Mock<News>() { }.Object,
                new Mock<News>() { }.Object
            }.AsQueryable();

            var expectedCollection = mockedNewsCollection.OrderBy(m => m.Views).Take(topCount).ToList();

            var mockedNewsRepository = new Mock<IRepository<News>>();
            mockedNewsRepository.Setup(m => m.GetAll).Returns(mockedNewsCollection);

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
            var collectionRes = newsService.GetTopNews(topCount);

            //Assert
            CollectionAssert.AreEqual(expectedCollection, collectionRes);
        }
    }
}
