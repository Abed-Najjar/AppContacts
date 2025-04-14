using API.DTOs;
using API.Entities;
using API.Response;
using API.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController(IUnitOfWork unitOfWork) : ControllerBase
    {
        [HttpGet]
        public async Task<AppResponse<List<Contacts>>> GetAll()
        {
            var contacts = await unitOfWork.ContactsRepository.GetAllContacts();

            if(contacts == null) return new AppResponse<List<Contacts>>(null, "No contacts were found", 404,false);
            
            return new AppResponse<List<Contacts>>(contacts.Data);
        }

        [HttpGet("{id:int}")]
        public async Task<AppResponse<Contacts>> GetById(int id)
        {
            var contact = await unitOfWork.ContactsRepository.GetContactById(id);
            
            if(contact.Data == null) return new AppResponse<Contacts>(null, "Contact does not exist", 404 ,false);
             
            try
            {
                return new AppResponse<Contacts>(contact.Data);
            }
            catch (Exception ex)
            {
                return new AppResponse<Contacts>(null, ex.Message, 500, false);
            }
        }

        [HttpPost("Create")]
        public async Task<AppResponse<Contacts>> CreateContact(ContactDto contactDto)
        {
            var contact = new Contacts()
            {
                UserName = contactDto.Username.ToLower(),
                Email = contactDto.Email
            };

            if(contact == null) return new AppResponse<Contacts>(null, "Check the entered username or password", 404, false);
            
            try
            {
                await unitOfWork.ContactsRepository.AddContact(contact);
                await unitOfWork.Complete();
            }
            catch(Exception ex)
            {
                return new AppResponse<Contacts>(null, ex.Message, 500, false);
            }
            return new AppResponse<Contacts>(contact);
        }

        [HttpDelete("{id:int}")]
        public async Task<AppResponse<Contacts>> RemoveContact(int id)
        {
            var contact = await unitOfWork.ContactsRepository.GetContactById(id);

            if(contact == null) return new AppResponse<Contacts>(null, "Failed to remove contact (does not exist)",404,false);
            
            try
            {
                await unitOfWork.ContactsRepository.DeleteContact(id);
                await unitOfWork.Complete();
                return new AppResponse<Contacts>(contact.Data); 
            }
            catch (Exception ex)
            {
                return new AppResponse<Contacts>(null, ex.Message, 404,false);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<AppResponse<Contacts>> UpdateContact(int id, ContactDto updatedContactDto)
        {
            var contactResponse = await unitOfWork.ContactsRepository.GetContactById(id);

            // if the data of the response is empty or the response has failed, it returns the contact was not found
            if(contactResponse.Data == null || !contactResponse.Success) 
                return new AppResponse<Contacts>(null, "Contact not found", 404, false);
            
            var contact = contactResponse.Data;

            // Input validation
            if(updatedContactDto.Username == null) return new AppResponse<Contacts>(null, "Please enter a username", 404, false);
            
            try
            {
                contact.UserName = updatedContactDto.Username;
                contact.Email = updatedContactDto.Email;   
                await unitOfWork.ContactsRepository.UpdateContact(contact);
                await unitOfWork.Complete();
                return new AppResponse<Contacts>(contact);
            }
            catch (Exception ex)
            {
                return new AppResponse<Contacts>(null, ex.Message, 404, false);
            }
        }
    }
}
