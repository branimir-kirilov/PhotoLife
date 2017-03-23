using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhotoLife.Models.Enums;

namespace PhotoLife.ViewModels.Post
{
    public class PostDetailsViewModel
    {
        public PostDetailsViewModel(Models.Post post)
        {
            this.Id = post.PostId;
            this.Title = post.Title;
            this.Description = post.Description;
            this.ImageUrl = post.ImageUrl;
            this.DatePublished = post.DatePublished;
            this.Votes = post.Votes;
            this.Category = post.Category.Name;
        }

        public PostDetailsViewModel()
        {

        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DatePublished { get; set; }
        public int Votes { get; set; }
        public CategoryEnum Category { get; set; }
    }
}