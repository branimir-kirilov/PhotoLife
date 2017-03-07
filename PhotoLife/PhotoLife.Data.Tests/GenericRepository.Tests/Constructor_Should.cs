using System;
using NUnit.Framework;
using PhotoLife.Data.Tests.GenericRepository.Tests.Mocks;
using PhotoLife.Data;

namespace PhotoLife.Data.Tests.GenericRepository.Tests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void _Throw_ArgumentNullException_IfDbContext_Null()
        {
            //Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new GenericRepository<MockedGenericRepositoryType>(null));
        }
    }
}
