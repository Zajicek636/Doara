using System;
using System.ComponentModel.DataAnnotations;
using Doara.Sklady.Constants;

namespace Doara.Sklady.Dto.ContainerItem;

public class ContainerItemUpdateInputDto
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(ContainerItemConstants.MaxNameLength)]
    public string Name { get; set; } = null!;
}