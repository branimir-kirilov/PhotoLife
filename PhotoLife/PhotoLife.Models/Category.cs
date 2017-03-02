using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoLife.Models.Enums;

namespace PhotoLife.Models
{
    public class Category
    {

        private ICollection<News> News;

        public Category()
        {
            this.News = new HashSet<News>();
        }

        public Category(CategoryEnum name)
        {
            this.Name = name;
        }

        [Key]
        public int CategoryId { get; set; }

        public CategoryEnum Name { get; set; }

        public virtual ICollection<News> Products
        {
            get { return this.News; }
            set { this.News = value; }
        }
    }
}
