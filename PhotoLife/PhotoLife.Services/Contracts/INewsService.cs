using System.Collections.Generic;
using PhotoLife.Models;

namespace PhotoLife.Services.Contracts
{
    public interface INewsService
    {
        News GetNewsById(string id);
        IEnumerable<News> GetAll();
        IEnumerable<News> GetTopNews(int topCount);
        IEnumerable<News> GetTopByComments(int topCount);
    }
}
