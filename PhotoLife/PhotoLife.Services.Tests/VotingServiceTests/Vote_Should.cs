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


        [TestCase(7, "userId1")]
        [TestCase(9, "userId123423#$")]
        public void _Call_PostService_GetById_IfVoteNotNull(int postId, string userId)
        {
            //Arrange
            var vote = new Vote();
            var votes = new List<Vote>()
            {
                vote
            }.AsQueryable();

            var mockedRepository = new Mock<IRepository<Vote>>();
            mockedRepository.Setup(r => r.GetAll).Returns(votes);

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
            mockedPostService.Verify(r => r.GetPostById(postId), Times.Once);
        }
        
        [TestCase(7, "userId1")]
        [TestCase(9, "userId123423#$")]
        public void _Call_VoteFactory_CreateVote_IfVoteNotNull(int postId, string userId)
        {
            //Arrange
            var vote = new Vote();
            var votes = new List<Vote>()
            {
                vote
            }.AsQueryable();

            var post = new Post();

            var mockedRepository = new Mock<IRepository<Vote>>();
            mockedRepository.Setup(r => r.GetAll).Returns(votes);

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedVoteFactory = new Mock<IVoteFactory>();

            var mockedPostService = new Mock<IPostService>();
            mockedPostService.Setup(p=> p.GetPostById(It.IsAny<int>())).Returns(post);

            var voteService = new VotingService(
                mockedRepository.Object,
                mockedUnitOfWork.Object,
                mockedVoteFactory.Object,
                mockedPostService.Object);

            //Act
            voteService.Vote(postId, userId);

            //Assert
            mockedVoteFactory.Verify(r => r.CreateVote(postId, userId), Times.Once);
        }

        [TestCase(7, "userId1")]
        [TestCase(9, "userId123423#$")]
        public void _Call_Repsitory_Add(int postId, string userId)
        {
            //Arrange
            var vote = new Vote();
            var votes = new List<Vote>()
            {
                vote
            }.AsQueryable();

            var post = new Post();

            var mockedRepository = new Mock<IRepository<Vote>>();
            mockedRepository.Setup(r => r.GetAll).Returns(votes);

            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var mockedVoteFactory = new Mock<IVoteFactory>();
            mockedVoteFactory.Setup(v => v.CreateVote(It.IsAny<int>(), It.IsAny<string>())).Returns(vote);

            var mockedPostService = new Mock<IPostService>();
            mockedPostService.Setup(p => p.GetPostById(It.IsAny<int>())).Returns(post);

            var voteService = new VotingService(
                mockedRepository.Object,
                mockedUnitOfWork.Object,
                mockedVoteFactory.Object,
                mockedPostService.Object);

            //Act
            voteService.Vote(postId, userId);

            //Assert
            mockedRepository.Verify(r => r.Add(vote), Times.Once);
        }

        [TestCase(7, "userId1")]
        [TestCase(9, "userId123423#$")]
        public void _Call_UnitOfWork_Commit(int postId, string userId)
        {
            //Arrange
            var vote = new Vote();
            var votes = new List<Vote>()
            {
                vote
            }.AsQueryable();

            var post = new Post();

            var mockedRepository = new Mock<IRepository<Vote>>();
            mockedRepository.Setup(r => r.GetAll).Returns(votes);

            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var mockedVoteFactory = new Mock<IVoteFactory>();
            mockedVoteFactory.Setup(v => v.CreateVote(It.IsAny<int>(), It.IsAny<string>())).Returns(vote);

            var mockedPostService = new Mock<IPostService>();
            mockedPostService.Setup(p => p.GetPostById(It.IsAny<int>())).Returns(post);

            var voteService = new VotingService(
                mockedRepository.Object,
                mockedUnitOfWork.Object,
                mockedVoteFactory.Object,
                mockedPostService.Object);

            //Act
            voteService.Vote(postId, userId);

            //Assert
            mockedUnitOfWork.Verify(r => r.Commit(), Times.Once);
        }
    }
}
