using System.ComponentModel.DataAnnotations;

namespace Doara.Sklady.Dto.WarehouseWorker;

public class WarehouseWorkerCreateInputDto
{
    [Required]
    [StringLength(SkladyRemoteServiceConsts.WarehouseWorkerMaxNameLength)]
    public string Name { get; set; } = null!;
}