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
    public class Vote_Should
    {
        [TestCase(7, "userId1")]
        [TestCase(9, "userId123423#$")]
        public void _Call_Repository_GetAll(int postId, string userId)
        {
            //Arrange
            var mockedRepository = new Mock<IRepository<Vote>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedVoteFactory = new Mock<IVoteFactory>();
            var mockedPostService = new Mock<IPostService>();
            
            var voteService = new VotingService(
                mockedRepository.Object,
                mockedUnitOfWork.Object,
                mockedVoteFactory.Object,
                mockedPostService.Object);

            //Act
            voteService.Vote(postId, userId);

            //Assert
            mockedRepository.Verify(r => r.GetAll, Times.Once);
        }
    }
}
