using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoLife.ViewModels.News
{
    public class ShortNewsViewModel
    {
        public ShortNewsViewModel(Models.News news)
        {
            this.Id = news.NewsId;
            this.Title = news.Title;
            this.ImageUrl = news.ImageUrl;
            this.DatePublished = news.DatePublished;
            this.Views = news.Views;
        }
        public ShortNewsViewModel()
        {
            
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DatePublished { get; set; }
        public int Views { get; set; }
    }
}