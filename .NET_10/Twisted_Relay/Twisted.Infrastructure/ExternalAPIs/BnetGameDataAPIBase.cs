using System.Net.Http.Headers;

namespace Twisted.Infrastructure.ExternalAPIs
{
    /// <summary>
    /// Shared setup for Battle.net game data APIs (WoW Retail, Classic, etc.).
    /// All of them authenticate with the same OAuth2 bearer token.
    /// </summary>
    public abstract class BnetGameDataAPIBase
    {
        private readonly HttpClient _client;

        public BnetGameDataAPIBase(HttpClient client)
        {
            _client = client;
        }

        public Task<HttpResponseMessage> SendAuthorizedAsync(HttpMethod method, string uri, string token, HttpContent? content = null)
        {
            var request = new HttpRequestMessage(method, uri)
            {
                Content = content
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return _client.SendAsync(request);
        }
    }
}
