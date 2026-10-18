using System.Reflection;

using Microsoft.EntityFrameworkCore;

namespace A_MNY9M.Infrastructure.Database;

public class AmnyamDbContext(DbContextOptions<AmnyamDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}