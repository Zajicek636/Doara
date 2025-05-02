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
    
    [StringLength(SubjectConstants.MaxIcLength)]
    public string? Ic { get; set; }
    
    [StringLength(SubjectConstants.MaxDicLength)]
    public string? Dic { get; set; }
    
    [Required]
    public bool IsVatPayer { get; set; }
}