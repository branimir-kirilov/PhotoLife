using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoLife.Authentication.Providers;
using PhotoLife.Services.Contracts;
using PhotoLife.ViewModels.Comment;

namespace PhotoLife.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;
        private readonly IAuthenticationProvider authProvider;

        public CommentController(ICommentService commentService, IAuthenticationProvider authProvider)
        {
            if (commentService == null)
            {
                throw new ArgumentNullException(nameof(commentService));
            }

            if (authProvider == null)
            {
                throw new ArgumentNullException(nameof(authProvider));
            }

            this.commentService = commentService;
            this.authProvider = authProvider;
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CommentPost(AddCommentViewModel model)
        {
            var userId = this.authProvider.CurrentUserId;

            this.commentService.AddCommentToPost(model.Content, model.CommentedItemId, userId);

            return this.RedirectToAction("Details", "Post", new { id = model.CommentedItemId});
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CommentNews(AddCommentViewModel model)
        {
            var userId = this.authProvider.CurrentUserId;

            this.commentService.AddCommentToNews(model.Content, model.CommentedItemId, userId);

            return this.RedirectToAction("Details", "News", new { id = model.CommentedItemId });
        }
    }
}