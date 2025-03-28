using Doara.Sklady.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Doara.Sklady.EntityFrameworkCore;

public static class SkladyDbContextModelCreatingExtensions
{
    public static void ConfigureSklady(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<ContainerItem>(b =>
        {
            //Configure table & schema name
            b.ToTable(SkladyDbProperties.DbTablePrefix + nameof(ContainerItem), SkladyDbProperties.DbSchema);
            b.ConfigureByConvention();

            //Properties
            b.Property(ci => ci.Name).IsRequired().HasMaxLength(255);
            
            //Indexes
            b.HasIndex(ci => ci.Id);
        });
        
        builder.Entity<WarehouseWorker>(b =>
        {
            //Configure table & schema name
            b.ToTable(SkladyDbProperties.DbTablePrefix + nameof(WarehouseWorker), SkladyDbProperties.DbSchema);
            b.ConfigureByConvention();

            //Properties
            b.Property(ww => ww.Name).IsRequired().HasMaxLength(255);
            
            //Indexes
            b.HasIndex(ww => ww.Id);
        });

        builder.Entity<Container>(b =>
        {
            //Configure table & schema name
            b.ToTable(SkladyDbProperties.DbTablePrefix + nameof(Container), SkladyDbProperties.DbSchema);
            b.ConfigureByConvention();

            //Properties
            b.Property(c => c.Name).IsRequired().HasMaxLength(255);

            //Relations
            b.HasMany(c => c.WarehouseWorkers).WithOne().HasForeignKey(ww => ww.Id);
            b.HasMany(c => c.Items).WithOne().HasForeignKey(ci => ci.Id);

            //Indexes
            b.HasIndex(c => c.Id);
        });
    }
}
