using API.Data;
using API.DTOs.AppUserDtos;
using API.Entities;
using API.Response;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class UserRepository<T>(AppDbContext context) : IUserRepository<AppUser>
{
   private readonly AppDbContext _context = context;

    public async Task<AppResponse<List<AppUser>>> GetAllUsers()
    {
        try
        {
            var users = await _context.Users.ToListAsync();

            return new AppResponse<List<AppUser>>(users);
        }
        catch (Exception ex)
        {
            return new AppResponse<List<AppUser>>(null, ex.Message, 500, false);
        }

    }

    public async Task<AppResponse<AppUser>> GetUserById(int id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return new AppResponse<AppUser>(null, "User was not found", 404, false);
            }

            return new AppResponse<AppUser>(user);
        }
        catch (Exception ex)
        {
            return new AppResponse<AppUser>(null, ex.Message, 500, false);
        }

    }

    public async Task<AppResponse<AppUser>> GetUserByName(string username)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
                return new AppResponse<AppUser>(null, "User does not exist", 404, false);

            if(user.UserName != username)
                return new AppResponse<AppUser>(null, "Username does not match with anyone",404,false);

            return new AppResponse<AppUser>(user);
        }
        catch (Exception ex)
        {
            return new AppResponse<AppUser>(null, ex.Message, 500, false);
        }
    }

    public async Task<AppResponse<AppUser>> AddUser(AppUser register)
    {
        var existingUser = await _context.Users
            .AnyAsync(u => u.UserName == register.UserName || u.Email == register.Email);

        if (existingUser)
        {
            return new AppResponse<AppUser>(null, "User already exists", 404, false);
        }

        try
        {
            var user = new AppUser
            {
                UserName = register.UserName,
                Email = register.Email,
                PasswordHash = register.PasswordHash 
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return new AppResponse<AppUser>(user);
        }
        catch (Exception ex)
        {
            return new AppResponse<AppUser>(null, ex.Message, 500, false);
        }
    }

    public async Task<AppResponse<AppUser>> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return new AppResponse<AppUser>(null, "Failed to remove user (does not exist)", 404, false);
        }

        try
        {
            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return new AppResponse<AppUser>(user, "User removed successfully", 200, true);
        }
        catch (Exception ex)
        {

            return new AppResponse<AppUser>(null, ex.Message, 500, false);
        }

    }

    public async Task<AppResponse<AppUser>> UpdateUser(int id, AppUser updatedUserDetails)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null || updatedUserDetails.UserName == null)
        {
            return new AppResponse<AppUser>(null, "Failed to update user (does not exist)", 404, false);
        }

        try
        {
            user.UserName = updatedUserDetails.UserName;
            user.Email = updatedUserDetails.Email;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new AppResponse<AppUser>(user, "Updated user successfully", 200, true);
        }
        catch (Exception ex)
        {
            return new AppResponse<AppUser>(null, ex.Message, 500, false);
        }


    }
}
