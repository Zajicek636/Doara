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
    Task<InvoiceItem> CreateAsync(InvoiceItem invoiceItem);
    Task<InvoiceItem> UpdateAsync(InvoiceItem invoiceItem);
    Task DeleteAsync(Guid id);
    Task<bool> AnyAsync(Expression<Func<InvoiceItem, bool>> predicate);
}