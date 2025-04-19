using System;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Dto.Invoice;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.IAppServices;

public interface IInvoiceAppService
{
    Task<InvoiceDto> GetAsync(Guid id);
    Task<PagedResultDto<InvoiceDto>> GetAllAsync(PagedAndSortedResultRequestDto input);
    Task<InvoiceDto> CreateAsync(InvoiceCreateInputDto input);
    Task<InvoiceDto> UpdateAsync(InvoiceUpdateInputDto input);
    Task DeleteAsync(Guid id);
}