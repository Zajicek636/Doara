using Doara.Ucetnictvi.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Doara.Ucetnictvi.EntityFrameworkCore.Base;

[ConnectionStringName(UcetnictviDbProperties.ConnectionStringName)]
public class UcetnictviDbContext : AbpDbContext<UcetnictviDbContext>, IUcetnictviDbContext
{
    public UcetnictviDbContext(DbContextOptions<UcetnictviDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureUcetnictvi();
    }
    
    public DbSet<Country> CountrySet { get; set; }
    public DbSet<Address> AddressSet { get; set; }
    public DbSet<Subject> SubjectSet { get; set; }
    public DbSet<InvoiceItem> InvoiceItemSet { get; set; }
    public DbSet<Invoice> InvoiceSet { get; set; }
}
