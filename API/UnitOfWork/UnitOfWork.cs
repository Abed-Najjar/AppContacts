using API.Data;
using API.Entities;
using API.Repositories;

namespace API.UnitOfWork;

public class UnitOfWork(AppDbContext context, IContactsRepository<Contacts> contactsRepository) : IUnitOfWork
{
    public IContactsRepository<Contacts> ContactsRepository => contactsRepository;


    public async Task<bool> Complete()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
