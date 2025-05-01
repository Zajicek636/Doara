using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Doara.Sklady.Dto.ContainerItem;
using Doara.Sklady.Dto.StockMovement;
using Doara.Sklady.Entities;
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
    public async Task<ContainerItemDetailDto> GetAsync(Guid id)
    {
        var res = await containerItemRepository.GetAsync(id);
        return ObjectMapper.Map<ContainerItem, ContainerItemDetailDto>(res); 
    }

    [Authorize(SkladyPermissions.ReadContainerItemPermission)]
    public async Task<PagedResultDto<ContainerItemDto>> GetAllAsync(ContainerItemGetAllDto input)
    {
        Expression<Func<ContainerItem, bool>>? filter =
            input.ContainerId != null ? i => i.ContainerId == input.ContainerId : null;
        var res = await containerItemRepository.GetAllAsync(input.SkipCount, input.MaxResultCount, input.Sorting ?? nameof(ContainerItem.Id), false,
            filter);
        var totalCount = await containerItemRepository.GetCountAsync(filter);
        return new PagedResultDto<ContainerItemDto>
        {
            Items = ObjectMapper.Map<List<ContainerItem>, List<ContainerItemDto>>(res),
            TotalCount = totalCount
        };
    }
    
    [Authorize(SkladyPermissions.ReadContainerItemPermission)]
    public async Task<PagedResultDto<ContainerItemDetailDto>> GetAllWithDetailAsync(ContainerItemGetAllDto input)
    {
        Expression<Func<ContainerItem, bool>>? filter =
            input.ContainerId != null ? i => i.ContainerId == input.ContainerId : null;
        var res = await containerItemRepository.GetAllAsync(input.SkipCount, input.MaxResultCount, input.Sorting ?? nameof(ContainerItem.Id), true,
            filter);
        var totalCount = await containerItemRepository.GetCountAsync(filter);
        return new PagedResultDto<ContainerItemDetailDto>
        {
            Items = ObjectMapper.Map<List<ContainerItem>, List<ContainerItemDetailDto>>(res),
            TotalCount = totalCount
        };
    }

    [Authorize(SkladyPermissions.CreateContainerItemPermission)]
    public async Task<ContainerItemDetailDto> CreateAsync(ContainerItemCreateInputDto input)
    {
        var container = await containerRepository.GetAsync(input.ContainerId);
        var guid = GuidGenerator.Create();
        var containerItem = new ContainerItem(guid, input.Name, input.Description, 
            input.RealPrice, input.Markup ?? 0, input.MarkupRate ?? 0, input.Discount ?? 0, 
            input.DiscountRate ?? 0, input.PurchaseUrl, container.Id, input.QuantityType);
        var res = await containerItemRepository.CreateAsync(containerItem);
        res.SetContainer(container);
        return ObjectMapper.Map<ContainerItem, ContainerItemDetailDto>(res); 
    }

    [Authorize(SkladyPermissions.UpdateContainerItemPermission)]
    public async Task<ContainerItemDetailDto> UpdateAsync(Guid id, ContainerItemUpdateInputDto input)
    {
        var container = await containerRepository.GetAsync(input.ContainerId);
        var containerItem = await containerItemRepository.GetAsync(id);
        containerItem.SetName(input.Name).SetDescription(input.Description)
            .SetRealPrice(input.RealPrice).SetMarkup(input.Markup ?? 0).SetMarkupRate(input.MarkupRate ?? 0)
            .SetDiscount(input.Discount ?? 0).SetDiscountRate(input.DiscountRate ?? 0)
            .SetPurchaseUrl(input.PurchaseUrl).SetContainer(container.Id)
            .SetQuantityType(input.QuantityType);
        var res = await containerItemRepository.UpdateAsync(containerItem);
        res.SetContainer(container);
        return ObjectMapper.Map<ContainerItem, ContainerItemDetailDto>(res); 
    }

    [Authorize(SkladyPermissions.DeleteContainerItemPermission)]
    public async Task DeleteAsync(Guid id)
    {
        if (!await containerItemRepository.AnyAsync(x => x.Id == id))
        {
            throw new EntityNotFoundException(typeof(ContainerItem), id);
        }
        await containerItemRepository.DeleteAsync(id);
    }

    public async Task<ContainerItemDetailDto> AddStockAsync(Guid id, StockMovementCreateInputDto input)
    {
        var containerItem = await containerItemRepository.GetAsync(id);
        var guid = GuidGenerator.Create();
        containerItem.AddStock(input.Quantity, guid, input.RelatedDocumentId);
        await containerItemRepository.UpdateAsync(containerItem);
        return ObjectMapper.Map<ContainerItem, ContainerItemDetailDto>(containerItem);
    }

    public async Task<ContainerItemDetailDto> RemoveMovementAsync(Guid id, Guid stockMovementId)
    {
        var containerItem = await containerItemRepository.GetAsync(id);
        containerItem.RemoveMovement(stockMovementId);
        await containerItemRepository.UpdateAsync(containerItem);
        return ObjectMapper.Map<ContainerItem, ContainerItemDetailDto>(containerItem);
    }

    public async Task<ContainerItemDetailDto> ReserveItemAsync(Guid id, StockMovementCreateInputDto input)
    {
        var containerItem = await containerItemRepository.GetAsync(id);
        var guid = GuidGenerator.Create();
        containerItem.Reserve(input.Quantity, guid, input.RelatedDocumentId);
        await containerItemRepository.UpdateAsync(containerItem);
        return ObjectMapper.Map<ContainerItem, ContainerItemDetailDto>(containerItem);
    }

    public async Task<ContainerItemDetailDto> UseItemAsync(Guid id, StockMovementCreateInputDto input)
    {
        var containerItem = await containerItemRepository.GetAsync(id);
        var guid = GuidGenerator.Create();
        containerItem.Use(input.Quantity, guid, input.RelatedDocumentId);
        await containerItemRepository.UpdateAsync(containerItem);
        return ObjectMapper.Map<ContainerItem, ContainerItemDetailDto>(containerItem);
    }

    public async Task<ContainerItemDetailDto> ConvertReservationToUsageAsync(Guid id, Guid stockMovementId)
    {
        var containerItem = await containerItemRepository.GetAsync(id);
        var guid = GuidGenerator.Create();
        containerItem.Use(stockMovementId, guid);
        await containerItemRepository.UpdateAsync(containerItem);
        return ObjectMapper.Map<ContainerItem, ContainerItemDetailDto>(containerItem);
    }
}