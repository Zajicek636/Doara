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

public class EfCoreCountryRepository(IDbContextProvider<UcetnictviDbContext> dbContextProvider)
    : EfCoreRepository<UcetnictviDbContext, Country>(dbContextProvider), ICountryRepository
{
    public async Task<Country> GetAsync(Guid id)
    {
        var country = await FindAsync(x => x.Id == id);
        if (country == null)
        {
            throw new EntityNotFoundException(typeof(Country), id);
        }

        return country;
    }

    public async Task<List<Country>> GetAllAsync(int skip, int take, string sortBy, Expression<Func<Country, bool>>? filter = null)
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

    public async Task<long> GetCountAsync(Expression<Func<Country, bool>>? filter = null)
    {
        var query = await GetQueryableAsync();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.CountAsync();
    }

    public async Task<Country> CreateAsync(Country country)
    {
        return await base.InsertAsync(country);
    }

    public async Task<Country> UpdateAsync(Country country)
    {
        return await base.UpdateAsync(country);
    }

    public async Task DeleteAsync(Guid id)
    {
        await base.DeleteAsync(x => x.Id == id);
    }

    public async Task<bool> AnyAsync(Expression<Func<Country, bool>> predicate)
    {
        var query = await GetQueryableAsync();
        return await query.AnyAsync(predicate);
    }
}