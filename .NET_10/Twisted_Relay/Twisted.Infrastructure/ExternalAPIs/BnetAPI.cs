using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Twisted.Domain.DTOs;
using Twisted.Domain.Exceptions;
using Twisted.Domain.Models;
using Twisted.Infrastructure.Interfaces;

namespace Twisted.Infrastructure.ExternalAPIs
{
    public class BnetAPI : IBnetAPI
    {
        private readonly IConfiguration _configuration;
        public BnetAPI(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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

        public async Task<BnetTokenDTO> GetTokenInformation(Dictionary<string, string> tokenForm, string accessTokenURL)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.PostAsync(accessTokenURL, new FormUrlEncodedContent(tokenForm));
            var jsonContent = await response.Content.ReadAsStringAsync();

            BnetTokenModel.RetrivalTime = DateTime.Now; //set the retrival time as soon as possible

            if (!response.IsSuccessStatusCode)
            {
                throw new APIFailureException($"Error retrieving token information: {response.StatusCode} - {jsonContent}");
            }

            var bnetTokenDTO = JsonConvert.DeserializeObject<BnetTokenDTO>(jsonContent);

            if(bnetTokenDTO == null)
            {
                throw new APIFailureException($"Error deserializing token information, null bnetTokenDTO: {jsonContent}");
            }

            BnetTokenModel.AccessToken = bnetTokenDTO.access_token;
            BnetTokenModel.TokenType = bnetTokenDTO.token_type;
            BnetTokenModel.SecondsUntilExpiration = bnetTokenDTO.expires_in;
            BnetTokenModel.Sub = bnetTokenDTO.sub;

            return bnetTokenDTO;
        }
        private void BuildInitialRequestObject(HttpClient client, string token)
        {
            string baseURL = "https://us.api.blizzard.com";
            client.BaseAddress = new Uri(baseURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }

    }
}
