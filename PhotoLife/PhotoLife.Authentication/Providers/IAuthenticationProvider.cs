using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PhotoLife.Models;

namespace PhotoLife.Authentication.Providers
{
    public interface IAuthenticationProvider
    {
        bool IsAuthenticated { get; }

        string CurrentUserId { get; }

        IdentityResult RegisterAndLoginUser(User user, string password, bool isPersistent, bool rememberBrowser);

        SignInStatus SignInWithPassword(string email, string password, bool rememberMe, bool shouldLockout);

        void SignOut();

        bool IsInRole(string userId, string roleName);

        IdentityResult AddToRole(string userId, string roleName);

        IdentityResult RemoveFromRole(string userId, string roleName);

    }
}
