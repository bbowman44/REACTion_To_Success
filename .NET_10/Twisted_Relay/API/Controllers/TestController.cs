using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Twisted.API.Attributes;
using Twisted.UseCases.Handlers;

namespace Twisted.API.Controllers
{
    [ApiController]
    [ApiKey]
    [Route("[controller]")]
    public class TestController : Controller
    {
        private readonly IEncryptionHandler _encryptHandler;

        public TestController(IEncryptionHandler encryptionHandler)
        {
            _encryptHandler = encryptionHandler;
        }

        //I should also try to implement a hashing method as well for passwords and what not
        //I didn't know there was a difference between encryption and hashing!

        [HttpGet]
        [Route("SymmetricEncrypt")]
        public async Task<string> SymmetricEncrpytionTest()
        {
            //need to call the symmetric encryption function
            var testResult = _encryptHandler.SymmetricEncrpytionTest();
            return testResult;
        }

        [HttpGet]
        [Route("AsymmetricEncrypt")]
        public async Task<string> AsymmetricEncrpytionTest()
        {
            //need to call the 
            var testResult = _encryptHandler.AsymmetricEncrpytionTest("This is a test of rsa asymmtetric encryption");
            return testResult;
        }

        [HttpGet]
        [Route("Hashing")]
        public async Task<string> HashingTest()
        {
            //need to call the symmetric encryption function
            var testResult = _encryptHandler.HashTest();
            return testResult;
        }
    }
}
