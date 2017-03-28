using System;

namespace PhotoLife.ViewModels.Comment
{
    public class CommentViewModel
    {
        public CommentViewModel(Models.Comment comment)
        {
            this.CommentId = comment.CommentId;
            this.Date = comment.DatePublished;
            this.User = comment.Author.UserName;
            this.Content = comment.Text;
        }

        public bool CanEdit { get; set; }

        public int CommentId { get; set; }

        public DateTime Date { get; set; }

        public string User { get; set; }
        
        public string Content { get; set; }
    }
}