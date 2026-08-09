using System.Reflection;

using Microsoft.EntityFrameworkCore;

namespace A_MNY9M.Infrastructure.Database.EF.Contexts;

public class AmnyamDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}