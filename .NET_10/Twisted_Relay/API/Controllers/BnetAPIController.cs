using Microsoft.AspNetCore.Mvc;
using Twisted.UseCases.Handlers;

namespace Twisted.API.Controllers
{
    [ApiController]
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

            //I decided not to store this token in the db but instead store it in a static class that holds that information during runtime.
            //I still would like to know how encryption works so I will still encrypt it just for fun and learn how to decrypt it as well.
            //Just need to look into how to do that. I will probably make it into it's own handler/functionality so I can call it whenever I need to.
            //going to make a test controller as well so I can call things when I want to test out my repo without vastly altering the controllers and solution.

            //we should also authorize this endpoint to prevent anyone from calling it and using our api key and token.
            //very important if not the most important aspect if I plan to deploy this

            //need to also figure out how to set up my cors policy because why not. Don't really need it besides on localhost but it would be nice if I deploy it at all.
        }
    }
}
