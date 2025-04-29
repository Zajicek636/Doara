using System;
using System.Threading.Tasks;
using Doara.Sklady.Entities;
using Doara.Sklady.EntityFrameworkCore.Base;
using Doara.Sklady.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Doara.Sklady.EntityFrameworkCore.Repositories;

public class EfCoreWarehouseWorkerRepository(IDbContextProvider<SkladyDbContext> dbContextProvider)
    : EfCoreRepository<SkladyDbContext, WarehouseWorker>(dbContextProvider), IWarehouseWorkerRepository
{
    public async Task<WarehouseWorker> GetAsync(Guid id)
    {
        return await base.GetAsync(x => x.Id == id);
    }

    public async Task<WarehouseWorker> CreateAsync(WarehouseWorker warehouseWorker)
    {
        return await base.InsertAsync(warehouseWorker);
    }

    public async Task<WarehouseWorker> UpdateAsync(WarehouseWorker warehouseWorker)
    {
        return await base.UpdateAsync(warehouseWorker);
    }

    public async Task DeleteAsync(Guid id)
    {
        await base.DeleteAsync(x => x.Id == id);
    }
}