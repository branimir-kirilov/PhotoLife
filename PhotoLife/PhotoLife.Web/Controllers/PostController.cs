using System;
using System.Web.Mvc;
using CloudinaryDotNet;
using PhotoLife.Authentication.Providers;
using PhotoLife.Factories;
using PhotoLife.Models.Post;
using PhotoLife.Providers.Contracts;
using PhotoLife.Services.Contracts;

namespace PhotoLife.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostFactory postFactory;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IAuthenticationProvider authenticationProvider;
        private readonly IPostService postService;

        private readonly Cloudinary cloudinary;

        public PostController(
            IPostFactory postFactory,
            IDateTimeProvider dateTimeProvider,
            IAuthenticationProvider authenticationProvider,
            IPostService postService,
            Cloudinary cloudinary)
        {
            if (postFactory == null)
            {
                throw new ArgumentNullException(nameof(postFactory));
            }

            if (dateTimeProvider == null)
            {
                throw new ArgumentNullException(nameof(dateTimeProvider));
            }

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

            this.postFactory = postFactory;
            this.dateTimeProvider = dateTimeProvider;
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
        [AllowAnonymous]
        public ActionResult Add()
        {
            return View(new AddPostViewModel(this.cloudinary));
        }

        public ActionResult Add(AddPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dateCreated = this.dateTimeProvider.GetCurrentDate();

                var post = this.postFactory.CreatePost(
                    model.Title,
                    model.Description,
                    model.ProfilePicUrl,
                    this.authenticationProvider.CurrentUserId,
                    model.Category,
                    dateCreated
                   );


            }
        }
    }
}