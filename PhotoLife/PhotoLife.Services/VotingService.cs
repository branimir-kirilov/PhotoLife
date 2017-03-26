using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoLife.Data.Contracts;
using PhotoLife.Factories;
using PhotoLife.Models;
using PhotoLife.Services.Contracts;

namespace PhotoLife.Services
{
    public class VotingService : IVotingService
    {
        private readonly IRepository<Vote> voteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IVoteFactory voteFactory;
        private readonly IPostService postService;

        public VotingService(IRepository<Vote> voteRepository, IUnitOfWork unitOfWork, IVoteFactory voteFactory, IPostService postService)
        {
            if (voteRepository == null)
            {
                throw new ArgumentNullException(nameof(voteRepository));
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            if (postService == null)
            {
                throw new ArgumentNullException(nameof(postService));
            }

            if (voteFactory == null)
            {
                throw new ArgumentNullException(nameof(voteFactory));
            }

            this.voteRepository = voteRepository;
            this.unitOfWork = unitOfWork;
            this.postService = postService;
            this.voteFactory = voteFactory;
        }

        public int Vote(int postId, string userId)
        {
            var userVoteOnLog = this.voteRepository
                .GetAll
                .FirstOrDefault(v => v.PostId.Equals(postId) && v.UserId.Equals(userId));

            var notVoted = (userVoteOnLog == null);

            if (notVoted)
            {
                var log = this.postService.GetPostById(postId);

                if (log != null)
                {
                    var vote = this.voteFactory.CreateVote(postId, userId);

                    this.voteRepository.Add(vote);
                    this.unitOfWork.Commit();

                    return log.Votes.Count;
                }
            }

            return -1;
        }
    }
}
