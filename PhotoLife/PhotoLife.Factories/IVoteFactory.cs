using PhotoLife.Models;

namespace PhotoLife.Factories
{
    public interface IVoteFactory
    {
        Vote CreateVote(int postId, string userId);
    }
}
