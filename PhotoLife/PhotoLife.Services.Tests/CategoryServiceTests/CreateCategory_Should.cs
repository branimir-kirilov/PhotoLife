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

namespace PhotoLife.Services.Tests.CategoryServiceTests
{
    [TestFixture]
    public class CreateCategory_Should
    {
        [TestCase(CategoryEnum.Abstract)]
        [TestCase(CategoryEnum.BlackAndWhite)]
        public void _Call_CategoryFactory_CreateCategory(CategoryEnum categoryEnum)
        {
            //Arrange
            var mockedCategoryRepository = new Mock<IRepository<Category>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            
            var categoryService = new CategoryService(mockedCategoryRepository.Object, mockedUnitOfWork.Object, mockedCategoryFactory.Object);

            //Act
            categoryService.CreateCategory(categoryEnum);

            //Assert & Assert
            mockedCategoryFactory.Verify(f => f.CreateCategory(categoryEnum), Times.Once);
        }

        [TestCase(CategoryEnum.Abstract)]
        [TestCase(CategoryEnum.BlackAndWhite)]
        public void _Call_CategoryRepository_Add(CategoryEnum categoryEnum)
        {
            //Arrange
            var category = new Category();

            var mockedCategoryRepository = new Mock<IRepository<Category>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            mockedCategoryFactory.Setup(f => f.CreateCategory(It.IsAny<CategoryEnum>())).Returns(category);

            var categoryService = new CategoryService(mockedCategoryRepository.Object, mockedUnitOfWork.Object, mockedCategoryFactory.Object);

            //Act
            categoryService.CreateCategory(categoryEnum);

            //Assert & Assert
            mockedCategoryRepository.Verify(r => r.Add(category), Times.Once);
        }

        [TestCase(CategoryEnum.Abstract)]
        [TestCase(CategoryEnum.BlackAndWhite)]
        public void _Call_UnitOfWork_Commit(CategoryEnum categoryEnum)
        {
            //Arrange
            var mockedCategoryRepository = new Mock<IRepository<Category>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            var categoryService = new CategoryService(mockedCategoryRepository.Object, mockedUnitOfWork.Object, mockedCategoryFactory.Object);

            //Act
            categoryService.CreateCategory(categoryEnum);

            //Assert & Assert
            mockedUnitOfWork.Verify(u => u.Commit(), Times.Once);
        }

        [TestCase(CategoryEnum.Abstract)]
        [TestCase(CategoryEnum.BlackAndWhite)]
        public void _Return_Correctly(CategoryEnum categoryEnum)
        {
            //Arrange
            var category = new Category();

            var mockedCategoryRepository = new Mock<IRepository<Category>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            mockedCategoryFactory.Setup(f => f.CreateCategory(It.IsAny<CategoryEnum>())).Returns(category);

            var categoryService = new CategoryService(mockedCategoryRepository.Object, mockedUnitOfWork.Object, mockedCategoryFactory.Object);

            //Act
            var res =  categoryService.CreateCategory(categoryEnum);

            //Assert & Assert
            Assert.AreSame(category, res);
        }
    }
}
