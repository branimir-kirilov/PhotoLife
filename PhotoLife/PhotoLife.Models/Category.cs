using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PhotoLife.Models.Enums;

namespace PhotoLife.Models
{
    public class Category
    {

        private ICollection<News> News;
        private ICollection<Post> Posts;

        public Category()
        {
            this.News = new HashSet<News>();
            this.Posts = new HashSet<Post>();
        }

        public Category(CategoryEnum name)
        {
            this.Name = name;
        }

        [Key]
        public int CategoryId { get; set; }

        public CategoryEnum Name { get; set; }

        public virtual ICollection<News> NewsCollection
        {
            get { return this.News; }
            set { this.News = value; }
        }

        public virtual ICollection<Post> PostsCollection
        {
            get { return this.Posts; }
            set { this.Posts = value; }
        }
    }
}
