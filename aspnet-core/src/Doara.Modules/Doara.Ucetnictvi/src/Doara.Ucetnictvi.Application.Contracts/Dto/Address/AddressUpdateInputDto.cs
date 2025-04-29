using System;
using System.ComponentModel.DataAnnotations;
using Doara.Ucetnictvi.Constants;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.Dto.Address;

public class AddressUpdateInputDto : EntityDto<Guid> 
{
    [Required]
    [StringLength(AddressConstants.MaxStreetLength)]
    public string Street { get; set; } = null!;
    
    [Required]
    [StringLength(AddressConstants.MaxCityLength)]
    public string City { get; set; } = null!;
    
    [Required]
    [StringLength(AddressConstants.MaxPostalCodeLength)]
    public string PostalCode { get; set; } = null!;
    
    [Required]
    public Guid CountryId { get; set; }
}