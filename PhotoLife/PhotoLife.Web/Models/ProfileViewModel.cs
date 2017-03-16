namespace PhotoLife.Models
{
    public class ProfileViewModel
    {
        public ProfileViewModel(User user)
        {
            this.Id = user.Id;
            this.Username = user.UserName;
            this.Name = user.Name;
            this.Email = user.Email;
            this.Description = user.Description;
            this.ProfilePicUrl = user.ProfilePicUrl;
        }

        public string Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string ProfilePicUrl { get; set; }
    }
}