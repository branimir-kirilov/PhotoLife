using Moq;
using NUnit.Framework;
using PhotoLife.Data.Contracts;
using PhotoLife.Data.Tests.GenericRepository.Tests.Mocks;

namespace PhotoLife.Data.Tests.GenericRepository.Tests
{
    [TestFixture]
    public class Update_Should
    {
        [Test]
        public void _Call_SetUpdated_Correctly()
        {
            //Arrange
            var mockedDbContext = new Mock<IPhotoLifeEntities>();

            var repository = new GenericRepository<MockedGenericRepositoryType>(mockedDbContext.Object);

            var entity = new Mock<MockedGenericRepositoryType>();

            //Act
            repository.Update(entity.Object);

            //Assert
            mockedDbContext.Verify(mdb => mdb.SetUpdated(entity.Object), Times.Once);
        }
    }
}
