using API.Response;
using AutoMapper;
using API.Entities;
using API.UoW;
using API.DTOs.ContactsDtos;

namespace API.Services.Contact;

public class ContactService(IUnitOfWork unitOfWork, IMapper mapper) : IContactService
{
    private readonly IUnitOfWork UnitOfWork = unitOfWork;
    private readonly IMapper Mapper = mapper;

    public async Task<AppResponse<CreateContactDto>> CreateContact(CreateContactDto contactDto)
    {

            if(contactDto == null) 
                return new AppResponse<CreateContactDto>(null, "Check the entered username or password", 404, false);

            try
            {
                var contact = Mapper.Map<Contacts>(contactDto);
                await UnitOfWork.ContactsRepository.AddContact(contact);
                await UnitOfWork.Complete();
                var resultDto = Mapper.Map<CreateContactDto>(contact);
                return new AppResponse<CreateContactDto>(resultDto);
            }
            catch(Exception ex)
            {
                return new AppResponse<CreateContactDto>(null, ex.Message, 500, false);
            }
    }

    public async Task<AppResponse<List<ContactDto>>> GetAll()
    {
        var contacts = await UnitOfWork.ContactsRepository.GetAllContacts();

            if(contacts == null) 
                return new AppResponse<List<ContactDto>>(null, "No contacts were found", 404,false);

            try
            {
                var contactDtos = Mapper.Map<List<ContactDto>>(contacts.Data); // Maps all of the contacts entity to a contactDto. 
                return new AppResponse<List<ContactDto>>(contactDtos); // Returns contactDto to the client.
            }
            catch (Exception ex)
            {
                return new AppResponse<List<ContactDto>>(null, ex.Message, 404, false);
            }
    }

        public async Task<AppResponse<ContactDto>> GetById(int id)
        {
            var contact = await UnitOfWork.ContactsRepository.GetContactById(id);
            
            if(contact.Data == null) return new AppResponse<ContactDto>(null, "Contact does not exist", 404 ,false);
             
            try
            {
                var contactDto = Mapper.Map<ContactDto>(contact.Data); // Maps all of the contacts entity to a contactDto. 
                return new AppResponse<ContactDto>(contactDto); // Returns contactDto to the client.

            }
            catch (Exception ex)
            {
                return new AppResponse<ContactDto>(null, ex.Message, 500, false);
            }
        }

        public async Task<AppResponse<RemoveContactDto>> RemoveContact(int id)
        {
            var contact = await UnitOfWork.ContactsRepository.GetContactById(id);

            if(contact == null || contact.Data == null)
                return new AppResponse<RemoveContactDto>(null, "Failed to remove contact (does not exist)",404,false);
            
            var removedContactDto = Mapper.Map<RemoveContactDto>(contact.Data);

            try
            {   
                await UnitOfWork.ContactsRepository.DeleteContact(id);
                await UnitOfWork.Complete();
                return new AppResponse<RemoveContactDto>(removedContactDto); 
            }
            catch (Exception ex)
            {
                return new AppResponse<RemoveContactDto>(null, ex.Message, 404,false);
            }
        }

        public async Task<AppResponse<UpdateContactDto>> UpdateContact(int id, UpdateContactDto updatedContactDto)
        {
            var contactResponse = await UnitOfWork.ContactsRepository.GetContactById(id);

            // if the data of the response is empty or the response has failed, it returns the contact was not found
            if(contactResponse.Data == null || !contactResponse.Success) 
                return new AppResponse<UpdateContactDto>(null, "Contact not found", 404, false);
            
            var contact = contactResponse.Data;

            // Input validation
            if(updatedContactDto.Username == null) return new AppResponse<UpdateContactDto>(null, "Please enter a username", 404, false);
            
            try
            {
                Mapper.Map(updatedContactDto, contact);
                await UnitOfWork.ContactsRepository.UpdateContact(contact);
                await UnitOfWork.Complete();
                
                var resultDto = Mapper.Map<UpdateContactDto>(contact); // ✅ Map to DTO
                return new AppResponse<UpdateContactDto>(resultDto);
            }
            catch (Exception ex)
            {
                return new AppResponse<UpdateContactDto>(null, ex.Message, 404, false);
            }
        }


}
