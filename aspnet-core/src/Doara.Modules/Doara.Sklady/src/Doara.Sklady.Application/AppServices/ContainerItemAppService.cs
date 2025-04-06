using System;
using System.Threading.Tasks;
using Doara.Sklady.Dto.ContainerItem;
using Doara.Sklady.Entities;
using Doara.Sklady.Enums;
using Doara.Sklady.IAppServices;
using Doara.Sklady.Repositories;

namespace Doara.Sklady.AppServices;

public class ContainerItemAppService(IContainerItemRepository containerItemRepository) : SkladyAppService, IContainerItemAppService
{
    //[Authorize] using Microsoft.AspNetCore.Authorization;
    public async Task<ContainerItemDto> GetAsync(Guid id)
    {
        var res = await containerItemRepository.GetAsync(id);
        return ObjectMapper.Map<ContainerItem, ContainerItemDto>(res); 
    }
    
    //[Authorize]
    public async Task<ContainerItemDto> CreateAsync(ContainerItemCreateInputDto input)
    {
        var guid = GuidGenerator.Create();
        var container = new ContainerItem(guid, input.Name, "", 0, 0, 0, 
            0, 0, null, guid, 0, QuantityType.Unknown);
        var res = await containerItemRepository.CreateAsync(container);
        return ObjectMapper.Map<ContainerItem, ContainerItemDto>(res); 
    }

    public async Task<ContainerItemDto> UpdateAsync(ContainerItemUpdateInputDto input)
    {
        var container = await containerItemRepository.GetAsync(input.Id);
        container.SetName(input.Name);
        var res = await containerItemRepository.UpdateAsync(container);
        return ObjectMapper.Map<ContainerItem, ContainerItemDto>(res); 
    }

    public async Task DeleteAsync(Guid id)
    {
        await GetAsync(id);
        await containerItemRepository.DeleteAsync(id);
    }

    public async Task<ContainerItemDto> ChangeStateAsync(ContainerItemChangeStateInputDto input)
    {
        var container = await containerItemRepository.GetAsync(input.Id);
        container.SetName(input.Name);
        var res = await containerItemRepository.UpdateAsync(container);
        return ObjectMapper.Map<ContainerItem, ContainerItemDto>(res); 
    }
}