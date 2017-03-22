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
    public class NewsService : INewsService
    {
        private readonly IRepository<News> newsRepository;
        private readonly IUserService userService;
        private readonly IUnitOfWork unitOfWork;
        private readonly INewsFactory newsFactory;
        private readonly ICategoryFactory categoryFactory;

        private readonly IDateTimeProvider dateTimeProvider;

        public NewsService(
            IRepository<News> newsRepository,
            IUserService userService,
            IUnitOfWork unitOfWork, 
            INewsFactory newsFactory,
            ICategoryFactory categoryFactory,
            IDateTimeProvider dateTimeProvider)
        {
            if (newsRepository == null)
            {
                throw new ArgumentNullException("newsRepository");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWorks");
            }

            if (newsFactory == null)
            {
                throw new ArgumentNullException("newsFactory");
            }

            if (categoryFactory == null)
            {
                throw new ArgumentNullException("categoryFactory");
            }

            if (dateTimeProvider == null)
            {
                throw new ArgumentNullException("dateTimeProvider");
            }

            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }

            this.newsRepository = newsRepository;
            this.unitOfWork = unitOfWork;
            this.userService = userService;
            this.newsFactory = newsFactory;
            this.categoryFactory = categoryFactory;
            this.dateTimeProvider = dateTimeProvider;
        }

        public News GetNewsById(string id)
        {
            return this.newsRepository.GetById(id);
        }

        public IEnumerable<News> GetAll()
        {
            var res = this.newsRepository.GetAll.ToList();

            return res;
        }

        public IEnumerable<News> GetTopNews(int topCount)
        {
            var res = this.newsRepository.GetAll.OrderBy(u => u.Views).Take(topCount);

            return res;
        }

        public IEnumerable<News> GetTopByComments(int topCount)
        {
            var res = this.newsRepository.GetAll.OrderBy(u => u.Comments.Count).Take(topCount);

            return res;
        }

        public News CreateNews(string userId, string title, string text, string coverPicture, CategoryEnum categoryEnum)
        {
            var user = this.userService.GetUserById(userId);

            var datePublished = this.dateTimeProvider.GetCurrentDate();

            Category category = this.categoryFactory.CreateCategory(categoryEnum);

            var news = this.newsFactory.CreateNews(title, text, coverPicture, user, category, datePublished);

            this.newsRepository.Add(news);
            this.unitOfWork.Commit();

            return news;
        }


        public void EditNews(object id, string title, string text, string imageUrl, Category category)
        {
            var news = this.newsRepository.GetById(id);

            if (news != null)
            {
                news.Title = title;
                news.Text = text;
                news.Category = category;
                news.ImageUrl = imageUrl;
                
                this.newsRepository.Update(news);
                this.unitOfWork.Commit();
            }
        }
    }
}
