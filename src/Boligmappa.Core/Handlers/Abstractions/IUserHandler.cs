using Boligmappa.Core.Entities;

namespace Boligmappa.Core.Handlers.Abstractions;

public interface IUserHandler
{
    Task<int> StoreUsers();
    Task<IEnumerable<User>> GetPopularUsers();
    Task<IEnumerable<User>> GetMasterCardUsers();
}