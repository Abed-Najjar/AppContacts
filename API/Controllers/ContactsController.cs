using API.DTOs.ContactsDtos;
using API.Response;
using API.Services.Contact;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController(IContactService contactService) : ControllerBase
    {
        [HttpGet("get-contacts")]
        public async Task<AppResponse<List<ContactDto>>> GetAll()
        {
            return await contactService.GetAll();
        }

        [HttpGet("get-contact/{id:int}")]
        public async Task<AppResponse<ContactDto>> GetById(int id)
        {            
            return await contactService.GetById(id);   
        }

        [HttpPost("create-contact")]
        public async Task<AppResponse<CreateContactDto>> CreateContact(CreateContactDto contactDto)
        {
            return await contactService.CreateContact(contactDto);
        }

        [HttpDelete("delete-contact/{id:int}")]
        public async Task<AppResponse<RemoveContactDto>> RemoveContact(int id)
        {
            return await contactService.RemoveContact(id);
        }

        [HttpPut("update-contact/{id:int}")]
        public async Task<AppResponse<UpdateContactDto>> UpdateContact(int id, UpdateContactDto updatedContactDto)
        {
            return await contactService.UpdateContact(id, updatedContactDto);
        }
    }
}
