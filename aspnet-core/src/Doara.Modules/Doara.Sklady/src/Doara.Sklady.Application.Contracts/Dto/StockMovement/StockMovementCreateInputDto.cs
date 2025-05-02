using System;

namespace Doara.Sklady.Dto.StockMovement;

public class StockMovementCreateInputDto
{
    public decimal Quantity { get; set; }
    public Guid? RelatedDocumentId { get; set; }
}