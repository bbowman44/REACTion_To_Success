using Microsoft.AspNetCore.Mvc;
using Twisted.API.Contracts.Test;
using Twisted.Domain;
using Twisted.Infrastructure.Interfaces;

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

            //after we get the token information we should store it in the database with a timestamp so we can check that it's valid
            //this will help us avoiding unecessary calls to the oauth2 endpoint and the token can stay the same as well for a while.

            //we should also encrypt this information before we store it in the DB as well.
            //would need to figure out how to encrypt it but more importantly how to decrypt it and if a key is needed and where to store it.

            //we should also authorize this endpoint to prevent anyone from calling it and using our api key and token.
            //very important if not the most important aspect

            //this should also call the handler that can route all the information to the correct places like the external api, the database, the encrypt/decrypt functions, etc.

            var tokenPrice = await _repository.GetWoWTokenPrice(tokenInformation, "dynamic-us", "en_US");

            var test = String.Format("{0:N0}", Convert.ToInt64(tokenPrice)); //this comes back as a huge number. This needs to be formated so it reads like the gold value of wow tokens, not the hughe number.

            return test;
        }
    }
}
