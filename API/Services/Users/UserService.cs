using API.DTOs.AppUserDtos;
using API.Entities;
using API.Response;
using API.UoW;
using AutoMapper;

namespace API.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMapper Mapper;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public async Task<AppResponse<List<GetUsersDto>>> GetAll()
        {
            try
            {
                var users = await UnitOfWork.UserRepository.GetAllUsers();

                if(users.Data == null || !users.Success)
                    return new AppResponse<List<GetUsersDto>>(null, "Users not found", 500,false);

                var usersDto = Mapper.Map<List<GetUsersDto>>(users);

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

                var userDto = Mapper.Map<GetUserDto>(user); 

                return new AppResponse<GetUserDto>(userDto);
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

                var user = Mapper.Map<AppUser>(registerDto);                
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
                var removedUserDto = Mapper.Map<RemoveUserDto>(user);
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
            var userRespone = await UnitOfWork.UserRepository.GetUserById(id);

            if (userRespone.Data == null || !userRespone.Success)
                return new AppResponse<UpdateUserDto>(null, "Failed to update user (does not exist)", 404, false);

            var user = userRespone.Data;

            try
            {
                Mapper.Map(updatedUserDto, user);
                await UnitOfWork.UserRepository.UpdateUser(id, user);
                await UnitOfWork.Complete();
                return new AppResponse<UpdateUserDto>(updatedUserDto, "Updated user successfully", 200,true);
            }
            catch(Exception ex)
            {
                return new AppResponse<UpdateUserDto>(null, ex.Message,500,false);
            }


        }
    }
}
