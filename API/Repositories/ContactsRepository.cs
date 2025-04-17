using API.Data;
using API.Entities;
using API.Response;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class ContactsRepository<T>(AppDbContext context) : IContactsRepository<Contacts>
{
    public async Task<AppResponse<Contacts>> AddContact(Contacts contact)
    {

        if(contact == null)
        {
            return new AppResponse<Contacts>(null, "Failed to insert the contact into the database", 500, false);
        }

        try
        {
            await context.Contacts.AddAsync(contact);
            return new AppResponse<Contacts>(contact);
        }
        catch(Exception ex)
        {
            return new AppResponse<Contacts>(null, ex.Message, 500, false);
        }
    }

    public async Task<AppResponse<Contacts>> DeleteContact(int id)
    {
        var removeContact = await context.Contacts.FindAsync(id);

        if(removeContact == null)
        {
            return new AppResponse<Contacts>(null, "Couldnt remove contact from database",500, false);
        }
        try
        {
            context.Remove(removeContact);
            return new AppResponse<Contacts>(removeContact);   
        }
        catch (Exception ex)
        {
            return new AppResponse<Contacts>(null, ex.Message, 500, false);   
        }
    }

    public async Task<AppResponse<List<Contacts>>> GetAllContacts()
    {
        try
        {
            var contacts = await context.Contacts.ToListAsync();
            if(contacts == null) return new AppResponse<List<Contacts>>(null, "No contacts were inserted in database yet", 404,false);
            return new AppResponse<List<Contacts>>(contacts);
        }
        catch (Exception ex)
        {
            return new AppResponse<List<Contacts>>(null, ex.Message, 500, false);
        }
        
    }

    public async Task<AppResponse<Contacts>> GetContactById(int id)
    {
        var contact = await context.Contacts.FindAsync(id);

        if(contact == null) return new AppResponse<Contacts>(null, "Contact was not found in database",500,false);

        try
        {
            return new AppResponse<Contacts>(contact);
        }
        catch(Exception ex)
        {
            return new AppResponse<Contacts>(null, ex.Message,500,false);
        }
        
    }

    public async Task<AppResponse<Contacts>> UpdateContact(Contacts updatedContactDetails)
    {
        var contact = await context.Contacts.FindAsync(updatedContactDetails.Id);

        if(contact == null) return new AppResponse<Contacts>(null, "The contact does not exist",404,false);

        try
        {
            // All fields gets updated again, even if some of them were not changed.
            context.Update(updatedContactDetails); 
            
            // Copies values from updatedContactDetails into the tracked entity,
            // allowing more control over what changes.
            //context.Entry(contact).CurrentValues.SetValues(updatedContactDetails);

            return new AppResponse<Contacts>(contact);
        }
        catch (Exception ex)
        {
            return new AppResponse<Contacts>(null, ex.Message, 500,false);
        }
    }


}
