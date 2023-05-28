using Boligmappa.Core.Entities.Abstractions;

namespace Boligmappa.Core.Entities;

public class Todo : Entity
{
    public string Description { get; init; }
    public bool Completed { get; init; }
    public int UserId { get; init; }

    public Todo(int id) : base(id) { }
}