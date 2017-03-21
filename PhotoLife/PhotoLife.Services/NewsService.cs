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

        //public IEnumerable<News> GetTopPosts(int countOfPosts)
        //{
        //    var res =
        //        this.newsRepository.GetAll(
        //            (News news) => true,
        //            (News news) => news.Views, true)
        //            .Take(countOfPosts);

        //    return res;
        //}
    }
}
