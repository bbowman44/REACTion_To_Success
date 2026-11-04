using Microsoft.AspNetCore.Mvc;
using Twisted.API.Contracts.Test;
using Twisted.Domain;

namespace Twisted.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route("example")]
        public int Example()
        {
            return 42;
        }

        [HttpGet]
        [Route("StringExample")]
        public TestDTO StringExample(TestRequest TR)
        {
            return new TestDTO
            {
                FieldString = TR.TestString,
                FieldInt = TR.TestInt,
                FieldBool = TR.TestBool
            };
        }
    }
}
