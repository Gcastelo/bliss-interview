using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bliss.Data;

namespace Bliss.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly ILogger<QuestionsController> _logger;

        public QuestionsController(ILogger<QuestionsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> Get([FromQuery]int limit, [FromQuery]int offset = 0, [FromQuery]String filter = null)
        {
          return await Task.Run( () => new List<Question>());
        }

        [HttpGet]
        public async Task<ActionResult<Question>> GetById(int id)
        {
          return  await Task.Run( () => new Question{});
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Question question)
        {
          await Task.Run( () =>  null );

          return CreatedAtAction(nameof(GetById), 0, new Question());
        }

        
        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody]Question question)
        {
          await Task.Run( () =>  null );

          return NoContent();
        }
    }
}
