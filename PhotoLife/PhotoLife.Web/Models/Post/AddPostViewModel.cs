using System.ComponentModel.DataAnnotations;
using CloudinaryDotNet;

namespace PhotoLife.Models.Post
{
    public class AddPostViewModel
    {
        public AddPostViewModel(Cloudinary cloudinary)
        {
            this.Cloudinary = cloudinary;
        }

        public AddPostViewModel()
        {
        }

        [Required]
        [Display(Name = "Title")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(350, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 0)]
        public string Description { get; set; }

        public string ProfilePicUrl { get; set; }
        public Category Category { get; set; }
        
        public Cloudinary Cloudinary { get; set; } 

    }
}