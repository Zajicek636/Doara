using System;
using System.ComponentModel.DataAnnotations;

namespace Doara.Sklady.Dto.Container;

public class ContainerUpdateInputDto
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(SkladyRemoteServiceConsts.ContainerMaxNameLength)]
    public string Name { get; set; } = null!;
}