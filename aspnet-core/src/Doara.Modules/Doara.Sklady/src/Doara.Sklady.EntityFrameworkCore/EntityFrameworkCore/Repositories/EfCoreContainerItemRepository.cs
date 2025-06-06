﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doara.Sklady.Entities;
using Doara.Sklady.EntityFrameworkCore.Base;
using Doara.Sklady.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace Doara.Sklady.EntityFrameworkCore.Repositories;

public class EfCoreContainerItemRepository(IDbContextProvider<SkladyDbContext> dbContextProvider)
: EfCoreRepository<SkladyDbContext, ContainerItem, Guid>(dbContextProvider), IContainerItemRepository
{
    public async Task<ContainerItem> GetAsync(Guid id)
    {
        var containerItem = await FindAsync(id);
        if (containerItem == null)
        {
            throw new EntityNotFoundException(typeof(ContainerItem), id);
        }

        return containerItem;
    }

    public async Task<List<ContainerItem>> GetAllAsync(int skip, int take, string sortBy, bool withDetail, Expression<Func<ContainerItem, bool>>? filter = null)
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

    public async Task<long> GetCountAsync(Expression<Func<ContainerItem, bool>>? filter = null)
    {
        var query = await GetQueryableAsync();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.CountAsync();
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
        await base.DeleteAsync(id);
    }

    public async Task<bool> AnyAsync(Expression<Func<ContainerItem, bool>> predicate)
    {
        var query = await GetQueryableAsync();
        return await query.AnyAsync(predicate);
    }

    public async Task<List<ContainerItem>> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        return (await WithDetailsAsync()).Where(x => ids.Contains(x.Id)).ToList();
    }

    public override async Task<IQueryable<ContainerItem>> WithDetailsAsync()
    {
        return (await base.WithDetailsAsync())
            .Include(x => x.Container)
            .Include(x => x.Movements);
    }
}