using System;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Dto.Invoice;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.IAppServices;

public interface IInvoiceAppService
{
    Task<InvoiceDetailDto> GetAsync(Guid id);
    Task<PagedResultDto<InvoiceDto>> GetAllAsync(PagedAndSortedResultRequestDto input);
    Task<PagedResultDto<InvoiceDetailDto>> GetAllWithDetailAsync(PagedAndSortedResultRequestDto input);
    Task<InvoiceDetailDto> CreateAsync(InvoiceCreateInputDto input);
    Task<InvoiceDetailDto> UpdateAsync(Guid id, InvoiceUpdateInputDto input);
    Task<InvoiceDetailDto> CompleteAsync(Guid id);
    Task DeleteAsync(Guid id);
}