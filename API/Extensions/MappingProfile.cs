using API.DTOs.ContactsDtos;
using API.DTOs.AppUserDtos;
using API.Entities;
using AutoMapper;

namespace API.Extensions;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Contact Mapping.
        CreateMap<Contacts, ContactDto>().ReverseMap(); 
        CreateMap<Contacts, CreateContactDto>().ReverseMap();
        CreateMap<Contacts, UpdateContactDto>().ReverseMap();
        CreateMap<Contacts, RemoveContactDto>().ReverseMap();

        //User Mapping.
        CreateMap<AppUser, GetUserDto>().ReverseMap();
        CreateMap<AppUser, GetUsersDto>().ReverseMap();
        CreateMap<AppUser, RemoveUserDto>().ReverseMap();
        CreateMap<AppUser, UpdateUserDto>().ReverseMap();   
        CreateMap<AppUser, LoginDto>().ReverseMap();
        CreateMap<AppUser, RegisterDto>().ReverseMap();
        
        
    }
}
