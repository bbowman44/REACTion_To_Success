using Newtonsoft.Json;
using Twisted.Domain.DTOs;
using Twisted.Domain.Exceptions;
using Twisted.Domain.Models;
using Twisted.Infrastructure.Interfaces;

namespace Twisted.Infrastructure.ExternalAPIs
{
    /// <summary>
    /// Battle.net OAuth2 client credentials flow. The access token is shared by every
    /// game data API (WoW Retail, Classic, etc.) and cached in <see cref="BnetTokenModel"/>.
    /// </summary>
    public class BnetOAuthAPI : IBnetOAuthAPI
    {
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
    }
}
