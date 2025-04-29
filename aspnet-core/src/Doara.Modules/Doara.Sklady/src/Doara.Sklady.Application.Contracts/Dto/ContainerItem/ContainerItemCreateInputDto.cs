using System;
using System.ComponentModel.DataAnnotations;
using Doara.Sklady.Constants;
using Doara.Sklady.Enums;

namespace Doara.Sklady.Dto.ContainerItem;

public class ContainerItemCreateInputDto
{
    [Required]
    public QuantityType QuantityType { get; set; }
    
    [Required]
    [StringLength(ContainerItemConstants.MaxNameLength)]
    public string Name { get; set; } = null!;
    
    [Required]
    [StringLength(ContainerItemConstants.MaxDescriptionLength)]
    public string Description { get; set; } = null!;
    
    [StringLength(ContainerItemConstants.MaxPurchaseUrlLength)]
    public string? PurchaseUrl { get; set; } = null!;
    
    [Required] 
    public decimal Quantity { get; set; }
    
    [Required] 
    public decimal RealPrice { get; set; }
    
    public decimal? Markup { get; set; }
    public decimal? MarkupRate { get; set; }
    public decimal? Discount { get; set; }
    public decimal? DiscountRate { get; set; }
    
    [Required]
    public Guid ContainerId { get; set; }
}