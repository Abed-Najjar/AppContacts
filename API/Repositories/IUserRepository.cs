
using API.Entities;
using API.Response;
namespace API.Repositories;

public interface IUserRepository<AppUser>
{
    Task<AppResponse<AppUser>> AddUser(AppUser user);
    Task<AppResponse<List<AppUser>>> GetAllUsers();
    Task<AppResponse<AppUser>> GetUserById(int id);
    Task<AppResponse<AppUser>> UpdateUser(int id, AppUser updatedUserDetails);
    Task<AppResponse<AppUser>> DeleteUser(int id);
}
