namespace Twisted.UseCases.Handlers
{
    public interface IEncryptionHandler
    {
        string SymmetricEncrpytionTest();
        string AsymmetricEncrpytionTest(string testStringInput);
        string HashTest();
    }
}