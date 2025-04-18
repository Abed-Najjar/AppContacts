using API.DTOs.AppUserDtos;
using API.Response;
using API.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService userService) : ControllerBase
    {


        [HttpGet("get-users")]
        public async Task<AppResponse<List<GetUsersDto>>> GetAll()
        {
            return await userService.GetAll();
        }

        [HttpGet("get-user/{id:int}")]
        public async Task<AppResponse<GetUserDto>> GetById(int id)
        {            
            return await userService.GetById(id);   
        }

        [HttpGet("get-user/{username}")]
        public async Task<AppResponse<GetUserDto>> GetByName(string username)
        {            
            return await userService.GetByUsername(username);
        }


        [HttpPost("create-user")]
        public async Task<AppResponse<RegisterDto>> CreateUser(RegisterDto registerDto)
        {
            return await userService.CreateUser(registerDto);
        }

        [HttpDelete("delete-user/{id:int}")]
        public async Task<AppResponse<RemoveUserDto>> RemoveUser(int id)
        {
            return await userService.RemoveUser(id);
        }

        [HttpPut("update-user/{id:int}")]
        public async Task<AppResponse<UpdateUserDto>> UpdateUser(int id, UpdateUserDto updatedUserDto)
        {
            return await userService.UpdateUser(id, updatedUserDto);
        }
        [HttpPost("login")]
        public async Task<AppResponse<LoginDto>> Login(LoginDto loginDto)
        {
            return await userService.Login(loginDto);
        }
    }
}
