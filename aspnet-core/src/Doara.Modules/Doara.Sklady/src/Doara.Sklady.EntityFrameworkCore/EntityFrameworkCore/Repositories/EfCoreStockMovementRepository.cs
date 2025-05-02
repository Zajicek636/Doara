using System;
using Doara.Sklady.Entities;
using Doara.Sklady.EntityFrameworkCore.Base;
using Doara.Sklady.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Doara.Sklady.EntityFrameworkCore.Repositories;

public class EfCoreStockMovementRepository(IDbContextProvider<SkladyDbContext> dbContextProvider)
    : EfCoreRepository<SkladyDbContext, StockMovement, Guid>(dbContextProvider), IStockMovementRepository;