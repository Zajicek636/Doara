using System;
using System.Threading.Tasks;
using Doara.Sklady.Entities;

namespace Doara.Sklady.Repositories;

public interface IWarehouseWorkerRepository
{
    Task<WarehouseWorker> GetAsync(Guid id);
    Task<WarehouseWorker> CreateAsync(WarehouseWorker containerItem);
    Task<WarehouseWorker> UpdateAsync(WarehouseWorker containerItem);
    Task DeleteAsync(Guid id);
}