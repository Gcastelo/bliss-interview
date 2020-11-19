using NUnit.Framework;
using Bliss.Api.Controllers;

namespace Bliss.Api.Tests.Controllers
{
    public class QuestionsControllerTests
    {
        QuestionsController controller;

        [SetUp]
        public void Setup()
        {
            controller = new QuestionsController(null);
        }

        [Test]
        public void GetReturnsListOfQuestions()
        {
            

            
            Assert.Pass();
        }
    }
}