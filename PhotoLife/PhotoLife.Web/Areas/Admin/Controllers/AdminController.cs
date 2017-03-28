using System.Web.Mvc;

namespace PhotoLife.Area.Admin.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles = "Administrator")]
        public class AdministrationController : Controller
        {
            public ActionResult Index()
            {
                return this.View();
            }
        }
    }
}