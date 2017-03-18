using PhotoLife.Models;

namespace PhotoLife.Factories
{
    public interface IViewModelFactory
    {
        ProfileViewModel CreateUserProfileViewModel(User user);
    }
}
