using System;
using System.ComponentModel.DataAnnotations;
using Doara.Ucetnictvi.Constants;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.Dto.Country;

public class CountryUpdateInputDto : EntityDto<Guid>
{
    
    [Required]
    [StringLength(CountryConstants.MaxNameLength)]
    public string Name { get; set; } = null!;
    
    [Required]
    [StringLength(CountryConstants.MaxCodeLength)]
    public string Code { get; set; } = null!;
}