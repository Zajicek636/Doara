using System;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.Dto.InvoiceItem;

public class InvoiceItemGetAllDto : PagedAndSortedResultRequestDto
{
    public Guid? InvoiceId { get; set; }
}