using API.DTOs.AppUserDtos;
using API.Entities;
using API.Response;
using API.Services.Argon;
using API.UoW;
using AutoMapper;

namespace API.Services.Users
{
    public class UserService(IMapper mapper, IUnitOfWork unitOfWork, IArgonPasswordHasher passwordHasher) : IUserService
    {
        private readonly IUnitOfWork UnitOfWork = unitOfWork;
        private readonly IMapper Mapper = mapper;
        private readonly IArgonPasswordHasher PasswordHasher = passwordHasher;

        public async Task<AppResponse<List<GetUsersDto>>> GetAll()
        {
            try
            {
                var users = await UnitOfWork.UserRepository.GetAllUsers();

                if(users.Data == null || users.Data.Count == 0)
                    return new AppResponse<List<GetUsersDto>>(null, "Users not found", 500,false);

                var usersDto = Mapper.Map<List<GetUsersDto>>(users.Data);

                return new AppResponse<List<GetUsersDto>>(usersDto);
            }
            catch (Exception ex)
            {
                return new AppResponse<List<GetUsersDto>>(null, ex.Message ,500, false);
            }

        }

        public async Task<AppResponse<GetUserDto>> GetById(int id)
        {
            try
            {
                var user = await UnitOfWork.UserRepository.GetUserById(id);

                if (user.Data == null || !user.Success)
                {
                    return new AppResponse<GetUserDto>(null, "User was not found", 404, false);
                }

                var userDto = Mapper.Map<GetUserDto>(user.Data); 

                return new AppResponse<GetUserDto>(userDto);
            }
            catch (Exception ex)
            {
                return new AppResponse<GetUserDto>(null, ex.Message, 500, false);
            }

        }
        public async Task<AppResponse<GetUserDto>> GetByUsername(string username)
        {
            try
            {
                username = username.ToLower();

                var user = await UnitOfWork.UserRepository.GetUserByName(username);

                if (user.Data == null || !user.Success)
                {
                    return new AppResponse<GetUserDto>(null, "User was not found", 404, false);
                }

                var resultDto = Mapper.Map<GetUserDto>(user.Data); 

                return new AppResponse<GetUserDto>(resultDto);
            }
            catch (Exception ex)
            {
                return new AppResponse<GetUserDto>(null, ex.Message, 500, false);
            }

        }

        public async Task<AppResponse<RegisterDto>> CreateUser(RegisterDto registerDto)
        {
            
            if(registerDto == null) 
                return new AppResponse<RegisterDto>(null,"Check the entered registration information", 404, false);
            

            try
            {
                registerDto.Username = registerDto.Username.ToLower();
                var user = Mapper.Map<AppUser>(registerDto);     
                user.PasswordHash = PasswordHasher.HashPassword(registerDto.Passwordhash);         
                await UnitOfWork.UserRepository.AddUser(user);
                await UnitOfWork.Complete();
                var resultDto = Mapper.Map<RegisterDto>(user);
                return new AppResponse<RegisterDto>(resultDto);              
            }
            catch (Exception ex)
            {
                return new AppResponse<RegisterDto>(null, ex.Message, 500, false);
            }
        }

        public async Task<AppResponse<RemoveUserDto>> RemoveUser(int id)
        {
            var user = await UnitOfWork.UserRepository.GetUserById(id);

            if (user == null || user.Data == null)
                return new AppResponse<RemoveUserDto>(null, "Failed to remove user (does not exist)", 404, false);
            
            try
            {
                var removedUserDto = Mapper.Map<RemoveUserDto>(user.Data);
                await UnitOfWork.UserRepository.DeleteUser(id);
                await UnitOfWork.Complete();
                return new AppResponse<RemoveUserDto>(removedUserDto);                 
            }
            catch (Exception ex)
            {
                return new AppResponse<RemoveUserDto>(null, ex.Message, 500, false);
            }

        }

        public async Task<AppResponse<UpdateUserDto>> UpdateUser(int id, UpdateUserDto updatedUserDto)
        {
            if(updatedUserDto == null)
                return new AppResponse<UpdateUserDto>(null, "Enter your information", 404, false);

            var userResponse = await UnitOfWork.UserRepository.GetUserById(id);

            if (userResponse.Data == null || !userResponse.Success)
                return new AppResponse<UpdateUserDto>(null, "Failed to update user (does not exist)", 404, false);

            var user = userResponse.Data;

            try
            {
                updatedUserDto.Username = updatedUserDto.Username!.ToLower();
                Mapper.Map(updatedUserDto, user);
                if(user.PasswordHash != null)
                {
                    user.PasswordHash = PasswordHasher.HashPassword(user.PasswordHash);
                }

                await UnitOfWork.UserRepository.UpdateUser(id, user);
                await UnitOfWork.Complete();
                return new AppResponse<UpdateUserDto>(updatedUserDto, "Updated user successfully", 200,true);
            }
            catch(Exception ex)
            {
                return new AppResponse<UpdateUserDto>(null, ex.Message,500,false);
            }
        }

        public async Task<AppResponse<LoginDto>> Login(LoginDto loginDto)
        {
            try
            {
                if(loginDto == null) 
                    return new AppResponse<LoginDto>(null, "Enter a valid username or password", 404,false);

                loginDto.Username = loginDto.Username.ToLower();
                
                var user = await UnitOfWork.UserRepository.GetUserByName(loginDto.Username);

                if(user.Data == null || user.Data.UserName != loginDto.Username) // checking if username entered matches any username.
                    return new AppResponse<LoginDto>(null, "Invaild username or password", 404, false);
            else
            {   // if username matched, check hashed password and entered password.
                var verifyPassword = PasswordHasher.VerifyHashedPassword(user.Data.PasswordHash, loginDto.Password);
                if(verifyPassword != Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success)
                    return new AppResponse<LoginDto>(null, "Invalid password",404,false);
            }
            
            return new AppResponse<LoginDto>(loginDto);

            }
            catch (Exception ex)
            {
                return new AppResponse<LoginDto>(null, ex.Message, 500,false);
            }

        }       

    }
}
