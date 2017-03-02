using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PhotoLife.Models
{
    public class User : IdentityUser
    {
        public User() : base(string.Empty)
        {
            
        }

        public User(string username, string email, string name, string description)
        {
            this.UserName = username;
            this.Email = email;
            this.Description = description;
        }

        public string Description { get; set; }

        public Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            return manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}
