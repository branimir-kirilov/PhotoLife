using System;
using NUnit.Framework;
using Moq;
using PhotoLife.Authentication.Providers;
using PhotoLife.Controllers;
using PhotoLife.Services.Contracts;

namespace PhotoLife.Web.Tests.Controllers.Comment
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void _InitializeInstance_Correctly_WhenAllDependencies_AreNotNull()
        {
            //Arrange
            var mockedCommentService = new Mock<ICommentService>();
            var mockedAuthProvider = new Mock<IAuthenticationProvider>();

            //Act
            var commentControllerSUT = new CommentController(mockedCommentService.Object, mockedAuthProvider.Object);

            //Assert
            Assert.IsNotNull(commentControllerSUT);
        }

        [Test]
        public void _Throw_ArgumentNullException_When_CommentService_IsNull()
        {
            //Arrange
            var mockedAuthProvider = new Mock<IAuthenticationProvider>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CommentController(null, mockedAuthProvider.Object));
        }

        [Test]
        public void _Throw_ArgumentNullException_When_AuthenticationPRovider_IsNull()
        {
            //Arrange
            var mockedCommentService = new Mock<ICommentService>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CommentController(mockedCommentService.Object, null));
        }
    }
}
