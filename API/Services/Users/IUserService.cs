using API.DTOs.AppUserDtos;
using API.Entities;
using API.Response;
using Microsoft.AspNetCore.Mvc;

namespace API.Services.Users;

public interface IUserService
{
    Task<AppResponse<RegisterDto>> CreateUser(RegisterDto registerDto);
    Task<AppResponse<List<GetUsersDto>>> GetAll();
    Task<AppResponse<GetUserDto>> GetById(int id);
    Task<AppResponse<GetUserDto>> GetByUsername([FromRoute]string username);
    Task<AppResponse<RemoveUserDto>> RemoveUser(int id);
    Task<AppResponse<UpdateUserDto>> UpdateUser(int id, UpdateUserDto updatedUserDto);
    Task<AppResponse<LoginDto>> Login(LoginDto loginDto);
}
