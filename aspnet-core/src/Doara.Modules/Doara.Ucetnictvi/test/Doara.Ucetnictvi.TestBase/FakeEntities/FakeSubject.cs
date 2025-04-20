using System;
using System.Text.Json;
using Doara.Ucetnictvi.Entities;
using Doara.Ucetnictvi.Utils;
using Doara.Ucetnictvi.Utils.Converters;
using TestHelper.FakeEntities;

namespace Doara.Ucetnictvi.FakeEntities;

public class FakeSubject : IFakeEntity<Subject>
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public Guid? TenantId { get; set; }
    public string Name { get; set; } = null!;
    public Guid AddressId { get; set; }
    public string? Ic { get; set; }
    public string? Dic { get; set; }
    public bool IsVatPayer { get; set; }
    
    public string SerializeToJson()
    {
        return JsonSerializer.Serialize(this, JsonConfig.DefaultJsonSerializerOptions);
    }

    public Subject CreateOriginalEntity(bool checkIfNotThrow = true)
    {
        return Converter.CreateOriginalEntity(this, checkIfNotThrow);
    }

    public void CheckIfSame(Subject entity)
    {
        Comparator.CheckIfSame(this, entity);
    }
    
    public const int MaxNameLength = 100;
    public const int MaxIcLength = 12;
    public const int MaxDicLength = 14;
}