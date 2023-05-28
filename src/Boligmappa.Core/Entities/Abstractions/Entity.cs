namespace Boligmappa.Core.Entities.Abstractions;

public abstract class Entity
{
    public int Id { get; }

    public Entity(int id)
    {
        Id = id;
    }
}