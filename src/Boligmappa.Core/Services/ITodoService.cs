using Boligmappa.Core.Entities;

namespace Boligmappa.Core.Services;

public interface ITodoService
{
    Task<IEnumerable<Todo>> GetTodos();
}