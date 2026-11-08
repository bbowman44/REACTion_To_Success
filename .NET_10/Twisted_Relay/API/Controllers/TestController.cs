using Microsoft.AspNetCore.Mvc;
using Twisted.UseCases.Handlers;

namespace Twisted.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        private readonly IEncryptionHandler _encryptHandler;

        public TestController(IEncryptionHandler encryptionHandler)
        {
            _encryptHandler = encryptionHandler;
        }

        [HttpGet]
        [Route("SymmetricEncrypt")]
        public async Task<string> SymmetricEncrpytionTest()
        {
            //need to call the symmetric encryption function
            var testResult = _encryptHandler.SymmetricEncrpytionTest();
            return "test controller results";
        }

        [HttpGet]
        [Route("AsymmetricEncrypt")]
        public async Task<string> AsymmetricEncrpytionTest()
        {
            //need to call the 
            var testResult = _encryptHandler.AsymmetricEncrpytionTest();
            return "test controller results";
        }
    }
}
