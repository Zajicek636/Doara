using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Doara.Sklady.Entities;

namespace Doara.Sklady.Repositories;

public interface IContainerRepository
{
    Task<Container> GetAsync(Guid id);
    Task<List<Container>> GetAllAsync(int skip, int take, string sortBy, bool withDetail, Expression<Func<Container, bool>>? filter = null);
    Task<long> GetCountAsync(Expression<Func<Container, bool>>? filter = null);
    Task<Container> CreateAsync(Container container);
    Task<Container> UpdateAsync(Container container);
    Task DeleteAsync(Guid id);
    Task<bool> AnyAsync(Expression<Func<Container, bool>> predicate);
}