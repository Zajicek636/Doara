using System;
using System.Threading.Tasks;
using Doara.Sklady.Entities;
using Doara.Sklady.EntityFrameworkCore.Base;
using Doara.Sklady.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Doara.Sklady.EntityFrameworkCore.Repositories;

public class EfCoreContainerRepository(IDbContextProvider<SkladyDbContext> dbContextProvider)
    : EfCoreRepository<SkladyDbContext, Container>(dbContextProvider), IContainerRepository
{
    public async Task<Container> GetAsync(Guid id)
    {
        return await base.GetAsync(x => x.Id == id);
    }

    public async Task<Container> CreateAsync(Container container)
    {
        return await base.InsertAsync(container);
    }

    public async Task<Container> UpdateAsync(Container container)
    {
        return await base.UpdateAsync(container);
    }

    public async Task DeleteAsync(Guid id)
    {
        await base.DeleteAsync(x => x.Id == id);
    }
}