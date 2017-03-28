using System;
using System.Collections.Generic;
using PagedList;
using PhotoLife.Models;
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
            this.Comments = post.Comments;
        }

        public PostDetailsViewModel()
        {

        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DatePublished { get; set; }
        public IEnumerable<Vote> Votes { get; set; }
        public IEnumerable<Models.Comment> Comments { get; set; }
        public CategoryEnum Category { get; set; }
    }
}