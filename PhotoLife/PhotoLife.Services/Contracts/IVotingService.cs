namespace PhotoLife.Services.Contracts
{
    public interface IVotingService
    {
        int Vote(int postId, string userId);
    }
}
