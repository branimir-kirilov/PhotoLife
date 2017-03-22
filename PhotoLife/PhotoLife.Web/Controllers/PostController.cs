using System;
using System.Web.Mvc;
using CloudinaryDotNet;
using PhotoLife.Authentication.Providers;
using PhotoLife.Services.Contracts;
using PhotoLife.ViewModels.Post;

namespace PhotoLife.Controllers
{
    public class PostController : Controller
    {
        private readonly IAuthenticationProvider authenticationProvider;
        private readonly IPostService postService;

        private readonly Cloudinary cloudinary;

        public PostController(
            IAuthenticationProvider authenticationProvider,
            IPostService postService,
            Cloudinary cloudinary)
        {
            if (authenticationProvider == null)
            {
                throw new ArgumentNullException(nameof(authenticationProvider));
            }

            if (postService == null)
            {
                throw new ArgumentNullException(nameof(postService));
            }

            if (cloudinary == null)
            {
                throw new ArgumentNullException(nameof(cloudinary));
            }

            this.authenticationProvider = authenticationProvider;
            this.postService = postService;
            this.cloudinary = cloudinary;
        }
        // Get: All
        [AllowAnonymous]
        public ActionResult All()
        {
            return View();
        }

        // Post: Add
        [Authorize]
        public ActionResult Add()
        {
            return View(new AddPostViewModel(this.cloudinary));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Add(AddPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = this.authenticationProvider.CurrentUserId;

                this.postService.CreatePost(userId, model.Title, model.Description, model.ProfilePicUrl, model.Category);
            }

            return RedirectToAction("All", "Post");
        }
    }
}