using System;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Dto.InvoiceItem;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.IAppServices;

public interface IInvoiceItemAppService
{
    Task<InvoiceItemDto> GetAsync(Guid id);
    Task<PagedResultDto<InvoiceItemDto>> GetAllAsync(PagedAndSortedResultRequestDto input);
    Task<InvoiceItemManageReportDto> ManageManyAsync(InvoiceItemManageManyInputDto input);
}