using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Doara.Sklady.Dto;
using Doara.Sklady.Dto.Container;
using Doara.Sklady.IAppServices;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace Doara.Sklady.Controllers;

[Area(SkladyRemoteServiceConsts.ModuleName)]
[RemoteService(Name = SkladyRemoteServiceConsts.RemoteServiceName)]
[Route("api/Sklady/Container")]
public class ContainerController(IContainerAppService containerAppService)
    : SkladyController, IContainerAppService
{
    [HttpGet]
    //[Route("authorized")]
    //[Authorize]
    public async Task<ContainerDto> GetAsync([Required] Guid id)
    {
        return await containerAppService.GetAsync(id);
    }

    [HttpPost]
    public async Task<ContainerDto> CreateAsync(ContainerCreateInputDto input)
    {
        return await containerAppService.CreateAsync(input);
    }
    
    [HttpPut]
    public async Task<ContainerDto> UpdateAsync(ContainerUpdateInputDto input)
    {
        return await containerAppService.UpdateAsync(input);
    }
    
    [HttpDelete]
    public async Task DeleteAsync([Required] Guid id)
    {
        await containerAppService.DeleteAsync(id);
    }
    
    [HttpPatch]
    public async Task<ContainerDto> ChangeStateAsync(ContainerChangeStateInputDto input)
    {
        return await containerAppService.ChangeStateAsync(input);
    }
}