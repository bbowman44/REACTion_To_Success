namespace Twisted.Domain.Interfaces
{
    public interface IBnetAPIServices
    {
        string FormatTokenPrice(string tokenPrice);
        Dictionary<string, string> CreateOAuth2FormBody(string clientId, string clientSecret, string grantType = "client_credentials");
    }
}