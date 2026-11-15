using Twisted.Domain.DTOs;

namespace Twisted.Infrastructure.Interfaces
{
    public interface IBnetOAuthAPI
    {
        Task<BnetTokenDTO> GetTokenInformation(Dictionary<string, string> tokenForm, string accessTokenURL);
    }
}
