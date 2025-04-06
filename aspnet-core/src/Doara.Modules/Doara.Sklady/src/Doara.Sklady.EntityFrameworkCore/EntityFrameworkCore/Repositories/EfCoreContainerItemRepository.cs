using System;
using System.Threading.Tasks;
using Doara.Sklady.Entities;
using Doara.Sklady.EntityFrameworkCore.Base;
using Doara.Sklady.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Doara.Sklady.EntityFrameworkCore.Repositories;

public class EfCoreContainerItemRepository(IDbContextProvider<SkladyDbContext> dbContextProvider)
: EfCoreRepository<SkladyDbContext, ContainerItem>(dbContextProvider), IContainerItemRepository
{
    public async Task<ContainerItem> GetAsync(Guid id)
    {
        return await base.GetAsync(x => x.Id == id);
    }

    public async Task<ContainerItem> CreateAsync(ContainerItem containerItem)
    {
        return await base.InsertAsync(containerItem);
    }

    public async Task<ContainerItem> UpdateAsync(ContainerItem containerItem)
    {
        return await base.UpdateAsync(containerItem);
    }

    public async Task DeleteAsync(Guid id)
    {
        await base.DeleteAsync(x => x.Id == id);
    }
}