using API.DTOs.ContactsDtos;
using API.DTOs.AppUserDtos;
using API.Entities;
using AutoMapper;

namespace API.Extensions;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        
        CreateMap<Contacts, ContactDto>().ReverseMap(); // Mapping from contacts to contactDto on input.
        CreateMap<Contacts, CreateContactDto>().ReverseMap();
        CreateMap<Contacts, UpdateContactDto>().ReverseMap();
        CreateMap<Contacts, RemoveContactDto>().ReverseMap();
        CreateMap<AppUser, GetUserDto>().ReverseMap();
        CreateMap<AppUser, GetUsersDto>().ReverseMap();
        CreateMap<AppUser, RemoveUserDto>().ReverseMap();
        CreateMap<AppUser, UpdateUserDto>().ReverseMap();   
        CreateMap<AppUser, LoginDto>().ReverseMap();
        CreateMap<AppUser, RegisterDto>().ReverseMap();

    }
}
