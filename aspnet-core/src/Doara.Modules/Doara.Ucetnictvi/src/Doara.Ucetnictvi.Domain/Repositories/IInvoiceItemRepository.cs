using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Entities;

namespace Doara.Ucetnictvi.Repositories;

public interface IInvoiceItemRepository
{
    Task<InvoiceItem> GetAsync(Guid id);
    Task<List<InvoiceItem>> GetAllAsync(int skip, int take, string sortBy, Expression<Func<InvoiceItem, bool>>? filter = null);
    Task<long> GetCountAsync(Expression<Func<InvoiceItem, bool>>? filter = null);
    Task CreateManyAsync(IEnumerable<InvoiceItem> invoiceItems);
    Task UpdateManyAsync(IEnumerable<InvoiceItem> invoiceItems);
    Task DeleteManyAsync(IEnumerable<InvoiceItem> invoiceItems);
    Task<InvoiceItem> CreateAsync(InvoiceItem invoiceItem);
    Task<bool> AnyAsync(Expression<Func<InvoiceItem, bool>> predicate);
}