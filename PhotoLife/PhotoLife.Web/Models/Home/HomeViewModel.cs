using System.Collections.Generic;

namespace PhotoLife.Models.Home
{
    public class HomeViewModel
    {
        public HomeViewModel(IEnumerable<News> topNews, IEnumerable<Post> topPosts)
        {
            this.TopNews = topNews;
            this.TopPosts = topPosts;
        }

        public HomeViewModel()
        {
            
        }

        public IEnumerable<News> TopNews { get; set; }
        public IEnumerable<Post> TopPosts { get; set; }
    }
}