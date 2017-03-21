using System.Web.Mvc;
using PhotoLife.Models.Post;

namespace PhotoLife.Controllers
{
    public class PostController : Controller
    {
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
            return View();
        }

        public ActionResult Add(AddPostViewModel model)
        {
            
        }
    }
}