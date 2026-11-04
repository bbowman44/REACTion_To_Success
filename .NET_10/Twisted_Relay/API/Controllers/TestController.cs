using Microsoft.AspNetCore.Mvc;
using Twisted.API.Contracts.Test;
using Twisted.Domain;
using Twisted.Infrastructure.ExternalAPIs;

namespace Twisted.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IBnetAPI _repository;

        public TestController(IBnetAPI bNetAPI)
        {
            _repository = bNetAPI;
        }

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

        [HttpGet]
        [Route("BnetAPI/TokenPrice")]
        public async Task<int> TokenPrice()
        {
            var temp = await _repository.GetTokenInformation();
            return await _repository.GetTokenInformation2();
        }
    }
}
