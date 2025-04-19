using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Entities;
using Doara.Ucetnictvi.EntityFrameworkCore.Base;
using Doara.Ucetnictvi.Repositories;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq;

namespace Doara.Ucetnictvi.EntityFrameworkCore.Repositories;

public class EfCoreAddressRepository(IDbContextProvider<UcetnictviDbContext> dbContextProvider)
    : EfCoreRepository<UcetnictviDbContext, Address>(dbContextProvider), IAddressRepository
{
    public async Task<Address> GetAsync(Guid id)
    {
        var address = await FindAsync(x => x.Id == id);
        if (address == null)
        {
            throw new EntityNotFoundException(typeof(Address), id);
        }

        return address;
    }

    public async Task<List<Address>> GetAllAsync(int skip, int take, string sortBy, Expression<Func<Address, bool>>? filter = null)
    {
        var query = await GetQueryableAsync();
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

    public async Task<long> GetCountAsync(Expression<Func<Address, bool>>? filter = null)
    {
        var query = await GetQueryableAsync();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.CountAsync();
    }

    public async Task<Address> CreateAsync(Address address)
    {
        return await base.InsertAsync(address);
    }

    public async Task<Address> UpdateAsync(Address address)
    {
        return await base.UpdateAsync(address);
    }

    public async Task DeleteAsync(Guid id)
    {
        await base.DeleteAsync(x => x.Id == id);
    }

    public async Task<bool> AnyAsync(Expression<Func<Address, bool>> predicate)
    {
        var query = await GetQueryableAsync();
        return await query.AnyAsync(predicate);
    }
}