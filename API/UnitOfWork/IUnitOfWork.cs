using API.Entities;
using API.Repositories;

namespace API.UnitOfWork;

public interface IUnitOfWork
{
        IContactsRepository<Contacts> ContactsRepository { get; }
        Task<bool> Complete();
}
