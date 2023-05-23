namespace Boligmappa.Core.Entities.Abstractions;

public abstract record Entity
{
    public int Id { get; }

    public Entity(int id)
    {
        Id = id;
    }
}