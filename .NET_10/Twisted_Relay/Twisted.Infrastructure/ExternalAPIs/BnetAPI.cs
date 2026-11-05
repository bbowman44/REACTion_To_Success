using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using Twisted.Domain.DTOs;
using Twisted.Domain.Exceptions;

namespace Twisted.Infrastructure.ExternalAPIs
{
    public class BnetAPI : IBnetAPI
    {
        //private HttpClient client = new HttpClient();

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

        public async Task<BnetTokenDTO> GetTokenInformation()
        {
            HttpClient client = new HttpClient();

            var accessTokenURL = "https://oauth.battle.net/token";
            var clientId = "asdf";
            var clientSecret = "asdf";
            var grant_type = "client_credentials";

            //the mapping of the form needs to move into the domain with the business logic.
            //all mappings should be in their own file if possible in the domain layer. 
            var form = new Dictionary<string, string>
            {
                { "grant_type", grant_type },
                { "client_id", clientId },
                { "client_secret", clientSecret }
            };

            HttpResponseMessage response = await client.PostAsync(accessTokenURL, new FormUrlEncodedContent(form));
            var jsonContent = await response.Content.ReadAsStringAsync();

            //be sure to check the response status code here as well.
            if (!response.IsSuccessStatusCode)
            {
                throw new APIFailureException($"Error retrieving token information: {response.StatusCode} - {jsonContent}");
            }

            //try to deserialize it into a dynamic object with all the fields from the token endpoint.
            var bnetTokenDTO = JsonConvert.DeserializeObject<BnetTokenDTO>(jsonContent);

            if(bnetTokenDTO == null)
            {
                throw new APIFailureException($"Error deserializing token information, null bnetTokenDTO: {jsonContent}");
            }

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
