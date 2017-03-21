using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoLife.Controllers
{
    public class PostController : Controller
    {
        // GET: AddPost
        public ActionResult AddPost()
        {
            return View();
        }
    }
}