using Microsoft.AspNetCore.Mvc;
using Twisted.API.Attributes;
using Twisted.UseCases.Handlers;

namespace Twisted.API.Controllers
{
    [ApiController]
    [ApiKey]
    [Route("[controller]")]
    public class BnetAPIController : ControllerBase
    {
        private readonly IBnetAPIHandler _handler;

        public BnetAPIController(IBnetAPIHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        [Route("TokenPrice")]
        public async Task<string> GetTokenPriceAsync()
        {
            var tokenInformation = await _handler.GetOauth2Token();
            return await _handler.GetTokenPriceAsync(tokenInformation);
        }
    }
}
