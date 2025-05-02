using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Doara.Ucetnictvi.Dto.InvoiceItem;

public class InvoiceItemManageManyInputDto
{
    [Required]
    public Guid InvoiceId { get; set; }
    
    public List<InvoiceItemManageManyDto> Items { get; set; } = [];
    
    public List<Guid> ItemsForDelete { get; set; } = [];
}