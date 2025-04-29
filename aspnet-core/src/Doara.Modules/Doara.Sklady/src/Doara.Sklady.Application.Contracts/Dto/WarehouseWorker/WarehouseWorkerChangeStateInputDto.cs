using System;
using System.ComponentModel.DataAnnotations;

namespace Doara.Sklady.Dto.WarehouseWorker;

public class WarehouseWorkerChangeStateInputDto
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(SkladyRemoteServiceConsts.WarehouseWorkerMaxNameLength)]
    public string Name { get; set; } = null!;
}