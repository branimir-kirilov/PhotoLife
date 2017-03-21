using System;
using System.Collections.Generic;
using System.Linq;
using PhotoLife.Data.Contracts;
using PhotoLife.Models;
using PhotoLife.Services.Contracts;

namespace PhotoLife.Services
{
    public class NewsService : INewsService
    {
        private readonly IRepository<News> newsRepository;
        private readonly IUnitOfWork unitOfWork;

        public NewsService(
            IRepository<News> newsRepository,
            IUnitOfWork unitOfWork)
        {
            if (newsRepository == null)
            {
                throw new ArgumentNullException("newsRepository");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWorks");
            }

            this.newsRepository = newsRepository;
            this.unitOfWork = unitOfWork;
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

        

        public void EditPost(object id, string title, string text, string imageUrl, Category category)
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
