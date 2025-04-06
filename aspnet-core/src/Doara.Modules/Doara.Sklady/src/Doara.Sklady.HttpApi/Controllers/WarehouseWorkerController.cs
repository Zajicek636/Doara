using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Doara.Sklady.Dto.WarehouseWorker;
using Doara.Sklady.IAppServices;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace Doara.Sklady.Controllers;

[Area(SkladyRemoteServiceConsts.ModuleName)]
[RemoteService(Name = SkladyRemoteServiceConsts.RemoteServiceName)]
[Route("api/Sklady/WarehouseWorker")]
public class WarehouseWorkerController(IWarehouseWorkerAppService warehouseWorkerAppService)
    : SkladyController, IWarehouseWorkerAppService
{
    [HttpGet]
    //[Route("authorized")]
    //[Authorize]
    public async Task<WarehouseWorkerDto> GetAsync([Required] Guid id)
    {
        return await warehouseWorkerAppService.GetAsync(id);
    }

    [HttpPost]
    public async Task<WarehouseWorkerDto> CreateAsync(WarehouseWorkerCreateInputDto input)
    {
        return await warehouseWorkerAppService.CreateAsync(input);
    }
    
    [HttpPut]
    public async Task<WarehouseWorkerDto> UpdateAsync(WarehouseWorkerUpdateInputDto input)
    {
        return await warehouseWorkerAppService.UpdateAsync(input);
    }
    
    [HttpDelete]
    public async Task DeleteAsync([Required] Guid id)
    {
        await warehouseWorkerAppService.DeleteAsync(id);
    }
    
    [HttpPatch]
    public async Task<WarehouseWorkerDto> ChangeStateAsync(WarehouseWorkerChangeStateInputDto input)
    {
        return await warehouseWorkerAppService.ChangeStateAsync(input);
    }
}