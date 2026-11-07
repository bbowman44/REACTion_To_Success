using Twisted.Domain.Interfaces;

namespace Twisted.Domain.Services
{
    public class BnetAPIServices : IBnetAPIServices
    {
        public string FormatTokenPrice(string tokenPrice)
        {
            tokenPrice = tokenPrice.Remove(tokenPrice.Length - 4); //strip the silver and gold values off the token price.

            return String.Format("{0:N0}", Convert.ToDouble(tokenPrice)) + "g";
        }

        public Dictionary<string, string> CreateOAuth2FormBody(string clientId, string clientSecret, string grantType = "client_credentials")
        {
            return new Dictionary<string, string>
            {
                { "grant_type", grantType },
                { "client_id", clientId },
                { "client_secret", clientSecret }
            };
        }
    }
}
