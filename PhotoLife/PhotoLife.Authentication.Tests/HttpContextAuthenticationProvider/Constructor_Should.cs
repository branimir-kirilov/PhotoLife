using System;
using Moq;
using NUnit.Framework;
using PhotoLife.Providers.Contracts;

namespace PhotoLife.Authentication.Tests.HttpContextAuthenticationProvider
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void _Throw_ArgumentNullException_WhenIHttpContextProvider_IsNull()
        {
            //Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(
                () => new Providers.HttpContextAuthenticationProvider(null));
        }

        [Test]
        public void _NotThrow_ArgumentNullException_WhenIHttpContextProvider_IsCorrect()
        {
            //Arrange 
            var mockedProvider = new Mock<IHttpContextProvider>();

            //Act & Assert
            Assert.DoesNotThrow(
                () => new Providers.HttpContextAuthenticationProvider(mockedProvider.Object));
        }

        [Test]
        public void _Initilize_NotNull_WhenIHttpContextProvider_IsCorrect()
        {
            //Arrange 
            var mockedProvider = new Mock<IHttpContextProvider>();

            //Act
            var provider = new Providers.HttpContextAuthenticationProvider(mockedProvider.Object);

            //Assert
            Assert.IsNotNull(provider);
        }

        [Test]
        public void _Intialize_CorrectInstance_WhenIHttpContextProvider_IsCorrect()
        {
            //Arrange 
            var mockedProvider = new Mock<IHttpContextProvider>();

            //Act
            var provider = new Providers.HttpContextAuthenticationProvider(mockedProvider.Object);

            //Assert
            Assert.IsInstanceOf<Providers.HttpContextAuthenticationProvider>(provider);
        }

    }
}
