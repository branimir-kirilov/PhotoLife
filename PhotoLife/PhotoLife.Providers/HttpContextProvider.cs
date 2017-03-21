using System.Security.Principal;
using System.Web;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.Owin;
using PhotoLife.Providers.Contracts;

namespace PhotoLife.Providers
{
    public class HttpContextProvider : IHttpContextProvider
    {
        public HttpContext CurrentHttpContext
        {
            get { return HttpContext.Current; }
        }

        public IOwinContext CurrentOwinContext
        {
            get { return this.CurrentHttpContext.GetOwinContext(); }
        }

        public IIdentity CurrentIdentity
        {
            get { return this.CurrentHttpContext.User.Identity; }
        }

        public TManager GetUserManager<TManager>()
        {
            return this.CurrentOwinContext.GetUserManager<TManager>();
        }
    }
}
