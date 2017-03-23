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
            //Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
           
            //Act & Assert
            Assert.Throws<ArgumentNullException>(
                () => new Providers.HttpContextAuthenticationProvider(null, mockedDateTimeProvider.Object));
        }

        [Test]
        public void _Throw_ArgumentNullException_WhenIDateTimeProvider_IsNull()
        {
            //Arrange
            var mockedHttpContextProvider = new Mock<IHttpContextProvider>();
            
            //Act & Assert
            Assert.Throws<ArgumentNullException>(
                () => new Providers.HttpContextAuthenticationProvider(mockedHttpContextProvider.Object, null));
        }

        [Test]
        public void _NotThrow_ArgumentNullException_WhenEverything_IsCorrect()
        {
            //Arrange 
            var mockedProvider = new Mock<IHttpContextProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act & Assert
            Assert.DoesNotThrow(
                () => new Providers.HttpContextAuthenticationProvider(mockedProvider.Object, mockedDateTimeProvider.Object));
        }

        [Test]
        public void _Initilize_NotNull_WhenIHttpContextProvider_IsCorrect()
        {
            //Arrange 
            var mockedProvider = new Mock<IHttpContextProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();


            //Act
            var provider = new Providers.HttpContextAuthenticationProvider(mockedProvider.Object, mockedDateTimeProvider.Object);

            //Assert
            Assert.IsNotNull(provider);
        }

        [Test]
        public void _Intialize_CorrectInstance_WhenIHttpContextProvider_IsCorrect()
        {
            //Arrange 
            var mockedProvider = new Mock<IHttpContextProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act
            var provider = new Providers.HttpContextAuthenticationProvider(mockedProvider.Object, mockedDateTimeProvider.Object);

            //Assert
            Assert.IsInstanceOf<Providers.HttpContextAuthenticationProvider>(provider);
        }

    }
}
