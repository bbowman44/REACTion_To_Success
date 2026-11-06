using Twisted.Infrastructure.Interfaces;

namespace Twisted.Infrastructure.Repositories
{
    public class CredentialsRepository : ICredentialsRepository
    {
        public Task<string> StoreBnetAPIToken()
        {
            //figure out how to store the token in the database with a timestamp so we can check that it's valid
            //figure out how to make a code first migration to create the table and all information for the place to store it.
            return Task.FromResult("test");
        }
        public async Task<string> GetEncryptedBnetAPIToken()
        {
            //figure out how to call that table and retrieve the encrypted token from the DB
            return await Task.FromResult("test");
        }
    }
}
