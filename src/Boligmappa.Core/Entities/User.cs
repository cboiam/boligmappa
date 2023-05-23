using Boligmappa.Core.Entities.Abstractions;

namespace Boligmappa.Core.Entities;

public record User : Entity
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Image { get; init; }
    public string CreditCard { get; init; }
    public string UserName { get; init; }
    public int PostCount { get; private set; }
    public int TodoCount { get; private set; }
    public IEnumerable<Todo> Todos { get; private set; }

    public User(int id) : base(id) { }

    public void SetPostCount(int count) => PostCount = count;
    public void SetTodoCount(int count) => TodoCount = count;
    internal void SetTodos(IEnumerable<Todo> todos) => Todos = todos;
}