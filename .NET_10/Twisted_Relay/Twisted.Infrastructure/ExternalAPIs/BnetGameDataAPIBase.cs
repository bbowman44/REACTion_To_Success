namespace Twisted.Infrastructure.ExternalAPIs
{
    /// <summary>
    /// Shared setup for Battle.net game data APIs (WoW Retail, Classic, etc.).
    /// All of them authenticate with the same OAuth2 bearer token.
    /// </summary>
    public abstract class BnetGameDataAPIBase
    {
        protected void BuildInitialRequestObject(HttpClient client, string token)
        {
            string baseURL = "https://us.api.blizzard.com";
            client.BaseAddress = new Uri(baseURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }
    }
}
