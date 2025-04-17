namespace API.Services.ArgonPasswordHasher;

public interface IPasswordHasher<AppUser>
{
    string HashPassword(string password, string salt);
    bool VerifyPassword(string hashedPassword, string password);
}
