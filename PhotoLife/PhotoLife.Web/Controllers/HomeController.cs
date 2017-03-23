using System;
using System.Web.Mvc;
using PhotoLife.Factories;
using PhotoLife.Services.Contracts;

namespace PhotoLife.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsService newsService;
        private readonly IViewModelFactory viewModelFactory;
        public HomeController(INewsService newsService, IViewModelFactory viewModelFactory)
        {
            if (newsService == null)
            {
                throw new ArgumentNullException("newsService");
            }

            if (viewModelFactory == null)
            {
                throw new ArgumentNullException("viewModelFactory");
            }

            this.newsService = newsService;
            this.viewModelFactory = viewModelFactory;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            int topCount = 3;
            var topNews = this.newsService.GetTopNews(topCount);
            var model = this.viewModelFactory.CreateHomeViewModel(topNews);

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}