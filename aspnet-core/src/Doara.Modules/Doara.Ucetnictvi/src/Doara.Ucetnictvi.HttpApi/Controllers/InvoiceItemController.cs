using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Dto.InvoiceItem;
using Doara.Ucetnictvi.IAppServices;
using Doara.Ucetnictvi.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.Controllers;

[Area(UcetnictviRemoteServiceConsts.ModuleName)]
[RemoteService(Name = UcetnictviRemoteServiceConsts.RemoteServiceName)]
[Route("api/Ucetnictvi/InvoiceItem")]
public class InvoiceItemController(IInvoiceItemAppService invoiceItemAppService) : UcetnictviController, IInvoiceItemAppService
{
    [HttpGet]
    [Authorize(UcetnictviPermissions.ReadInvoiceItemPermission)]
    public async Task<InvoiceItemDto> GetAsync([Required] Guid id)
    {
        return await invoiceItemAppService.GetAsync(id);
    }
    
    [HttpGet("GetAll")]
    [Authorize(UcetnictviPermissions.ReadInvoiceItemPermission)]
    public async Task<PagedResultDto<InvoiceItemDto>> GetAllAsync(PagedAndSortedResultRequestDto input)
    {
        return await invoiceItemAppService.GetAllAsync(input);
    }

    [HttpPost]
    [Authorize(UcetnictviPermissions.CreateInvoiceItemPermission)]
    public async Task<InvoiceItemDto> CreateAsync(InvoiceItemCreateInputDto input)
    {
        return await invoiceItemAppService.CreateAsync(input);
    }
    
    [HttpPut]
    [Authorize(UcetnictviPermissions.UpdateInvoiceItemPermission)]
    public async Task<InvoiceItemDto> UpdateAsync(InvoiceItemUpdateInputDto input)
    {
        return await invoiceItemAppService.UpdateAsync(input);
    }
    
    [HttpDelete]
    [Authorize(UcetnictviPermissions.DeleteInvoiceItemPermission)]
    public async Task DeleteAsync([Required] Guid id)
    {
        await invoiceItemAppService.DeleteAsync(id);
    }
}