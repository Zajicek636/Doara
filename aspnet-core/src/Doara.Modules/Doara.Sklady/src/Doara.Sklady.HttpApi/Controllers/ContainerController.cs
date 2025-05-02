using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Doara.Sklady.Dto.Container;
using Doara.Sklady.IAppServices;
using Doara.Sklady.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Doara.Sklady.Controllers;

[Area(SkladyRemoteServiceConsts.ModuleName)]
[RemoteService(Name = SkladyRemoteServiceConsts.RemoteServiceName)]
[Route("api/Sklady/Container")]
public class ContainerController(IContainerAppService containerAppService)
    : SkladyController, IContainerAppService
{
    [HttpGet]
    [Authorize(SkladyPermissions.ReadContainerPermission)]
    public async Task<ContainerDetailDto> GetAsync([Required] Guid id)
    {
        return await containerAppService.GetAsync(id);
    }
    
    [HttpGet("GetAll")]
    [Authorize(SkladyPermissions.ReadContainerPermission)]
    public async Task<PagedResultDto<ContainerDto>> GetAllAsync(PagedAndSortedResultRequestDto input)
    {
        return await containerAppService.GetAllAsync(input);
    }
    
    [HttpGet("GetAllWithDetail")]
    [Authorize(SkladyPermissions.ReadContainerPermission)]
    public async Task<PagedResultDto<ContainerDetailDto>> GetAllWithDetailAsync(PagedAndSortedResultRequestDto input)
    {
        return await containerAppService.GetAllWithDetailAsync(input);
    }

    [HttpPost]
    [Authorize(SkladyPermissions.CreateContainerPermission)]
    public async Task<ContainerDetailDto> CreateAsync(ContainerCreateInputDto input)
    {
        return await containerAppService.CreateAsync(input);
    }
    
    [HttpPut]
    [Authorize(SkladyPermissions.UpdateContainerPermission)]
    public async Task<ContainerDetailDto> UpdateAsync([Required] Guid id, ContainerUpdateInputDto input)
    {
        return await containerAppService.UpdateAsync(id, input);
    }
    
    [HttpDelete]
    [Authorize(SkladyPermissions.DeleteContainerPermission)]
    public async Task DeleteAsync([Required] Guid id)
    {
        await containerAppService.DeleteAsync(id);
    }
}