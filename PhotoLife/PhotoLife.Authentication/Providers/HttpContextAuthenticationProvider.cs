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

        public HttpContextAuthenticationProvider(IHttpContextProvider httpContextProvider)
        {
            if (httpContextProvider == null)
            {
                throw new ArgumentNullException("httpContextProvider");
            }

            this.httpContextProvider = httpContextProvider;
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
    }
}
