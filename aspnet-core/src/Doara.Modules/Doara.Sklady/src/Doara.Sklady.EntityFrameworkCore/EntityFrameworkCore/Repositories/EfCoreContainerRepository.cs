using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Doara.Sklady.Entities;
using Doara.Sklady.EntityFrameworkCore.Base;
using Doara.Sklady.Repositories;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq;

namespace Doara.Sklady.EntityFrameworkCore.Repositories;

public class EfCoreContainerRepository(IDbContextProvider<SkladyDbContext> dbContextProvider)
    : EfCoreRepository<SkladyDbContext, Container, Guid>(dbContextProvider), IContainerRepository
{
    public async Task<Container> GetAsync(Guid id)
    {
        return await base.GetAsync(id);
    }

    public async Task<List<Container>> GetAllAsync(int skip, int take, string sortBy, bool withDetail, Expression<Func<Container, bool>>? filter = null)
    {
        var query = withDetail ? await WithDetailsAsync() : await GetQueryableAsync();
        if (filter != null)
        {
            query = query.Where(filter);
        }
        return await query
            .OrderBy(sortBy)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

    public async Task<long> GetCountAsync(Expression<Func<Container, bool>>? filter = null)
    {
        var query = await GetQueryableAsync();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.CountAsync();
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
        await base.DeleteAsync(id);
    }

    public async Task<bool> AnyAsync(Expression<Func<Container, bool>> predicate)
    {
        var query = await GetQueryableAsync();
        return await query.AnyAsync(predicate);
    }
    
    public override async Task<IQueryable<Container>> WithDetailsAsync()
    {
        return (await base.WithDetailsAsync())
            .Include(x => x.WarehouseWorkers)
            .Include(x => x.Items);
    }
}