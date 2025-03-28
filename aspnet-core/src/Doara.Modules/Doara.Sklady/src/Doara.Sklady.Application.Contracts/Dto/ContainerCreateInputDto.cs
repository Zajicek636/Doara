using System.ComponentModel.DataAnnotations;

namespace Doara.Sklady.Dto;

public class ContainerCreateInputDto
{
    [Required]
    [StringLength(SkladyRemoteServiceConsts.ContainerMaxNameLength)]
    public string Name { get; set; } = null!;
}