using System;
using System.ComponentModel.DataAnnotations;
using Doara.Ucetnictvi.Constants;
using Doara.Ucetnictvi.Enums;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.Dto.InvoiceItem;

public class InvoiceItemUpdateInputDto : EntityDto<Guid>
{
    [Required]
    public Guid InvoiceId { get; set; }
    
    [Required]
    [StringLength(InvoiceItemConstants.MaxDescriptionLength)]
    public string Description { get; set; } = null!;
    
    [Required]
    public decimal Quantity { get; set; }
    
    [Required]
    public decimal UnitPrice { get; set; }
    
    [Required]
    public decimal NetAmount { get; set; }
    
    public VatRate? VatRate { get; set; }
        
    [Required]
    public decimal VatAmount { get; set; }
    
    [Required]
    public decimal GrossAmount { get; set; }
}