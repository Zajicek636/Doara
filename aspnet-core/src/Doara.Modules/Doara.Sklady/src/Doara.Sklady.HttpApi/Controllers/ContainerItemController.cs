using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Doara.Sklady.Dto.ContainerItem;
using Doara.Sklady.Dto.StockMovement;
using Doara.Sklady.IAppServices;
using Doara.Sklady.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Doara.Sklady.Controllers;

[Area(SkladyRemoteServiceConsts.ModuleName)]
[RemoteService(Name = SkladyRemoteServiceConsts.RemoteServiceName)]
[Route("api/Sklady/ContainerItem")]
public class ContainerItemController(IContainerItemAppService containerItemAppService)
    : SkladyController, IContainerItemAppService
{
    [HttpGet]
    [Authorize(SkladyPermissions.ReadContainerItemPermission)]
    public async Task<ContainerItemDetailDto> GetAsync([Required] Guid id)
    {
        return await containerItemAppService.GetAsync(id);
    }
    
    [HttpGet("GetAll")]
    [Authorize(SkladyPermissions.ReadContainerItemPermission)]
    public async Task<PagedResultDto<ContainerItemDto>> GetAllAsync(ContainerItemGetAllDto input)
    {
        return await containerItemAppService.GetAllAsync(input);
    }
    
    [HttpGet("GetAllWithDetail")]
    [Authorize(SkladyPermissions.ReadContainerItemPermission)]
    public async Task<PagedResultDto<ContainerItemDetailDto>> GetAllWithDetailAsync(ContainerItemGetAllDto input)
    {
        return await containerItemAppService.GetAllWithDetailAsync(input);
    }

    [HttpPost]
    [Authorize(SkladyPermissions.CreateContainerItemPermission)]
    public async Task<ContainerItemDetailDto> CreateAsync(ContainerItemCreateInputDto input)
    {
        return await containerItemAppService.CreateAsync(input);
    }
    
    [HttpPut]
    [Authorize(SkladyPermissions.UpdateContainerItemPermission)]
    public async Task<ContainerItemDetailDto> UpdateAsync([Required] Guid id, ContainerItemUpdateInputDto input)
    {
        return await containerItemAppService.UpdateAsync(id, input);
    }
    
    [HttpDelete]
    [Authorize(SkladyPermissions.DeleteContainerItemPermission)]
    public async Task DeleteAsync([Required] Guid id)
    {
        await containerItemAppService.DeleteAsync(id);
    }

    [HttpPost("AddStock")]
    [Authorize(SkladyPermissions.UpdateContainerItemPermission)]
    public async Task<ContainerItemDetailDto> AddStockAsync([Required] Guid id, StockMovementCreateInputDto input)
    {
        return await containerItemAppService.AddStockAsync(id, input);
    }

    [HttpPost("RemoveMovement")]
    [Authorize(SkladyPermissions.UpdateContainerItemPermission)]
    public async Task<ContainerItemDetailDto> RemoveMovementAsync([Required] Guid id, [Required] Guid stockMovementId)
    {
        return await containerItemAppService.RemoveMovementAsync(id, stockMovementId);
    }

    [HttpPost("Reserve")]
    [Authorize(SkladyPermissions.UpdateContainerItemPermission)]
    public async Task<ContainerItemDetailDto> ReserveItemAsync([Required] Guid id, StockMovementCreateInputDto input)
    {
        return await containerItemAppService.ReserveItemAsync(id, input);
    }

    [HttpPost("Use")]
    [Authorize(SkladyPermissions.UpdateContainerItemPermission)]
    public async Task<ContainerItemDetailDto> UseItemAsync([Required] Guid id, StockMovementCreateInputDto input)
    {
        return await containerItemAppService.UseItemAsync(id, input);
    }

    [HttpPost("ReservationToUsage")]
    [Authorize(SkladyPermissions.UpdateContainerItemPermission)]
    public async Task<ContainerItemDetailDto> ConvertReservationToUsageAsync([Required] Guid id, [Required] Guid stockMovementId)
    {
        return await containerItemAppService.ConvertReservationToUsageAsync(id, stockMovementId);
    }
}