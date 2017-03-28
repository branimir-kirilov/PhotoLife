using System;

namespace PhotoLife.ViewModels.Comment
{
    public class CommentViewModel
    {
        public static Func<Models.Comment, string, bool, CommentViewModel> FromComment
        {
            get
            {
                return (comment, userId, isAdmin) => new CommentViewModel
                {
                    Date = comment.DatePublished,
                    CanEdit = comment.UserId.Equals(userId) || isAdmin,
                    CommentId = comment.CommentId,
                    Content = comment.Text,
                    User = comment.Author.UserName
                };
            }
        }

        public bool CanEdit { get; set; }

        public int CommentId { get; set; }

        public DateTime Date { get; set; }

        public string User { get; set; }
        
        public string Content { get; set; }
    }
}