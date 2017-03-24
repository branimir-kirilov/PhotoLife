using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using PhotoLife.Data.Contracts;
using PhotoLife.Factories;
using PhotoLife.Models;
using PhotoLife.Models.Enums;

namespace PhotoLife.Services.Tests.CategoryServiceTests
{
    [TestFixture]
    public class GetCategoryByName_Should
    {
        [TestCase(CategoryEnum.Abstract)]
        [TestCase(CategoryEnum.City)]
        public void _Call_CategoryRepsitory_GetAll(CategoryEnum categoryEnum)
        {
            //Arrange
            var mockedCategoryRepository = new Mock<IRepository<Category>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            
            var categoryService = new CategoryService(mockedCategoryRepository.Object, mockedUnitOfWork.Object,
                mockedCategoryFactory.Object);

            //Act
            categoryService.GetCategoryByName(categoryEnum);

            //Assert
            mockedCategoryRepository.Verify(r => r.GetAll, Times.Once);
        }

        [TestCase(CategoryEnum.Abstract)]
        [TestCase(CategoryEnum.City)]
        public void _Return_Correctly(CategoryEnum categoryEnum)
        {
            //Arrange
            var category = new Category();
            category.Name = categoryEnum;
            var categoryList = new List<Category>()
            {
                category,
            }.AsQueryable();
            
            var mockedCategoryRepository = new Mock<IRepository<Category>>();
            mockedCategoryRepository.Setup(r => r.GetAll).Returns(categoryList);

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedCategoryFactory = new Mock<ICategoryFactory>();

            var categoryService = new CategoryService(mockedCategoryRepository.Object, mockedUnitOfWork.Object,
                mockedCategoryFactory.Object);

            //Act
            var res = categoryService.GetCategoryByName(categoryEnum);

            //Assert
            Assert.AreSame(category, res);
        }
    }
}
