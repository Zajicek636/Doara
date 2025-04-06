using System;
using System.Threading.Tasks;
using Doara.Sklady.Dto.WarehouseWorker;
using Volo.Abp.Application.Services;

namespace Doara.Sklady.IAppServices;

public interface IWarehouseWorkerAppService : IApplicationService
{
    Task<WarehouseWorkerDto> GetAsync(Guid id);
    Task<WarehouseWorkerDto> CreateAsync(WarehouseWorkerCreateInputDto input);
    Task<WarehouseWorkerDto> UpdateAsync(WarehouseWorkerUpdateInputDto input);
    Task DeleteAsync(Guid id);
    Task<WarehouseWorkerDto> ChangeStateAsync(WarehouseWorkerChangeStateInputDto input);
}