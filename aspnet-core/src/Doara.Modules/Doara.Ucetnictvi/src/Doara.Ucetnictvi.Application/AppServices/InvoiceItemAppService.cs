using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Dto.InvoiceItem;
using Doara.Ucetnictvi.Entities;
using Doara.Ucetnictvi.IAppServices;
using Doara.Ucetnictvi.Permissions;
using Doara.Ucetnictvi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Doara.Ucetnictvi.AppServices;

public class InvoiceItemAppService(IInvoiceItemRepository invoiceItemRepository, IInvoiceRepository invoiceRepository) : UcetnictviAppService, IInvoiceItemAppService
{
    [Authorize(UcetnictviPermissions.ReadInvoiceItemPermission)]
    public async Task<InvoiceItemDto> GetAsync(Guid id)
    {
        var res = await invoiceItemRepository.GetAsync(id);
        return ObjectMapper.Map<InvoiceItem, InvoiceItemDto>(res); 
    }

    [Authorize(UcetnictviPermissions.ReadInvoiceItemPermission)]
    public async Task<PagedResultDto<InvoiceItemDto>> GetAllAsync(PagedAndSortedResultRequestDto input)
    {
        var res = await invoiceItemRepository.GetAllAsync(input.SkipCount, input.MaxResultCount, input.Sorting ?? nameof(InvoiceItem.Id));
        var totalCount = await invoiceItemRepository.GetCountAsync();
        return new PagedResultDto<InvoiceItemDto>
        {
            Items = ObjectMapper.Map<List<InvoiceItem>, List<InvoiceItemDto>>(res),
            TotalCount = totalCount
        };
    }

    [Authorize(UcetnictviPermissions.CreateInvoiceItemPermission)]
    public async Task<InvoiceItemDto> CreateAsync(InvoiceItemCreateInputDto input)
    {
        if (!await invoiceRepository.AnyAsync(x => x.Id == input.InvoiceId))
        {
            throw new EntityNotFoundException(typeof(Invoice), input.InvoiceId);
        }
        
        var guid = GuidGenerator.Create();
        var invoiceItem = new InvoiceItem(guid, input.InvoiceId, input.Description, 
            input.Quantity, input.UnitPrice, input.NetAmount, input.VatRate, 
            input.VatAmount, input.GrossAmount);
        var res = await invoiceItemRepository.CreateAsync(invoiceItem);
        return ObjectMapper.Map<InvoiceItem, InvoiceItemDto>(res); 
    }

    [Authorize(UcetnictviPermissions.UpdateInvoiceItemPermission)]
    public async Task<InvoiceItemDto> UpdateAsync(InvoiceItemUpdateInputDto input)
    {
        if (!await invoiceRepository.AnyAsync(x => x.Id == input.InvoiceId))
        {
            throw new EntityNotFoundException(typeof(Invoice), input.InvoiceId);
        }
        
        var invoiceItem = await invoiceItemRepository.GetAsync(input.Id);
        invoiceItem.SetInvoice(input.InvoiceId).SetDescription(input.Description)
            .SetQuantity(input.Quantity).SetUnitPrice(input.UnitPrice)
            .SetNetAmount(input.NetAmount).SetVatRate(input.VatRate)
            .SetVatAmount(input.VatAmount).SetGrossAmount(input.GrossAmount);
        var res = await invoiceItemRepository.UpdateAsync(invoiceItem);
        return ObjectMapper.Map<InvoiceItem, InvoiceItemDto>(res); 
    }

    [Authorize(UcetnictviPermissions.DeleteInvoiceItemPermission)]
    public async Task DeleteAsync(Guid id)
    {
        if (!await invoiceItemRepository.AnyAsync(x => x.Id == id))
        {
            throw new EntityNotFoundException(typeof(InvoiceItem), id);
        }
        await invoiceItemRepository.DeleteAsync(id);
    }
}