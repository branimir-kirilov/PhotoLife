using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PhotoLife.Models.Tests.UserModelTests
{
    [TestFixture]
    public class Property_
    {
        [Test]
        public void _Name_Should_SetName_Correctly()
        {
            //Arrange
            var name = "me";
            var user = new User();

            //Act 
            user.Name = name;

            //Assert  
            Assert.AreEqual(name, user.Name);
        }

        [Test]
        public void _Description_Should_Description_Correctly()
        {
            //Arrange
            var description = "I am me";
            var user = new User();

            //Act 
            user.Description = description;

            //Assert  
            Assert.AreEqual(description, user.Description);
        }

        [Test]
        public void _ProfilePicUrl_Should_SetProfilePicUrl_Correctly()
        {
            //Arrange
            var profilePicUrl = "cloudinary.com/somecloud/somepic";
            var user = new User();

            //Act 
            user.ProfilePicUrl = profilePicUrl;

            //Assert  
            Assert.AreEqual(profilePicUrl, user.ProfilePicUrl);
        }
    }
}
