using System.ComponentModel.DataAnnotations;
using CloudinaryDotNet;

namespace PhotoLife.Models.Account
{
    public class RegisterViewModel
    {
        public RegisterViewModel(Cloudinary cloudinary)
        {
            this.Cloudinary = cloudinary;
        }

        public RegisterViewModel()
        {

        }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required]
        [Display(Name = "Username")]
        [StringLength(60, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string UserName { get; set; }

        [Display(Name = "Name")]
        [StringLength(60, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(350, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Description { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string ProfilePicUrl { get; set; }

        public Cloudinary Cloudinary { get; set; }

    }
}