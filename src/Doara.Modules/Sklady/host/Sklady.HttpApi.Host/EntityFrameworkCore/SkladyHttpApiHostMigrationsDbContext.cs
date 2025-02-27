using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Sklady.EntityFrameworkCore;

public class SkladyHttpApiHostMigrationsDbContext : AbpDbContext<SkladyHttpApiHostMigrationsDbContext>
{
    public SkladyHttpApiHostMigrationsDbContext(DbContextOptions<SkladyHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureSklady();
    }
}
