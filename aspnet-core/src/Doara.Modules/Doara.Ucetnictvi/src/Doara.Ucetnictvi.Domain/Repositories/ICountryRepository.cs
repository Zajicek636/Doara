using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Entities;

namespace Doara.Ucetnictvi.Repositories;

public interface ICountryRepository
{
    Task<Country> GetAsync(Guid id);
    Task<List<Country>> GetAllAsync(int skip, int take, string sortBy, Expression<Func<Country, bool>>? filter = null);
    Task<long> GetCountAsync(Expression<Func<Country, bool>>? filter = null);
    Task<Country> CreateAsync(Country country);
    Task<Country> UpdateAsync(Country country);
    Task DeleteAsync(Guid id);
    Task<bool> AnyAsync(Expression<Func<Country, bool>> predicate); 
}