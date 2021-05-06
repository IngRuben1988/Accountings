

namespace  VTAworldpass.VTAServices.Services.accounts.security
{
    public interface IManagerPasswordHash
    {
        string HashPassword(string password);
        bool VerifyHashedPassword(string hashedPassword, string password);
        string TokenEncode(string token);
    }
}
