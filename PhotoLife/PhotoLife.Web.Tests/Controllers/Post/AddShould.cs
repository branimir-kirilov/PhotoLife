using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class AddShould
    {
        [TestCase("fake", "fake", "fake")]
        public void _Call_ViewModelFactory(string cloud, string apiKey, string apiSecret)
        {
            //Arrange
            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            var mockedPostService = new Mock<IPostService>();
            var mockedViewModelFactory = new Mock<IViewModelFactory>();

            var mockedAcc = new CloudinaryDotNet.Account(cloud, apiKey, apiSecret);
            var mockedCloudinary = new Mock<Cloudinary>(mockedAcc);

            var postController = new PostController(mockedAuthProvider.Object, mockedPostService.Object,
                mockedViewModelFactory.Object, mockedCloudinary.Object);

            //Act
            var res = postController.Add() as ViewResult;

            //Assert
            mockedViewModelFactory.Verify(v => v.CreateAddPostViewModel(mockedCloudinary.Object), Times.Once);
        }

        [TestCase("fake", "fake", "fake")]
        public void _Returns_View_WithModel(string cloud, string apiKey, string apiSecret)
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
            var res = postController.Add() as ViewResult;

            //Assert
            Assert.AreEqual(model, res.Model);
        }
    }
}
