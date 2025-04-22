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

public class EfCoreInvoiceRepository(IDbContextProvider<UcetnictviDbContext> dbContextProvider)
    : EfCoreRepository<UcetnictviDbContext, Invoice, Guid>(dbContextProvider), IInvoiceRepository
{
    public async Task<Invoice> GetAsync(Guid id)
    {
        var invoice = await FindAsync(id);
        if (invoice == null)
        {
            throw new EntityNotFoundException(typeof(Invoice), id);
        }

        return invoice;
    }

    public async Task<List<Invoice>> GetAllAsync(int skip, int take, string sortBy, bool withDetail, Expression<Func<Invoice, bool>>? filter = null)
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

    public async Task<long> GetCountAsync(Expression<Func<Invoice, bool>>? filter = null)
    {
        var query = await GetQueryableAsync();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.CountAsync();
    }

    public async Task<Invoice> CreateAsync(Invoice invoice)
    {
        return await base.InsertAsync(invoice);
    }

    public async Task<Invoice> UpdateAsync(Invoice invoice)
    {
        return await base.UpdateAsync(invoice);
    }

    public async Task DeleteAsync(Guid id)
    {
        await base.DeleteAsync(id);
    }

    public async Task<bool> AnyAsync(Expression<Func<Invoice, bool>> predicate)
    {
        var query = await GetQueryableAsync();
        return await query.AnyAsync(predicate);
    }
    
    public override async Task<IQueryable<Invoice>> WithDetailsAsync()
    {
        return (await base.WithDetailsAsync())
            .Include(x => x.Customer)
            .Include(x => x.Customer.Address)
            .Include(x => x.Customer.Address.Country)
            .Include(x => x.Supplier)
            .Include(x => x.Supplier.Address)
            .Include(x => x.Supplier.Address.Country)
            .Include(x => x.Items);
    }
}