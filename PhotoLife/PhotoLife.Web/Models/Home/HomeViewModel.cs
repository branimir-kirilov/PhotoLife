using System.Collections.Generic;

namespace PhotoLife.Models.Home
{
    public class HomeViewModel
    {
        public HomeViewModel(IEnumerable<News> topNews)
        {
            this.TopNews = topNews;
        }

        public HomeViewModel()
        {
            
        }

        public IEnumerable<News> TopNews { get; set; }
    }
}