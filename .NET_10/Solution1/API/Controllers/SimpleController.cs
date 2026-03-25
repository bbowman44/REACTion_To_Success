using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimpleController
    {
        private readonly ILogger<SimpleController> _logger;
        public SimpleController(ILogger<SimpleController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("hello")]
        public string HelloWorld()
        {
            _logger.LogInformation("HelloWorld endpoint was called.");
            return "Hello, World!";
        }
    }
}
