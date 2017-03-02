using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoLife.Models.Enums;

namespace PhotoLife.Models
{
    public class News
    {
        public News()
        {
            
        }

        public News(string title, string text, string imageUrl, string author, Category category, int views)
        {
            this.Title = title;
            this.Text = text;
            this.ImageUrl = imageUrl;
            this.Author = author;
            this.Category = category;
            this.Views = views;
        }

        [Key]
        public int ProductId { get; set; }

        public int Views { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public string Author { get; set; }

        public string ImageUrl { get; set; }

        public string Text { get; set; }

        public string Title { get; set; }
    }
}
