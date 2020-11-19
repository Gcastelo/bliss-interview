using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bliss.Data;
using Bliss.Data.Provider;
using System.Linq;

namespace Bliss.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly ILogger<QuestionsController> _logger;
        private readonly IDataProvider _provider;
        public QuestionsController(ILogger<QuestionsController> logger, IDataProvider provider)
        {
            _provider = provider;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int limit = -1, [FromQuery] int offset = 0, [FromQuery] String filter = null)
        {
            var questions = await _provider.Get(limit, offset, filter);
            return Ok(questions);
        }

        [HttpGet("/questions/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var question = await _provider.GetById(id);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewQuestion question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseStatus() { Status = "All fields are mandatory" });
            }

            SavedQuestion savedQuestion = new SavedQuestion();
            savedQuestion.ImageUrl = question.ImageUrl;
            savedQuestion.ThumbUrl = question.ThumbUrl;
            savedQuestion.QuestionText = question.QuestionText;
            savedQuestion.Choices = question.Choices.Select(c => new ChoicesVotes() { Choice = c, Votes = 0 }).ToList();
            savedQuestion.PublishedAt = DateTime.Now;

            string id = await _provider.Save(savedQuestion);

            return CreatedAtAction(nameof(GetById), new { Id = id }, savedQuestion);
        }


        [HttpPut]
        public async Task<IActionResult> Put(string id, [FromBody] SavedQuestion question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseStatus() { Status = "All fields are mandatory" });
            }

            await _provider.Save(question);

            return CreatedAtAction(nameof(GetById), new { Id = id }, question);
        }
    }
}
