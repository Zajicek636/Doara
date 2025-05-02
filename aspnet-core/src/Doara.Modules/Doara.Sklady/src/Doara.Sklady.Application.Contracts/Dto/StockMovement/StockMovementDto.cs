using System;
using Doara.Sklady.Enums;
using Volo.Abp.Application.Dtos;

namespace Doara.Sklady.Dto.StockMovement;

public class StockMovementDto : EntityDto<Guid>
{
    public decimal Quantity { get; set; }
    public MovementCategory MovementCategory { get; set; }
    public Guid? RelatedDocumentId { get; set; }
}