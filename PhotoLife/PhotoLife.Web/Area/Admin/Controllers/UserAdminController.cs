using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PagedList;
using PhotoLife.Area.Admin.Models;
using PhotoLife.Authentication.Providers;
using PhotoLife.Services.Contracts;

namespace PhotoLife.Area.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserAdminController : Controller
    {
        private readonly IUserService userService;
        private readonly IAuthenticationProvider authProvider;

        public UserAdminController(IUserService userService, IAuthenticationProvider authProvider)
        {
            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }

            if (authProvider == null)
            {
                throw new ArgumentNullException("authProvider");
            }

            this.userService = userService;
            this.authProvider = authProvider;
        }

        // GET: UserAdmin
        public ActionResult Index(int page = 1, int count = 15)
        {
            var users = this.userService.GetUsers();

            var model = new List<UserViewModel>();

            foreach (var user in users)
            {
                var isAdmin = this.authProvider.IsInRole(user.Id, "Administrator");
                var viewModel = new UserViewModel(user, isAdmin);
                model.Add(viewModel);
            }

            return this.View(model.ToPagedList(page, count));
        }

        public ActionResult RemoveAdmin(string userId, int page)
        {
            this.authProvider.RemoveFromRole(userId, "Administrator");

            return this.RedirectToAction("Index", new { page = page });
        }

        public ActionResult AddAdmin(string userId, int page)
        {
            this.authProvider.AddToRole(userId, "Administrator");

            return this.RedirectToAction("Index", new { page = page });
        }
    }
}