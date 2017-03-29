using System;
using System.Collections.Generic;
using PhotoLife.Models.Enums;

namespace PhotoLife.ViewModels.News
{
    public class NewsDetailsViewModel
    {
        public NewsDetailsViewModel(Models.News news)
        {
            this.Id = news.NewsId;
            this.Title = news.Title;
            this.Text = news.Text;
            this.ImageUrl = news.ImageUrl;
            this.DatePublished = news.DatePublished;
            this.Views = news.Views;
            this.Category = news.Category.Name;
            this.Author = news.Author.UserName;
            this.Comments = news.Comments;
        }

        public NewsDetailsViewModel()
        {
            
        }


        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DatePublished { get; set; }
        public int Views { get; set; }
        public CategoryEnum Category { get; set; }
        public string Author { get; set; }
        public IEnumerable<Models.Comment> Comments { get; set; }
    }
}