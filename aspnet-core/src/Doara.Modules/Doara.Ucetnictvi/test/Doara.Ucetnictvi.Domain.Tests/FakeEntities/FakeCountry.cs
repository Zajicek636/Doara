using System;
using System.Text.Json;
using Doara.Ucetnictvi.Entities;
using Doara.Ucetnictvi.Utils;
using Doara.Ucetnictvi.Utils.Converters;
using TestHelper.FakeEntities;

namespace Doara.Ucetnictvi.FakeEntities;

public class FakeCountry : IFakeEntity<Country>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public Guid? TenantId { get; set; }
    public bool IsDeleted { get; set; }
    
    public string SerializeToJson()
    {
        return JsonSerializer.Serialize(this, JsonConfig.DefaultJsonSerializerOptions);
    }

    public Country CreateOriginalEntity(bool checkIfNotThrow = true)
    {
        return Converter.CreateOriginalEntity(this, checkIfNotThrow);
    }

    public void CheckIfSame(Country entity)
    {
        Comparator.CheckIfSame(this, entity);
    }
    
    public const int MaxNameLength = 100;
    public const int MaxCodeLength = 3;
}