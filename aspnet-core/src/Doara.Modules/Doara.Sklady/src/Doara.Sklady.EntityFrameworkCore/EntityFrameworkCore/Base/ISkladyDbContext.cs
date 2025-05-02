using Doara.Sklady.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Doara.Sklady.EntityFrameworkCore.Base;

[ConnectionStringName(SkladyDbProperties.ConnectionStringName)]
public interface ISkladyDbContext : IEfCoreDbContext
{
    DbSet<Container> ContainerSet { get; }
    DbSet<ContainerItem> ContainerItemSet { get; }
    DbSet<StockMovement> StockMovementSet { get; }
}
