using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhotoLife.Models;

namespace PhotoLife.ViewModels.Post
{
    public class ShortPostViewModel
    {
        public ShortPostViewModel(Models.Post post)
        {
            this.Id = post.PostId;
            this.Title = post.Title;
            this.ImageUrl = post.ImageUrl;
            this.DatePublished = post.DatePublished;
            this.Votes = post.Votes;
        }

        public ShortPostViewModel()
        {
                
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DatePublished { get; set; }
        public IEnumerable<Vote> Votes { get; set; }
    }
}