using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Doara.Sklady.Dto.ContainerItem;
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
    public async Task<ContainerItemDto> GetAsync([Required] Guid id)
    {
        return await containerItemAppService.GetAsync(id);
    }
    
    [HttpGet("GetAll")]
    [Authorize(SkladyPermissions.ReadContainerItemPermission)]
    public async Task<PagedResultDto<ContainerItemDto>> GetAllAsync(PagedAndSortedResultRequestDto input)
    {
        return await containerItemAppService.GetAllAsync(input);
    }

    [HttpPost]
    [Authorize(SkladyPermissions.CreateContainerItemPermission)]
    public async Task<ContainerItemDto> CreateAsync(ContainerItemCreateInputDto input)
    {
        return await containerItemAppService.CreateAsync(input);
    }
    
    [HttpPut]
    [Authorize(SkladyPermissions.UpdateContainerItemPermission)]
    public async Task<ContainerItemDto> UpdateAsync(ContainerItemUpdateInputDto input)
    {
        return await containerItemAppService.UpdateAsync(input);
    }
    
    [HttpDelete]
    [Authorize(SkladyPermissions.DeleteContainerItemPermission)]
    public async Task DeleteAsync([Required] Guid id)
    {
        await containerItemAppService.DeleteAsync(id);
    }
    
    [HttpPatch]
    [Authorize(SkladyPermissions.UpdateContainerItemPermission)]
    public async Task<ContainerItemDto> ChangeStateAsync(ContainerItemChangeStateInputDto input)
    {
        return await containerItemAppService.ChangeStateAsync(input);
    }
}