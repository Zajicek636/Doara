using System;
using System.ComponentModel.DataAnnotations;

namespace Doara.Sklady.Dto.StockMovement;

public class StockMovementCreateInputDto
{
    [Required]
    public decimal Quantity { get; set; }
    public Guid? RelatedDocumentId { get; set; }
}