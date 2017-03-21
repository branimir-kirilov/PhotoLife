using System.Web.Mvc;

namespace PhotoLife.Controllers
{
    public class NewsController : Controller
    {
        // Get: All
        public ActionResult All()
        {
            return View();
        }

        // Post: News
        [Authorize(Roles="Administrators")]
        [HttpPost]
        public ActionResult Add()
        {
            return View();
        }
    }
}