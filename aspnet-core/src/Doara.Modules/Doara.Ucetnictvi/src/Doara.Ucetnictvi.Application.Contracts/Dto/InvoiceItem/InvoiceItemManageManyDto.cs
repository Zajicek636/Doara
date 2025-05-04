using System;
using System.ComponentModel.DataAnnotations;
using Doara.Ucetnictvi.Constants;
using Doara.Ucetnictvi.Enums;

namespace Doara.Ucetnictvi.Dto.InvoiceItem;

public class InvoiceItemManageManyDto
{
    public Guid? Id { get; set; }
    
    public Guid? ContainerItemId { get; set; }
    
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