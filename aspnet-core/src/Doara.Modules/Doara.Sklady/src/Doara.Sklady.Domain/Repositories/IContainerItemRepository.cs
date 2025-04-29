using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Doara.Sklady.Entities;

namespace Doara.Sklady.Repositories;

public interface IContainerItemRepository
{
    Task<ContainerItem> GetAsync(Guid id);
    Task<List<ContainerItem>> GetAllAsync(int skip, int take, string sortBy, Expression<Func<ContainerItem, bool>>? filter = null);
    Task<long> GetCountAsync(Expression<Func<ContainerItem, bool>>? filter = null);
    Task<ContainerItem> CreateAsync(ContainerItem containerItem);
    Task<ContainerItem> UpdateAsync(ContainerItem containerItem);
    Task DeleteAsync(Guid id);
    Task<bool> AnyAsync(Expression<Func<ContainerItem, bool>> predicate);
}