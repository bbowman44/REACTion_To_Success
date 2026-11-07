using Twisted.Domain.DTOs;

namespace Twisted.UseCases.Handlers
{
    public interface IBnetAPIHandler
    {
        Task<string> GetTokenPriceAsync(BnetTokenDTO tokenInformation);
        Task<BnetTokenDTO> GetOauth2Token();
    }
}