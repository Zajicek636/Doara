using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Entities;

namespace Doara.Ucetnictvi.Repositories;

public interface IInvoiceRepository
{
    Task<Invoice> GetAsync(Guid id);
    Task<List<Invoice>> GetAllAsync(int skip, int take, string sortBy, Expression<Func<Invoice, bool>>? filter = null);
    Task<long> GetCountAsync(Expression<Func<Invoice, bool>>? filter = null);
    Task<Invoice> CreateAsync(Invoice invoice);
    Task<Invoice> UpdateAsync(Invoice invoice);
    Task DeleteAsync(Guid id);
    Task<bool> AnyAsync(Expression<Func<Invoice, bool>> predicate);
}