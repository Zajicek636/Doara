using System;
using Doara.Ucetnictvi.Dto.Address;

namespace Doara.Ucetnictvi.Utils.Converters;

public static partial class Converter
{
    public const string DefaultAddressStreet = "Unter den Linden 77";
    public const string DefaultAddressCity = "Berlin";
    public const string DefaultAddressPostalCode = "10117";
    
    public static AddressCreateInputDto CreateAddressInput(Guid countryId, string street = DefaultAddressStreet, 
        string city = DefaultAddressCity, string postalCode = DefaultAddressPostalCode)
    {
        return new AddressCreateInputDto
        {
            Street = street,
            City = city,
            PostalCode = postalCode,
            CountryId = countryId
        };
    }

    public static AddressUpdateInputDto Convert2UpdateInput(AddressDetailDto input)
    {
        return new AddressUpdateInputDto
        {
            Street = input.Street,
            City = input.City,
            PostalCode = input.PostalCode,
            CountryId = input.Country.Id,
            Id = input.Id
        };
    }
}