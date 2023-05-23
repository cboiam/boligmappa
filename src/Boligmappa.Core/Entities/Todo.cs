using Boligmappa.Core.Entities.Abstractions;

namespace Boligmappa.Core.Entities;

public record Todo : Entity
{
    public string Description { get; init; }
    public string Completed { get; init; }
    public int UserId { get; init; }

    public Todo(int id) : base(id) { }
}