using Boligmappa.Core.Entities;

namespace Boligmappa.Core.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsers();
}