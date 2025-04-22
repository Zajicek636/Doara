using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.Dto.InvoiceItem;

public class InvoiceItemManageManyInputDto : EntityDto<Guid>
{
    [Required]
    public Guid InvoiceId { get; set; }
    
    public List<InvoiceItemManageManyDto> Items { get; set; } = [];
    
    public List<Guid> ItemsForDelete { get; set; } = [];
    public bool DeleteMissingItems { get; set; }
}