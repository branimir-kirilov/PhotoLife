using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PhotoLife.Authentication.Providers;
using PhotoLife.Factories;
using PhotoLife.Models.Account;

namespace PhotoLife.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //private ApplicationSignInManager _signInManager;
        //private ApplicationUserManager _userManager;
        private readonly IAuthenticationProvider authenticationProvider;
        private readonly IUserFactory userFactory;
        private readonly ICloudinaryFactory cloudinaryFactory;

        public AccountController(
            IAuthenticationProvider authProvider, 
            IUserFactory userFactory, 
            ICloudinaryFactory cloudinaryFactory)
        {
            if (authProvider == null)
            {
                throw new ArgumentNullException(nameof(authProvider));
            }

            if (userFactory == null)
            {
                throw new ArgumentNullException(nameof(userFactory));
            }

            if (cloudinaryFactory == null)
            {
                throw new ArgumentNullException(nameof(cloudinaryFactory));
            }

            this.authenticationProvider = authProvider;
            this.userFactory = userFactory;
            this.cloudinaryFactory = cloudinaryFactory;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            returnUrl = string.IsNullOrEmpty(returnUrl) ? "/Home/Index" : returnUrl;
            
            var result = authenticationProvider.SignInWithPassword(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return this.Redirect(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            var cloudinary = this.cloudinaryFactory.GetCloudinary();

            return View(new RegisterViewModel(cloudinary));
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = this.userFactory.CreateUser(
                    model.UserName,
                    model.Email,
                    model.Name,
                    model.Description,
                    model.ProfilePicUrl);
                var result = this.authenticationProvider.RegisterAndLoginUser(user, model.Password, isPersistent: false, rememberBrowser: true);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            this.authenticationProvider.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        #endregion
    }
}