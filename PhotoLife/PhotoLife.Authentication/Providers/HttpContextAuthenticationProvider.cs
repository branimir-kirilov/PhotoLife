using System;
using PhotoLife.Authentication.ApplicationManagers;
using PhotoLife.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PhotoLife.Providers.Contracts;

namespace PhotoLife.Authentication.Providers
{
    public class HttpContextAuthenticationProvider : IAuthenticationProvider
    {
        private readonly IHttpContextProvider httpContextProvider;
        private readonly IDateTimeProvider dateTimeProvider;

        public HttpContextAuthenticationProvider(
            IHttpContextProvider httpContextProvider,
            IDateTimeProvider dateTimeProvider)
        {
            if (httpContextProvider == null)
            {
                throw new ArgumentNullException("httpContextProvider");
            }

            if (dateTimeProvider == null)
            {
                throw new ArgumentNullException("dateTimeProvider");
            }

            this.httpContextProvider = httpContextProvider;
            this.dateTimeProvider = dateTimeProvider;
        }
        protected ApplicationSignInManager SignInManager
        {
            get
            {
                return this.httpContextProvider.GetUserManager<ApplicationSignInManager>();
            }
        }
        protected ApplicationUserManager UserManager
        {
            get
            {
                return this.httpContextProvider.GetUserManager<ApplicationUserManager>();
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return this.httpContextProvider.CurrentIdentity.IsAuthenticated;
            }
        }

        public string CurrentUserId
        {
            get
            {
                return this.httpContextProvider.CurrentIdentity.GetUserId();
            }
        }

        public IdentityResult RegisterAndLoginUser(User user, string password, bool isPersistent, bool rememberBrowser)
        {
            var result = this.UserManager.Create(user, password);

            if (result.Succeeded)
            {
                this.SignInManager.SignIn(user, isPersistent, rememberBrowser);
            }

            return result;
        }

        public SignInStatus SignInWithPassword(string email, string password, bool rememberMe, bool shouldLockout)
        {
            return this.SignInManager.PasswordSignIn(email, password, rememberMe, shouldLockout);
        }

        public void SignOut()
        {
            this.httpContextProvider.CurrentOwinContext.Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public bool IsInRole(string userId, string roleName)
        {
            return userId != null && this.UserManager.IsInRole(userId, roleName);
        }

        public IdentityResult AddToRole(string userId, string roleName)
        {
            return this.UserManager.AddToRole(userId, roleName);
        }

        public IdentityResult RemoveFromRole(string userId, string roleName)
        {
            return this.UserManager.RemoveFromRole(userId, roleName);
        }

    }
}
