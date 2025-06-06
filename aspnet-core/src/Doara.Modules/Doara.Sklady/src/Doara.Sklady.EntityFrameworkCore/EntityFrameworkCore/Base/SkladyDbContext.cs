﻿using Doara.Sklady.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Doara.Sklady.EntityFrameworkCore.Base;

[ConnectionStringName(SkladyDbProperties.ConnectionStringName)]
public class SkladyDbContext : AbpDbContext<SkladyDbContext>, ISkladyDbContext
{
    public DbSet<Container> ContainerSet { get; set; }
    public DbSet<ContainerItem> ContainerItemSet { get; set; }
    public DbSet<StockMovement> StockMovementSet { get; set; }

    public SkladyDbContext(DbContextOptions<SkladyDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureSklady();
    }
}
