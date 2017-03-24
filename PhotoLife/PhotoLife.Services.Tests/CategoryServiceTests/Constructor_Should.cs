using System;
using Moq;
using NUnit.Framework;
using PhotoLife.Data.Contracts;
using PhotoLife.Factories;
using PhotoLife.Models;

namespace PhotoLife.Services.Tests.CategoryServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void _Initialize_Correctly_WhenEverythingPassed_Valid()
        {
            //Arrange
            var mockedCategoryRepository = new Mock<IRepository<Category>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();

            //Act
            var categoryService = new CategoryService(mockedCategoryRepository.Object, mockedUnitOfWork.Object, mockedCategoryFactory.Object);

            //Assert & Assert
            Assert.IsNotNull(categoryService);
        }

        [Test]
        public void _Throw_ArgumentNullException_When_Repository_IsNull()
        {
            //Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CategoryService(null, mockedUnitOfWork.Object, mockedCategoryFactory.Object));
        }

        [Test]
        public void _Throw_ArgumentNullException_When_UnitOfWork_IsNull()
        {
            //Arrange
            var mockedCategoryRepository = new Mock<IRepository<Category>>();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CategoryService(mockedCategoryRepository.Object, null, mockedCategoryFactory.Object));
        }

        [Test]
        public void _Throw_ArgumentNullException_When_CategoryFactory_IsNull()
        {
            //Arrange
            var mockedCategoryRepository = new Mock<IRepository<Category>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CategoryService(mockedCategoryRepository.Object, mockedUnitOfWork.Object, null));
        }
    }
}
