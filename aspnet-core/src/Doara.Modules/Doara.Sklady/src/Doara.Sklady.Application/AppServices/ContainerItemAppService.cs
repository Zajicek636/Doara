using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doara.Sklady.Dto.ContainerItem;
using Doara.Sklady.Entities;
using Doara.Sklady.Enums;
using Doara.Sklady.IAppServices;
using Doara.Sklady.Permissions;
using Doara.Sklady.Repositories;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Doara.Sklady.AppServices;

public class ContainerItemAppService(IContainerItemRepository containerItemRepository, IContainerRepository containerRepository) : SkladyAppService, IContainerItemAppService
{
    [Authorize(SkladyPermissions.ReadContainerItemPermission)]
    public async Task<ContainerItemDto> GetAsync(Guid id)
    {
        var res = await containerItemRepository.GetAsync(id);
        return ObjectMapper.Map<ContainerItem, ContainerItemDto>(res); 
    }

    [Authorize(SkladyPermissions.ReadContainerItemPermission)]
    public async Task<PagedResultDto<ContainerItemDto>> GetAllAsync(PagedAndSortedResultRequestDto input)
    {
        var res = await containerItemRepository.GetAllAsync(input.SkipCount, input.MaxResultCount, input.Sorting ?? nameof(ContainerItem.Id));
        var totalCount = await containerItemRepository.GetCountAsync();
        return new PagedResultDto<ContainerItemDto>
        {
            Items = ObjectMapper.Map<List<ContainerItem>, List<ContainerItemDto>>(res),
            TotalCount = totalCount
        };
    }

    [Authorize(SkladyPermissions.CreateContainerItemPermission)]
    public async Task<ContainerItemDto> CreateAsync(ContainerItemCreateInputDto input)
    {
        if (!await containerRepository.AnyAsync(x => x.Id == input.ContainerId))
        {
            throw new EntityNotFoundException(typeof(Container), input.ContainerId);
        }
        var container = await containerRepository.GetAsync(input.ContainerId);

        var guid = GuidGenerator.Create();
        var containerItem = new ContainerItem(guid, input.Name, input.Description, 
            input.RealPrice, input.Markup ?? 0, input.MarkupRate ?? 0, input.Discount ?? 0, 
            input.DiscountRate ?? 0, input.PurchaseUrl, input.ContainerId, 
            input.Quantity, input.QuantityType);
        var res = await containerItemRepository.CreateAsync(containerItem);
        return ObjectMapper.Map<ContainerItem, ContainerItemDto>(res); 
    }

    [Authorize(SkladyPermissions.UpdateContainerItemPermission)]
    public async Task<ContainerItemDto> UpdateAsync(ContainerItemUpdateInputDto input)
    {
        var container = await containerItemRepository.GetAsync(input.Id);
        container.SetName(input.Name);
        var res = await containerItemRepository.UpdateAsync(container);
        return ObjectMapper.Map<ContainerItem, ContainerItemDto>(res); 
    }

    [Authorize(SkladyPermissions.DeleteContainerItemPermission)]
    public async Task DeleteAsync(Guid id)
    {
        await GetAsync(id);
        await containerItemRepository.DeleteAsync(id);
    }

    [Authorize(SkladyPermissions.UpdateContainerItemPermission)]
    public async Task<ContainerItemDto> ChangeStateAsync(ContainerItemChangeStateInputDto input)
    {
        var container = await containerItemRepository.GetAsync(input.Id);
        container.SetName(input.Name);
        var res = await containerItemRepository.UpdateAsync(container);
        return ObjectMapper.Map<ContainerItem, ContainerItemDto>(res); 
    }
}