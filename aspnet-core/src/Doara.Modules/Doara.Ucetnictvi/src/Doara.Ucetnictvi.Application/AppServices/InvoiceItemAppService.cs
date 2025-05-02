using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Doara.Sklady.Dto.StockMovement;
using Doara.Sklady.Repositories;
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
    IContainerItemRepository containerItemRepository) : UcetnictviAppService, IInvoiceItemAppService
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
        if (input is { ItemsForDelete.Count: 0, Items.Count: 0 })
        {
            return ObjectMapper.Map<InvoiceItemManageReport, InvoiceItemManageReportDto>(report);
        }
        var invoice = await invoiceRepository.GetAsync(input.InvoiceId);
        var (itemsForCreate, itemsForUpdate, movementsForCreate, movementsForUpdate) = ProcessCreateAndUpdate(
            input.Items.Where(x => x.Id == null || !input.ItemsForDelete.Contains((Guid)x.Id)), invoice, report);

        var (itemsForDelete, movementsForDelete) = ProcessDelete(input.ItemsForDelete, invoice, report);
        
        await ManageManyMovementsAsync(movementsForCreate, movementsForUpdate, movementsForDelete);
        
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

    private async Task ManageManyMovementsAsync(List<StockMovementCreateInput> movementsForCreate, List<StockMovementUpdateInput> movementsForUpdate, List<StockMovementDeleteInput> movementsForDelete)
    {
        var ids = movementsForUpdate.Where(x => x.ContainerItemId != null)
            .Select(x => (Guid)x.ContainerItemId!).ToList();
        ids.AddRange(movementsForCreate.Select(x => x.ContainerItemId));
        ids.AddRange(movementsForDelete.Select(x => x.ContainerItemId));
        var containerItems = await containerItemRepository.GetByIdsAsync(ids);

        foreach (var m in movementsForDelete)
        {
            containerItems.First(x => x.Id == m.ContainerItemId).RemoveMovement(m.StockMovementId);
        }

        var (delete, change) = SplitList(movementsForUpdate, x => x.ContainerItemId == null);
        foreach (var m in delete)
        {
            if (m.OldContainerItemId != null)
            {
                containerItems.First(x => x.Id == m.OldContainerItemId).RemoveMovement((Guid)m.OldStockMovementId!);
                m.SetStockMovementId(null);
            }
        }
        
        foreach (var m in change)
        {
            if (m.OldContainerItemId != null)
            {
                containerItems.First(x => x.Id == m.OldContainerItemId).RemoveMovement((Guid)m.OldStockMovementId!);
            }

            var guid = GuidGenerator.Create();
            containerItems.First(x => x.Id == m.ContainerItemId).Reserve(m.Quantity, guid, m.RelatedDocumentId);
            m.SetStockMovementId(guid);
        }

        foreach (var m in movementsForCreate)
        {
            var guid = GuidGenerator.Create();
            containerItems.First(x => x.Id == m.ContainerItemId).Reserve(m.Quantity, guid, m.RelatedDocumentId);
            m.SetStockMovementId(guid);
        }
    }

    private (List<InvoiceItem>, List<InvoiceItem>, List<StockMovementCreateInput>, List<StockMovementUpdateInput>) ProcessCreateAndUpdate(IEnumerable<InvoiceItemManageManyDto> input, Invoice invoice, InvoiceItemManageReport report)
    {
        var itemsForCreate = new List<InvoiceItem>();
        var itemsForUpdate = new List<InvoiceItem>();
        var movementsForCreate = new List<StockMovementCreateInput>();
        var movementsForUpdate = new List<StockMovementUpdateInput>();
        foreach (var item in input)
        {
            if (item.Id == null)
            {
                var (res, movement) = ProcessCreate(item, invoice, report);
                if (res != null)
                {
                    itemsForCreate.Add(res);
                    if (movement != null)
                    {
                        movementsForCreate.Add(movement);
                    }
                }
            }
            else
            {
                var (res, movement) = ProcessUpdate(item, invoice, report);
                if (res != null)
                {
                    itemsForUpdate.Add(res);
                    if (movement != null)
                    {
                        movementsForUpdate.Add(movement);
                    }
                }      
            }
        }
        return (itemsForCreate, itemsForUpdate, movementsForCreate, movementsForUpdate);
    }
    
    private (InvoiceItem?, StockMovementCreateInput?) ProcessCreate(InvoiceItemManageManyDto input, Invoice invoice, InvoiceItemManageReport report)
    {
        var guid = GuidGenerator.Create();
        try
        {
            StockMovementCreateInput? stockMovementInput = null;
            var invoiceItem = new InvoiceItem(guid, invoice.Id, input.Description,
                input.Quantity, input.UnitPrice, input.NetAmount, input.VatRate,
                input.VatAmount, input.GrossAmount, input.ContainerItemId, null);
            if (input.ContainerItemId != null)
            {
                stockMovementInput = new StockMovementCreateInput { 
                    ContainerItemId = (Guid)input.ContainerItemId,                     
                    Quantity = input.Quantity,
                    RelatedDocumentId = invoice.Id,
                    SetStockMovementId = invoiceItem.SetStockMovementId
                };
            }
            
            return (invoiceItem, stockMovementInput);
        }
        catch (Exception e)
        {
            report.Errors.Add(localizer[UcetnictviErrorCodes.InvoiceItemCreateGeneralError]);
            logger.LogException(e);
            return (null, null);
        }
    }
    
    private (InvoiceItem?, StockMovementUpdateInput?) ProcessUpdate(InvoiceItemManageManyDto input, Invoice invoice, InvoiceItemManageReport report)
    {
        var invoiceItem = invoice.Items.FirstOrDefault(x => x.Id == input.Id);
        if (invoiceItem == null)
        {
            report.Errors.Add(localizer[UcetnictviErrorCodes.InvoiceItemNotExistInInvoice, input.Id!, invoice.Id]);
            return (null, null);
        }
        
        var copy = invoiceItem.GetCopy();
        try
        {
            copy.SetDescription(input.Description)
                .SetQuantity(input.Quantity).SetUnitPrice(input.UnitPrice)
                .SetNetAmount(input.NetAmount).SetVatRate(input.VatRate)
                .SetVatAmount(input.VatAmount).SetGrossAmount(input.GrossAmount)
                .SetContainerItemId(input.ContainerItemId);
        }
        catch (Exception e)
        {
            report.Errors.Add(localizer[UcetnictviErrorCodes.InvoiceItemUpdateGeneralError, input.Id!]);
            logger.LogException(e);
            return (null, null);
        }

        invoiceItem = invoiceItem.SetDescription(input.Description)
            .SetQuantity(input.Quantity).SetUnitPrice(input.UnitPrice)
            .SetNetAmount(input.NetAmount).SetVatRate(input.VatRate)
            .SetVatAmount(input.VatAmount).SetGrossAmount(input.GrossAmount);
        
        StockMovementUpdateInput? stockMovementInput = null;
        if (invoiceItem.ContainerItemId != input.ContainerItemId)
        {
            stockMovementInput = new StockMovementUpdateInput { 
                ContainerItemId = input.ContainerItemId,                     
                Quantity = input.Quantity,
                RelatedDocumentId = invoiceItem.InvoiceId,
                OldContainerItemId = invoiceItem.ContainerItemId,
                OldStockMovementId = invoiceItem.StockMovementId,
                SetStockMovementId = invoiceItem.SetStockMovementId
            };
        }
        
        return (invoiceItem, stockMovementInput);
    }

    private (List<InvoiceItem>, List<StockMovementDeleteInput>) ProcessDelete(List<Guid> ids, Invoice invoice, InvoiceItemManageReport report)
    {
        var itemsForDelete = new List<InvoiceItem>();
        var movementsForDelete = new List<StockMovementDeleteInput>();
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
                if (invoiceItem.StockMovementId != null)
                {
                    movementsForDelete.Add(new StockMovementDeleteInput
                    {
                        StockMovementId = (Guid)invoiceItem.StockMovementId,
                        ContainerItemId = (Guid)invoiceItem.ContainerItemId!,
                    });  
                }
            }
        }
        
        return (itemsForDelete, movementsForDelete);
    }
    
    private class StockMovementCreateInput : StockMovementCreateInputDto
    {
        public Guid ContainerItemId { get; set; }
        public Func<Guid, InvoiceItem> SetStockMovementId { get; set; } = null!;
    }
    
    private class StockMovementDeleteInput
    {
        public Guid ContainerItemId { get; set; }
        public Guid StockMovementId { get; set; }
    }
    
    private class StockMovementUpdateInput : StockMovementCreateInputDto
    {
        public Guid? ContainerItemId { get; set; }
        public Func<Guid?, InvoiceItem> SetStockMovementId { get; set; } = null!;
        public Guid? OldContainerItemId { get; set; }
        public Guid? OldStockMovementId { get; set; }
    }

    private static (List<T>, List<T>) SplitList<T>(List<T> data, Func<T, bool> predicate)
    {
        var trueList = new List<T>(data.Count / 2);
        var falseList = new List<T>(data.Count / 2);
        foreach (var x in data)
        {
            if (predicate(x))
            {
                trueList.Add(x);
            }
            else
            {
                falseList.Add(x);
            }
        }
        return (trueList, falseList);
    }
}