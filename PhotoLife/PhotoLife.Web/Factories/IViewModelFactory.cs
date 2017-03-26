using System.Collections.Generic;
using CloudinaryDotNet;
using PhotoLife.Models;
using PhotoLife.Models.Home;
using PhotoLife.ViewModels.News;
using PhotoLife.ViewModels.Post;

namespace PhotoLife.Factories
{
    public interface IViewModelFactory
    {
        //Home
        HomeViewModel CreateHomeViewModel(IEnumerable<News> topNews);

        //Profile
        ProfileViewModel CreateUserProfileViewModel(User user);

        //Post
        AddPostViewModel CreateAddPostViewModel(Cloudinary cloudinary);
        PostDetailsViewModel CreatePostDetailsViewModel(Post post);
        ShortPostViewModel CreateShortPostViewModel(Post post);
        
        //News
        AddNewsViewModel CreateAddNewsViewModel(Cloudinary cloudinary);

        NewsDetailsViewModel CreateNewsDetailsViewModel(News news);

    }
}
