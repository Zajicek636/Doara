using System;
using Doara.Ucetnictvi.Dto.Country;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.Dto.Address;

public class AddressDetailDto : EntityDto<Guid> 
{
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public CountryDto Country { get; set; } = null!;
}