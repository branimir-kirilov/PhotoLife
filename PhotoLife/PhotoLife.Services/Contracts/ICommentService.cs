namespace PhotoLife.Services.Contracts
{
    public interface ICommentService
    {
        void AddCommentToNews(string content, int newsId, string userId);
        void AddCommentToPost(string content, int postId, string userId);
        void EditComment(string content, int commentId);
    }
}
