using API.DTOs.ContactsDtos;
using API.Response;

namespace API.Services.Contact;

public interface IContactService
{
    Task<AppResponse<CreateContactDto>> CreateContact(CreateContactDto contactDto);
    Task<AppResponse<List<ContactDto>>> GetAll();
    Task<AppResponse<ContactDto>> GetById(int id);
    Task<AppResponse<RemoveContactDto>> RemoveContact(int id);
    Task<AppResponse<UpdateContactDto>> UpdateContact(int id, UpdateContactDto updatedContactDto);
}
