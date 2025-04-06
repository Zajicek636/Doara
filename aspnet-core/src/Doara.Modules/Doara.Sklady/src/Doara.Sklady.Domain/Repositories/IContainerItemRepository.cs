using System;
using System.Threading.Tasks;
using Doara.Sklady.Entities;

namespace Doara.Sklady.Repositories;

public interface IContainerItemRepository
{
    Task<ContainerItem> GetAsync(Guid id);
    Task<ContainerItem> CreateAsync(ContainerItem containerItem);
    Task<ContainerItem> UpdateAsync(ContainerItem containerItem);
    Task DeleteAsync(Guid id);
}