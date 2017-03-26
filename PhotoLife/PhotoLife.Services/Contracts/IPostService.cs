using System.Collections.Generic;
using PhotoLife.Models;
using PhotoLife.Models.Enums;

namespace PhotoLife.Services.Contracts
{
    public interface IPostService
    {
        Post GetPostById(int id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetTopPosts(int topCount);
        IEnumerable<Post> GetTopByComments(int topCount);
        Post CreatePost(string userId, string title, string description, string profilePicUrl, CategoryEnum category);
        void EditPost(int id, string title, string description, CategoryEnum categoryEnum);
        void AddComment(int postId, Comment comment);
    }
}
