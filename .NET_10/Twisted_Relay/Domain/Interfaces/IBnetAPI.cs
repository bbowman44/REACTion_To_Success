namespace Twisted.Infrastructure.ExternalAPIs
{
    public interface IBnetAPI
    {
        Task<int> GetTokenInformation();
        Task<int> GetTokenInformation2();
        Task<int> GetWoWTokenPrice();
    }
}