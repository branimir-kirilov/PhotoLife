using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using PhotoLife.Authentication.Providers;
using PhotoLife.Controllers;
using PhotoLife.Services.Contracts;
using PhotoLife.ViewModels.Comment;

namespace PhotoLife.Web.Tests.Controllers.Comment
{
    [TestFixture]
    public class CommentPost_Should
    {
        [Test]
        public void _Call_AuthProvider_CurrentUserId()
        {
            //Arrange
            var model = new AddCommentViewModel();

            var mockedCommentService = new Mock<ICommentService>();
            var mockedAuthProvider = new Mock<IAuthenticationProvider>();

            var commentControllerSUT = new CommentController(mockedCommentService.Object, mockedAuthProvider.Object);
            commentControllerSUT.ModelState.Clear();

            //Act
            var res = commentControllerSUT.CommentPost(model) as ActionResult;

            //Assert
            mockedAuthProvider.Verify(a => a.CurrentUserId, Times.Once);
        }


        [TestCase(7, "userid1", "SomeFakeContent")]
        [TestCase(9, "userid7", "Other Fake Content <h1>Content</h1>")]
        public void _Call_CommentService_AddCommentToPost(int postId, string userId, string content)
        {
            //Arrange
            var model = new AddCommentViewModel()
            {
                Content = content,
                CommentedItemId = postId
            };

            var mockedCommentService = new Mock<ICommentService>();

            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            mockedAuthProvider.Setup(a => a.CurrentUserId).Returns(userId);

            var commentControllerSUT = new CommentController(mockedCommentService.Object, mockedAuthProvider.Object);
            commentControllerSUT.ModelState.Clear();

            //Act
            var res = commentControllerSUT.CommentPost(model) as ActionResult;

            //Assert
            mockedCommentService.Verify(s => s.AddCommentToPost(model.Content, model.CommentedItemId, userId));
        }
        
        
        [TestCase(7, "userid1", "SomeFakeContent")]
        [TestCase(9, "userid7", "Other Fake Content <h1>Content</h1>")]
        public void _Return_Correct_Action_RedirectToAction_AddCommentToPost(int postId, string userId, string content)
        {
            //Arrange
            var model = new AddCommentViewModel()
            {
                Content = content,
                CommentedItemId = postId
            };

            var mockedCommentService = new Mock<ICommentService>();

            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            mockedAuthProvider.Setup(a => a.CurrentUserId).Returns(userId);

            var commentControllerSUT = new CommentController(mockedCommentService.Object, mockedAuthProvider.Object);
            commentControllerSUT.ModelState.Clear();

            //Act
            var res = commentControllerSUT.CommentPost(model) as RedirectToRouteResult;
            
            //Assert
            Assert.AreEqual("Details", res.RouteValues["action"]);
        }

        [TestCase(7, "userid1", "SomeFakeContent")]
        [TestCase(9, "userid7", "Other Fake Content <h1>Content</h1>")]
        public void _Return_Correct_Controller_RedirectToAction_AddCommentToPost(int postId, string userId, string content)
        {
            //Arrange
            var model = new AddCommentViewModel()
            {
                Content = content,
                CommentedItemId = postId
            };

            var mockedCommentService = new Mock<ICommentService>();

            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            mockedAuthProvider.Setup(a => a.CurrentUserId).Returns(userId);

            var commentControllerSUT = new CommentController(mockedCommentService.Object, mockedAuthProvider.Object);
            commentControllerSUT.ModelState.Clear();

            //Act
            var res = commentControllerSUT.CommentPost(model) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual("Post", res.RouteValues["controller"]);
        }

        [TestCase(7, "userid1", "SomeFakeContent")]
        [TestCase(9, "userid7", "Other Fake Content <h1>Content</h1>")]
        public void _Return_RedirectToRouteResult_WithFalsePermanenet_CommentToPost(int postId, string userId, string content)
        {
            //Arrange
            var model = new AddCommentViewModel()
            {
                Content = content,
                CommentedItemId = postId
            };

            var mockedCommentService = new Mock<ICommentService>();

            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            mockedAuthProvider.Setup(a => a.CurrentUserId).Returns(userId);

            var commentControllerSUT = new CommentController(mockedCommentService.Object, mockedAuthProvider.Object);
            commentControllerSUT.ModelState.Clear();

            //Act
            var res = commentControllerSUT.CommentPost(model) as RedirectToRouteResult;

            //Assert
            Assert.IsFalse(res.Permanent);
        }
    }
}
