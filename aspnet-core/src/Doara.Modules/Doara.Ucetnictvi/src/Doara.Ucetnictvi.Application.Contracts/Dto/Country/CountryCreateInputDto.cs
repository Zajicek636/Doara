using System.ComponentModel.DataAnnotations;
using Doara.Ucetnictvi.Constants;

namespace Doara.Ucetnictvi.Dto.Country;

public class CountryCreateInputDto
{
    [Required]
    [StringLength(CountryConstants.MaxNameLength)]
    public string Name { get; set; } = null!;
    
    [Required]
    [StringLength(CountryConstants.MaxCodeLength)]
    public string Code { get; set; } = null!;
}