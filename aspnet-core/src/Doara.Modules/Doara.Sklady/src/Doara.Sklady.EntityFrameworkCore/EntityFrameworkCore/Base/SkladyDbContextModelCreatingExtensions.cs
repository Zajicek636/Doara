using Doara.Sklady.Constants;
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

        builder.Entity<Container>(b =>
        {
            //Configure table & schema name
            b.ToTable(SkladyDbProperties.DbTablePrefix + "_" + nameof(Container), SkladyDbProperties.DbSchema);
            b.ConfigureByConvention();

            //Properties
            b.Property(c => c.Name).IsRequired().HasMaxLength(ContainerConstants.MaxNameLength);
            b.Property(c => c.Description).IsRequired().HasMaxLength(ContainerConstants.MaxDescriptionLength);
            
            //Relations
            b.HasMany(c => c.Items).WithOne(ci => ci.Container).HasForeignKey(ci => ci.ContainerId)
                .OnDelete(DeleteBehavior.Cascade);

            //Indexes
            b.HasIndex(c => c.Id);
        });
        
        builder.Entity<ContainerItem>(b =>
        {
            //Configure table & schema name
            b.ToTable(SkladyDbProperties.DbTablePrefix + "_" + nameof(ContainerItem), SkladyDbProperties.DbSchema);
            b.ConfigureByConvention();

            //Properties
            b.Property(ci => ci.Name).IsRequired().HasMaxLength(ContainerItemConstants.MaxNameLength);
            b.Property(ci => ci.QuantityType).HasConversion(
                    x => (char)x, 
                    x => (QuantityType)x)
                .IsRequired();
            b.Property(ci => ci.Description).IsRequired().HasMaxLength(ContainerItemConstants.MaxDescriptionLength);
            b.Property(ci => ci.PurchaseUrl).HasMaxLength(ContainerItemConstants.MaxPurchaseUrlLength);
            b.Property(ci => ci.RealPrice).IsRequired();
            b.Property(ci => ci.PresentationPrice).IsRequired();
            b.Property(ci => ci.Markup).IsRequired();
            b.Property(ci => ci.MarkupRate).IsRequired();
            b.Property(ci => ci.Discount).IsRequired();
            b.Property(ci => ci.DiscountRate).IsRequired();
            //Indexes
            b.HasIndex(ci => ci.Id);
            b.HasOne(ci => ci.Container).WithMany(c => c.Items)
                .HasForeignKey(x => x.ContainerId).OnDelete(DeleteBehavior.Cascade);
            
            b.HasMany(c => c.Movements).WithOne(ci => ci.ContainerItem).HasForeignKey(ci => ci.ContainerItemId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<StockMovement>(b =>
        {
            b.Property(ci => ci.Quantity).IsRequired();
            b.Property(ci => ci.MovementCategory).HasConversion(
                    x => (char)x, 
                    x => (MovementCategory)x)
                .IsRequired();
            b.Property(ci => ci.RelatedDocumentId);
            
            b.HasIndex(c => c.Id);
            b.HasOne(ci => ci.ContainerItem).WithMany(c => c.Movements)
                .HasForeignKey(x => x.ContainerItemId).OnDelete(DeleteBehavior.Cascade);
        });
    }
}
