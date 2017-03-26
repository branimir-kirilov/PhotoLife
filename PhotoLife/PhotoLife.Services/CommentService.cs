using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoLife.Data.Contracts;
using PhotoLife.Factories;
using PhotoLife.Models;
using PhotoLife.Providers.Contracts;
using PhotoLife.Services.Contracts;

namespace PhotoLife.Services
{
    public class CommentService : ICommentService
    {
        private readonly IPostService postsService;
        private readonly INewsService newsService;
        private readonly IUserService userService;
        
        private readonly ICommentFactory commentFactory;

        private readonly IRepository<Comment> commentRepository;
        private readonly IUnitOfWork unitOfWork;

        private readonly IDateTimeProvider dateTimeProvider;

        public CommentService(
            IPostService postsService,
            INewsService newsService,
            ICommentFactory commentFactory,
            IUserService userService,
            IRepository<Comment> commentRepository,
            IDateTimeProvider dateTimeProvider,
            IUnitOfWork unitOfWork)
        {
            if (postsService == null)
            {
                throw new ArgumentNullException(nameof(postsService));
            }

            if (newsService == null)
            {
                throw new ArgumentNullException(nameof(newsService));
            }


            if (userService == null)
            {
                throw new ArgumentNullException(nameof(userService));
            }

            if (dateTimeProvider == null)
            {
                throw new ArgumentNullException(nameof(dateTimeProvider));
            }

            if (commentFactory == null)
            {
                throw new ArgumentNullException(nameof(commentFactory));
            }

            if (commentRepository == null)
            {
                throw new ArgumentNullException(nameof(commentRepository));
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            this.postsService = postsService;
            this.newsService = newsService;
            this.userService = userService;

            this.commentFactory = commentFactory;

            this.commentRepository = commentRepository;
            this.unitOfWork = unitOfWork;

            this.dateTimeProvider = dateTimeProvider;
        }
        public void AddCommentToNews(string content, int postId, string userId)
        {
            var date = this.dateTimeProvider.GetCurrentDate();

            var user = this.userService.GetUserById(userId);

            var comment = this.commentFactory.CreateComment(user, date, content);

            this.postsService.AddComment(postId, comment);

        }

        public void AddCommentToPost(string content, int newsId, string userId)
        {
            var date = this.dateTimeProvider.GetCurrentDate();

            var user = this.userService.GetUserById(userId);

            var comment = this.commentFactory.CreateComment(user, date, content);

            this.newsService.AddComment(newsId, comment);
        }

        public void EditComment(string content, int commentId)
        {
            var comment = this.commentRepository.GetById(commentId);

            if (comment != null)
            {
                comment.Text = content;

                this.commentRepository.Update(comment);
                this.unitOfWork.Commit();
            }
        }
    }
}
