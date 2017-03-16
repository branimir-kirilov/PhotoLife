using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoLife.Authentication.Providers;
using PhotoLife.Services.Contracts;

namespace PhotoLife.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IAuthenticationProvider AuthenticationProvider;
        private readonly IUserService UserSerivce;

        public ProfileController(IAuthenticationProvider authProvider, IUserService userService)
        {
            if (AuthenticationProvider == null)
            {
                throw new ArgumentNullException(nameof(AuthenticationProvider));
            }

            if (UserSerivce == null)
            {
                throw new ArgumentNullException(nameof(UserSerivce));
            }

            this.AuthenticationProvider = authProvider;
            this.UserSerivce = userService;
        }
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }
    }
}