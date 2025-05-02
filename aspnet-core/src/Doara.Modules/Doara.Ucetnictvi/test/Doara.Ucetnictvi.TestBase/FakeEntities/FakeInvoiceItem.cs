using System;
using System.Text.Json;
using Doara.Ucetnictvi.Entities;
using Doara.Ucetnictvi.Enums;
using Doara.Ucetnictvi.Utils;
using Doara.Ucetnictvi.Utils.Converters;
using TestHelper.FakeEntities;

namespace Doara.Ucetnictvi.FakeEntities;

public class FakeInvoiceItem : IFakeEntity<InvoiceItem>
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public Guid? TenantId { get; set; }
    public Guid? ContainerItemId { get; set; }
    public Guid InvoiceId { get; set; }
    public string Description { get; set; } = null!;
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal NetAmount { get; set; }
    public VatRate? VatRate { get; set; }
    public decimal VatAmount { get; set; }
    public decimal GrossAmount { get; set; }
    
    public string SerializeToJson()
    {
        return JsonSerializer.Serialize(this, JsonConfig.DefaultJsonSerializerOptions);
    }

    public InvoiceItem CreateOriginalEntity(bool checkIfNotThrow = true)
    {
        return Converter.CreateOriginalEntity(this, checkIfNotThrow);
    }

    public void CheckIfSame(InvoiceItem entity)
    {
        Comparator.CheckIfSame(this, entity);
    }
    
    public const int MaxDescriptionLength = 200;
    public const int MinQuantity = 0;
    public const int MinVatRate = 0;
}