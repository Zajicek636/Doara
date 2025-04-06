using System.ComponentModel.DataAnnotations;

namespace Doara.Sklady.Dto.ContainerItem;

public class ContainerItemCreateInputDto
{
    [Required]
    [StringLength(SkladyRemoteServiceConsts.ContainerItemMaxNameLength)]
    public string Name { get; set; } = null!;
}