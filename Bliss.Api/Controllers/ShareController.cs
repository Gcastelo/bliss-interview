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
    public class ShareController : ControllerBase
    {

        private readonly ILogger<ShareController> _logger;

        public ShareController(ILogger<ShareController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Share([FromQuery] string email, [FromQuery] string content_url)
        {
          return Ok(new ResponseStatus{ Status = "OK"});
        }

    }
}
