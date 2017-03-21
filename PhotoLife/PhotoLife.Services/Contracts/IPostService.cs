using System.Collections.Generic;
using PhotoLife.Models;

namespace PhotoLife.Services.Contracts
{
    public interface IPostService
    {
        Post GetPostById(string id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetTopPosts(int topCount);
        IEnumerable<Post> GetTopByComments(int topCount);
    }
}
