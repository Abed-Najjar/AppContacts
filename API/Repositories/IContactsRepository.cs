using API.Entities;
using API.Response;

namespace API.Repositories;

public interface IContactsRepository<T>
{
    Task<AppResponse<Contacts>> AddContact(Contacts contact);
    Task<AppResponse<List<Contacts>>> GetAllContacts();
    Task<AppResponse<Contacts>> GetContactById(int id);
    Task<AppResponse<Contacts>> UpdateContact(Contacts updatedContactDetails);
    Task<AppResponse<Contacts>> DeleteContact(int id);
}
