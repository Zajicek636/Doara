using System;
using System.Threading.Tasks;
using Doara.Sklady.Dto.WarehouseWorker;
using Doara.Sklady.Entities;
using Doara.Sklady.IAppServices;
using Doara.Sklady.Repositories;

namespace Doara.Sklady.AppServices;

public class WarehouseWorkerAppService(IWarehouseWorkerRepository warehouseWorkerRepository) : SkladyAppService, IWarehouseWorkerAppService
{
    //[Authorize] using Microsoft.AspNetCore.Authorization;
    public async Task<WarehouseWorkerDto> GetAsync(Guid id)
    {
        var res = await warehouseWorkerRepository.GetAsync(id);
        return ObjectMapper.Map<WarehouseWorker, WarehouseWorkerDto>(res); 
    }
    
    //[Authorize]
    public async Task<WarehouseWorkerDto> CreateAsync(WarehouseWorkerCreateInputDto input)
    {
        var guid = GuidGenerator.Create();
        var warehouseWorker = new WarehouseWorker(guid);
        var res = await warehouseWorkerRepository.CreateAsync(warehouseWorker);
        return ObjectMapper.Map<WarehouseWorker, WarehouseWorkerDto>(res); 
    }

    public async Task<WarehouseWorkerDto> UpdateAsync(WarehouseWorkerUpdateInputDto input)
    {
        var warehouseWorker = await warehouseWorkerRepository.GetAsync(input.Id);
        //warehouseWorker.SetName(input.Name);
        var res = await warehouseWorkerRepository.UpdateAsync(warehouseWorker);
        return ObjectMapper.Map<WarehouseWorker, WarehouseWorkerDto>(res); 
    }

    public async Task DeleteAsync(Guid id)
    {
        await GetAsync(id);
        await warehouseWorkerRepository.DeleteAsync(id);
    }

    public async Task<WarehouseWorkerDto> ChangeStateAsync(WarehouseWorkerChangeStateInputDto input)
    {
        var warehouseWorker = await warehouseWorkerRepository.GetAsync(input.Id);
        //warehouseWorker.SetName(input.Name);
        var res = await warehouseWorkerRepository.UpdateAsync(warehouseWorker);
        return ObjectMapper.Map<WarehouseWorker, WarehouseWorkerDto>(res); 
    }
}