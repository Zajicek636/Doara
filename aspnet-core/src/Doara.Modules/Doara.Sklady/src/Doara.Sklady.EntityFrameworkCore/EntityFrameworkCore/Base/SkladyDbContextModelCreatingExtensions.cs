using System;
using Doara.Sklady.Entities;
using Doara.Sklady.Enums;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Doara.Sklady.EntityFrameworkCore.Base;

public static class SkladyDbContextModelCreatingExtensions
{
    public static void ConfigureSklady(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<ContainerItem>(b =>
        {
            //Configure table & schema name
            b.ToTable(SkladyDbProperties.DbTablePrefix + "_" + nameof(ContainerItem), SkladyDbProperties.DbSchema);
            b.ConfigureByConvention();

            //Properties
            b.Property(ci => ci.Name).IsRequired().HasMaxLength(255);
            b.Property(ci => ci.State).HasConversion(
                    x => (char)x, 
                    x => Enum.Parse<ContainerItemState>(x.ToString()))
                .IsRequired();
            b.Property(ci => ci.QuantityType).HasConversion(
                    x => (char)x, 
                    x => Enum.Parse<QuantityType>(x.ToString()))
                .IsRequired();
            b.Property(ci => ci.Description).IsRequired().HasMaxLength(4000);
            b.Property(ci => ci.PurchaseUrl).HasMaxLength(1000);
            b.Property(ci => ci.RealPrice);
            //b.Property(ci => ci.PresentationPrice);
            b.Property(ci => ci.Markup);
            b.Property(ci => ci.MarkupRate);
            b.Property(ci => ci.Discount);
            b.Property(ci => ci.DiscountRate);
            b.Property(ci => ci.Quantity);
            //Indexes
            b.HasIndex(ci => ci.Id);
            b.HasOne<Container>(ci => ci.Container).WithMany()
                .HasForeignKey(x => x.ContainerId).IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        });
        
        builder.Entity<WarehouseWorker>(b =>
        {
            //Configure table & schema name
            b.ToTable(SkladyDbProperties.DbTablePrefix + "_" + nameof(WarehouseWorker), SkladyDbProperties.DbSchema);
            b.ConfigureByConvention();
            
            //Indexes
            b.HasIndex(ww => ww.Id);
        });

        builder.Entity<Container>(b =>
        {
            //Configure table & schema name
            b.ToTable(SkladyDbProperties.DbTablePrefix + "_" + nameof(Container), SkladyDbProperties.DbSchema);
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
