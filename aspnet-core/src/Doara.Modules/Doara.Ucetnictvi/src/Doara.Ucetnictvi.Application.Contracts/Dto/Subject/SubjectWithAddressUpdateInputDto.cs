using System.ComponentModel.DataAnnotations;
using Doara.Ucetnictvi.Constants;
using Doara.Ucetnictvi.Dto.Address;

namespace Doara.Ucetnictvi.Dto.Subject;

public class SubjectWithAddressUpdateInputDto
{
    [Required]
    [StringLength(SubjectConstants.MaxNameLength)]
    public string Name { get; set; } = null!;

    [Required] 
    public AddressUpdateInputDto Address { get; set; } = null!;
    
    [StringLength(SubjectConstants.MaxNameLength)]
    public string? Ic { get; set; }
    
    [StringLength(SubjectConstants.MaxNameLength)]
    public string? Dic { get; set; }
    
    [Required]
    public bool IsVatPayer { get; set; }
}