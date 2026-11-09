using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

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
            string unencryptedInformation;

            using (Aes myAes = Aes.Create())
            {
                Aes aes = Aes.Create(); //F1 on AES to see so many good examples of how to use it
                aes.Key = Convert.FromBase64String(_configuration["EncryptionDetails:SymmetricEncryptionKey"]!); //could be null so might not hurt to check but it's fine for now so suppressed
                var iVSalt = aes.IV; //store this with the encrypted value for extra salting

                var stringToEncrypt = "Hello, I have become encrypted and unencrypted!";

                var encryptedInformation = EncryptStringToBytes(stringToEncrypt, aes.Key, aes.IV);
                unencryptedInformation = DencryptStringFromBytes(encryptedInformation, aes.Key, aes.IV);

            }
            return unencryptedInformation;
        }

        public string AsymmetricEncrpytionTest()
        {
            return "test2";
        }

        private byte[] EncryptStringToBytes(string unencryptedString, byte[] Key, byte[] IV)
        {
            //check the arguments if needed // not sure if I actually need to or not
            if (unencryptedString == null || unencryptedString.Length <= 0)
                throw new ArgumentNullException("plainText was null and/or not a valid length more than 0");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key was null and/or not a valid length more than 0");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV was null and/or not a valid length more than 0");

            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                //creating the streams used for encryption
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(unencryptedString); //here we are writting all of the data to the stream to encrypt it using
                        }
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }

            return encrypted;
        }

        private string DencryptStringFromBytes(byte[] encryptedString, byte[] Key, byte[] IV)
        {
            //check the arguments if needed // not sure if I actually need to or not
            if (encryptedString == null || encryptedString.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key was null and/or not a valid length more than 0");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV was null and/or not a valid length more than 0");

            string unencryptedString = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                //creating the streams used for encryption
                using (MemoryStream msDecrypt = new MemoryStream(encryptedString))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            unencryptedString = srDecrypt.ReadToEnd(); //here we are writting all of the data to the stream to encrypt it using
                        }
                    }
                }
            }

            return unencryptedString;
        }
    }
}
