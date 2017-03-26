using System.Collections.Generic;
using PhotoLife.Models;
using PhotoLife.Models.Enums;

namespace PhotoLife.Services.Contracts
{
    public interface INewsService
    {
        News GetNewsById(int id);
        IEnumerable<News> GetAll();
        IEnumerable<News> GetTopNews(int topCount);
        IEnumerable<News> GetTopByComments(int topCount);
        News CreateNews(string userId, string title, string text, string coverPicture, CategoryEnum category);
        void EditNews(int id, string title, string text, string imageUrl, CategoryEnum categoryEnum);
        void AddComment(int newsId, Comment comment);
    }
}
