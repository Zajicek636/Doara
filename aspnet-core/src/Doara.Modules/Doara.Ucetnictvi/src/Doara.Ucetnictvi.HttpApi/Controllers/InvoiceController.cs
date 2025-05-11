using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Dto.Invoice;
using Doara.Ucetnictvi.IAppServices;
using Doara.Ucetnictvi.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.Controllers;

[Area(UcetnictviRemoteServiceConsts.ModuleName)]
[RemoteService(Name = UcetnictviRemoteServiceConsts.RemoteServiceName)]
[Route("api/Ucetnictvi/Invoice")]
public class InvoiceController(IInvoiceAppService invoiceAppService) : UcetnictviController, IInvoiceAppService
{
    [HttpGet]
    [Authorize(UcetnictviPermissions.ReadInvoicePermission)]
    public async Task<InvoiceDetailDto> GetAsync([Required] Guid id)
    {
        return await invoiceAppService.GetAsync(id);
    }
    
    [HttpGet("GetAll")]
    [Authorize(UcetnictviPermissions.ReadInvoicePermission)]
    public async Task<PagedResultDto<InvoiceDto>> GetAllAsync(PagedAndSortedResultRequestDto input)
    {
        return await invoiceAppService.GetAllAsync(input);
    }
    
    [HttpGet("GetAllWithDetail")]
    [Authorize(UcetnictviPermissions.ReadInvoicePermission)]
    public async Task<PagedResultDto<InvoiceDetailDto>> GetAllWithDetailAsync(PagedAndSortedResultRequestDto input)
    {
        return await invoiceAppService.GetAllWithDetailAsync(input);
    }

    [HttpPost]
    [Authorize(UcetnictviPermissions.CreateInvoicePermission)]
    public async Task<InvoiceDetailDto> CreateAsync(InvoiceCreateInputDto input)
    {
        return await invoiceAppService.CreateAsync(input);
    }
    
    [HttpPost("Complete")]
    [Authorize(UcetnictviPermissions.UpdateInvoicePermission)]
    public async Task<InvoiceDetailDto> CompleteAsync([Required] Guid id)
    {
        return await invoiceAppService.CompleteAsync(id);
    }
    
    [HttpPut]
    [Authorize(UcetnictviPermissions.UpdateInvoicePermission)]
    public async Task<InvoiceDetailDto> UpdateAsync([Required] Guid id, InvoiceUpdateInputDto input)
    {
        return await invoiceAppService.UpdateAsync(id, input);
    }
    
    [HttpDelete]
    [Authorize(UcetnictviPermissions.DeleteInvoicePermission)]
    public async Task DeleteAsync([Required] Guid id)
    {
        await invoiceAppService.DeleteAsync(id);
    }
}