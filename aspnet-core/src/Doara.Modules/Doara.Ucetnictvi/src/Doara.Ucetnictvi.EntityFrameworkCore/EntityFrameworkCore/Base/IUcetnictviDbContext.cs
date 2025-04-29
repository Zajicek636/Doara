using Doara.Ucetnictvi.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Doara.Ucetnictvi.EntityFrameworkCore.Base;

[ConnectionStringName(UcetnictviDbProperties.ConnectionStringName)]
public interface IUcetnictviDbContext : IEfCoreDbContext
{
    DbSet<Country> CountrySet { get; }
    DbSet<Address> AddressSet { get; }
    DbSet<Subject> SubjectSet { get; }
    DbSet<InvoiceItem> InvoiceItemSet { get; }
    DbSet<Invoice> InvoiceSet { get; }
}
