using Boligmappa.Core.Entities;

namespace Boligmappa.Core.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetMasterCardUsers();
    Task<IEnumerable<User>> GetPopularUsers();
    Task Save(User user);   
}