using CloudinaryDotNet;
using PhotoLife.Models;
using PhotoLife.Models.News;
using PhotoLife.ViewModels.Post;

namespace PhotoLife.Factories
{
    public interface IViewModelFactory
    {
        ProfileViewModel CreateUserProfileViewModel(User user);
        AddPostViewModel CreateAddPostViewModel(Cloudinary cloudinary);

        AddNewsViewModel CreateAddNewsViewModel(Cloudinary cloudinary);
    }
}
