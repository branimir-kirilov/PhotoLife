using System;
using System.Collections.Generic;
using System.Linq;
using PhotoLife.Data.Contracts;
using PhotoLife.Models;
using PhotoLife.Services.Contracts;

namespace PhotoLife.Services
{
    public class PostsService : IPostService
    {
        private readonly IRepository<Post> postsRepository;
        private readonly IUnitOfWork unitOfWork;

        public PostsService(
            IRepository<Post> postsRepository,
            IUnitOfWork unitOfWork)
        {
            if (postsRepository == null)
            {
                throw new ArgumentNullException("postsRepository");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWorks");
            }

            this.postsRepository = postsRepository;
            this.unitOfWork = unitOfWork;
        }
        public Post GetPostById(string id)
        {
            return this.postsRepository.GetById(id);
        }

        public IEnumerable<Post> GetTopPosts(int countOfPosts)
        {
            var res =
                this.postsRepository.GetAll(
                    (Post post) => true, 
                    (Post post) => post.Votes, true)
                    .Take(countOfPosts);

            return res;
        }

    }
}
