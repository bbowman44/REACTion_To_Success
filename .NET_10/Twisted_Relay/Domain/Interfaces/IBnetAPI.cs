using Twisted.Domain.DTOs;

namespace Twisted.Infrastructure.ExternalAPIs
{
    public interface IBnetAPI
    {
        Task<string> GetWoWTokenPrice(BnetTokenDTO tokenInformation, string space, string locale);
        Task<BnetTokenDTO> GetTokenInformation();
    }
}