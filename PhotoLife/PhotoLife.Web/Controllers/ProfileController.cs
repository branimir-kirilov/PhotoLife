using System;
using System.Web.Mvc;
using PhotoLife.Authentication.Providers;
using PhotoLife.Factories;
using PhotoLife.Services.Contracts;

namespace PhotoLife.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IAuthenticationProvider AuthenticationProvider;
        private readonly IUserService UserSerivce;
        private readonly IViewModelFactory ViewModelFactory;

        public ProfileController(IAuthenticationProvider authProvider, IUserService userService)
        {
            if (authProvider == null)
            {
                throw new ArgumentNullException(nameof(AuthenticationProvider));
            }

            if (userService == null)
            {
                throw new ArgumentNullException(nameof(UserSerivce));
            }

            this.AuthenticationProvider = authProvider;
            this.UserSerivce = userService;
        }

        // GET: Profile
        public ActionResult UserProfile(string username)
        {
            var user = this.UserSerivce.GetUserByUsername(username);

            var id = this.AuthenticationProvider.CurrentUserId;

            var model = this.ViewModelFactory.CreateUserProfileViewModel(user);

            return this.View(model);
        }
    }
}   