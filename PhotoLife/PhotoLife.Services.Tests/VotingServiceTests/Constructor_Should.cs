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
    }
}
