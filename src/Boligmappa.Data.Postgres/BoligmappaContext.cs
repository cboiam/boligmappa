using Boligmappa.Data.Postgres.Models;
using Microsoft.EntityFrameworkCore;

namespace Boligmappa.Data.Postgres;

public class BoligmappaContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public BoligmappaContext(DbContextOptions<BoligmappaContext> options)
        : base(options) { }
}
