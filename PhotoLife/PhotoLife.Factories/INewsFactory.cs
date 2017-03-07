using System;
using PhotoLife.Models;

namespace PhotoLife.Factories
{
    public interface INewsFactory
    {
        News CreateNews(
            string title,
            string text,
            string imageUrl,
            User author,
            Category category,
            int views,
            DateTime datePublished);
    }
}
