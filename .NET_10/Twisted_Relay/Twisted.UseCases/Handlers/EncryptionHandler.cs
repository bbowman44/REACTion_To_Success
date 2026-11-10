using Microsoft.AspNetCore.Identity;
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
                //need to also return the IV with the value or store it along side it because you will need it when you encrypt and decrypt.

            }
            return unencryptedInformation;
        }

        public string AsymmetricEncrpytionTest(string testStringInput)
        {
            //will implement later if needed. I think I went out encryption and hashing enough to understand exactally what needs to happen for it to succeed.

            //just to reiterate for the future is that asymmetric encryption handles smaller amount of data and takes in two seperate keys, one for public use and another for private use.
            //those keys work in tandem to encrypt and decrypt information related to whatever it may be with RSA.
            //with RSA you give out the public key to anyone and keep the private key for yourself.
            //that public key is kind of like a secondary protection in case anyone were to log into the hosted environment and take all the encrypted data out and get access to the private key.
            //simplistic implementation below just for reference.

            //for first time generate you could store it in a file or convert it to base64 The azure secret manager or most secret/key vault managers can use pem files if you wanted from what I found.
            //File.WriteAllText("public.pem", rsa.ExportSubjectPublicKeyInfoPem());
            //File.WriteAllText("private.pem", rsa.ExportPkcs8PrivateKeyPem());
            //base64 version
            //string publicB64 = Convert.ToBase64String(rsa.ExportSubjectPublicKeyInfo());
            //string privateB64 = Convert.ToBase64String(rsa.ExportPkcs8PrivateKey());

            using RSA rsa = RSA.Create(2048); //creates the rsa object
            string publicB64 = Convert.ToBase64String(rsa.ExportSubjectPublicKeyInfo()); //gets the public key and converts to base64, you can hand this out to the user who calls the api if you wanted to or anyone using the application
            string privateB64 = Convert.ToBase64String(rsa.ExportPkcs8PrivateKey()); //get shte private key and convertes to base 64

            // Encrypt (uses the public key)
            byte[] encrypted = rsa.Encrypt(Encoding.UTF8.GetBytes(testStringInput), RSAEncryptionPadding.OaepSHA256); //have to convert the string to utf8 and get the bytes if you want to encrypt

            // Decrypt (uses the private key)
            byte[] decrypted = rsa.Decrypt(encrypted, RSAEncryptionPadding.OaepSHA256);

            return Encoding.UTF8.GetString(decrypted);
        }

        public string HashTest()
        {
            //two ways to hash a string
            var testStringInput = "This is a test string for hashing a string or password";

            //the built in way recommended throught the microsoft documentation for passwords
            //https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-10.0

            var hasher = new PasswordHasher<object>();
            var hashedPassword = hasher.HashPassword(null!, testStringInput);
            var resultHashComparison = hasher.VerifyHashedPassword(null!, hashedPassword, testStringInput); 
            var failedResultHash = hasher.VerifyHashedPassword(null!, hashedPassword, "Not the right comparison");

            //this one is fine but it is recommended by microsoft to use the other method if dealing with passwords and users specifically
            //They both use the same hashing algorithm so not much is different but there is an extra layer of security imposed with the PasswordHasher object

            //if (resultHashComparison == PasswordVerificationResult.Success)
            //this is how you would check the result comparison of the hash and the given string password input
            //if(failedResultHash == PasswordVerificationResult.Failed)
            //this is how you would check the result comparison of the hash and the given string password input

            //The other way using the Rfc2898DeriveBytes.Pbkdf2 implemenation using the System.Security.Cryptography using.

            var hashofPbkdf2 = HashPasswordPbkdf2(testStringInput);
            var verificationHashPbkdf2 = VerifyPasswordPbkdf2(testStringInput, hashofPbkdf2);
            var verificationFailHashPbkdf2 = VerifyPasswordPbkdf2(testStringInput, hashofPbkdf2);

            //ideally I would make my own enums and what not to represnt the output of the verification of what I just made and we would have the similar checks like in the password hasher above.
            //no big deal though, I understand the concept of taking the password, hashing it, then when someone wants to verify the password they give us the pass and we take the hash and compare the new hash to the given pass they gave us.

            return $"This is the output for PasswordHasher: \n\nHashed output: {hashedPassword}\nHashed input: {testStringInput}\nresult success: {resultHashComparison}\nResult failure: {failedResultHash}\n\nPbkdf2 self implemented hasher:\nHashed string: {hashofPbkdf2}\nSuccessful hashing: {verificationHashPbkdf2}\nFailed Hashing attempt: {verificationFailHashPbkdf2}"; ;
        }

        public string HashPasswordPbkdf2(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(16);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                password, salt, 600_000, HashAlgorithmName.SHA256, 32);

            return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }
        static bool VerifyPasswordPbkdf2(string password, string stored)
        {
            var parts = stored.Split('.');
            var salt = Convert.FromBase64String(parts[0]);
            var expected = Convert.FromBase64String(parts[1]);

            var actual = Rfc2898DeriveBytes.Pbkdf2(password, salt, 600_000, HashAlgorithmName.SHA256, 32);

            return CryptographicOperations.FixedTimeEquals(actual, expected);
        }

        private byte[] EncryptStringToBytes(string unencryptedString, byte[] Key, byte[] IV)
        {
            //check the arguments if needed // not sure if I actually need to or not but it doesn't hurt
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
