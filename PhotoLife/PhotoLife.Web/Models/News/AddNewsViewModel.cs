using System.ComponentModel.DataAnnotations;
using CloudinaryDotNet;
using PhotoLife.Models.Enums;

namespace PhotoLife.Models.News
{
    public class AddNewsViewModel
    {
        public AddNewsViewModel(Cloudinary cloudinary)
        {
            this.Cloudinary = cloudinary;
        }

        public AddNewsViewModel()
        {
        }

        [Required]
        [Display(Name = "Title")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Text")]
        [StringLength(350, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 0)]
        public string Text { get; set; }

        public string CoverPicture { get; set; }
        public CategoryEnum Category { get; set; }

        public Cloudinary Cloudinary { get; set; }
    }
}