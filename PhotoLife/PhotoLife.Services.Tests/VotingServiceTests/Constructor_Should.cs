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
using PhotoLife.Services.Contracts;

namespace PhotoLife.Services.Tests.VotingServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void _Initialize_NotNull_WhenEverythingPassed_Correctly()
        {
            //Arrange
            var mockedRepository = new Mock<IRepository<Vote>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedVoteFactory = new Mock<IVoteFactory>();
            var mockedPostService = new Mock<IPostService>();

            //Act
            var voteService = new VotingService(
                mockedRepository.Object,
                mockedUnitOfWork.Object,
                mockedVoteFactory.Object,
                mockedPostService.Object);

            //Assert
            Assert.IsNotNull(voteService);
        }

        [Test]
        public void _Throw_ArgumentNullException_WhenRepository_IsNull()
        {
            //Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedVoteFactory = new Mock<IVoteFactory>();
            var mockedPostService = new Mock<IPostService>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new VotingService(
                null,
                mockedUnitOfWork.Object,
                mockedVoteFactory.Object,
                mockedPostService.Object));
        }

        public void _Throw_ArgumentNullException_WhenUnitOfWork_IsNull()
        {
            //Arrange
            var mockedRepository = new Mock<IRepository<Vote>>();
            var mockedVoteFactory = new Mock<IVoteFactory>();
            var mockedPostService = new Mock<IPostService>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new VotingService(
                mockedRepository.Object,
                null,
                mockedVoteFactory.Object,
                mockedPostService.Object));
        }

        public void _Throw_ArgumentNullException_WhenVotesFactory_IsNull()
        {
            //Arrange
            var mockedRepository = new Mock<IRepository<Vote>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedPostService = new Mock<IPostService>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new VotingService(
                mockedRepository.Object,
                mockedUnitOfWork.Object,
                null,
                mockedPostService.Object));
        }

        public void _Throw_ArgumentNullException_WhenPostService_IsNull()
        {
            //Arrange
            var mockedRepository = new Mock<IRepository<Vote>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedVoteFactory = new Mock<IVoteFactory>();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => new VotingService(
                mockedRepository.Object,
                mockedUnitOfWork.Object,
                mockedVoteFactory.Object,
                null));
        }
    }
}
