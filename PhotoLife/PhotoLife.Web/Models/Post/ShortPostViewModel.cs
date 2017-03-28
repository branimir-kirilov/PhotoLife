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
            this.VotesCount = post.Votes.Count;
            this.CommentsCount = post.Comments.Count;
            this.Author = post.Author.UserName;
        }

        public ShortPostViewModel()
        {
                
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DatePublished { get; set; }
        public int VotesCount { get; set; }
        public int CommentsCount { get; set; }
        public string Author { get; set; }
    }
}