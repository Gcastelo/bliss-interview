using NUnit.Framework;
using Bliss.Api.Controllers;
using Bliss.Data.Provider;
using System.Threading.Tasks;
using Moq;
using System.Collections.Generic;
using Bliss.Data;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Bliss.Api.Tests.Controllers
{
    public class ShareControllerTests
    {
        ShareController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new ShareController(null);
        }

        [Test]
        public void ShareReturnsOkResult()
        {
            string email = "";
            string content_url = "";

            var result =  _controller.Share(email, content_url);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}