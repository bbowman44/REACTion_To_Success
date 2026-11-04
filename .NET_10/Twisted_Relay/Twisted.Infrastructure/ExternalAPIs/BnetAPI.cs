using System;
using System.Collections.Generic;
using System.Text;

namespace Twisted.Infrastructure.ExternalAPIs
{
    public class BnetAPI : IBnetAPI
    {
        private static HttpClient client = new HttpClient();

        public async Task<int> GetWoWTokenPrice()
        {
            buildBnetClient();
            string temp = "/token";

            HttpResponseMessage response = await client.GetAsync(temp);
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("Success");
            }

            return 0;
        }

        public async Task<int> GetTokenInformation()
        {
            var accessTokenURL = "https://oauth.battle.net/token";
            var clientId = "asdf";
            var clientSecret = "asdf";
            var grant_type = "client_credentials";

            var form = new Dictionary<string, string>
            {
                { "grant_type", grant_type },
                { "client_id", clientId },
                { "client_secret", clientSecret }
            };

            HttpResponseMessage response = await client.PostAsync(accessTokenURL, new FormUrlEncodedContent(form));
            var jsonContent = await response.Content.ReadAsStringAsync();

            return 0;
        }
        public async Task<int> GetTokenInformation2()
        {
            var accessTokenURL = "https://oauth.battle.net/token";
            var clientId = "asdf";
            var clientSecret = "asdf";
            var grant_type = "client_credentials";

            var form = new Dictionary<string, string>
            {
                { "grant_type", grant_type },
                { "client_id", clientId },
                { "client_secret", clientSecret }
            };

            HttpResponseMessage response = await client.PostAsync(accessTokenURL, new FormUrlEncodedContent(form));
            var jsonContent = await response.Content.ReadAsStringAsync();

            return 0;
        }

        private void buildBnetClient()
        {
            string baseURL = "https://oauth.battle.net";
            client.BaseAddress = new Uri(baseURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("client_id", "asdf");
            client.DefaultRequestHeaders.Add("client_secret", "asdf");
        }
    }
}
