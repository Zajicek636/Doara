using Doara.Ucetnictvi.EntityFrameworkCore.Base;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Doara.Ucetnictvi.EntityFrameworkCore;

public class UcetnictviHttpApiHostMigrationsDbContext : AbpDbContext<UcetnictviHttpApiHostMigrationsDbContext>
{
    public UcetnictviHttpApiHostMigrationsDbContext(DbContextOptions<UcetnictviHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureUcetnictvi();
    }
}
