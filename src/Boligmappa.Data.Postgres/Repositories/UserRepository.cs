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
        var existingUser = await repository.Get(user.Id);
        if(existingUser != null)
        {
            existingUser.CreditCard = user.CreditCard;
            existingUser.PostCount = user.PostCount;
            existingUser.TodoCount = user.TodoCount;
            existingUser.UserName = user.UserName;

            await repository.Update(existingUser);
            return;
        }

        var newUser = new User
        {
            Id = user.Id,
            CreditCard = user.CreditCard,
            PostCount = user.PostCount,
            TodoCount = user.TodoCount,
            UserName = user.UserName
        };
        await repository.Add(newUser);
    }
}