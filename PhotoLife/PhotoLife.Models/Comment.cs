using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoLife.Models
{
    public class Comment
    {
        public Comment()
        {
            
        }

        public Comment(User author, DateTime datePublished, string text)
        {
            this.Author = author;
            this.DatePublished = datePublished;
            this.Text = text;
        }

        [Key]
        public int CommentId { get; set; }

        public string Text { get; set; }

        public DateTime DatePublished { get; set; }

        [ForeignKey("Author")]
        public string UserId { get; set; }

        public virtual User Author { get; set; }
    }
}
