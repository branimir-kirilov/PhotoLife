using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PhotoLife.Providers.Contracts;

namespace PhotoLife.Authentication.Tests
{
    [TestFixture]
    public class GetCurrentUserId_Should
    {
        [Test]
        public void _Call_HttpContextProviderCurrentIdentity()
        {
            //Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var mockedIdentity = new Mock<IIdentity>();

            var mockedHttpContextProvider = new Mock<IHttpContextProvider>();
            mockedHttpContextProvider.Setup(provider => provider.CurrentIdentity).Returns(mockedIdentity.Object);

            var httpContextAuthProvider = new Providers.HttpContextAuthenticationProvider(mockedHttpContextProvider.Object,
                mockedDateTimeProvider.Object);
            
            //Act
            var result = httpContextAuthProvider.CurrentUserId;

            //Assert
            mockedHttpContextProvider.Verify(provider => provider.CurrentIdentity, Times.Once);
        }
    }
}
