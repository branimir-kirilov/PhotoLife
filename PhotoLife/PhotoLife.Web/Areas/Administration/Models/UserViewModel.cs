using PhotoLife.Models;

namespace PhotoLife.Areas.Administration.Models
{
    public class UserViewModel
    {
        public UserViewModel(User user, bool isAdministrator)
        {
            this.UserId = user.Id;
            this.Email = user.Email;
            this.UserName = user.UserName;
            this.ProfilePicUrl = user.ProfilePicUrl;
            this.IsAdministrator = isAdministrator;
        }
        public UserViewModel()
        {
            
        }

        public string UserId { get; set; }

        public string Email { get; set; }
        
        public string UserName { get; set; }

        public string ProfilePicUrl { get; set; }

        public bool IsAdministrator { get; set; }
    }
}