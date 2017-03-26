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
    public class AddComment_Should
    {
        [TestCase(7)]
        [TestCase(9)]
        public void _Call_PostRepository_GetById(int id)
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

            var comment = new Comment();

            //Act
            postService.AddComment(id, comment);

            //Assert
            mockedPostRepository.Verify(r => r.GetById(id), Times.Once);

        }

        [TestCase(7)]
        [TestCase(9)]

        public void _Call_UnitOfWork_Commit(int id)
        {
            //Arrange
            var post = new Post();

            var mockedPostRepository = new Mock<IRepository<Post>>();
            mockedPostRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(post);

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

            var comment = new Comment();

            //Act
            postService.AddComment(id, comment);

            //Assert
            mockedUnitOfWork.Verify(u => u.Commit(), Times.Once);

        }
    }
}
