using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Dto.Invoice;
using Doara.Ucetnictvi.Entities;
using Doara.Ucetnictvi.IAppServices;
using Doara.Ucetnictvi.Permissions;
using Doara.Ucetnictvi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Doara.Ucetnictvi.AppServices;

public class InvoiceAppService(IInvoiceRepository invoiceRepository) : UcetnictviAppService, IInvoiceAppService
{
    [Authorize(UcetnictviPermissions.ReadInvoicePermission)]
    public async Task<InvoiceDto> GetAsync(Guid id)
    {
        var res = await invoiceRepository.GetAsync(id);
        return ObjectMapper.Map<Invoice, InvoiceDto>(res); 
    }

    [Authorize(UcetnictviPermissions.ReadInvoicePermission)]
    public async Task<PagedResultDto<InvoiceDto>> GetAllAsync(PagedAndSortedResultRequestDto input)
    {
        var res = await invoiceRepository.GetAllAsync(input.SkipCount, input.MaxResultCount, input.Sorting ?? nameof(Invoice.Id));
        var totalCount = await invoiceRepository.GetCountAsync();
        return new PagedResultDto<InvoiceDto>
        {
            Items = ObjectMapper.Map<List<Invoice>, List<InvoiceDto>>(res),
            TotalCount = totalCount
        };
    }

    [Authorize(UcetnictviPermissions.CreateInvoicePermission)]
    public async Task<InvoiceDto> CreateAsync(InvoiceCreateInputDto input)
    {
        var guid = GuidGenerator.Create();
        var invoice = new Invoice(guid, input.InvoiceNumber, input.SupplierId, input.CustomerId, 
            input.IssueDate, input.TaxDate, input.DeliveryDate, input.TotalNetAmount, input.TotalVatAmount,
            input.TotalGrossAmount, input.PaymentTerms, input.VatRate, 
            input.VariableSymbol, input.ConstantSymbol, input.SpecificSymbol);
        var res = await invoiceRepository.CreateAsync(invoice);
        return ObjectMapper.Map<Invoice, InvoiceDto>(res); 
    }

    [Authorize(UcetnictviPermissions.UpdateInvoicePermission)]
    public async Task<InvoiceDto> UpdateAsync(InvoiceUpdateInputDto input)
    {
        var invoice = await invoiceRepository.GetAsync(input.Id);
        invoice.SetInvoiceNumber(input.InvoiceNumber).SetSupplier(input.SupplierId)
            .SetCustomer(input.CustomerId).SetIssueDate(input.IssueDate)
            .SetTaxDate(input.TaxDate).SetDeliveryDate(input.DeliveryDate)
            .SetTotalNetAmount(input.TotalNetAmount).SetTotalVatAmount(input.TotalVatAmount)
            .SetTotalGrossAmount(input.TotalGrossAmount).SetPaymentTerms(input.PaymentTerms)
            .SetVatRate(input.VatRate).SetVariableSymbol(input.VariableSymbol)
            .SetConstantSymbol(input.ConstantSymbol).SetSpecificSymbol(input.SpecificSymbol);
        var res = await invoiceRepository.UpdateAsync(invoice);
        return ObjectMapper.Map<Invoice, InvoiceDto>(res); 
    }

    [Authorize(UcetnictviPermissions.DeleteInvoicePermission)]
    public async Task DeleteAsync(Guid id)
    {
        if (!await invoiceRepository.AnyAsync(x => x.Id == id))
        {
            throw new EntityNotFoundException(typeof(Invoice), id);
        }
        await invoiceRepository.DeleteAsync(id);
    }
}