using Entities = Boligmappa.Core.Entities;
using Boligmappa.Core.Repositories;
using Boligmappa.Data.Postgres.Repositories.Abstractions;
using Boligmappa.Data.Postgres.Models;
using Microsoft.EntityFrameworkCore;
using Boligmappa.Core.Extensions;

namespace Boligmappa.Data.Postgres.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IRepository<User, Entities.User> repository;

    public UserRepository(IRepository<User, Entities.User> repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<Entities.User>> GetMasterCardUsers()
    {
        var users = await repository.DbSet.Where(u => u.CreditCard == "mastercard")
            .AsNoTracking()
            .ToListAsync();
        return users.ToEntity();
    }

    public async Task<IEnumerable<Entities.User>> GetPopularUsers()
    {
        var users = await repository.DbSet.Where(u => u.PostCount > 2)
            .AsNoTracking()
            .ToListAsync();
        return users.ToEntity();
    }

    public async Task Save(Entities.User user)
    {
        var newUser = new User
        {
            Id = user.Id,
            CreditCard = user.CreditCard,
            PostCount = user.PostCount,
            TodoCount = user.TodoCount,
            UserName = user.UserName
        };

        if(await repository.Exists(newUser.Id))
        {
            await repository.Update(newUser);
            return;
        }
        await repository.Add(newUser);
    }
}