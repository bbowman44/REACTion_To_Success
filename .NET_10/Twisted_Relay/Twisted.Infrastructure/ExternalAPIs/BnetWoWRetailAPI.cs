using Newtonsoft.Json.Linq;
using Twisted.Domain.DTOs;
using Twisted.Domain.Exceptions;
using Twisted.Infrastructure.Interfaces;

namespace Twisted.Infrastructure.ExternalAPIs
{
    /// <summary>
    /// World of Warcraft Retail game data endpoints.
    /// </summary>
    public class BnetWoWRetailAPI : BnetGameDataAPIBase, IBnetWoWRetailAPI
    {
        public BnetWoWRetailAPI(HttpClient client) : base(client) { }

        public async Task<string> GetWoWTokenPrice(BnetTokenDTO tokenInformation, string space, string locale)
        {
            using var response = await SendAuthorizedAsync(
                HttpMethod.Get,
                $"/data/wow/token/index?namespace={space}&locale={locale}",
                tokenInformation.access_token);

            var jsonContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new APIFailureException($"Token price request failed: {response.StatusCode} - {jsonContent}");
            }

            var price = JObject.Parse(jsonContent).GetValue("price");

            return price?.ToString() ?? throw new APIFailureException("Price not found in API response");
        }
    }
}
