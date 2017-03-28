using System.Web.Mvc;

namespace PhotoLife.Areas.Administration.Controllers
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