using PhotoLife.Models;

namespace PhotoLife.Services.Contracts
{
    public interface IPostService
    {
        Post GetPostById(string id);
    }
}
