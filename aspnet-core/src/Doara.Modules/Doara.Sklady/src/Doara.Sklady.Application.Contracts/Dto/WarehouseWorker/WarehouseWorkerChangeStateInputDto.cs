using System;
using System.ComponentModel.DataAnnotations;
using Doara.Sklady.Constants;

namespace Doara.Sklady.Dto.WarehouseWorker;

public class WarehouseWorkerChangeStateInputDto
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(WarehouseWorkerConstants.MaxNameLength)]
    public string Name { get; set; } = null!;
}