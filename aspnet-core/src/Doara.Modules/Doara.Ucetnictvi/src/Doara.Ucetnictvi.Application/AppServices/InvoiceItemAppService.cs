using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Doara.Sklady.IAppServices;
using Doara.Ucetnictvi.Dto.InvoiceItem;
using Doara.Ucetnictvi.Entities;
using Doara.Ucetnictvi.IAppServices;
using Doara.Ucetnictvi.Localization;
using Doara.Ucetnictvi.Permissions;
using Doara.Ucetnictvi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.AppServices;

public class InvoiceItemAppService(IInvoiceItemRepository invoiceItemRepository, IInvoiceRepository invoiceRepository, 
    ILogger<UcetnictviApplicationModule> logger, IStringLocalizer<UcetnictviResource> localizer, 
    IContainerItemAppService _containerItem) : UcetnictviAppService, IInvoiceItemAppService
{
    [Authorize(UcetnictviPermissions.ReadInvoiceItemPermission)]
    public async Task<InvoiceItemDto> GetAsync(Guid id)
    {
        var res = await invoiceItemRepository.GetAsync(id);
        return ObjectMapper.Map<InvoiceItem, InvoiceItemDto>(res); 
    }

    [Authorize(UcetnictviPermissions.ReadInvoiceItemPermission)]
    public async Task<PagedResultDto<InvoiceItemDto>> GetAllAsync(InvoiceItemGetAllDto input)
    {
        Expression<Func<InvoiceItem, bool>>? filter =
            input.InvoiceId != null ? i => i.InvoiceId == input.InvoiceId : null;
        var res = await invoiceItemRepository.GetAllAsync(input.SkipCount, input.MaxResultCount, input.Sorting ?? nameof(InvoiceItem.Id),
            filter);
        var totalCount = await invoiceItemRepository.GetCountAsync(filter);
        return new PagedResultDto<InvoiceItemDto>
        {
            Items = ObjectMapper.Map<List<InvoiceItem>, List<InvoiceItemDto>>(res),
            TotalCount = totalCount
        };
    }

    [Authorize(UcetnictviPermissions.CreateInvoiceItemPermission)]
    [Authorize(UcetnictviPermissions.UpdateInvoiceItemPermission)]
    [Authorize(UcetnictviPermissions.DeleteInvoiceItemPermission)]
    public async Task<InvoiceItemManageReportDto> ManageManyAsync(InvoiceItemManageManyInputDto input)
    {
        var report = new InvoiceItemManageReport();
        if (input is { DeleteMissingItems: false, ItemsForDelete.Count: 0, Items.Count: 0 })
        {
            return ObjectMapper.Map<InvoiceItemManageReport, InvoiceItemManageReportDto>(report);
        }
        var invoice = await invoiceRepository.GetAsync(input.InvoiceId);
        var (itemsForCreate, itemsForUpdate) = ProcessCreateAndUpdate(
            input.Items.Where(x => x.Id == null || !input.ItemsForDelete.Contains((Guid)x.Id)), invoice, report);

        var itemsForDelete = new List<InvoiceItem>();
        if (input.DeleteMissingItems && itemsForUpdate.Count != invoice.Items.Count)
        {
            itemsForDelete = invoice.Items.Where(del => itemsForUpdate.Count == 0 || itemsForUpdate.Any(upd => upd.Id != del.Id)).ToList();
        }
        itemsForDelete.AddRange(ProcessDelete(input.ItemsForDelete, invoice, report));

        if (itemsForCreate.Count > 0)
        {
            await invoiceItemRepository.CreateManyAsync(itemsForCreate);
        }
        if (itemsForUpdate.Count > 0)
        {
            await invoiceItemRepository.UpdateManyAsync(itemsForUpdate);
        }
        if (itemsForDelete.Count > 0)
        {
            await invoiceItemRepository.DeleteManyAsync(itemsForDelete);
        }
        return ObjectMapper.Map<InvoiceItemManageReport, InvoiceItemManageReportDto>(report);
    }

    private (List<InvoiceItem>, List<InvoiceItem>) ProcessCreateAndUpdate(IEnumerable<InvoiceItemManageManyDto> input, Invoice invoice, InvoiceItemManageReport report)
    {
        var itemsForCreate = new List<InvoiceItem>();
        var itemsForUpdate = new List<InvoiceItem>();
        foreach (var item in input)
        {
            if (item.Id == null)
            {
                var res = ProcessCreate(item, invoice, report);
                if (res != null)
                {
                    itemsForCreate.Add(res);
                }
            }
            else
            {
                var res = ProcessUpdate(item, invoice, report);
                if (res != null)
                {
                    itemsForUpdate.Add(res);
                }      
            }
        }
        return (itemsForCreate, itemsForUpdate);
    }

    private InvoiceItem? ProcessCreate(InvoiceItemManageManyDto input, Invoice invoice, InvoiceItemManageReport report)
    {
        var guid = GuidGenerator.Create();
        try
        {
            var invoiceItem = new InvoiceItem(guid, invoice.Id, input.Description,
                input.Quantity, input.UnitPrice, input.NetAmount, input.VatRate,
                input.VatAmount, input.GrossAmount);
            return invoiceItem;
        }
        catch (Exception e)
        {
            report.Errors.Add(localizer[UcetnictviErrorCodes.InvoiceItemCreateGeneralError]);
            logger.LogException(e);
            return null;
        }
    }
    
    private InvoiceItem? ProcessUpdate(InvoiceItemManageManyDto input, Invoice invoice, InvoiceItemManageReport report)
    {
        var invoiceItem = invoice.Items.FirstOrDefault(x => x.Id == input.Id);
        if (invoiceItem == null)
        {
            report.Errors.Add(localizer[UcetnictviErrorCodes.InvoiceItemNotExistInInvoice, input.Id!, invoice.Id]);
            return null;
        }
        
        var copy = invoiceItem.GetCopy();
        try
        {
            copy.SetDescription(input.Description)
                .SetQuantity(input.Quantity).SetUnitPrice(input.UnitPrice)
                .SetNetAmount(input.NetAmount).SetVatRate(input.VatRate)
                .SetVatAmount(input.VatAmount).SetGrossAmount(input.GrossAmount);
        }
        catch (Exception e)
        {
            report.Errors.Add(localizer[UcetnictviErrorCodes.InvoiceItemUpdateGeneralError, input.Id!]);
            logger.LogException(e);
            return null;
        }

        return invoiceItem.SetDescription(input.Description)
            .SetQuantity(input.Quantity).SetUnitPrice(input.UnitPrice)
            .SetNetAmount(input.NetAmount).SetVatRate(input.VatRate)
            .SetVatAmount(input.VatAmount).SetGrossAmount(input.GrossAmount);
    }

    private List<InvoiceItem> ProcessDelete(List<Guid> ids, Invoice invoice, InvoiceItemManageReport report)
    {
        var itemsForDelete = new List<InvoiceItem>();
        foreach (var id in ids)
        {
            var invoiceItem = invoice.Items.FirstOrDefault(x => x.Id == id);
            if (invoiceItem == null)
            {
                report.Errors.Add(localizer[UcetnictviErrorCodes.InvoiceItemNotExistInInvoice, id, invoice.Id]);
            }
            else
            {
                itemsForDelete.Add(invoiceItem);
            }
        }
        
        return itemsForDelete;
    }
}