using Boligmappa.Core.Entities.Abstractions;

namespace Boligmappa.Core.Models;

public abstract class Model<TEntity>
    where TEntity : Entity
{
    public int Id { get; set; }
    public abstract TEntity ToEntity();
}