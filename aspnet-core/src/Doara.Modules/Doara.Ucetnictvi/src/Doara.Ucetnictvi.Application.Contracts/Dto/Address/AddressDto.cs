﻿using System;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.Dto.Address;

public class AddressDto : EntityDto<Guid> 
{
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public Guid CountryId { get; set; }
}