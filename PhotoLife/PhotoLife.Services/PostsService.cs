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
        private readonly ICategoryService categoryService;

        private readonly IDateTimeProvider datetimeProvider;

        public PostsService(
            IRepository<Post> postsRepository,
            IUserService userService,
            IUnitOfWork unitOfWork,
            IPostFactory postFactory,
            ICategoryService categoryService,
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

            if (categoryService == null)
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
            this.categoryService = categoryService;

            this.datetimeProvider = dateTimeProvider;
        }
        public Post GetPostById(int id)
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
            var res = this.postsRepository.GetAll.OrderBy(u => u.Votes).Take(topCount).ToList();

            return res;
        }

        public IEnumerable<Post> GetTopByComments(int topCount)
        {
            var res = this.postsRepository.GetAll.OrderBy(u => u.Comments.Count).Take(topCount).ToList();

            return res;
        }

        public Post CreatePost(string userId, string title, string description, string profilePicUrl, CategoryEnum categoryEnum)
        {
            var user = this.userService.GetUserById(userId);

            var datePublished = this.datetimeProvider.GetCurrentDate();

            Category category = this.categoryService.GetCategoryByName(categoryEnum);

            var post = this.postFactory.CreatePost(title, description, profilePicUrl, user, category, datePublished);

            this.postsRepository.Add(post);
            this.unitOfWork.Commit();

            return post;
        }

        public void EditPost(int id, string title, string description, CategoryEnum categoryEnum)
        {
            var post = this.postsRepository.GetById(id);
            var category = this.categoryService.GetCategoryByName(categoryEnum);
            
            if (post != null && category != null)
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
