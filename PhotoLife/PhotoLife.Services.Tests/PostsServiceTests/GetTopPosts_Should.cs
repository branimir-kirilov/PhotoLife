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

namespace PhotoLife.Services.Tests.PostsServiceTests
{
    [TestFixture]
    public class GetTopPosts_Should
    {
        [TestCase(0)]
        [TestCase(15)]
        public void _Call_PostRepository_GetAll(int topCount)
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
            postService.GetTopPosts(topCount);

            //Assert
            mockedPostRepository.Verify(r => r.GetAll, Times.Once);
        }

        [TestCase(0)]
        [TestCase(15)]
        public void _Return_Correct_Collection(int topCount)
        {
            //Arrange
            var mockedPostCollection = new List<Post>()
            {
                new Post() { },
                new Post() { }
            }.AsQueryable();

            var expectedCollection = mockedPostCollection.OrderBy(m => m.Votes.Count).Take(topCount).ToList();

            var mockedPostRepository = new Mock<IRepository<Post>>();
            mockedPostRepository.Setup(m => m.GetAll).Returns(mockedPostCollection);

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
            var collectionRes = postService.GetTopPosts(topCount);

            //Assert
            CollectionAssert.AreEqual(expectedCollection, collectionRes);
        }
    }
}
