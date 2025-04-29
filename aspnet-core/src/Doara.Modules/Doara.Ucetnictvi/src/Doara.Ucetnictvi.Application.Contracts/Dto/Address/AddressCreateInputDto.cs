using System;
using System.ComponentModel.DataAnnotations;
using Doara.Ucetnictvi.Constants;

namespace Doara.Ucetnictvi.Dto.Address;

public class AddressCreateInputDto
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