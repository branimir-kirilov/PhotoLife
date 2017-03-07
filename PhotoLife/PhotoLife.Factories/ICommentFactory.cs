using System;
using PhotoLife.Models;

namespace PhotoLife.Factories
{
    public interface ICommentFactory
    {
        Comment CreateComment(User author, DateTime datePublished, string text);
    }
}
