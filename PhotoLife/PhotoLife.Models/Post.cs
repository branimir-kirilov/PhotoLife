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
        private ICollection<Comment> comments;

        public Post()
        {
            this.comments = new HashSet<Comment>();
        }

        public Post(
            string title,
            string description,
            string imageUrl,
            User author,
            Category category,
            DateTime datePublished)
        {
            this.Title = title;
            this.Description = description;
            this.ImageUrl = imageUrl;
            this.Author = author;
            this.Category = category;
            this.DatePublished = datePublished;
            this.Votes = 0;
        }

        [Key]
        public int PostId { get; set; }

        public int Votes { get; set; }

        public DateTime DatePublished { get; set; }

        [ForeignKey("Author")]
        public string UserId { get; set; }

        public virtual User Author { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public int CategoryId { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}
