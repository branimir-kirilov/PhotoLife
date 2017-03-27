using System.Web.Mvc;
using CloudinaryDotNet;
using Moq;
using NUnit.Framework;
using PhotoLife.Authentication.Providers;
using PhotoLife.Controllers;
using PhotoLife.Factories;
using PhotoLife.Services.Contracts;
using PhotoLife.ViewModels.Post;

namespace PhotoLife.Web.Tests.Controllers.Post
{
    [TestFixture]
    public class Details_Should
    {
        [TestCase("fake", "fake", "fake", 7)]
        public void _Returns_View_WithModel(
            string cloud, string apiKey, string apiSecret, int postId)
        {
            //Arrange
            var model = new AddPostViewModel();

            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            var mockedPostService = new Mock<IPostService>();

            var mockedViewModelFactory = new Mock<IViewModelFactory>();
            mockedViewModelFactory.Setup(v => v.CreateAddPostViewModel(It.IsAny<Cloudinary>())).Returns(model);

            var mockedAcc = new CloudinaryDotNet.Account(cloud, apiKey, apiSecret);
            var mockedCloudinary = new Mock<Cloudinary>(mockedAcc);

            var postController = new PostController(mockedAuthProvider.Object, mockedPostService.Object,
                mockedViewModelFactory.Object, mockedCloudinary.Object);

            //Act
            var res = postController.Details(postId) as ViewResult;

            //Assert
            mockedPostService.Verify(s => s.GetPostById(postId), Times.Once);
        }

        [TestCase("fake", "fake", "fake", 7)]
        public void _Call_ViewModelFactory_CreatePostDetailsViewModel(
           string cloud, string apiKey, string apiSecret, int postId)
        {
            //Arrange
            var post = new Models.Post();

            var mockedAuthProvider = new Mock<IAuthenticationProvider>();

            var mockedPostService = new Mock<IPostService>();
            mockedPostService.Setup(s=>s.GetPostById(It.IsAny<int>())).Returns(post);

            var mockedViewModelFactory = new Mock<IViewModelFactory>();

            var mockedAcc = new CloudinaryDotNet.Account(cloud, apiKey, apiSecret);
            var mockedCloudinary = new Mock<Cloudinary>(mockedAcc);

            var postController = new PostController(mockedAuthProvider.Object, mockedPostService.Object,
                mockedViewModelFactory.Object, mockedCloudinary.Object);

            //Act
            var res = postController.Details(postId) as ViewResult;

            //Assert
            mockedViewModelFactory.Verify(v => v.CreatePostDetailsViewModel(post), Times.Once);
        }

        [TestCase("fake", "fake", "fake", 7)]
        public void _Returns_Correct_Model(
           string cloud, string apiKey, string apiSecret, int postId)
        {
            //Arrange
            var post = new Models.Post();
            var model = new PostDetailsViewModel();

            var mockedAuthProvider = new Mock<IAuthenticationProvider>();

            var mockedPostService = new Mock<IPostService>();
            mockedPostService.Setup(s => s.GetPostById(It.IsAny<int>())).Returns(post);

            var mockedViewModelFactory = new Mock<IViewModelFactory>();
            mockedViewModelFactory.Setup(v => v.CreatePostDetailsViewModel(It.IsAny<Models.Post>())).Returns(model);

            var mockedAcc = new CloudinaryDotNet.Account(cloud, apiKey, apiSecret);
            var mockedCloudinary = new Mock<Cloudinary>(mockedAcc);

            var postController = new PostController(mockedAuthProvider.Object, mockedPostService.Object,
                mockedViewModelFactory.Object, mockedCloudinary.Object);

            //Act
            var res = postController.Details(postId) as ViewResult;

            //Assert
            Assert.AreEqual(model, res.Model);
        }


    }
}
