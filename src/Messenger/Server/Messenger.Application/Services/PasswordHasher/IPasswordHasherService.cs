namespace Messenger.Application.Services.PasswordHasher
{
    public interface IPasswordHasherService
    {
        string Encrypt(string password, string salt);
        bool Verify(string hash, string password, string salt);
    }
}
