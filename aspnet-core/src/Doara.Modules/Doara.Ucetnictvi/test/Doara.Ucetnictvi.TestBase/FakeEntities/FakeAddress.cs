using System;
using System.Text.Json;
using Doara.Ucetnictvi.Entities;
using Doara.Ucetnictvi.Utils;
using Doara.Ucetnictvi.Utils.Converters;
using TestHelper.FakeEntities;

namespace Doara.Ucetnictvi.FakeEntities;

public class FakeAddress : IFakeEntity<Address>
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public Guid? TenantId { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public Guid CountryId { get; set; }
    
    public string SerializeToJson()
    {
        return JsonSerializer.Serialize(this, JsonConfig.DefaultJsonSerializerOptions);
    }

    public Address CreateOriginalEntity(bool checkIfNotThrow = true)
    {
        return Converter.CreateOriginalEntity(this, checkIfNotThrow);
    }

    public void CheckIfSame(Address entity)
    {
        Comparator.CheckIfSame(this, entity);
    }
    
    public const int MaxStreetLength = 200;
    public const int MaxCityLength = 100;
    public const int MaxPostalCodeLength = 20;
}