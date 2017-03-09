using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using NUnit.Framework;
using PhotoLife.Data.Contracts;
using PhotoLife.Data.Tests.GenericRepository.Tests.Mocks;

namespace PhotoLife.Data.Tests.GenericRepository.Tests
{
    [TestFixture]
    public class GetAll_Should
    {
        private IQueryable<MockedGenericRepositoryType> InitializeFakeData()
        {
            var data = new List<MockedGenericRepositoryType>
            {
                new MockedGenericRepositoryType(),
                new MockedGenericRepositoryType(),
                new MockedGenericRepositoryType()
            }.AsQueryable();

            return data;
        }

        private Mock<IDbSet<MockedGenericRepositoryType>> initializeFakeSet(IQueryable<MockedGenericRepositoryType> data)
        {
            var mockedSet = new Mock<IDbSet<MockedGenericRepositoryType>>();
            mockedSet.Setup(s => s.Provider).Returns(data.Provider);
            mockedSet.Setup(s => s.ElementType).Returns(data.ElementType);
            mockedSet.Setup(s => s.Expression).Returns(data.Expression);
            mockedSet.Setup(s => s.GetEnumerator()).Returns(data.GetEnumerator());

            return mockedSet;
        }

        [Test]
        public void _Call_DbSet_Correctly()
        {
            //Arrange
            var data = this.InitializeFakeData();
            var mockedSet = this.initializeFakeSet(data);

            var mockedDbContext = new Mock<IPhotoLifeEntities>();

            mockedDbContext.Setup(x => x.DbSet<MockedGenericRepositoryType>()).Returns(mockedSet.Object);

            var repository = new GenericRepository<MockedGenericRepositoryType>(mockedDbContext.Object);

            //Act
            repository.GetAll();

            //Assert
            mockedDbContext.Verify(mdb => mdb.DbSet<MockedGenericRepositoryType>(), Times.Once);
        }

        [Test]
        public void _Return_Correctly_WithNoExpression()
        {
            //Arrange
            var data = this.InitializeFakeData();
            var mockedSet = this.initializeFakeSet(data);

            var mockedDbContext = new Mock<IPhotoLifeEntities>();
            mockedDbContext.Setup(x => x.DbSet<MockedGenericRepositoryType>()).Returns(mockedSet.Object);

            var repository = new GenericRepository<MockedGenericRepositoryType>(mockedDbContext.Object);

            //Act
            var result = repository.GetAll();

            //Assert
            CollectionAssert.AreEqual(data, result);
        }

        [TestCase(true, true)]
        [TestCase(false, false)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        public void _Return_Correctly_WithSortingExpressino(bool first, bool second)
        {
            //Arrange
            var data = new List<MockedGenericRepositoryType>
            {
               new MockedGenericRepositoryType {IsTrue = first},
               new MockedGenericRepositoryType {IsTrue = second}
            }.AsQueryable();

            var mockedSet = this.initializeFakeSet(data);

            var mockedDbContext = new Mock<IPhotoLifeEntities>();
            mockedDbContext.Setup(x => x.DbSet<MockedGenericRepositoryType>()).Returns(mockedSet.Object);

            var repository = new GenericRepository<MockedGenericRepositoryType>(mockedDbContext.Object);

            Expression<Func<MockedGenericRepositoryType, bool>> sortingExpression =
                (MockedGenericRepositoryType t) => t.IsTrue;

            var actual = data.Where(sortingExpression);

            //Act
            var result = repository.GetAll(sortingExpression);

            //Assert
            Assert.AreEqual(result, actual);
        }

        [TestCase(true, true)]
        [TestCase(false, false)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        public void _Return_Correctly_With_Sorting_Ordering_Expressino(bool first, bool second)
        {
            //Arrange
            var data = new List<MockedGenericRepositoryType>
            {
               new MockedGenericRepositoryType {IsTrue = first},
               new MockedGenericRepositoryType {IsTrue = second}
            }.AsQueryable();

            var mockedSet = this.initializeFakeSet(data);

            var mockedDbContext = new Mock<IPhotoLifeEntities>();
            mockedDbContext.Setup(x => x.DbSet<MockedGenericRepositoryType>()).Returns(mockedSet.Object);

            var repository = new GenericRepository<MockedGenericRepositoryType>(mockedDbContext.Object);

            Expression<Func<MockedGenericRepositoryType, bool>> sortingExpression =
                (MockedGenericRepositoryType t) => t.IsTrue;

            Expression<Func<MockedGenericRepositoryType, int>> orderExpression =
                (t) => t.GetHashCode();

            var actual = data
                .Where(sortingExpression)
                .OrderBy(orderExpression);

            //Act
            var result = repository.GetAll(sortingExpression, orderExpression, true);

            //Assert
            Assert.AreEqual(result, actual);
        }

        [TestCase(true, true)]
        [TestCase(false, false)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        public void _Return_Correctly_With_Sorting_Ordering_Select_Expressino(bool first, bool second)
        {
            //Arrange
            var data = new List<MockedGenericRepositoryType>
            {
               new MockedGenericRepositoryType {IsTrue = first},
               new MockedGenericRepositoryType {IsTrue = second}
            }.AsQueryable();

            var mockedSet = this.initializeFakeSet(data);

            var mockedDbContext = new Mock<IPhotoLifeEntities>();
            mockedDbContext.Setup(x => x.DbSet<MockedGenericRepositoryType>()).Returns(mockedSet.Object);

            var repository = new GenericRepository<MockedGenericRepositoryType>(mockedDbContext.Object);

            Expression<Func<MockedGenericRepositoryType, bool>> sortingExpression =
                (MockedGenericRepositoryType t) => t.IsTrue;

            Expression<Func<MockedGenericRepositoryType, int>> orderExpression =
                (t) => t.GetHashCode();

            Expression<Func<MockedGenericRepositoryType, bool>> selectExpression =
                (t) => t.IsTrue;

            var actual = data
                .Where(sortingExpression)
                .OrderBy(orderExpression)
                .Select(selectExpression);

            //Act
            var result = repository.GetAll(sortingExpression, orderExpression, selectExpression);

            //Assert
            Assert.AreEqual(result, actual);
        }
    }
}
