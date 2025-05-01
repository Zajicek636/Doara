using System;
using System.Collections.Generic;
using Doara.Sklady.Dto.Container;
using Doara.Sklady.Dto.StockMovement;
using Doara.Sklady.Enums;
using Volo.Abp.Application.Dtos;

namespace Doara.Sklady.Dto.ContainerItem;

public class ContainerItemDetailDto : EntityDto<Guid> 
{
    public QuantityType QuantityType { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? PurchaseUrl { get; set; }
    public decimal Available { get; set; } //Dostupné Unused
    public decimal OnHand { get; set; } //Na skladě Unused + Reserved
    public decimal Reserved { get; set; }
    public decimal RealPrice { get; set; }
    public decimal PresentationPrice { get; set; }
    public decimal Markup { get; set; } //Marže
    public decimal MarkupRate { get; set; } //Marže %
    public decimal Discount { get; set; } //Sleva
    public decimal DiscountRate { get; set; } //Sleva %
    public ContainerDto Container { get; set; } = null!;
    public List<StockMovementDto> Movements { get; set; } = [];
}