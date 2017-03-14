using System;
using Moq;
using NUnit.Framework;
using PhotoLife.Factories;
using PhotoLife.Controllers;

namespace PhotoLife.Web.Tests.Controllers.Account
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void _Throw_ArgumentNullException_IfAuthenticationProvider_IsNull()
        {
            //Arrange
            var mockedFactory = new Mock<IUserFactory>();

            //Act & Assert 
            Assert.Throws<ArgumentNullException>(() => new AccountController(null, mockedFactory.Object));

        }
    }
}
