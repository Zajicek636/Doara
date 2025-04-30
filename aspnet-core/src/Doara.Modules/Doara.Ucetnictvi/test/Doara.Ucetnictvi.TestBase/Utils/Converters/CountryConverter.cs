using System;
using Doara.Ucetnictvi.Dto.Country;

namespace Doara.Ucetnictvi.Utils.Converters;

public static partial class Converter
{
    public const string DefaultCountryName = "Deutschland";
    public const string DefaultCountryCode = "DE";
    
    public static CountryCreateInputDto CreateCountryInput(string name = DefaultCountryName, string code = DefaultCountryCode)
    {
        return new CountryCreateInputDto
        {
            Name = name,
            Code = code
        };
    }
    
    public static CountryUpdateInputDto Convert2UpdateInput(CountryDto input)
    {
        return new CountryUpdateInputDto
        {
            Name = input.Name,
            Code = input.Code
        };
    }
}