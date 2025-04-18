using Microsoft.AspNetCore.Identity;

namespace API.Services.Argon;

public interface IArgonPasswordHasher
{
    string HashPassword(string password);
    public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword);
}
