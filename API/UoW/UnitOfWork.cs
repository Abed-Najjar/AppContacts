using API.Data;
using API.Entities;
using API.Repositories;

namespace API.UoW;

public class UnitOfWork(AppDbContext context, IContactsRepository<Contacts> contactsRepository, IUserRepository<AppUser> userRepository) : IUnitOfWork
{
    public IContactsRepository<Contacts> ContactsRepository => contactsRepository;
    public IUserRepository<AppUser> UserRepository => userRepository;

    public async Task<bool> Complete()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
