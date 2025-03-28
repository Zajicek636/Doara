using System;
using System.Threading.Tasks;
using Doara.Sklady.Entities;

namespace Doara.Sklady.Repositories;

public interface IContainerRepository
{
    Task<Container> GetAsync(Guid id);
    Task<Container> CreateAsync(Container container);
    Task<Container> UpdateAsync(Container container);
    Task DeleteAsync(Guid id);
}