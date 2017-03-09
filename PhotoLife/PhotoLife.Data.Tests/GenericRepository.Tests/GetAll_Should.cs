using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

    }
}
