using Twisted.Domain.DTOs;

namespace Twisted.Infrastructure.Interfaces
{
    public interface IBnetWoWRetailAPI
    {
        Task<string> GetWoWTokenPrice(BnetTokenDTO tokenInformation, string space, string locale);
    }
}
