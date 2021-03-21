namespace Authentication.Domain.User.Services
{
    public interface IEncryptionService
    {
        bool VerifyPassword(byte[] passwordHash, string password);
        byte[] EncodePassword(string password);
    }
}
