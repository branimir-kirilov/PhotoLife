using PhotoLife.Models;

namespace PhotoLife.Factories
{
    public interface IUserFactory
    {
        User CreateUser(string username, string email, string name, string description);
    }
}
