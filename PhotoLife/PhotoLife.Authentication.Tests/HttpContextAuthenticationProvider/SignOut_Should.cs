using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Moq;
using NUnit.Framework;
using PhotoLife.Providers.Contracts;

namespace PhotoLife.Authentication.Tests.HttpContextAuthenticationProvider
{
    [TestFixture]
    public class SignOut_Should
    {
        [Test]
        public void _Call_HttpContextProvider_CurrentOwinContext()
        {
            //Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var mockedAuthenticationManager = new Mock<IAuthenticationManager>();

            var mockedOwinCtx = new Mock<IOwinContext>();
            mockedOwinCtx.Setup(ctx => ctx.Authentication).Returns(mockedAuthenticationManager.Object);

            var mockedHttpContextProvider = new Mock<IHttpContextProvider>();
            mockedHttpContextProvider.Setup(p => p.CurrentOwinContext)
                .Returns(mockedOwinCtx.Object);

            var provider = new Providers.HttpContextAuthenticationProvider(mockedHttpContextProvider.Object, mockedDateTimeProvider.Object);

            //Act
            provider.SignOut();


            //Assert
            mockedHttpContextProvider.Verify(m => m.CurrentOwinContext, Times.Once);

        }

        [Test]
        public void _Call_AuthenticationManager_CurrentOwinContext()
        {
            //Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            var mockedAuthenticationManager = new Mock<IAuthenticationManager>();

            var mockedOwinCtx = new Mock<IOwinContext>();
            mockedOwinCtx.Setup(ctx => ctx.Authentication).Returns(mockedAuthenticationManager.Object);

            var mockedHttpContextProvider = new Mock<IHttpContextProvider>();
            mockedHttpContextProvider.Setup(p => p.CurrentOwinContext)
                .Returns(mockedOwinCtx.Object);

            var provider = new Providers.HttpContextAuthenticationProvider(mockedHttpContextProvider.Object, mockedDateTimeProvider.Object);

            //Act
            provider.SignOut();


            //Assert
            mockedAuthenticationManager.Verify(m => m.SignOut(It.IsAny<string>()), Times.Once);

        }
    }
}
