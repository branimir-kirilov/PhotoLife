using System.Web.Mvc;
using CloudinaryDotNet;
using Moq;
using NUnit.Framework;
using PhotoLife.Authentication.Providers;
using PhotoLife.Controllers;
using PhotoLife.Factories;
using PhotoLife.Models.Enums;
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
        public void _Return_RedirectToRouteResult_WithFalsePermanent()
        {
            //Arrange
            var fakeAcc = new CloudinaryDotNet.Account("fake", "fake", "fake");
            var mockedCloudinary = new Mock<Cloudinary>(fakeAcc);

            var model = new AddNewsViewModel(mockedCloudinary.Object)
            {
                Title = "Some title",
                Text = "Some text",
                Category = Models.Enums.CategoryEnum.Abstract,
                CoverPicture = "SomeUrl",
            };

            var news = new Models.News();

            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            mockedAuthProvider.Setup(a => a.CurrentUserId).Returns("userId");

            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(s => s.CreateNews(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CategoryEnum>())).Returns(news);

            var mockedViewModelFactory = new Mock<IViewModelFactory>();
            mockedViewModelFactory.Setup(v => v.CreateAddNewsViewModel(It.IsAny<Cloudinary>())).Returns(model);

            var newsControllerSUT = new NewsController(mockedAuthProvider.Object, mockedNewsService.Object, mockedViewModelFactory.Object, mockedCloudinary.Object);
            newsControllerSUT.ModelState.Clear();

            //Act
            var res = newsControllerSUT.Add(model) as RedirectToRouteResult;

            //Assert
            Assert.IsFalse(res.Permanent);
        }

        [Test]
        public void _Return_RedirectToRoute_WithCorrect_Controller()
        {
            //Arrange
            var fakeAcc = new CloudinaryDotNet.Account("fake", "fake", "fake");
            var mockedCloudinary = new Mock<Cloudinary>(fakeAcc);

            var model = new AddNewsViewModel(mockedCloudinary.Object)
            {
                Title = "Some title",
                Text = "Some text",
                Category = Models.Enums.CategoryEnum.Abstract,
                CoverPicture = "SomeUrl",
            };

            var news = new Models.News();

            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            mockedAuthProvider.Setup(a => a.CurrentUserId).Returns("userId");

            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(s => s.CreateNews(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CategoryEnum>())).Returns(news);

            var mockedViewModelFactory = new Mock<IViewModelFactory>();
            mockedViewModelFactory.Setup(v => v.CreateAddNewsViewModel(It.IsAny<Cloudinary>())).Returns(model);

            var newsControllerSUT = new NewsController(mockedAuthProvider.Object, mockedNewsService.Object, mockedViewModelFactory.Object, mockedCloudinary.Object);
            newsControllerSUT.ModelState.Clear();

            //Act
            var res = newsControllerSUT.Add(model) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual("Details", res.RouteValues["action"]);
        }

        [Test]
        public void _Return_RedirectToRoute_WithCorrect_Action()
        {
            //Arrange
            var fakeAcc = new CloudinaryDotNet.Account("fake", "fake", "fake");
            var mockedCloudinary = new Mock<Cloudinary>(fakeAcc);

            var model = new AddNewsViewModel(mockedCloudinary.Object)
            {
                Title = "Some title",
                Text = "Some text",
                Category = Models.Enums.CategoryEnum.Abstract,
                CoverPicture = "SomeUrl",
            };

            var news = new Models.News();

            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            mockedAuthProvider.Setup(a => a.CurrentUserId).Returns("userId");

            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(s => s.CreateNews(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CategoryEnum>())).Returns(news);

            var mockedViewModelFactory = new Mock<IViewModelFactory>();
            mockedViewModelFactory.Setup(v => v.CreateAddNewsViewModel(It.IsAny<Cloudinary>())).Returns(model);

            var newsControllerSUT = new NewsController(mockedAuthProvider.Object, mockedNewsService.Object, mockedViewModelFactory.Object, mockedCloudinary.Object);
            newsControllerSUT.ModelState.Clear();

            //Act
            var res = newsControllerSUT.Add(model) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual("News", res.RouteValues["controller"]);
        }

        [Test]
        public void _Call_AuthProvider_CurrentUserId()
        {
            //Arrange
            var fakeAcc = new CloudinaryDotNet.Account("fake", "fake", "fake");
            var mockedCloudinary = new Mock<Cloudinary>(fakeAcc);

            var model = new AddNewsViewModel(mockedCloudinary.Object)
            {
                Title = "Some title",
                Text = "Some text",
                Category = Models.Enums.CategoryEnum.Abstract,
                CoverPicture = "SomeUrl",
            };

            var news = new Models.News();

            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            mockedAuthProvider.Setup(a => a.CurrentUserId).Returns("userId");

            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(s => s.CreateNews(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CategoryEnum>())).Returns(news);

            var mockedViewModelFactory = new Mock<IViewModelFactory>();
            mockedViewModelFactory.Setup(v => v.CreateAddNewsViewModel(It.IsAny<Cloudinary>())).Returns(model);

            var newsControllerSUT = new NewsController(mockedAuthProvider.Object, mockedNewsService.Object, mockedViewModelFactory.Object, mockedCloudinary.Object);
            newsControllerSUT.ModelState.Clear();

            //Act
            var res = newsControllerSUT.Add(model) as RedirectToRouteResult;

            //Assert
            mockedAuthProvider.Verify(a => a.CurrentUserId, Times.Once);
        }

        [TestCase("userId", "Title1", "Some text", CategoryEnum.BlackAndWhite, "some-url.com")]
        [TestCase("userId", "Title7", "Some text", CategoryEnum.Celebrity, "some-url.com")]
        public void _Call_NewsService_CreateNews(
            string userId,
            string title,
            string text,
            CategoryEnum category,
            string coverPicUrl)
        {
            //Arrange
            var fakeAcc = new CloudinaryDotNet.Account("fake", "fake", "fake");
            var mockedCloudinary = new Mock<Cloudinary>(fakeAcc);

            var model = new AddNewsViewModel(mockedCloudinary.Object)
            {
                Title = title,
                Text = text,
                Category = category,
                CoverPicture = coverPicUrl,
            };

            var news = new Models.News();

            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            mockedAuthProvider.Setup(a => a.CurrentUserId).Returns(userId);

            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(s => s.CreateNews(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CategoryEnum>())).Returns(news);

            var mockedViewModelFactory = new Mock<IViewModelFactory>();
            mockedViewModelFactory.Setup(v => v.CreateAddNewsViewModel(It.IsAny<Cloudinary>())).Returns(model);

            var newsControllerSUT = new NewsController(mockedAuthProvider.Object, mockedNewsService.Object, mockedViewModelFactory.Object, mockedCloudinary.Object);
            newsControllerSUT.ModelState.Clear();

            //Act
            var res = newsControllerSUT.Add(model) as RedirectToRouteResult;

            //Assert
            mockedNewsService.Verify(n => n.CreateNews(userId, title, text, coverPicUrl, category), Times.Once);
        }
    }
}
