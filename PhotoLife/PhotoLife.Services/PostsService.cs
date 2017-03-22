using System;
using System.Collections.Generic;
using System.Linq;
using PhotoLife.Data.Contracts;
using PhotoLife.Factories;
using PhotoLife.Models;
using PhotoLife.Models.Enums;
using PhotoLife.Providers.Contracts;
using PhotoLife.Services.Contracts;

namespace PhotoLife.Services
{
    public class PostsService : IPostService
    {
        private readonly IRepository<Post> postsRepository;
        private readonly IUserService userService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPostFactory postFactory;
        private readonly ICategoryFactory categoryFactory;

        private readonly IDateTimeProvider datetimeProvider;

        public PostsService(
            IRepository<Post> postsRepository,
            IUserService userService,
            IUnitOfWork unitOfWork,
            IPostFactory postFactory,
            ICategoryFactory categoryFactory,
            IDateTimeProvider dateTimeProvider)
        {
            if (postsRepository == null)
            {
                throw new ArgumentNullException("postsRepository");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWorks");
            }

            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }

            if (postFactory == null)
            {
                throw new ArgumentNullException("postFactory");
            }

            if (categoryFactory == null)
            {
                throw new ArgumentNullException("categoryFactory");
            }

            if (dateTimeProvider == null)
            {
                throw new ArgumentNullException("datetimeProvider");
            }

            this.postsRepository = postsRepository;
            this.userService = userService;
            this.unitOfWork = unitOfWork;
            this.postFactory = postFactory;
            this.categoryFactory = categoryFactory;

            this.datetimeProvider = dateTimeProvider;
        }
        public Post GetPostById(string id)
        {
            return this.postsRepository.GetById(id);
        }
        
        public IEnumerable<Post> GetAll()
        {
            var res = this.postsRepository.GetAll.ToList();

            return res;
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

        public Post CreatePost(string userId, string title, string description, string profilePicUrl, CategoryEnum categoryEnum)
        {
            var user = this.userService.GetUserById(userId);

            var datePublished = this.datetimeProvider.GetCurrentDate();

            Category category = this.categoryFactory.CreateCategory(categoryEnum);

            var post = this.postFactory.CreatePost(title, description, profilePicUrl, user, category, datePublished);

            this.postsRepository.Add(post);
            this.unitOfWork.Commit();

            return post;
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
