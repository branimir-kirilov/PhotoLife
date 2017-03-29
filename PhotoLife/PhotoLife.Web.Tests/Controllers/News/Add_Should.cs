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
using PhotoLife.ViewModels.News;

namespace PhotoLife.Web.Tests.Controllers.News
{
    [TestFixture]
    public class Add_Should
    {
        [Test]
        public void _Call_ViewModelFactory_CreateAddNewsViewModel()
        {
            //Arrange
            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            var mockedNewsService = new Mock<INewsService>();
            var mockedViewModelFactory = new Mock<IViewModelFactory>();

            var fakeAcc = new CloudinaryDotNet.Account("fake", "fake", "fake");
            var mockedCloudinary = new Mock<Cloudinary>(fakeAcc);
            var newsControllerSUT = new NewsController(mockedAuthProvider.Object, mockedNewsService.Object, mockedViewModelFactory.Object, mockedCloudinary.Object);

            //Act
            var res = newsControllerSUT.Add() as ViewResult;

            //Assert
            mockedViewModelFactory.Verify(m => m.CreateAddNewsViewModel(mockedCloudinary.Object), Times.Once);
        }

        [Test]
        public void _Return_View_With_Model()
        {
            //Arrange
            var model = new AddNewsViewModel();

            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            var mockedNewsService = new Mock<INewsService>();

            var mockedViewModelFactory = new Mock<IViewModelFactory>();
            mockedViewModelFactory.Setup(v => v.CreateAddNewsViewModel(It.IsAny<Cloudinary>())).Returns(model);

            var fakeAcc = new CloudinaryDotNet.Account("fake", "fake", "fake");
            var mockedCloudinary = new Mock<Cloudinary>(fakeAcc);
            var newsControllerSUT = new NewsController(mockedAuthProvider.Object, mockedNewsService.Object, mockedViewModelFactory.Object, mockedCloudinary.Object);

            //Act
            var res = newsControllerSUT.Add() as ViewResult;

            //Assert
            Assert.AreEqual(model, res.Model);
        }

        [Test]
        public void _Return_ith_Model()
        {
            //Arrange
            var fakeAcc = new CloudinaryDotNet.Account("fake", "fake", "fake");
            var mockedCloudinary = new Mock<Cloudinary>(fakeAcc);

            var model = new AddNewsViewModel()
            {
                Title = "Some title",
                Text = "Some text",
                Cloudinary = mockedCloudinary.Object
            };

            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            mockedAuthProvider.Setup(a => a.CurrentUserId).Returns("userId");

            var mockedNewsService = new Mock<INewsService>();

            var mockedViewModelFactory = new Mock<IViewModelFactory>();
            mockedViewModelFactory.Setup(v => v.CreateAddNewsViewModel(It.IsAny<Cloudinary>())).Returns(model);
            
            var newsControllerSUT = new NewsController(mockedAuthProvider.Object, mockedNewsService.Object, mockedViewModelFactory.Object, mockedCloudinary.Object);
            newsControllerSUT.ModelState.Clear();

            //Act
            var res = newsControllerSUT.Add(model) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual("News", res.RouteValues["action"]);
        }
    }
}
