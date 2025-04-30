using System.ComponentModel.DataAnnotations;
using Doara.Sklady.Constants;

namespace Doara.Sklady.Dto.WarehouseWorker;

public class WarehouseWorkerCreateInputDto
{
    [Required]
    [StringLength(WarehouseWorkerConstants.MaxNameLength)]
    public string Name { get; set; } = null!;
}