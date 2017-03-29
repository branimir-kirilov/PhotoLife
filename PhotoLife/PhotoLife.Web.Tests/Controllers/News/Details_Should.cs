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
    public class Details_Should
    {
        [TestCase("fake", "fake", "fake", 7)]
        [TestCase("fake", "fake", "fake", 9)]
        public void _Call_NewsService_GetNewsById(string cloud, string apiKey, string apiSecret, int newsId)
        {
            //Arrange
            var model = new AddNewsViewModel();

            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            var mockedNewsService = new Mock<INewsService>();

            var mockedViewModelFactory = new Mock<IViewModelFactory>();
            mockedViewModelFactory.Setup(v => v.CreateAddNewsViewModel(It.IsAny<Cloudinary>())).Returns(model);

            var fakeAcc = new CloudinaryDotNet.Account(cloud, apiKey, apiSecret);
            var mockedCloudinary = new Mock<Cloudinary>(fakeAcc);

            var newsControllerSUT = new NewsController(mockedAuthProvider.Object, mockedNewsService.Object, mockedViewModelFactory.Object, mockedCloudinary.Object);

            //Act
            var res = newsControllerSUT.Details(newsId) as ViewResult;

            //Assert
            mockedNewsService.Verify(n => n.GetNewsById(newsId), Times.Once);
        }

        [TestCase("fake", "fake", "fake", 7)]
        [TestCase("fake", "fake", "fake", 9)]
        public void _Call_ViewModelFActory_CreateNewsDetailsViewModel(string cloud, string apiKey, string apiSecret, int newsId)
        {
            //Arrange
            var model = new NewsDetailsViewModel();
            var news = new Models.News();

            var mockedAuthProvider = new Mock<IAuthenticationProvider>();

            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(n => n.GetNewsById(newsId)).Returns(news);

            var mockedViewModelFactory = new Mock<IViewModelFactory>();
            mockedViewModelFactory.Setup(v => v.CreateNewsDetailsViewModel(It.IsAny<Models.News>())).Returns(model);

            var fakeAcc = new CloudinaryDotNet.Account(cloud, apiKey, apiSecret);
            var mockedCloudinary = new Mock<Cloudinary>(fakeAcc);

            var newsControllerSUT = new NewsController(mockedAuthProvider.Object, mockedNewsService.Object, mockedViewModelFactory.Object, mockedCloudinary.Object);

            //Act
            var res = newsControllerSUT.Details(newsId) as ViewResult;

            //Assert
            mockedViewModelFactory.Verify(v => v.CreateNewsDetailsViewModel(news), Times.Once);
        }


        [TestCase("fake", "fake", "fake", 7)]
        [TestCase("fake", "fake", "fake", 9)]
        public void _Return_View_With_Model(string cloud, string apiKey, string apiSecret, int newsId)
        {
            //Arrange
            var model = new NewsDetailsViewModel();
            var news = new Models.News();

            var mockedAuthProvider = new Mock<IAuthenticationProvider>();

            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(n => n.GetNewsById(newsId)).Returns(news);

            var mockedViewModelFactory = new Mock<IViewModelFactory>();
            mockedViewModelFactory.Setup(v => v.CreateNewsDetailsViewModel(It.IsAny<Models.News>())).Returns(model);

            var fakeAcc = new CloudinaryDotNet.Account(cloud, apiKey, apiSecret);
            var mockedCloudinary = new Mock<Cloudinary>(fakeAcc);

            var newsControllerSUT = new NewsController(mockedAuthProvider.Object, mockedNewsService.Object, mockedViewModelFactory.Object, mockedCloudinary.Object);

            //Act
            var res = newsControllerSUT.Details(newsId) as ViewResult;

            //Assert
            Assert.AreEqual(model, res.Model);
        }
    }
}
