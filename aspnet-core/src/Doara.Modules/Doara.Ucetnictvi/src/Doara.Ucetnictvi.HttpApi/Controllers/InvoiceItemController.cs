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


    [Authorize(UcetnictviPermissions.CreateInvoiceItemPermission)]
    [Authorize(UcetnictviPermissions.UpdateInvoiceItemPermission)]
    [Authorize(UcetnictviPermissions.DeleteInvoiceItemPermission)]
    [HttpPost("ManageMany")]
    public async Task<InvoiceItemManageReportDto> ManageManyAsync(InvoiceItemManageManyInputDto input)
    {
        return await invoiceItemAppService.ManageManyAsync(input);
    }
}