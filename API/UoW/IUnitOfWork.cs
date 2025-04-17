using API.Entities;
using API.Repositories;

namespace API.UoW;

public interface IUnitOfWork
{
        IContactsRepository<Contacts> ContactsRepository { get; }
        IUserRepository<AppUser> UserRepository {get; }
        Task<bool> Complete();
}
