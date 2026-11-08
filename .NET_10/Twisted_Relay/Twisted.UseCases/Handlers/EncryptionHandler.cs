using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace Twisted.UseCases.Handlers
{
    public class EncryptionHandler : IEncryptionHandler
    {
        private readonly IConfiguration _configuration;
        public EncryptionHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //just going to implement everything here and move if I ever need to use it.
        //I can refactor later if it becomes relevant at all
        public string SymmetricEncrpytionTest()
        {
            Aes aes = Aes.Create();
            aes.Key = Convert.FromBase64String(_configuration["EncryptionDetails:SymmetricEncryptionKey"]!); //could be null so might not hurt to check but it's fine for now so suppressed
            var iVSalt = aes.IV; //store this with the encrypted value for extra salting

            

                return "test";
        }

        public string AsymmetricEncrpytionTest()
        {
            return "test2";
        }
    }
}
