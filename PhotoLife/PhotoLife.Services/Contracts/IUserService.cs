using System.Collections.Generic;
using PhotoLife.Models;

namespace PhotoLife.Services.Contracts
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();

        User GetUserById(string id);

        User GetUserByUsername(string username);
    }
}
