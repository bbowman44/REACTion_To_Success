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


            //documentation for authorization and jwt set up
            //https://learn.microsoft.com/en-us/aspnet/core/security/authentication/jwt-authn?view=aspnetcore-10.0
            //https://learn.microsoft.com/en-us/aspnet/core/security/authentication/configure-jwt-bearer-authentication?view=aspnetcore-10.0

            //need to also figure out how to set up my cors policy because why not. Don't really need it besides on localhost but it would be nice if I deploy it at all.
        }
    }
}
