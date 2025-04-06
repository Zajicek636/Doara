using System;
using System.ComponentModel.DataAnnotations;

namespace Doara.Sklady.Dto.ContainerItem;

public class ContainerItemUpdateInputDto
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(SkladyRemoteServiceConsts.ContainerItemMaxNameLength)]
    public string Name { get; set; } = null!;
}