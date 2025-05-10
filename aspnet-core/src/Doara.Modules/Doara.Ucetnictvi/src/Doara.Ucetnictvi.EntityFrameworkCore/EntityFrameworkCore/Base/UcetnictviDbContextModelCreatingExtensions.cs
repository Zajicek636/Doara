using Doara.Ucetnictvi.Constants;
using Doara.Ucetnictvi.Entities;
using Doara.Ucetnictvi.Enums;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Doara.Ucetnictvi.EntityFrameworkCore.Base;

public static class UcetnictviDbContextModelCreatingExtensions
{
    public static void ConfigureUcetnictvi(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<Country>(b =>
        {
            //Configure table & schema name
            b.ToTable(UcetnictviDbProperties.DbTablePrefix + "_" + nameof(Country), UcetnictviDbProperties.DbSchema);
            b.ConfigureByConvention();

            //Properties
            b.Property(c => c.Name).IsRequired().HasMaxLength(CountryConstants.MaxNameLength);
            b.Property(c => c.Code).IsRequired().HasMaxLength(CountryConstants.MaxCodeLength);

            //Relations
            b.HasMany(c => c.Addresses).WithOne(a => a.Country).HasForeignKey(a => a.CountryId);
            
            //Indexes
            b.HasIndex(c => c.Id);
        });
        
        builder.Entity<Address>(b =>
        {
            //Configure table & schema name
            b.ToTable(UcetnictviDbProperties.DbTablePrefix + "_" + nameof(Address), UcetnictviDbProperties.DbSchema);
            b.ConfigureByConvention();

            //Properties
            b.Property(a => a.Street).IsRequired().HasMaxLength(AddressConstants.MaxStreetLength);
            b.Property(a => a.City).IsRequired().HasMaxLength(AddressConstants.MaxCityLength);
            b.Property(a => a.PostalCode).IsRequired().HasMaxLength(AddressConstants.MaxPostalCodeLength);

            //Relations
            b.HasMany(c => c.Subjects).WithOne(s => s.Address).HasForeignKey(a => a.AddressId);
            
            //Indexes
            b.HasIndex(a => a.Id);
            b.HasOne(a => a.Country).WithMany(c => c.Addresses).HasForeignKey(a => a.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        builder.Entity<Subject>(b =>
        {
            //Configure table & schema name
            b.ToTable(UcetnictviDbProperties.DbTablePrefix + "_" + nameof(Subject), UcetnictviDbProperties.DbSchema);
            b.ConfigureByConvention();

            //Properties
            b.Property(s => s.Name).IsRequired().HasMaxLength(SubjectConstants.MaxNameLength);
            b.Property(s => s.Ic).HasMaxLength(SubjectConstants.MaxIcLength);
            b.Property(s => s.Dic).HasMaxLength(SubjectConstants.MaxDicLength);
            b.Property(s => s.IsVatPayer).IsRequired();

            //Relations
            b.HasMany(s => s.InvoicesPairAsCustomer).WithOne(i => i.Customer).HasForeignKey(a => a.CustomerId);
            b.HasMany(s => s.InvoicesPairAsSupplier).WithOne(i => i.Supplier).HasForeignKey(a => a.SupplierId);
            
            //Indexes
            b.HasIndex(s => s.Id);
            b.HasOne(s => s.Address).WithMany(a => a.Subjects).HasForeignKey(a => a.AddressId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        builder.Entity<InvoiceItem>(b =>
        {
            //Configure table & schema name
            b.ToTable(UcetnictviDbProperties.DbTablePrefix + "_" + nameof(InvoiceItem), UcetnictviDbProperties.DbSchema);
            b.ConfigureByConvention();

            //Properties
            b.Property(ii => ii.Description).IsRequired().HasMaxLength(InvoiceItemConstants.MaxDescriptionLength);
            b.Property(ii => ii.Quantity).IsRequired();
            b.Property(ii => ii.UnitPrice).IsRequired();
            b.Property(ii => ii.NetAmount).IsRequired();
            b.Property(ii => ii.VatRate).IsRequired();
            b.Property(ii => ii.VatAmount).IsRequired();
            b.Property(ii => ii.GrossAmount).IsRequired();
            b.Property(ii => ii.ContainerItemId);
            b.Property(ii => ii.StockMovementId);
            
            //Indexes
            b.HasIndex(s => s.Id);
            b.HasOne(s => s.Invoice).WithMany(a => a.Items).HasForeignKey(a => a.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        builder.Entity<Invoice>(b =>
        {
            //Configure table & schema name
            b.ToTable(UcetnictviDbProperties.DbTablePrefix + "_" + nameof(Invoice), UcetnictviDbProperties.DbSchema);
            b.ConfigureByConvention();

            //Properties
            b.Property(s => s.InvoiceNumber).IsRequired().HasMaxLength(InvoiceConstants.MaxInvoiceNumberLength);
            b.Property(s => s.IssueDate).IsRequired();
            b.Property(s => s.TaxDate);
            b.Property(s => s.DeliveryDate);
            b.Property(s => s.InvoiceType).HasConversion(
                    x => (char)x, 
                    x => (InvoiceType)x)
                .IsRequired();
            b.Property(s => s.TotalNetAmount).IsRequired();
            b.Property(s => s.TotalVatAmount).IsRequired();
            b.Property(s => s.TotalGrossAmount).IsRequired();
            b.Property(s => s.PaymentTerms).HasMaxLength(InvoiceConstants.MaxPaymentTermsLength);
            b.Property(ci => ci.VatRate).HasConversion(
                    x => (char)x, 
                    x => (VatRate)x)
                .IsRequired();
            b.Property(s => s.VariableSymbol).HasMaxLength(InvoiceConstants.MaxVariableSymbolLength);
            b.Property(s => s.ConstantSymbol).HasMaxLength(InvoiceConstants.MaxConstantSymbolLength);
            b.Property(s => s.SpecificSymbol).HasMaxLength(InvoiceConstants.MaxSpecificSymbolLength);

            //Relations
            b.HasMany(s => s.Items).WithOne(ii => ii.Invoice).HasForeignKey(ii => ii.InvoiceId);
            
            //Indexes
            b.HasIndex(s => s.Id);
            b.HasOne(s => s.Supplier).WithMany(a => a.InvoicesPairAsSupplier).HasForeignKey(a => a.SupplierId) 
                .OnDelete(DeleteBehavior.Restrict);
            b.HasOne(s => s.Customer).WithMany(a => a.InvoicesPairAsCustomer).HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
