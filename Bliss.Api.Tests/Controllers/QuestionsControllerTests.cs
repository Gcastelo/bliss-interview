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
    public class QuestionsControllerTests
    {
        QuestionsController controller;
        Mock<IDataProvider> _mockProvider;

        [SetUp]
        public void Setup()
        {

            _mockProvider = new Mock<IDataProvider>();
        }

        [Test]
        public async Task GetReturnsListOfQuestions()
        {
            _mockProvider.Setup(x => x.Get(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(() => new List<SavedQuestion>());
            controller = new QuestionsController(null, _mockProvider.Object);

            var result = await controller.Get();

            Assert.IsInstanceOf<OkObjectResult>(result);
        }


        [Test]
        public async Task GetByIdReturnsOkWhenQuestionExists()
        {
            string _testId = "";
            _mockProvider.Setup(x => x.GetById(_testId)).ReturnsAsync(() => new SavedQuestion());
            controller = new QuestionsController(null, _mockProvider.Object);

            var result = await controller.GetById(_testId);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task GetByIdReturnsNotFoundWhenQuestionDoesNotExist()
        {
            string _testId = "";
            _mockProvider.Setup(x => x.GetById(_testId)).ReturnsAsync(() => new SavedQuestion());
            controller = new QuestionsController(null, _mockProvider.Object);

            var result = await controller.GetById(null);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task PostReturnsCreatedAtActionResultWhenQuestionIsValidAndStored()
        {

            NewQuestion question = new NewQuestion { Choices = new[] { Choices.ObjectiveC }, ImageUrl = "", QuestionText = "Some question", ThumbUrl = "" };

            _mockProvider.Setup(x => x.Save(It.IsAny<SavedQuestion>())).ReturnsAsync(() => "");
            controller = new QuestionsController(null, _mockProvider.Object);

            var result = await controller.Post(question);

            Assert.IsInstanceOf<CreatedAtActionResult>(result);
        }

        [Test]
        public async Task PostReturnsCreatedAtBadResultWhenQuestionDoesNotHaveAllMandatoryFields()
        {
            NewQuestion question = new NewQuestion { };

            _mockProvider.Setup(x => x.Save(It.IsAny<SavedQuestion>())).ReturnsAsync(() => "");
            controller = new QuestionsController(null, _mockProvider.Object);
            controller.ModelState.AddModelError("InvalidFields", "All fields are mandatory");

            var result = await controller.Post(question);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual("All fields are mandatory", ((ResponseStatus)(result as BadRequestObjectResult).Value).Status);
        }

        [Test]
        public async Task PutReturnsCreatedAtActionResultWhenQuestionIsValidAndStored()
        {
            SavedQuestion question = new SavedQuestion { Choices = new[] { new ChoicesVotes { Choice = Choices.ObjectiveC, Votes = 0 } }, ImageUrl = "", QuestionText = "Some question", ThumbUrl = "" };

            _mockProvider.Setup(x => x.Save(question)).ReturnsAsync(() => "");
            controller = new QuestionsController(null, _mockProvider.Object);

            var result = await controller.Put(1, question);

            Assert.IsInstanceOf<CreatedAtActionResult>(result);
        }

        [Test]
        public async Task PutReturnsCreatedAtBadResultWhenQuestionDoesNotHaveAllMandatoryFields()
        {
            SavedQuestion question = new SavedQuestion { };

            _mockProvider.Setup(x => x.Save(question)).ReturnsAsync(() => "");
            controller = new QuestionsController(null, _mockProvider.Object);
            controller.ModelState.AddModelError("InvalidFields", "All fields are mandatory");

            var result = await controller.Put(1, question);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual("All fields are mandatory", ((ResponseStatus)(result as BadRequestObjectResult).Value).Status);
        }
    }
}