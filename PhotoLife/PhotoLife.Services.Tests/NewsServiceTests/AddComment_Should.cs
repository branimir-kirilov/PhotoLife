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
    public class AddComment_Should
    {
        [TestCase(7)]
        public void _Call_NewsRepository_GetById(int id)
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

            var comment = new Comment();
            
            //Act
            newsService.AddComment(id, comment);

            //Assert
            mockedNewsRepository.Verify(r => r.GetById(id), Times.Once);

        }

        [TestCase(7)]
        [TestCase(9)]
        public void _Call_UnitOfWorkCommit_GetById(int id)
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

            var comment = new Comment();

            //Act
            newsService.AddComment(id, comment);

            //Assert
            mockedUnitOfWork.Verify(u => u.Commit(), Times.Once);

        }
    }
}
