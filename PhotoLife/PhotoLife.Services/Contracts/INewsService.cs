using PhotoLife.Models;

namespace PhotoLife.Services.Contracts
{
    public interface INewsService
    {
        News GetNewsById(string id);
    }
}
