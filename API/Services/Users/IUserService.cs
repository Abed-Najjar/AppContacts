using API.DTOs.AppUserDtos;
using API.Entities;
using API.Response;

namespace API.Services.Users;

public interface IUserService
{
    Task<AppResponse<RegisterDto>> CreateUser(RegisterDto registerDto);
    Task<AppResponse<List<GetUsersDto>>> GetAll();
    Task<AppResponse<GetUserDto>> GetById(int id);
    Task<AppResponse<RemoveUserDto>> RemoveUser(int id);
    Task<AppResponse<UpdateUserDto>> UpdateUser(int id, UpdateUserDto updatedUserDto);
}
