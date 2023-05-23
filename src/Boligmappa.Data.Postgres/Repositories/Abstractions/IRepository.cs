using Boligmappa.Core.Entities.Abstractions;
using Boligmappa.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Boligmappa.Data.Postgres.Repositories.Abstractions;

public interface IRepository<TModel, TEntity>
    where TModel : Model<TEntity>
    where TEntity : Entity
{
    DbSet<TModel> DbSet { get; }
    Task<IEnumerable<TModel>> GetAll();
    Task<TModel> Get(int id);
    Task<bool> Exists(int id);
    Task<TModel> Add(TModel entity);
    Task<TModel> Update(TModel entity);
    Task<TModel> Delete(int id);
}