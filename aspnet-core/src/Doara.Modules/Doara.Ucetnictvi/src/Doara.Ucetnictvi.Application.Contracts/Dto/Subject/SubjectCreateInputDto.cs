using System;
using System.ComponentModel.DataAnnotations;
using Doara.Ucetnictvi.Constants;

namespace Doara.Ucetnictvi.Dto.Subject;

public class SubjectCreateInputDto
{
    [Required]
    [StringLength(SubjectConstants.MaxNameLength)]
    public string Name { get; set; } = null!;
    
    [Required]
    public Guid AddressId { get; set; }
    
    [StringLength(SubjectConstants.MaxNameLength)]
    public string? Ic { get; set; }
    
    [StringLength(SubjectConstants.MaxNameLength)]
    public string? Dic { get; set; }
    
    [Required]
    public bool IsVatPayer { get; set; }
}