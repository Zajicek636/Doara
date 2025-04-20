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

public class EfCoreInvoiceItemRepository(IDbContextProvider<UcetnictviDbContext> dbContextProvider)
    : EfCoreRepository<UcetnictviDbContext, InvoiceItem, Guid>(dbContextProvider), IInvoiceItemRepository
{
    public async Task<InvoiceItem> GetAsync(Guid id)
    {
        var invoiceItem = await FindAsync(id);
        if (invoiceItem == null)
        {
            throw new EntityNotFoundException(typeof(InvoiceItem), id);
        }

        return invoiceItem;
    }

    public async Task<List<InvoiceItem>> GetAllAsync(int skip, int take, string sortBy, Expression<Func<InvoiceItem, bool>>? filter = null)
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

    public async Task<long> GetCountAsync(Expression<Func<InvoiceItem, bool>>? filter = null)
    {
        var query = await GetQueryableAsync();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.CountAsync();
    }

    public async Task<InvoiceItem> CreateAsync(InvoiceItem invoiceItem)
    {
        return await base.InsertAsync(invoiceItem);
    }

    public async Task<InvoiceItem> UpdateAsync(InvoiceItem invoiceItem)
    {
        return await base.UpdateAsync(invoiceItem);
    }

    public async Task DeleteAsync(Guid id)
    {
        await base.DeleteAsync(id);
    }

    public async Task<bool> AnyAsync(Expression<Func<InvoiceItem, bool>> predicate)
    {
        var query = await GetQueryableAsync();
        return await query.AnyAsync(predicate);
    }
}