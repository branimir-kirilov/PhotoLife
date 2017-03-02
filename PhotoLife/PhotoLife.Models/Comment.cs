using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoLife.Models
{
    public class Comment
    {
        public Comment()
        {
            
        }

        public Comment(string author, DateTime datePublished, string text)
        {
            this.Author = author;
            this.DatePublished = datePublished;
            this.Text = text;
        }

        [Key]
        public int CommentId { get; set; }

        public string Text { get; set; }

        public DateTime DatePublished { get; set; }

        public string Author { get; set; }
    }
}
