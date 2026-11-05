using Microsoft.AspNetCore.Mvc;
using Twisted.API.Contracts.Test;
using Twisted.Domain;
using Twisted.Domain.DTOs;
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
        public async Task<string> TokenPrice()
        {
            var tokenInformation = await _repository.GetTokenInformation();

            var tokenPrice = await _repository.GetWoWTokenPrice(tokenInformation, "dynamic-us", "en_US");

            var test = String.Format("{0:N0}", Convert.ToInt64(tokenPrice)); //this comes back as a huge number. This needs to be formated so it reads like the gold value of wow tokens, not the hughe number.

            return test;
        }
    }
}
