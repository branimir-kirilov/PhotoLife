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

        public IEnumerable<Post> GetTopPosts(int topCount)
        {
            var res = this.postsRepository.GetAll.OrderBy(u => u.Votes).Take(topCount);

            return res;
        }

        public IEnumerable<Post> GetTopByComments(int topCount)
        {
            var res = this.postsRepository.GetAll.OrderBy(u => u.Comments.Count).Take(topCount);

            return res;
        }

        public IEnumerable<Post> GetAllPosts()
        {
            var res = this.postsRepository.GetAll.ToList();

            return res;
        }

        public void EditPost(object id, string title, string description, Category category)
        {
            var post = this.postsRepository.GetById(id);

            if (post != null)
            {
                post.Title = title;
                post.Description = description;
                post.Category = category;

                this.postsRepository.Update(post);
                this.unitOfWork.Commit();
            }
        }
    }
}
