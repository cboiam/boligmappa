using Boligmappa.Core.Entities.Abstractions;
using Boligmappa.Core.Models;

namespace Boligmappa.Core.Extensions;

public static class ModelExtensions
{
    public static IEnumerable<TEntity> ToEntity<TEntity>(this IEnumerable<Model<TEntity>> models)
        where TEntity : Entity
    {
        return models.Select(m => m.ToEntity());
    }
}