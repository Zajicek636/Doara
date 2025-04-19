using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Entities;

namespace Doara.Ucetnictvi.Repositories;

public interface IAddressRepository
{
    Task<Address> GetAsync(Guid id);
    Task<List<Address>> GetAllAsync(int skip, int take, string sortBy, Expression<Func<Address, bool>>? filter = null);
    Task<long> GetCountAsync(Expression<Func<Address, bool>>? filter = null);
    Task<Address> CreateAsync(Address address);
    Task<Address> UpdateAsync(Address address);
    Task DeleteAsync(Guid id);
    Task<bool> AnyAsync(Expression<Func<Address, bool>> predicate);
}