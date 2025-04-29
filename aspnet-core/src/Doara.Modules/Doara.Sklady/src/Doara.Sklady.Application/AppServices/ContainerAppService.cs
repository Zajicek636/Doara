using System;
using System.Threading.Tasks;
using Doara.Sklady.Dto;
using Doara.Sklady.Dto.Container;
using Doara.Sklady.Entities;
using Doara.Sklady.IAppServices;
using Doara.Sklady.Repositories;

namespace Doara.Sklady.AppServices;

public class ContainerAppService(IContainerRepository containerRepository) : SkladyAppService, IContainerAppService
{
    //[Authorize] using Microsoft.AspNetCore.Authorization;
    public async Task<ContainerDto> GetAsync(Guid id)
    {
        var res = await containerRepository.GetAsync(id);
        return ObjectMapper.Map<Container, ContainerDto>(res); 
    }
    
    //[Authorize]
    public async Task<ContainerDto> CreateAsync(ContainerCreateInputDto input)
    {
        var guid = GuidGenerator.Create();
        var container = new Container(guid, input.Name);
        var res = await containerRepository.CreateAsync(container);
        return ObjectMapper.Map<Container, ContainerDto>(res); 
    }

    public async Task<ContainerDto> UpdateAsync(ContainerUpdateInputDto input)
    {
        var container = await containerRepository.GetAsync(input.Id);
        container.SetName(input.Name);
        var res = await containerRepository.UpdateAsync(container);
        return ObjectMapper.Map<Container, ContainerDto>(res); 
    }

    public async Task DeleteAsync(Guid id)
    {
        await GetAsync(id);
        await containerRepository.DeleteAsync(id);
    }

    public async Task<ContainerDto> ChangeStateAsync(ContainerChangeStateInputDto input)
    {
        var container = await containerRepository.GetAsync(input.Id);
        container.SetName(input.Name);
        var res = await containerRepository.UpdateAsync(container);
        return ObjectMapper.Map<Container, ContainerDto>(res); 
    }
}