using System;
using System.ComponentModel.DataAnnotations;
using Doara.Ucetnictvi.Constants;

namespace Doara.Ucetnictvi.Dto.InvoiceItem;

public class InvoiceItemCreateInputDto
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
        
    [Required]
    public decimal VatRate { get; set; }
        
    [Required]
    public decimal VatAmount { get; set; }
    
    [Required]
    public decimal GrossAmount { get; set; }
}