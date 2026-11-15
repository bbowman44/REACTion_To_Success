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
        public async Task<string> GetWoWTokenPrice(BnetTokenDTO tokenInformation, string space, string locale)
        {
            HttpClient client = new HttpClient();

            string URI = $"/data/wow/token/index?namespace={space}&locale={locale}";

            BuildInitialRequestObject(client, tokenInformation.access_token);

            HttpResponseMessage response = await client.GetAsync(URI);

            if(!response.IsSuccessStatusCode)
            {
                Console.WriteLine("failed");
            }

            var jsonContent = await response.Content.ReadAsStringAsync();

            if(jsonContent == null)
            {
                throw new APIFailureException("API response was null");
            }

            var tokenIndexObject = JObject.Parse(jsonContent);

            if(tokenIndexObject.GetValue("price") != null)
                return tokenIndexObject.GetValue("price")!.ToString();
            else
                throw new Exception("Price not found in API response");
        }
    }
}
