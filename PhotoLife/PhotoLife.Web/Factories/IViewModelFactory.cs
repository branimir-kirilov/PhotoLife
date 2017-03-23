using CloudinaryDotNet;
using PhotoLife.Models;
using PhotoLife.ViewModels.News;
using PhotoLife.ViewModels.Post;

namespace PhotoLife.Factories
{
    public interface IViewModelFactory
    {
        //Profile
        ProfileViewModel CreateUserProfileViewModel(User user);

        //Post
        AddPostViewModel CreateAddPostViewModel(Cloudinary cloudinary);
        PostDetailsViewModel CreatePostDetailsViewModel(Post post);
        
        //News
        AddNewsViewModel CreateAddNewsViewModel(Cloudinary cloudinary);
    }
}
