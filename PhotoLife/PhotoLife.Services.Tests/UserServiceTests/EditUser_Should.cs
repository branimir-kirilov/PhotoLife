using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PhotoLife.Data.Contracts;
using PhotoLife.Models;

namespace PhotoLife.Services.Tests.UserServiceTests
{
    [TestFixture]
    public class EditUser_Should
    {
        [TestCase("somefancyid", "branimir", "Branimir", "I am branimir", "cloudinary.com/someid")]
        public void _CallRepository_GetById_Method(
            string id,
            string username,
            string name,
            string description,
            string profilePicUrl)
        {
            //Arrange
            var mockedRepository = new Mock<IRepository<User>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var userService = new UserService(mockedRepository.Object, mockedUnitOfWork.Object);

            //Act
            userService.EditUser(id, username, name, description, profilePicUrl);

            //Assert            
            mockedRepository.Verify(r => r.GetById(id), Times.Once);
        }

        [TestCase("somefancyid", "branimir", "Branimir", "I am branimir", "cloudinary.com/someid")]
        public void _ReturnCorrectly_GetAll_Method(
            string id,
            string username,
            string name,
            string description,
            string profilePicUrl)
        {
            //Arrange
            var mockedRepository = new Mock<IRepository<User>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var userService = new UserService(mockedRepository.Object, mockedUnitOfWork.Object);

            //Act
            userService.EditUser(id, username, name, description, profilePicUrl);

            //Assert            
            mockedUnitOfWork.Verify(u => u.Commit(), Times.Never);
        }

        [TestCase("somefancyid", "branimir", "Branimir", "I am branimir", "cloudinary.com/someid")]
        public void _ShouldSet_Username_Correctly(
            string id,
            string username,
            string name,
            string description,
            string profilePicUrl)
        {
            //Arrange
            var mockedUser = new User();

            var mockedRepository = new Mock<IRepository<User>>();
            mockedRepository.Setup(r => r.GetById(It.IsAny<object>())).Returns(mockedUser);

            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var userService = new UserService(mockedRepository.Object, mockedUnitOfWork.Object);

            //Act
            userService.EditUser(id, username, name, description, profilePicUrl);

            //Assert            
            Assert.AreEqual(username, mockedUser.UserName);
        }

        [TestCase("somefancyid", "branimir", "Branimir", "I am branimir", "cloudinary.com/someid")]
        public void _ShouldSet_Description_Correctly(
           string id,
           string username,
           string name,
           string description,
           string profilePicUrl)
        {
            //Arrange
            var mockedUser = new User();

            var mockedRepository = new Mock<IRepository<User>>();
            mockedRepository.Setup(r => r.GetById(It.IsAny<object>())).Returns(mockedUser);

            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var userService = new UserService(mockedRepository.Object, mockedUnitOfWork.Object);

            //Act
            userService.EditUser(id, username, name, description, profilePicUrl);

            //Assert            
            Assert.AreEqual(description, mockedUser.Description);
        }

        [TestCase("somefancyid", "branimir", "Branimir", "I am branimir", "cloudinary.com/someid")]
        public void _ShouldSet_Name_Correctly(
           string id,
           string username,
           string name,
           string description,
           string profilePicUrl)
        {
            //Arrange
            var mockedUser = new User();

            var mockedRepository = new Mock<IRepository<User>>();
            mockedRepository.Setup(r => r.GetById(It.IsAny<object>())).Returns(mockedUser);

            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var userService = new UserService(mockedRepository.Object, mockedUnitOfWork.Object);

            //Act
            userService.EditUser(id, username, name, description, profilePicUrl);

            //Assert            
            Assert.AreEqual(name, mockedUser.Name);
        }

        [TestCase("somefancyid", "branimir", "Branimir", "I am branimir", "cloudinary.com/someid")]
        public void _ShouldSet_ProfilePicUrl_Correctly(
           string id,
           string username,
           string name,
           string description,
           string profilePicUrl)
        {
            //Arrange
            var mockedUser = new User();

            var mockedRepository = new Mock<IRepository<User>>();
            mockedRepository.Setup(r => r.GetById(It.IsAny<object>())).Returns(mockedUser);

            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var userService = new UserService(mockedRepository.Object, mockedUnitOfWork.Object);

            //Act
            userService.EditUser(id, username, name, description, profilePicUrl);

            //Assert            
            Assert.AreEqual(profilePicUrl, mockedUser.ProfilePicUrl);
        }
    }
}
