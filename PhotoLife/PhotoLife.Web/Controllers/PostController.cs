using System;
using System.Linq;
using System.Web.Mvc;
using CloudinaryDotNet;
using PagedList;
using PhotoLife.Authentication.Providers;
using PhotoLife.Factories;
using PhotoLife.Services.Contracts;
using PhotoLife.ViewModels.Post;

namespace PhotoLife.Controllers
{
    public class PostController : Controller
    {
        private readonly IAuthenticationProvider authenticationProvider;
        private readonly IPostService postService;
        private readonly IViewModelFactory viewModelFactory;

        private readonly Cloudinary cloudinary;

        public PostController(
            IAuthenticationProvider authenticationProvider,
            IPostService postService,
            IViewModelFactory viewModelFactory,
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

            if (viewModelFactory == null)
            {
                throw new ArgumentNullException(nameof(viewModelFactory));
            }

            if (cloudinary == null)
            {
                throw new ArgumentNullException(nameof(cloudinary));
            }

            this.authenticationProvider = authenticationProvider;
            this.postService = postService;
            this.viewModelFactory = viewModelFactory;
            this.cloudinary = cloudinary;
        }

        // Get: All
        [AllowAnonymous]
        [OutputCache(Duration = 60 * 5, VaryByParam = "page")]
        public ActionResult All(int count = 3, int page = 1)
        {

            var posts = this.postService.GetAll().Select(p => this.viewModelFactory.CreateShortPostViewModel(p));
            var model = posts.ToPagedList(page, count);

            return this.PartialView("_PagedPostListPartial", model);
        }

        // Get: Add
        [Authorize]
        public ActionResult Add()
        {
            return View(this.viewModelFactory.CreateAddPostViewModel(this.cloudinary));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Add(AddPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = this.authenticationProvider.CurrentUserId;

                var post = this.postService.CreatePost(userId, model.Title, model.Description, model.PictureUrl, model.Category);

                return RedirectToAction("Details", "Post", new { postId = post.PostId});
            }

            model.Cloudinary = this.cloudinary;
            return this.View(model);
        }

        [AllowAnonymous]
        public ActionResult Details(int postId)
        {
            var post = this.postService.GetPostById(postId);

            var postModel = this.viewModelFactory.CreatePostDetailsViewModel(post);

            return View(postModel);
        }
        
    }
}