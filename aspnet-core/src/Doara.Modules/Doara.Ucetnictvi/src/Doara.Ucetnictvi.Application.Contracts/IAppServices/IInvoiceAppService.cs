using System;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Dto.Invoice;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.IAppServices;

public interface IInvoiceAppService
{
    Task<InvoiceDetailDto> GetAsync(Guid id);
    Task<PagedResultDto<InvoiceDto>> GetAllAsync(PagedAndSortedResultRequestDto input);
    Task<InvoiceDetailDto> CreateAsync(InvoiceCreateInputDto input);
    Task<InvoiceDetailDto> UpdateAsync(InvoiceUpdateInputDto input);
    Task DeleteAsync(Guid id);
}