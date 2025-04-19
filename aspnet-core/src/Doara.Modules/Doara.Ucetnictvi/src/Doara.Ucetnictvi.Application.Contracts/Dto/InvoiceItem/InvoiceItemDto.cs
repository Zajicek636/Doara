using System;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.Dto.InvoiceItem;

public class InvoiceItemDto : EntityDto<Guid>
{
    public Guid InvoiceId { get; set; }
    public string Description { get; set; } = null!;
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal NetAmount { get; set; }
    public decimal VatRate { get; set; }
    public decimal VatAmount { get; set; }
    public decimal GrossAmount { get; set; }
}