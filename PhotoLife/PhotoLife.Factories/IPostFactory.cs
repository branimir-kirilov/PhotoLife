using System;
using PhotoLife.Models;

namespace PhotoLife.Factories
{
    public interface IPostFactory
    {
        Post CreatePost(string title,
            string description,
            string imageUrl,
            User author,
            Category category,
            DateTime datePublished,
            int votes);
    }
}
