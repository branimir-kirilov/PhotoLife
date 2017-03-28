using System;
using System.Collections.Generic;
using System.Linq;
using PhotoLife.Data.Contracts;
using PhotoLife.Models;
using PhotoLife.Services.Contracts;

namespace PhotoLife.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserService(
            IRepository<User> userRepository,
            IUnitOfWork unitOfWork)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException("userRepository");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWorks");
            }

            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<User> GetUsers()
        {
            return this.userRepository.GetAll
                .ToList();
        }

        public User GetUserById(string id)
        {
            return this.userRepository.GetById(id);
        }

        public User GetUserByUsername(string username)
        {
            return this.userRepository.GetAll.FirstOrDefault(u => u.UserName.Equals(username));
        }

        public void EditUser(string id, string username, string name, string description, string profilePicUrl)
        {
            var user = this.userRepository.GetById(id);

            if (user != null)
            {
                user.UserName = username;
                user.Name = name;
                user.Description = description;
                user.ProfilePicUrl = profilePicUrl;

                this.userRepository.Update(user);
                this.unitOfWork.Commit();
            }
        }
    }
}
