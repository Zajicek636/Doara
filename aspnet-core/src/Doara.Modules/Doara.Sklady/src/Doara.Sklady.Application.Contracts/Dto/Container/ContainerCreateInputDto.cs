using System.ComponentModel.DataAnnotations;
using Doara.Sklady.Constants;

namespace Doara.Sklady.Dto.Container;

public class ContainerCreateInputDto
{
    [Required]
    [StringLength(ContainerConstants.MaxNameLength)]
    public string Name { get; set; } = null!;
    
    [Required]
    [StringLength(ContainerConstants.MaxDescriptionLength)]
    public string Description { get; set; } = null!;
}