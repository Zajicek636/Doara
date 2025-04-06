using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Doara.Sklady.Dto.ContainerItem;
using Doara.Sklady.IAppServices;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace Doara.Sklady.Controllers;

[Area(SkladyRemoteServiceConsts.ModuleName)]
[RemoteService(Name = SkladyRemoteServiceConsts.RemoteServiceName)]
[Route("api/Sklady/ContainerItem")]
public class ContainerItemController(IContainerItemAppService containerItemAppService)
    : SkladyController, IContainerItemAppService
{
    [HttpGet]
    //[Route("authorized")]
    //[Authorize]
    public async Task<ContainerItemDto> GetAsync([Required] Guid id)
    {
        return await containerItemAppService.GetAsync(id);
    }

    [HttpPost]
    public async Task<ContainerItemDto> CreateAsync(ContainerItemCreateInputDto input)
    {
        return await containerItemAppService.CreateAsync(input);
    }
    
    [HttpPut]
    public async Task<ContainerItemDto> UpdateAsync(ContainerItemUpdateInputDto input)
    {
        return await containerItemAppService.UpdateAsync(input);
    }
    
    [HttpDelete]
    public async Task DeleteAsync([Required] Guid id)
    {
        await containerItemAppService.DeleteAsync(id);
    }
    
    [HttpPatch]
    public async Task<ContainerItemDto> ChangeStateAsync(ContainerItemChangeStateInputDto input)
    {
        return await containerItemAppService.ChangeStateAsync(input);
    }
}