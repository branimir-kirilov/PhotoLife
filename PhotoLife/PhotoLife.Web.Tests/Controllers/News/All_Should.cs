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

namespace PhotoLife.Web.Tests.Controllers.News
{
    [TestFixture]
    public class All_Should
    {
        [Test]
        public void _Call_NewsService_GetAll_Method()
        {
            //Arrange
            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            var mockedNewsService = new Mock<INewsService>();
            var mockedViewModelFactory = new Mock<IViewModelFactory>();

            var fakeAcc = new CloudinaryDotNet.Account("fake", "fake", "fake");
            var mockedCloudinary = new Mock<Cloudinary>(fakeAcc);
            var newsControllerSUT = new NewsController(mockedAuthProvider.Object, mockedNewsService.Object, mockedViewModelFactory.Object, mockedCloudinary.Object);

            //Act 
            newsControllerSUT.All();

            //Assert
            mockedNewsService.Verify(m => m.GetAll(), Times.Once);
        }

        [Test]
        public void _Return_PartialView()
        {
            //Arrange
            var expectedPartialName = "_PagedNewsListPartial";

            var mockedAuthProvider = new Mock<IAuthenticationProvider>();
            var mockedNewsService = new Mock<INewsService>();
            var mockedViewModelFactory = new Mock<IViewModelFactory>();

            var fakeAcc = new CloudinaryDotNet.Account("fake", "fake", "fake");
            var mockedCloudinary = new Mock<Cloudinary>(fakeAcc);
            var newsControllerSUT = new NewsController(mockedAuthProvider.Object, mockedNewsService.Object, mockedViewModelFactory.Object, mockedCloudinary.Object);

            //Act
            var res = newsControllerSUT.All() as PartialViewResult;

            //Assert
            Assert.AreEqual(expectedPartialName, res.ViewName);

        }

    }
}
