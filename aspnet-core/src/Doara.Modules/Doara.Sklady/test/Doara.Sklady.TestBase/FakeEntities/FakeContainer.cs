using System;
using System.Text.Json;
using Doara.Sklady.Entities;
using Doara.Sklady.Utils;
using Doara.Sklady.Utils.Converters;
using TestHelper.FakeEntities;

namespace Doara.Sklady.FakeEntities;

public class FakeContainer : IFakeEntity<Container>
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set;}
    public Guid? TenantId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    
    public string SerializeToJson()
    {
        return JsonSerializer.Serialize(this, JsonConfig.DefaultJsonSerializerOptions);
    }

    public Container CreateOriginalEntity(bool checkIfNotThrow = true)
    {
        return Converter.CreateOriginalEntity(this, checkIfNotThrow);
    }

    public void CheckIfSame(Container entity)
    {
        Comparator.CheckIfSame(this, entity);
    }
    
    public const int MaxNameLength = 255;
    public const int MaxDescriptionLength = 2000;
}