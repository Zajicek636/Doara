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
    [HttpGet("{id:guid}")]
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
    
    [HttpPut("{id:guid}")]
    [Authorize(SkladyPermissions.UpdateContainerItemPermission)]
    public async Task<ContainerItemDetailDto> UpdateAsync([Required] Guid id, ContainerItemUpdateInputDto input)
    {
        return await containerItemAppService.UpdateAsync(id, input);
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(SkladyPermissions.DeleteContainerItemPermission)]
    public async Task DeleteAsync([Required] Guid id)
    {
        await containerItemAppService.DeleteAsync(id);
    }
}