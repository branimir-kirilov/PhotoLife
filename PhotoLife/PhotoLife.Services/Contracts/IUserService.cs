using PhotoLife.Models;

namespace PhotoLife.Services.Contracts
{
    public interface IUserService
    {
        User GetUserById(string id);

        User GetUserByUsername(string username);
    }
}
