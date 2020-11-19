using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bliss.Data;
using Bliss.Data.Provider;

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
        public async Task<IActionResult> Get([FromQuery]int limit = -1, [FromQuery]int offset = 0, [FromQuery]String filter = null)
        {
          var questions = await _provider.Get(limit, offset, filter);
          return Ok(questions);
        }

        [HttpGet("/questions/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
          var question =  await _provider.GetById(id);
          if( question == null){
            return NotFound();
          }
          
          return Ok(question);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Question question)
        {
          if(!ModelState.IsValid){
            return BadRequest(new ResponseStatus(){Status = "All fields are mandatory"});
          }

          await _provider.Save(question);

          return CreatedAtAction(nameof(GetById), 0, question);
        }

        
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody]Question question)
        {
          if(!ModelState.IsValid){
            return BadRequest(new ResponseStatus(){Status = "All fields are mandatory"});
          }

          await _provider.Save(question);

          return CreatedAtAction(nameof(GetById), 0, question);
        }
    }
}
