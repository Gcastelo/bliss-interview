using NUnit.Framework;
using Bliss.Api.Controllers;
using Bliss.Data.Provider;
using System.Threading.Tasks;
using Moq;
using System.Collections.Generic;
using Bliss.Data;
using Microsoft.AspNetCore.Mvc;

namespace Bliss.Api.Tests.Controllers
{
    public class HealthControllerTests
    {
        HealthController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new HealthController(null);
        }

        [Test]
        public void GetReturnsOKResult()
        {
            var result =  _controller.Get();

            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}