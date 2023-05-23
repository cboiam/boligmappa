using Boligmappa.Core.Entities.Abstractions;
using Boligmappa.Core.Models;
using Boligmappa.Data.Postgres.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Boligmappa.Data.Postgres.Repositories;

public sealed class Repository<TModel, TEntity> : IRepository<TModel, TEntity>
    where TModel : Model<TEntity>
    where TEntity : Entity
{
    public DbSet<TModel> DbSet { get; }
    private readonly BoligmappaContext context;
    private readonly ILogger<Repository<TModel, TEntity>> logger;

    public Repository(BoligmappaContext context, ILogger<Repository<TModel, TEntity>> logger)
    {
        this.context = context;
        DbSet = context.Set<TModel>();

        this.logger = logger;
    }

    public async Task<TModel> Add(TModel entity)
    {
        var result = await DbSet.AddAsync(entity);
        await Save();

        return result.Entity;
    }
    
    public async Task<TModel> Delete(int id)
    {
        var entity = await Get(id);
        DbSet.Remove(entity);
        await Save();

        return entity;
    }

    public async Task<TModel> Get(int id)
    {
        return await DbSet.Where(u => u.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<bool> Exists(int id)
    {
        var entity = await Get(id);
        return entity != null;
    }

    public async Task<IEnumerable<TModel>> GetAll()
    {
        return await DbSet.AsNoTracking()
            .ToListAsync();
    }

    public async Task<TModel> Update(TModel entity)
    {
        var result = DbSet.Update(entity);
        await Save();

        return result.Entity;
    }

    private async Task<bool> Save()
    {
        try
        {
            int linesChanged = await context.SaveChangesAsync();
            return linesChanged > 0;
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Error on updating database");            
            return false;
        }
    }
}