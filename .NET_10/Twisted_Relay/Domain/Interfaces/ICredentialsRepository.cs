namespace Twisted.Infrastructure.Interfaces
{
    public interface ICredentialsRepository
    {
        Task<string> StoreBnetAPIToken();
        Task<string> GetEncryptedBnetAPIToken();
    }
}