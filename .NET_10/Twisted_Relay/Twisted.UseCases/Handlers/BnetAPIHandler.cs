using Microsoft.Extensions.Configuration;
using Twisted.Domain.DTOs;
using Twisted.Domain.Interfaces;
using Twisted.Domain.Models;
using Twisted.Infrastructure.Interfaces;

namespace Twisted.UseCases.Handlers
{
    public class BnetAPIHandler : IBnetAPIHandler
    {
        private readonly IBnetOAuthAPI _oauthRepository;
        private readonly IBnetWoWRetailAPI _wowRetailRepository;
        private readonly IBnetAPIServices _services;
        private readonly IConfiguration _configuration;
        public BnetAPIHandler(IBnetOAuthAPI oauthRepository, IBnetWoWRetailAPI wowRetailRepository, IBnetAPIServices services, IConfiguration configuration)
        {
            _oauthRepository = oauthRepository;
            _wowRetailRepository = wowRetailRepository;
            _services = services;
            _configuration = configuration;
        }
        public async Task<string> GetTokenPriceAsync(BnetTokenDTO tokenInformation)
        {
            var tokenPrice = await _wowRetailRepository.GetWoWTokenPrice(tokenInformation, "dynamic-us", "en_US");

            var formattedTokenString = _services.FormatTokenPrice(tokenPrice);

            if (formattedTokenString == null)
                throw new NullReferenceException("formatted token string is null, please validate");

            return formattedTokenString;
        }

        public async Task<BnetTokenDTO> GetOauth2Token()
        {
            if (!BnetTokenModel.HasTokenExpired() && BnetTokenModel.AccessToken != null)
            {
                return new BnetTokenDTO()
                {
                    access_token = BnetTokenModel.AccessToken,
                    token_type = BnetTokenModel.TokenType ?? "bearer",
                    expires_in = BnetTokenModel.SecondsUntilExpiration ?? 86399, //one day in seconds since thats the standard
                    sub = BnetTokenModel.Sub ?? ""
                };
            }

            var clientId = _configuration["BnetCredentials:BnetClientId"];
            var clientSecret = _configuration["BnetCredentials:BnetClientSecret"];
            var BnetOAuth2Endpoint = _configuration["BnetCredentials:BnetTokenEndpointBase"];

            if (clientId == null || clientSecret == null || BnetOAuth2Endpoint == null)
                throw new ArgumentNullException("Tried to create OAuth2 form body without valid BnetCredentials configuration values. Please check the configuration appsettings.json or local secret manager for values.");

            var form = _services.CreateOAuth2FormBody(clientId, clientSecret);

            return await _oauthRepository.GetTokenInformation(form, BnetOAuth2Endpoint);
        }
    }
}