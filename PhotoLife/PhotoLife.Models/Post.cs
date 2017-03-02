using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoLife.Models
{
    public class Post
    {
        public Post()
        {

        }

        public Post(
            string title,
            string description,
            string imageUrl,
            string author,
            Category category,
            DateTime datePublished,
            int votes)
        {
            this.Title = title;
            this.Description = description;
            this.ImageUrl = imageUrl;
            this.Author = author;
            this.Category = category;
            this.DatePublished = datePublished;
            this.Votes = votes;
        }

        [Key]
        public int PostId { get; set; }

        public int Votes { get; set; }

        public DateTime DatePublished { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public string Author { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }
    }
}
