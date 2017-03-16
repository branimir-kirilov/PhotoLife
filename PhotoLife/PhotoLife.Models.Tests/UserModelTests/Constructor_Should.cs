using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PhotoLife.Models.Tests.UserModelTests
{
    [TestFixture]
    public class Constructor_Should
    {

        [TestCase("Branimir", "me@me.com", "Branimir", "I am branimir", "cloudinary.com/somecloud/somepic")]
        public void _Set_UserName_Correctly(
            string username,
            string email,
            string name,
            string description,
            string profilePicUrl)
        {
            //Arrange
            var user = new User(username, email, name, description, profilePicUrl);

            //Act & Assert  
            Assert.AreEqual(username, user.UserName);
        }

        [TestCase("Branimir", "me@me.com", "Branimir", "I am branimir", "cloudinary.com/somecloud/somepic")]
        public void _Set_Email_Correctly(
            string username,
            string email,
            string name,
            string description,
            string profilePicUrl)
        {
            //Arrange
            var user = new User(username, email, name, description, profilePicUrl);

            //Act & Assert  
            Assert.AreEqual(email, user.Email);
        }

        [TestCase("Branimir", "me@me.com", "Branimir", "I am branimir", "cloudinary.com/somecloud/somepic")]
        public void _Set_Name_Correctly(
           string username,
           string email,
           string name,
           string description,
           string profilePicUrl)
        {
            //Arrange
            var user = new User(username, email, name, description, profilePicUrl);

            //Act & Assert  
            Assert.AreEqual(name, user.Name);
        }

        [TestCase("Branimir", "me@me.com", "Branimir", "I am branimir", "cloudinary.com/somecloud/somepic")]
        public void _Set_Description_Correctly(
          string username,
          string email,
          string name,
          string description,
          string profilePicUrl)
        {
            //Arrange
            var user = new User(username, email, name, description, profilePicUrl);

            //Act & Assert  
            Assert.AreEqual(description, user.Description);
        }

        [TestCase("Branimir", "me@me.com", "Branimir", "I am branimir", "cloudinary.com/somecloud/somepic")]
        public void _Set_ProfilePicUrl_Correctly(
          string username,
          string email,
          string name,
          string description,
          string profilePicUrl)
        {
            //Arrange
            var user = new User(username, email, name, description, profilePicUrl);

            //Act & Assert  
            Assert.AreEqual(profilePicUrl, user.ProfilePicUrl);
        }
    }
}

